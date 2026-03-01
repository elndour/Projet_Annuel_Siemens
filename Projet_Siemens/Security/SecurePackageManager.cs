using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace Projet_Siemens.Security
{
    /// <summary>
    /// Orchestrateur qui combine packaging ZIP et cryptage AES
    /// Gère le workflow complet : Compression → Cryptage → Rapport
    /// </summary>
    public class SecurePackageManager
    {
        private ZipPackager zipPackager;
        private AESEncryption aesEncryption;

        public SecurePackageManager()
        {
            zipPackager = new ZipPackager();
            aesEncryption = new AESEncryption();
        }

        /// <summary>
        /// Package complet : Compression + Cryptage des données d'une machine
        /// </summary>
        /// <param name="machineId">ID de la machine</param>
        /// <param name="dataDirectory">Répertoire contenant les données (ex: Data/)</param>
        /// <param name="outputDirectory">Répertoire de sortie</param>
        /// <param name="password">Mot de passe de cryptage (null pour génération automatique)</param>
        /// <returns>Rapport de packaging</returns>
        public SecurePackageReport CreateSecurePackage(string machineId, string dataDirectory, string outputDirectory, string password = null)
        {
            var report = new SecurePackageReport
            {
                MachineId = machineId,
                StartTime = DateTime.Now,
                Success = false
            };

            try
            {
                // 1. Vérifier que les données existent
                string machineDataPath = Path.Combine(dataDirectory, machineId);
                if (!Directory.Exists(machineDataPath))
                {
                    report.ErrorMessage = $"Aucune donnée trouvée pour la machine '{machineId}'";
                    return report;
                }

                // 2. Générer un mot de passe si nécessaire
                if (string.IsNullOrEmpty(password))
                {
                    password = aesEncryption.GenerateSecurePassword(32);
                    report.GeneratedPassword = password;
                }

                // 3. Créer le répertoire de sortie
                Directory.CreateDirectory(outputDirectory);

                // 4. Créer l'archive ZIP
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string zipFileName = $"Siemens_Debug_{machineId}_{timestamp}.zip";
                string zipPath = Path.Combine(outputDirectory, zipFileName);

                report.ZipFilePath = zipPath;

                if (!zipPackager.CreateZipArchive(machineDataPath, zipPath))
                {
                    report.ErrorMessage = "Échec de la compression ZIP";
                    return report;
                }

                // 5. Obtenir les infos du ZIP
                var zipInfo = zipPackager.GetZipInfo(zipPath);
                report.ZipSizeMB = zipInfo.SizeMB;
                report.FileCount = zipInfo.FileCount;
                report.CompressionRatio = zipInfo.CompressionRatio;

                // 6. Crypter le ZIP
                string encryptedFileName = zipFileName + ".enc";
                string encryptedPath = Path.Combine(outputDirectory, encryptedFileName);

                report.EncryptedFilePath = encryptedPath;

                if (!aesEncryption.EncryptFile(zipPath, encryptedPath, password))
                {
                    report.ErrorMessage = "Échec du cryptage AES";
                    return report;
                }

                // 7. Calculer le hash du fichier crypté (pour vérification d'intégrité)
                report.FileHash = aesEncryption.CalculateFileHash(encryptedPath);

                // 8. Obtenir la taille du fichier crypté
                FileInfo encFileInfo = new FileInfo(encryptedPath);
                report.EncryptedSizeMB = encFileInfo.Length / (1024.0 * 1024.0);

                // 9. Supprimer le ZIP non crypté par sécurité (optionnel)
                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                    report.ZipFileDeleted = true;
                }

                report.Success = true;
                report.EndTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                report.Success = false;
                report.ErrorMessage = ex.Message;
                report.EndTime = DateTime.Now;
            }

            return report;
        }

        /// <summary>
        /// Décrypte et extrait un package sécurisé
        /// </summary>
        /// <param name="encryptedFilePath">Fichier .enc</param>
        /// <param name="password">Mot de passe de décryptage</param>
        /// <param name="outputDirectory">Répertoire de sortie</param>
        /// <returns>True si succès</returns>
        public bool ExtractSecurePackage(string encryptedFilePath, string password, string outputDirectory)
        {
            try
            {
                // 1. Décrypter le fichier
                string zipPath = encryptedFilePath.Replace(".enc", "");
                
                if (!aesEncryption.DecryptFile(encryptedFilePath, zipPath, password))
                {
                    return false;
                }

                // 2. Extraire le ZIP
                if (!zipPackager.ExtractZipArchive(zipPath, outputDirectory))
                {
                    return false;
                }

                // 3. Nettoyer le ZIP temporaire
                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'extraction :\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Sauvegarde le rapport de packaging en JSON
        /// </summary>
        public void SaveReport(SecurePackageReport report, string outputPath)
        {
            try
            {
                string json = JsonSerializer.Serialize(report, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                File.WriteAllText(outputPath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la sauvegarde du rapport :\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Génère un mot de passe sécurisé
        /// </summary>
        public string GeneratePassword(int length = 32)
        {
            return aesEncryption.GenerateSecurePassword(length);
        }
    }

    /// <summary>
    /// Rapport de création de package sécurisé
    /// </summary>
    public class SecurePackageReport
    {
        public string MachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        // Informations ZIP
        public string ZipFilePath { get; set; }
        public double ZipSizeMB { get; set; }
        public int FileCount { get; set; }
        public double CompressionRatio { get; set; }
        public bool ZipFileDeleted { get; set; }

        // Informations Cryptage
        public string EncryptedFilePath { get; set; }
        public double EncryptedSizeMB { get; set; }
        public string FileHash { get; set; }
        public string GeneratedPassword { get; set; }

        public TimeSpan Duration => EndTime - StartTime;

        public string GetSummary()
        {
            if (!Success)
            {
                return $"❌ ÉCHEC : {ErrorMessage}";
            }

            return $"✅ Package sécurisé créé avec succès\n\n" +
                   $"Machine : {MachineId}\n" +
                   $"Fichier : {Path.GetFileName(EncryptedFilePath)}\n" +
                   $"Taille : {EncryptedSizeMB:F2} MB\n" +
                   $"Fichiers inclus : {FileCount}\n" +
                   $"Taux de compression : {CompressionRatio * 100:F1}%\n" +
                   $"Hash SHA-256 : {FileHash?.Substring(0, 16)}...\n" +
                   $"Durée : {Duration.TotalSeconds:F2}s\n\n" +
                   (GeneratedPassword != null ? $"🔑 Mot de passe généré :\n{GeneratedPassword}\n\n⚠️ CONSERVEZ CE MOT DE PASSE !" : "🔑 Mot de passe fourni");
        }
    }
}
