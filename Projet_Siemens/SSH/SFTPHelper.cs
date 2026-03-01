using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace Projet_Siemens.SSH
{
    /// <summary>
    /// Helper pour gérer les transferts de fichiers via SFTP
    /// </summary>
    public class SFTPHelper
    {
        private string host;
        private int port;
        private string username;
        private string password;
        private SftpClient sftpClient;

        public SFTPHelper(string host, string username, string password, int port = 22)
        {
            this.host = host;
            this.port = port;
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// Teste la connexion SFTP
        /// </summary>
        public bool TestConnection()
        {
            try
            {
                using (var client = new SftpClient(host, port, username, password))
                {
                    client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(10);
                    client.Connect();
                    
                    if (client.IsConnected)
                    {
                        client.Disconnect();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion SFTP :\n{ex.Message}", "Erreur SFTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Établit une connexion SFTP persistante
        /// </summary>
        public bool Connect()
        {
            try
            {
                sftpClient = new SftpClient(host, port, username, password);
                sftpClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(10);
                sftpClient.Connect();
                return sftpClient.IsConnected;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion SFTP :\n{ex.Message}", "Erreur SFTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Ferme la connexion SFTP
        /// </summary>
        public void Disconnect()
        {
            if (sftpClient != null && sftpClient.IsConnected)
            {
                sftpClient.Disconnect();
                sftpClient.Dispose();
            }
        }

        /// <summary>
        /// Télécharge un fichier distant vers un chemin local
        /// </summary>
        public bool DownloadFile(string remoteFilePath, string localFilePath, Action<ulong> progressCallback = null)
        {
            try
            {
                if (sftpClient == null || !sftpClient.IsConnected)
                {
                    if (!Connect())
                        return false;
                }

                // Créer le répertoire local si nécessaire
                string localDir = Path.GetDirectoryName(localFilePath);
                if (!Directory.Exists(localDir))
                {
                    Directory.CreateDirectory(localDir);
                }

                using (var fileStream = new FileStream(localFilePath, FileMode.Create))
                {
                    sftpClient.DownloadFile(remoteFilePath, fileStream, progressCallback);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du téléchargement de {remoteFilePath} :\n{ex.Message}", "Erreur SFTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Télécharge tous les fichiers d'un répertoire distant avec une extension spécifique
        /// </summary>
        public int DownloadFilesByExtension(string remoteDirectory, string localDirectory, string extension, Action<string, int, int> progressCallback = null)
        {
            int downloadedCount = 0;

            try
            {
                if (sftpClient == null || !sftpClient.IsConnected)
                {
                    if (!Connect())
                        return 0;
                }

                // Vérifier que le répertoire distant existe
                if (!sftpClient.Exists(remoteDirectory))
                {
                    MessageBox.Show($"Le répertoire distant n'existe pas : {remoteDirectory}", "Erreur SFTP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return 0;
                }

                // Créer le répertoire local
                Directory.CreateDirectory(localDirectory);

                // Lister les fichiers distants
                var files = ListFiles(remoteDirectory, extension);

                int totalFiles = files.Count;
                int currentFile = 0;

                foreach (var file in files)
                {
                    currentFile++;
                    string fileName = Path.GetFileName(file.FullName);
                    string localPath = Path.Combine(localDirectory, fileName);

                    progressCallback?.Invoke(fileName, currentFile, totalFiles);

                    if (DownloadFile(file.FullName, localPath))
                    {
                        downloadedCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du téléchargement multiple :\n{ex.Message}", "Erreur SFTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return downloadedCount;
        }

        /// <summary>
        /// Liste les fichiers d'un répertoire distant avec une extension spécifique
        /// </summary>
        public List<ISftpFile> ListFiles(string remoteDirectory, string extension = "*")
        {
            var files = new List<ISftpFile>();

            try
            {
                if (sftpClient == null || !sftpClient.IsConnected)
                {
                    if (!Connect())
                        return files;
                }

                if (!sftpClient.Exists(remoteDirectory))
                    return files;

                var allFiles = sftpClient.ListDirectory(remoteDirectory);

                foreach (var file in allFiles)
                {
                    if (file.IsRegularFile)
                    {
                        if (extension == "*" || file.Name.EndsWith($".{extension}", StringComparison.OrdinalIgnoreCase))
                        {
                            files.Add(file);
                        }
                    }
                    else if (file.IsDirectory && file.Name != "." && file.Name != "..")
                    {
                        // Récursion dans les sous-répertoires
                        files.AddRange(ListFiles(file.FullName, extension));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la liste des fichiers :\n{ex.Message}", "Erreur SFTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return files;
        }

        /// <summary>
        /// Télécharge plusieurs types de fichiers (logs, config, xml, nfo)
        /// </summary>
        public Dictionary<string, int> DownloadMultipleExtensions(string remoteDirectory, string localBaseDirectory, List<string> extensions, Action<string, int, int> progressCallback = null)
        {
            var results = new Dictionary<string, int>();

            foreach (var extension in extensions)
            {
                string localDir = Path.Combine(localBaseDirectory, extension);
                int count = DownloadFilesByExtension(remoteDirectory, localDir, extension, progressCallback);
                results[extension] = count;
            }

            return results;
        }

        /// <summary>
        /// Récupère la taille d'un fichier distant
        /// </summary>
        public long GetFileSize(string remoteFilePath)
        {
            try
            {
                if (sftpClient == null || !sftpClient.IsConnected)
                {
                    if (!Connect())
                        return -1;
                }

                if (sftpClient.Exists(remoteFilePath))
                {
                    return sftpClient.GetAttributes(remoteFilePath).Size;
                }
            }
            catch
            {
                // Ignore
            }

            return -1;
        }

        /// <summary>
        /// Vérifie si un chemin distant existe
        /// </summary>
        public bool RemotePathExists(string remotePath)
        {
            try
            {
                if (sftpClient == null || !sftpClient.IsConnected)
                {
                    if (!Connect())
                        return false;
                }

                return sftpClient.Exists(remotePath);
            }
            catch
            {
                return false;
            }
        }
    }
}
