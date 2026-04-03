using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;
using Projet_Siemens.Class;

namespace Projet_Siemens.SSH
{
    /// <summary>
    /// Classe principale pour orchestrer la collecte de fichiers depuis les machines
    /// Combine SSH et SFTP pour récupérer logs, configs, xml, nfo
    /// </summary>
    public class FileCollector
    {
        private Machine machine;
        private string baseOutputDirectory;
        private SSHHelper sshHelper;
        private SFTPHelper sftpHelper;

        // Chemins de recherche par défaut pour les fichiers MES
        private readonly List<string> defaultLogPaths = new List<string>
        {
            "/var/log",
            "/opt/siemens/logs",
            "/usr/local/mes/logs",
            "/home/*/logs",
            "/home/testuser/test_files",  // Pour les tests locaux
            "/test_files",
            "./test_files",
            ".",
            "/Users/testuser/test_files",
            "C:/Users/testuser/test_files"
        };

        private readonly List<string> defaultConfigPaths = new List<string>
        {
            "/etc",
            "/opt/siemens/config",
            "/usr/local/mes/config",
            "/home/*/config",
            "/home/testuser/test_files",  // Pour les tests locaux
            "/test_files",
            "./test_files",
            ".",
            "/Users/testuser/test_files",
            "C:/Users/testuser/test_files"
        };

        public FileCollector(Machine machine, string outputBaseDirectory)
        {
            this.machine = machine;
            this.baseOutputDirectory = outputBaseDirectory;

            // Initialiser SSH et SFTP avec les credentials de la machine
            // Note: Pour les serveurs, on utilise un port SSH par défaut (22)
            // et des credentials à définir (à adapter selon votre infrastructure)
            this.sshHelper = new SSHHelper(machine.ip, "root", "password", 22);
            this.sftpHelper = new SFTPHelper(machine.ip, "root", "password", 22);
        }

        public FileCollector(Machine machine, string outputBaseDirectory, string sshUsername, string sshPassword, int sshPort = 22)
        {
            this.machine = machine;
            this.baseOutputDirectory = outputBaseDirectory;
            this.sshHelper = new SSHHelper(machine.ip, sshUsername, sshPassword, sshPort);
            this.sftpHelper = new SFTPHelper(machine.ip, sshUsername, sshPassword, sshPort);
        }

        /// <summary>
        /// Teste les connexions SSH et SFTP
        /// </summary>
        public bool TestConnections()
        {
            bool sshOk = sshHelper.TestConnection();
            bool sftpOk = sftpHelper.TestConnection();

            return sshOk && sftpOk;
        }

        /// <summary>
        /// Collecte les fichiers depuis la machine (SSH/SFTP ou local test)
        /// </summary>
        public FileCollectionReport CollectAllFiles(Action<string> statusCallback = null)
        {
            var report = new FileCollectionReport
            {
                MachineId = machine.id,
                MachineType = machine.type,
                MachineIp = machine.ip,
                StartTime = DateTime.Now,
                FilesCollected = new Dictionary<string, int>()
            };

            try
            {
                statusCallback?.Invoke($"Connexion à {machine.id} ({machine.ip})...");

                bool isLocalMachine = machine.ip == "127.0.0.1" || machine.ip == "localhost" || machine.ip == "::1";

                if (machine.id.Contains("Test") && isLocalMachine)
                {
                    statusCallback?.Invoke("Mode test local détecté - collecte depuis TestData...");
                    return CollectLocalFiles(statusCallback);
                }

                if (!TestConnections())
                {
                    report.Success = false;
                    report.ErrorMessage = "Échec de connexion SSH/SFTP";
                    return report;
                }

                statusCallback?.Invoke("Connexion établie !");

                string machineOutputDir = Path.Combine(baseOutputDirectory, machine.id, "files");
                Directory.CreateDirectory(machineOutputDir);

                statusCallback?.Invoke("Récupération des informations système...");
                CollectSystemInfo(machineOutputDir);

                statusCallback?.Invoke("Collecte des fichiers .log...");
                int logCount = CollectFilesByExtension("log", machineOutputDir, "logs");
                report.FilesCollected["log"] = logCount;

                statusCallback?.Invoke("Collecte des fichiers .xml...");
                int xmlCount = CollectFilesByExtension("xml", machineOutputDir, "config");
                report.FilesCollected["xml"] = xmlCount;

                statusCallback?.Invoke("Collecte des fichiers .config...");
                int configCount = CollectFilesByExtension("config", machineOutputDir, "config");
                report.FilesCollected["config"] = configCount;

                statusCallback?.Invoke("Collecte des fichiers .nfo...");
                int nfoCount = CollectFilesByExtension("nfo", machineOutputDir, "info");
                report.FilesCollected["nfo"] = nfoCount;

                report.Success = true;
                report.TotalFiles = report.FilesCollected.Values.Sum();
            }
            catch (Exception ex)
            {
                report.Success = false;
                report.ErrorMessage = ex.Message;
                statusCallback?.Invoke($"Erreur : {ex.Message}");
            }
            finally
            {
                report.EndTime = DateTime.Now;

                if (!(machine.id.Contains("Test") && (machine.ip == "127.0.0.1" || machine.ip == "localhost" || machine.ip == "::1")))
                {
                    // Fermer seulement les connexions distantes
                    sshHelper.Disconnect();
                    sftpHelper.Disconnect();
                }
            }

            return report;
        }

        /// <summary>
        /// Collecte les fichiers localement pour les machines de test
        /// </summary>
        private FileCollectionReport CollectLocalFiles(Action<string> statusCallback = null)
        {
            var report = new FileCollectionReport
            {
                MachineId = machine.id,
                MachineType = machine.type,
                MachineIp = machine.ip,
                StartTime = DateTime.Now,
                FilesCollected = new Dictionary<string, int>()
            };

            try
            {
                // Créer le répertoire de sortie
                string machineOutputDir = Path.Combine(baseOutputDirectory, machine.id, "files");
                Directory.CreateDirectory(machineOutputDir);

                // Chemin source des fichiers de test
                string testDataDir = Path.Combine(
                    Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName,
                    "TestData", machine.id, "test_files"
                );

                if (!Directory.Exists(testDataDir))
                {
                    report.Success = false;
                    report.ErrorMessage = $"Dossier de test introuvable : {testDataDir}";
                    return report;
                }

                // 1. Collecter les informations système (simulées)
                statusCallback?.Invoke("Récupération des informations système...");
                CollectLocalSystemInfo(machineOutputDir);

                // 2. Collecter les fichiers .log
                statusCallback?.Invoke("Collecte des fichiers .log...");
                int logCount = CollectLocalFilesByExtension("log", testDataDir, machineOutputDir, "logs");
                report.FilesCollected["log"] = logCount;

                // 3. Collecter les fichiers .xml
                statusCallback?.Invoke("Collecte des fichiers .xml...");
                int xmlCount = CollectLocalFilesByExtension("xml", testDataDir, machineOutputDir, "config");
                report.FilesCollected["xml"] = xmlCount;

                // 4. Collecter les fichiers .config
                statusCallback?.Invoke("Collecte des fichiers .config...");
                int configCount = CollectLocalFilesByExtension("config", testDataDir, machineOutputDir, "config");
                report.FilesCollected["config"] = configCount;

                // 5. Collecter les fichiers .nfo
                statusCallback?.Invoke("Collecte des fichiers .nfo...");
                int nfoCount = CollectLocalFilesByExtension("nfo", testDataDir, machineOutputDir, "info");
                report.FilesCollected["nfo"] = nfoCount;

                report.Success = true;
                report.TotalFiles = report.FilesCollected.Values.Sum();
            }
            catch (Exception ex)
            {
                report.Success = false;
                report.ErrorMessage = ex.Message;
                statusCallback?.Invoke($"Erreur : {ex.Message}");
            }
            finally
            {
                report.EndTime = DateTime.Now;
            }

            return report;
        }

        /// <summary>
        /// Collecte les informations système et les sauvegarde en JSON
        /// </summary>
        private void CollectSystemInfo(string outputDirectory)
        {
            try
            {
                var systemInfo = sshHelper.GetSystemInfo();

                if (systemInfo.Count > 0)
                {
                    string infoPath = Path.Combine(outputDirectory, "system_info.json");
                    string json = JsonSerializer.Serialize(systemInfo, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(infoPath, json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la collecte des infos système :\n{ex.Message}", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Collecte les informations système localement pour les tests
        /// </summary>
        private void CollectLocalSystemInfo(string outputDirectory)
        {
            try
            {
                var systemInfo = new Dictionary<string, string>
                {
                    ["os"] = "Windows Test Environment",
                    ["hostname"] = Environment.MachineName,
                    ["user"] = Environment.UserName,
                    ["test_mode"] = "true"
                };

                string infoPath = Path.Combine(outputDirectory, "system_info.json");
                string json = JsonSerializer.Serialize(systemInfo, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(infoPath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la collecte des infos système locales :\n{ex.Message}", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Collecte les fichiers d'une extension spécifique
        /// </summary>
        private int CollectFilesByExtension(string extension, string baseOutputDir, string subFolder)
        {
            int totalFiles = 0;
            string outputDir = Path.Combine(baseOutputDir, subFolder);

            try
            {
                // Déterminer les chemins de recherche selon l'extension
                List<string> searchPaths = extension == "log" ? defaultLogPaths : defaultConfigPaths;

                foreach (string searchPath in searchPaths)
                {
                    try
                    {
                        // Télécharger les fichiers de ce chemin
                        int count = sftpHelper.DownloadFilesByExtension(searchPath, outputDir, extension);
                        totalFiles += count;
                    }
                    catch
                    {
                        // Ignorer si le chemin n'existe pas
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la collecte des fichiers .{extension} :\n{ex.Message}", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return totalFiles;
        }

        /// <summary>
        /// Collecte les fichiers localement pour les tests
        /// </summary>
        private int CollectLocalFilesByExtension(string extension, string sourceDir, string baseOutputDir, string subFolder)
        {
            int totalFiles = 0;
            string outputDir = Path.Combine(baseOutputDir, subFolder);
            Directory.CreateDirectory(outputDir);

            try
            {
                var files = Directory.GetFiles(sourceDir, $"*.{extension}", SearchOption.TopDirectoryOnly);

                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    string destPath = Path.Combine(outputDir, fileName);
                    File.Copy(file, destPath, true);
                    totalFiles++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la collecte locale des fichiers .{extension} :\n{ex.Message}", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return totalFiles;
        }

        /// <summary>
        /// Collecte les fichiers depuis un chemin personnalisé
        /// </summary>
        public int CollectFromCustomPath(string remotePath, string localOutputDir, List<string> extensions)
        {
            int totalFiles = 0;

            try
            {
                Directory.CreateDirectory(localOutputDir);

                foreach (string extension in extensions)
                {
                    int count = sftpHelper.DownloadFilesByExtension(remotePath, localOutputDir, extension);
                    totalFiles += count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la collecte personnalisée :\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return totalFiles;
        }

        /// <summary>
        /// Sauvegarde le rapport de collecte en JSON
        /// </summary>
        public void SaveReport(FileCollectionReport report, string outputPath)
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
    }

    /// <summary>
    /// Rapport de collecte de fichiers
    /// </summary>
    public class FileCollectionReport
    {
        public string MachineId { get; set; }
        public string MachineType { get; set; }
        public string MachineIp { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<string, int> FilesCollected { get; set; }
        public int TotalFiles { get; set; }
        public TimeSpan Duration => EndTime - StartTime;
    }
}
