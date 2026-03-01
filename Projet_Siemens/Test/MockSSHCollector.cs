using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Projet_Siemens.SSH;
using Projet_Siemens.Class;

namespace Projet_Siemens.Test
{
    /// <summary>
    /// Teste la collecte SSH/SFTP sans serveur réel
    /// Simule les fichiers collectés
    /// </summary>
    public class MockSSHCollector
    {
        public static void SimulateFileCollection(Machine machine, string outputBaseDirectory)
        {
            try
            {
                string machineDir = Path.Combine(outputBaseDirectory, machine.id, "files");
                Directory.CreateDirectory(machineDir);

                // 1. Simuler system_info.json
                var systemInfo = new Dictionary<string, string>
                {
                    { "Hostname", $"mock-{machine.id}" },
                    { "OS", "Ubuntu 20.04 LTS" },
                    { "Kernel", "5.4.0-42-generic" },
                    { "Uptime", "5 days, 3 hours" },
                    { "CPU", "Intel Xeon E5-2670" },
                    { "RAM", "16GB" },
                    { "Disk", "250GB" }
                };

                string systemInfoPath = Path.Combine(machineDir, "system_info.json");
                File.WriteAllText(systemInfoPath, JsonSerializer.Serialize(systemInfo, new JsonSerializerOptions { WriteIndented = true }));

                // 2. Simuler fichiers .log
                string logsDir = Path.Combine(machineDir, "logs");
                Directory.CreateDirectory(logsDir);

                File.WriteAllText(Path.Combine(logsDir, "syslog"), 
                    $"2025-01-09 10:00:00 [INFO] System started\n" +
                    $"2025-01-09 10:05:00 [WARN] High CPU usage detected\n" +
                    $"2025-01-09 10:10:00 [ERROR] Connection timeout to database");

                File.WriteAllText(Path.Combine(logsDir, "application.log"),
                    $"2025-01-09 09:00:00 Application initialized\n" +
                    $"2025-01-09 09:05:00 MES module loaded\n" +
                    $"2025-01-09 09:10:00 Production line connected");

                // 3. Simuler fichiers .xml
                string configDir = Path.Combine(machineDir, "config");
                Directory.CreateDirectory(configDir);

                File.WriteAllText(Path.Combine(configDir, "app.config"),
                    "<?xml version=\"1.0\"?>\n" +
                    "<configuration>\n" +
                    "  <appSettings>\n" +
                    "    <add key=\"DatabaseServer\" value=\"192.168.1.10\" />\n" +
                    "    <add key=\"MESVersion\" value=\"2.5.1\" />\n" +
                    "  </appSettings>\n" +
                    "</configuration>");

                File.WriteAllText(Path.Combine(configDir, "database.xml"),
                    "<?xml version=\"1.0\"?>\n" +
                    "<database>\n" +
                    "  <connection>Server=192.168.1.10;Port=1521</connection>\n" +
                    "  <timeout>30</timeout>\n" +
                    "</database>");

                // 4. Simuler fichiers .nfo
                string infoDir = Path.Combine(machineDir, "info");
                Directory.CreateDirectory(infoDir);

                File.WriteAllText(Path.Combine(infoDir, "machine.nfo"),
                    $"Machine ID: {machine.id}\n" +
                    $"Type: {machine.type}\n" +
                    $"IP: {machine.ip}\n" +
                    $"Status: Running\n" +
                    $"Last Maintenance: 2025-01-01");

                // 5. Créer le rapport
                var report = new FileCollectionReport
                {
                    MachineId = machine.id,
                    MachineType = machine.type,
                    MachineIp = machine.ip,
                    StartTime = DateTime.Now.AddSeconds(-5),
                    EndTime = DateTime.Now,
                    Success = true,
                    FilesCollected = new Dictionary<string, int>
                    {
                        { "log", 2 },
                        { "xml", 2 },
                        { "config", 0 },
                        { "nfo", 1 }
                    },
                    TotalFiles = 5
                };

                string reportPath = Path.Combine(machineDir, "collection_report.json");
                File.WriteAllText(reportPath, JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true }));

                System.Windows.Forms.MessageBox.Show(
                    $"Simulation de collecte SSH réussie !\n\n" +
                    $"Machine : {machine.id}\n" +
                    $"Fichiers créés : 5\n" +
                    $"Dossier : {machineDir}",
                    "Test Mock SSH",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information
                );

                // Ouvrir le dossier
                System.Diagnostics.Process.Start("explorer.exe", machineDir);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Erreur : {ex.Message}", "Erreur", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
