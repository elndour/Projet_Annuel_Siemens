using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Renci.SshNet;

namespace Projet_Siemens.SSH
{
    /// <summary>
    /// Helper pour gérer les connexions SSH et l'exécution de commandes distantes
    /// </summary>
    public class SSHHelper
    {
        private string host;
        private int port;
        private string username;
        private string password;
        private SshClient sshClient;

        public SSHHelper(string host, string username, string password, int port = 22)
        {
            this.host = host;
            this.port = port;
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// Teste la connexion SSH
        /// </summary>
        public bool TestConnection()
        {
            try
            {
                using (var client = new SshClient(host, port, username, password))
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
                MessageBox.Show($"Erreur de connexion SSH :\n{ex.Message}", "Erreur SSH", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Établit une connexion SSH persistante
        /// </summary>
        public bool Connect()
        {
            try
            {
                sshClient = new SshClient(host, port, username, password);
                sshClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds(10);
                sshClient.Connect();
                return sshClient.IsConnected;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion SSH :\n{ex.Message}", "Erreur SSH", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Ferme la connexion SSH
        /// </summary>
        public void Disconnect()
        {
            if (sshClient != null && sshClient.IsConnected)
            {
                sshClient.Disconnect();
                sshClient.Dispose();
            }
        }

        /// <summary>
        /// Exécute une commande SSH et retourne le résultat
        /// </summary>
        public string ExecuteCommand(string command)
        {
            try
            {
                if (sshClient == null || !sshClient.IsConnected)
                {
                    if (!Connect())
                        return null;
                }

                using (var cmd = sshClient.CreateCommand(command))
                {
                    cmd.CommandTimeout = TimeSpan.FromSeconds(30);
                    string result = cmd.Execute();
                    
                    if (!string.IsNullOrEmpty(cmd.Error))
                    {
                        return $"STDOUT:\n{result}\n\nSTDERR:\n{cmd.Error}";
                    }
                    
                    return result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'exécution de la commande :\n{ex.Message}", "Erreur SSH", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Liste les fichiers d'un répertoire distant avec une extension spécifique
        /// </summary>
        public List<string> ListFiles(string remotePath, string extension = "*")
        {
            var files = new List<string>();

            try
            {
                string command = extension == "*" 
                    ? $"find {remotePath} -type f" 
                    : $"find {remotePath} -type f -name '*.{extension}'";

                string result = ExecuteCommand(command);
                
                if (!string.IsNullOrEmpty(result))
                {
                    string[] lines = result.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    files.AddRange(lines);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la liste des fichiers :\n{ex.Message}", "Erreur SSH", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return files;
        }

        /// <summary>
        /// Récupère les informations système de la machine distante
        /// </summary>
        public Dictionary<string, string> GetSystemInfo()
        {
            var info = new Dictionary<string, string>();

            try
            {
                // Nom de l'hôte
                info["Hostname"] = ExecuteCommand("hostname").Trim();

                // Système d'exploitation
                info["OS"] = ExecuteCommand("cat /etc/os-release | grep PRETTY_NAME | cut -d '\"' -f2").Trim();

                // Kernel
                info["Kernel"] = ExecuteCommand("uname -r").Trim();

                // Uptime
                info["Uptime"] = ExecuteCommand("uptime -p").Trim();

                // CPU
                info["CPU"] = ExecuteCommand("lscpu | grep 'Model name' | cut -d ':' -f2").Trim();

                // RAM
                info["RAM"] = ExecuteCommand("free -h | grep Mem | awk '{print $2}'").Trim();

                // Disque
                info["Disk"] = ExecuteCommand("df -h / | tail -1 | awk '{print $2}'").Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération des infos système :\n{ex.Message}", "Erreur SSH", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return info;
        }

        /// <summary>
        /// Vérifie si un chemin distant existe
        /// </summary>
        public bool RemotePathExists(string remotePath)
        {
            try
            {
                string result = ExecuteCommand($"[ -e {remotePath} ] && echo 'EXISTS' || echo 'NOT_FOUND'");
                return result != null && result.Trim() == "EXISTS";
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Crée un répertoire distant
        /// </summary>
        public bool CreateRemoteDirectory(string remotePath)
        {
            try
            {
                string result = ExecuteCommand($"mkdir -p {remotePath}");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la création du répertoire :\n{ex.Message}", "Erreur SSH", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
