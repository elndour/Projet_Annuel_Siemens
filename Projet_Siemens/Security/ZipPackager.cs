using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace Projet_Siemens.Security
{
    /// <summary>
    /// Gère la compression des données collectées en archives ZIP
    /// Prépare les données pour le cryptage et la transmission
    /// </summary>
    public class ZipPackager
    {
        /// <summary>
        /// Compresse un répertoire complet en fichier ZIP
        /// </summary>
        /// <param name="sourceDirectory">Répertoire source à compresser (ex: Data/machine-01/)</param>
        /// <param name="outputZipPath">Chemin du fichier ZIP de sortie</param>
        /// <param name="compressionLevel">Niveau de compression (Optimal par défaut)</param>
        /// <returns>True si succès, False sinon</returns>
        public bool CreateZipArchive(string sourceDirectory, string outputZipPath, CompressionLevel compressionLevel = CompressionLevel.Optimal)
        {
            try
            {
                // Vérifier que le répertoire source existe
                if (!Directory.Exists(sourceDirectory))
                {
                    MessageBox.Show($"Le répertoire source n'existe pas :\n{sourceDirectory}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Créer le répertoire de destination si nécessaire
                string outputDir = Path.GetDirectoryName(outputZipPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Supprimer le fichier ZIP s'il existe déjà
                if (File.Exists(outputZipPath))
                {
                    File.Delete(outputZipPath);
                }

                // Créer l'archive ZIP
                ZipFile.CreateFromDirectory(sourceDirectory, outputZipPath, compressionLevel, false);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la compression ZIP :\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Crée une archive ZIP pour une machine spécifique avec nom standardisé
        /// Format: Siemens_Debug_[MachineID]_[Date].zip
        /// </summary>
        /// <param name="machineId">ID de la machine</param>
        /// <param name="dataDirectory">Répertoire contenant les données (ex: Data/)</param>
        /// <param name="outputDirectory">Répertoire de sortie pour le ZIP</param>
        /// <returns>Chemin du fichier ZIP créé, ou null si échec</returns>
        public string PackageMachineData(string machineId, string dataDirectory, string outputDirectory)
        {
            try
            {
                // Construire le chemin source
                string sourceDir = Path.Combine(dataDirectory, machineId);

                if (!Directory.Exists(sourceDir))
                {
                    MessageBox.Show($"Aucune donnée trouvée pour la machine '{machineId}'", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                // Construire le nom du fichier ZIP
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string zipFileName = $"Siemens_Debug_{machineId}_{timestamp}.zip";
                string zipPath = Path.Combine(outputDirectory, zipFileName);

                // Créer l'archive
                if (CreateZipArchive(sourceDir, zipPath))
                {
                    return zipPath;
                }

                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du packaging :\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Récupère la taille d'un fichier ZIP en Mo
        /// </summary>
        public double GetZipSizeMB(string zipPath)
        {
            try
            {
                if (File.Exists(zipPath))
                {
                    FileInfo fileInfo = new FileInfo(zipPath);
                    return fileInfo.Length / (1024.0 * 1024.0);
                }
            }
            catch
            {
                // Ignore
            }

            return 0;
        }

        /// <summary>
        /// Compte le nombre de fichiers dans une archive ZIP
        /// </summary>
        public int GetZipFileCount(string zipPath)
        {
            try
            {
                if (File.Exists(zipPath))
                {
                    using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                    {
                        return archive.Entries.Count;
                    }
                }
            }
            catch
            {
                // Ignore
            }

            return 0;
        }

        /// <summary>
        /// Extrait une archive ZIP (pour tests/validation)
        /// </summary>
        public bool ExtractZipArchive(string zipPath, string destinationDirectory)
        {
            try
            {
                if (!File.Exists(zipPath))
                {
                    MessageBox.Show($"Le fichier ZIP n'existe pas :\n{zipPath}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Créer le répertoire de destination
                Directory.CreateDirectory(destinationDirectory);

                // Extraire
                ZipFile.ExtractToDirectory(zipPath, destinationDirectory);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'extraction ZIP :\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Obtient des informations détaillées sur une archive ZIP
        /// </summary>
        public ZipArchiveInfo GetZipInfo(string zipPath)
        {
            var info = new ZipArchiveInfo
            {
                FilePath = zipPath,
                Exists = File.Exists(zipPath)
            };

            if (!info.Exists)
                return info;

            try
            {
                FileInfo fileInfo = new FileInfo(zipPath);
                info.SizeBytes = fileInfo.Length;
                info.SizeMB = fileInfo.Length / (1024.0 * 1024.0);
                info.CreationTime = fileInfo.CreationTime;

                using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                {
                    info.FileCount = archive.Entries.Count;

                    // Calculer la taille totale décompressée
                    long totalUncompressed = 0;
                    foreach (var entry in archive.Entries)
                    {
                        totalUncompressed += entry.Length;
                    }

                    info.UncompressedSizeBytes = totalUncompressed;
                    info.UncompressedSizeMB = totalUncompressed / (1024.0 * 1024.0);
                    info.CompressionRatio = totalUncompressed > 0 ? (double)fileInfo.Length / totalUncompressed : 0;
                }
            }
            catch (Exception ex)
            {
                info.ErrorMessage = ex.Message;
            }

            return info;
        }
    }

    /// <summary>
    /// Informations détaillées sur une archive ZIP
    /// </summary>
    public class ZipArchiveInfo
    {
        public string FilePath { get; set; }
        public bool Exists { get; set; }
        public long SizeBytes { get; set; }
        public double SizeMB { get; set; }
        public long UncompressedSizeBytes { get; set; }
        public double UncompressedSizeMB { get; set; }
        public double CompressionRatio { get; set; }
        public int FileCount { get; set; }
        public DateTime CreationTime { get; set; }
        public string ErrorMessage { get; set; }

        public string GetSummary()
        {
            if (!Exists)
                return "Fichier ZIP introuvable";

            return $"Taille : {SizeMB:F2} MB ({SizeBytes:N0} bytes)\n" +
                   $"Décompressé : {UncompressedSizeMB:F2} MB\n" +
                   $"Taux de compression : {CompressionRatio * 100:F1}%\n" +
                   $"Nombre de fichiers : {FileCount}\n" +
                   $"Créé le : {CreationTime:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
