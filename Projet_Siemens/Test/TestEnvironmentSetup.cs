using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Projet_Siemens.Class;

namespace Projet_Siemens.Test
{
    /// <summary>
    /// Configure l'environnement de test local avec vos PCs personnels
    /// </summary>
    public class TestEnvironmentSetup
    {
        public string BaseDirectory { get; set; }
        public List<Machine> TestMachines { get; private set; }

        public TestEnvironmentSetup(string baseDirectory = null)
        {
            BaseDirectory = baseDirectory ?? Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName,
                "TestData"
            );
            TestMachines = new List<Machine>();
        }

        /// <summary>
        /// Crée des machines de test pour 2 PCs
        /// </summary>
        public List<Machine> CreateLocalTestMachines()
        {
            TestMachines.Clear();

            // PC 1 - Votre PC actuel (localhost)
            string localIp = GetLocalIPAddress();
            var pc1Server = new Machine(
                ip: "127.0.0.1",
                id: "PC1_TestServer",
                type: "TestServer"
            );
            TestMachines.Add(pc1Server);

            // PC 1 - Base de données de test (SQLite)
            var pc1Database = new DataBase(
                id: "PC1_TestDatabase",
                ip: "127.0.0.1",
                sshPort: 22,
                password: "test123",
                username: "testuser",
                type: "DataBase"
            );
            TestMachines.Add(pc1Database);

            // PC 2 - Deuxième ordinateur (à configurer avec son IP)
            // Remplacer par l'IP réelle du PC 2 dans votre réseau local
            var pc2Server = new Machine(
                ip: "192.168.1.100", // À MODIFIER avec l'IP du PC 2
                id: "PC2_TestServer",
                type: "TestServer"
            );
            TestMachines.Add(pc2Server);

            return TestMachines;
        }

        /// <summary>
        /// Génère des fichiers de test pour simuler un serveur Siemens
        /// </summary>
        public void GenerateTestFiles(string machineId, int fileCount = 20)
        {
            string machineFolder = Path.Combine(BaseDirectory, machineId, "test_files");
            Directory.CreateDirectory(machineFolder);

            var random = new Random();
            var extensions = new[] { "log", "xml", "config", "nfo" };

            for (int i = 0; i < fileCount; i++)
            {
                string ext = extensions[random.Next(extensions.Length)];
                string fileName = $"test_file_{i + 1:D3}.{ext}";
                string filePath = Path.Combine(machineFolder, fileName);

                string content = GenerateFileContent(ext, i);
                File.WriteAllText(filePath, content);
            }

            Console.WriteLine($"✓ {fileCount} fichiers de test générés dans {machineFolder}");
        }

        /// <summary>
        /// Génère du contenu réaliste selon le type de fichier
        /// </summary>
        private string GenerateFileContent(string extension, int index)
        {
            return extension switch
            {
                "log" => GenerateLogContent(index),
                "xml" => GenerateXmlContent(index),
                "config" => GenerateConfigContent(index),
                "nfo" => GenerateNfoContent(index),
                _ => $"Test file content {index}"
            };
        }

        private string GenerateLogContent(int index)
        {
            DateTime now = DateTime.Now.AddMinutes(-index * 15);
            return $@"[{now:yyyy-MM-dd HH:mm:ss}] INFO - System started
[{now.AddMinutes(1):yyyy-MM-dd HH:mm:ss}] DEBUG - Connection established to database
[{now.AddMinutes(2):yyyy-MM-dd HH:mm:ss}] WARNING - High memory usage detected: 85%
[{now.AddMinutes(3):yyyy-MM-dd HH:mm:ss}] INFO - Production order #{index:D6} created
[{now.AddMinutes(4):yyyy-MM-dd HH:mm:ss}] INFO - Machine status: RUNNING
[{now.AddMinutes(5):yyyy-MM-dd HH:mm:ss}] ERROR - Temporary connection timeout (retrying...)
[{now.AddMinutes(6):yyyy-MM-dd HH:mm:ss}] INFO - Connection restored
[{now.AddMinutes(7):yyyy-MM-dd HH:mm:ss}] INFO - Production order #{index:D6} completed successfully
";
        }

        private string GenerateXmlContent(int index)
        {
            return $@"<?xml version=""1.0"" encoding=""UTF-8""?>
<Configuration>
    <System>
        <Name>Siemens MES Test System</Name>
        <Version>8.{index % 10}.{index % 100}</Version>
        <Environment>Test</Environment>
    </System>
    <Database>
        <Server>localhost</Server>
        <Port>1433</Port>
        <DatabaseName>MES_Test_{index:D3}</DatabaseName>
        <Timeout>30</Timeout>
    </Database>
    <Network>
        <Enabled>true</Enabled>
        <Port>{8000 + index}</Port>
        <MaxConnections>100</MaxConnections>
    </Network>
    <Logging>
        <Level>INFO</Level>
        <MaxFileSize>10485760</MaxFileSize>
        <RetentionDays>30</RetentionDays>
    </Logging>
</Configuration>";
        }

        private string GenerateConfigContent(int index)
        {
            return $@"# Siemens MES Configuration File - Test #{index:D3}
# Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}

[General]
ApplicationName=Siemens MES Test
Version=1.0.{index}
Environment=Development
Debug=true

[Database]
ConnectionString=Server=localhost;Database=MES_Test;Integrated Security=true
CommandTimeout=60
MaxPoolSize=100

[Production]
DefaultLineSpeed={100 + index * 5}
QualityCheckInterval=300
AutoStartEnabled=true

[Alarms]
CriticalThreshold=95
WarningThreshold=75
EmailNotifications=true
";
        }

        private string GenerateNfoContent(int index)
        {
            return $@"System Information File - Test #{index:D3}
Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}

===========================================
SYSTEM DETAILS
===========================================
Machine ID: TEST_MACHINE_{index:D3}
Operating System: Windows Server 2022
CPU: Intel Xeon E5-2680 v4
RAM: 64 GB
Disk Space: 2 TB SSD

===========================================
SIEMENS MES INSTALLATION
===========================================
Installation Date: {DateTime.Now.AddDays(-index):yyyy-MM-dd}
Version: 8.0.{index}
License Type: Test/Development
License Expiry: {DateTime.Now.AddYears(1):yyyy-MM-dd}

===========================================
NETWORK CONFIGURATION
===========================================
IP Address: 192.168.1.{100 + index}
Subnet Mask: 255.255.255.0
Gateway: 192.168.1.1
DNS: 8.8.8.8

===========================================
PERFORMANCE METRICS (Last 24h)
===========================================
Uptime: 99.{95 + (index % 5)}%
Average CPU: {20 + (index % 30)}%
Average Memory: {40 + (index % 40)}%
Network Throughput: {50 + index} Mbps
";
        }

        /// <summary>
        /// Crée une base de données SQLite de test avec des données réalistes
        /// </summary>
        public string CreateTestSQLiteDatabase(string machineId)
        {
            string dbFolder = Path.Combine(BaseDirectory, machineId, "database");
            Directory.CreateDirectory(dbFolder);

            string dbPath = Path.Combine(dbFolder, "test_mes_database.db");

            // Utiliser SQLiteTestDatabaseCreator si disponible
            try
            {
                var creator = new SQLiteTestDatabaseCreator();
                creator.CreateTestDatabase(dbPath);
                Console.WriteLine($"✓ Base de données SQLite créée : {dbPath}");
                return dbPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Erreur création DB : {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Obtient l'adresse IP locale du PC
        /// </summary>
        public string GetLocalIPAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                return "127.0.0.1";
            }
            catch
            {
                return "127.0.0.1";
            }
        }

        /// <summary>
        /// Affiche un guide de configuration SSH pour Windows
        /// </summary>
        public string GetSSHSetupGuide()
        {
            return @"
═══════════════════════════════════════════════════════════
  GUIDE DE CONFIGURATION SSH SUR WINDOWS 10/11
═══════════════════════════════════════════════════════════

📋 ÉTAPE 1 : INSTALLER OPENSSH SERVER
--------------------------------------
1. Ouvrir PowerShell en Administrateur
2. Exécuter :
   
   Add-WindowsCapability -Online -Name OpenSSH.Server~~~~0.0.1.0

📋 ÉTAPE 2 : DÉMARRER LE SERVICE SSH
--------------------------------------
   Start-Service sshd
   Set-Service -Name sshd -StartupType 'Automatic'

📋 ÉTAPE 3 : CONFIGURER LE PARE-FEU
--------------------------------------
   New-NetFirewallRule -Name sshd -DisplayName 'OpenSSH Server (sshd)' `
       -Enabled True -Direction Inbound -Protocol TCP -Action Allow -LocalPort 22

📋 ÉTAPE 4 : CRÉER UN UTILISATEUR DE TEST
--------------------------------------
   net user testuser test123 /add
   net localgroup Administrators testuser /add

📋 ÉTAPE 5 : TESTER LA CONNEXION
--------------------------------------
   ssh testuser@localhost

📋 ÉTAPE 6 : CRÉER DES FICHIERS DE TEST
--------------------------------------
Créer un dossier : C:\TestData\Siemens_Files\
Ajouter des fichiers .log, .xml, .config, .nfo

═══════════════════════════════════════════════════════════
  CONFIGURATION PC 2 (Deuxième ordinateur)
═══════════════════════════════════════════════════════════

1. Répéter les étapes 1-4 sur le PC 2
2. Noter l'adresse IP du PC 2 :
   
   ipconfig
   
   Chercher 'Adresse IPv4' dans la section Ethernet/WiFi

3. Modifier TestEnvironmentSetup.cs :
   
   Remplacer ""192.168.1.100"" par l'IP réelle du PC 2

4. Vérifier la connectivité :
   
   ping [IP_DU_PC2]
   ssh testuser@[IP_DU_PC2]

═══════════════════════════════════════════════════════════
";
        }

        /// <summary>
        /// Génère un rapport complet de l'environnement de test
        /// </summary>
        public string GenerateTestEnvironmentReport()
        {
            var report = new System.Text.StringBuilder();
            report.AppendLine("═══════════════════════════════════════════════════════════");
            report.AppendLine("  RAPPORT D'ENVIRONNEMENT DE TEST");
            report.AppendLine("═══════════════════════════════════════════════════════════");
            report.AppendLine();
            report.AppendLine($"Date de génération : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            report.AppendLine($"Répertoire de test : {BaseDirectory}");
            report.AppendLine($"IP locale : {GetLocalIPAddress()}");
            report.AppendLine();
            report.AppendLine("MACHINES DE TEST CONFIGURÉES :");
            report.AppendLine("--------------------------------------");

            foreach (var machine in TestMachines)
            {
                report.AppendLine($"• {machine.id}");
                report.AppendLine($"  Type : {machine.type}");
                report.AppendLine($"  IP   : {machine.ip}");
                if (machine is DataBase db)
                {
                    report.AppendLine($"  Port : {db.sshPort}");
                    report.AppendLine($"  User : {db.username}");
                }
                report.AppendLine();
            }

            report.AppendLine("═══════════════════════════════════════════════════════════");
            return report.ToString();
        }
    }
}
