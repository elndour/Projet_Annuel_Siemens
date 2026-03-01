using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Machine = Projet_Siemens.Class.Machine;
using Projet_Siemens.Class;
using Projet_Siemens.BDD;
using Projet_Siemens.SSH;
using Projet_Siemens.Security;

namespace Projet_Siemens.Interface
{
    public partial class FormFileExtraction : Form
    {
        private Form2 parentForm;
        private List<string> detectedDataDirectories;

        public FormFileExtraction(Form2 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;

            // Détecter automatiquement les répertoires de données
            detectedDataDirectories = new List<string>();
            DetectDataDirectories();

            // Remplir la liste avec les machines du réseau + les répertoires détectés
            PopulateMachinesList();
        }

        /// <summary>
        /// Détecte automatiquement les répertoires de données dans Data/
        /// </summary>
        private void DetectDataDirectories()
        {
            try
            {
                string baseDirectory = Path.Combine(
                    Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName,
                    "Data"
                );

                if (Directory.Exists(baseDirectory))
                {
                    var directories = Directory.GetDirectories(baseDirectory);

                    foreach (var dir in directories)
                    {
                        string dirName = Path.GetFileName(dir);

                        // Ignorer les répertoires système
                        if (dirName != "SecurePackages" && !dirName.StartsWith("."))
                        {
                            // Vérifier si un répertoire contient des données
                            bool hasData = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories).Length > 0;

                            if (hasData)
                            {
                                // Vérifier si cette machine existe déjà dans le réseau
                                bool existsInNetwork = parentForm.network.machines.Any(m => m.id == dirName);

                                if (!existsInNetwork)
                                {
                                    detectedDataDirectories.Add(dirName);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Ignorer les erreurs de détection
                Console.WriteLine($"Erreur détection : {ex.Message}");
            }
        }

        /// <summary>
        /// Remplit la liste des machines avec les machines du réseau + les répertoires détectés
        /// </summary>
        private void PopulateMachinesList()
        {
            var items = new List<object>();

            // Ajouter les machines du réseau
            foreach (var machine in parentForm.network.machines)
            {
                items.Add(machine);
            }

            // Ajouter les répertoires détectés comme "machines virtuelles"
            foreach (var dataDir in detectedDataDirectories)
            {
                items.Add(new VirtualMachine { id = dataDir, displayName = $"📁 {dataDir} (Données détectées)", type = "VirtualData" });
            }

            machinesList.DataSource = items;
            machinesList.DisplayMember = "displayName";
        }

        private void kindOdextractionButton_Click(object sender, EventArgs e)
        {
            if (machinesList.SelectedItem == null)
            {
                MessageBox.Show("Please select a machine", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Machine selectedMachine = (Machine)machinesList.SelectedItem;

            if (selectedMachine.type == "DataBase")
            {
                // Extraction de données depuis la base de données
                ExtractDatabaseData(selectedMachine as DataBase);
            }
            else
            {
                // Extraction de fichiers depuis les serveurs (SSH/SFTP)
                ExtractServerFiles(selectedMachine);
            }
        }

        /// <summary>
        /// Extrait les fichiers depuis un serveur via SSH/SFTP
        /// </summary>
        private void ExtractServerFiles(Machine serverMachine)
        {
            try
            {
                // Demander les credentials SSH
                var credForm = new SSHCredentialsForm(serverMachine.ip);
                if (credForm.ShowDialog() != DialogResult.OK)
                    return;

                string baseDirectory = Path.Combine(
                    Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName,
                    "Data"
                );

                // Créer le FileCollector
                var collector = new FileCollector(
                    serverMachine,
                    baseDirectory,
                    credForm.Username,
                    credForm.Password,
                    credForm.Port
                );

                // Tester la connexion
                kindOdextractionButton.Enabled = false;
                kindOdextractionButton.Text = "Test connexion...";
                this.Cursor = Cursors.WaitCursor;

                if (!collector.TestConnections())
                {
                    kindOdextractionButton.Enabled = true;
                    kindOdextractionButton.Text = "Extract Data";
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Impossible de se connecter au serveur !", "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Confirmation
                var result = MessageBox.Show(
                    $"Voulez-vous lancer la collecte de fichiers depuis '{serverMachine.id}' ?\n\n" +
                    "Fichiers à récupérer :\n" +
                    "• Fichiers .log (journaux système)\n" +
                    "• Fichiers .xml (configurations)\n" +
                    "• Fichiers .config (paramètres)\n" +
                    "• Fichiers .nfo (informations)\n\n" +
                    $"Les fichiers seront sauvegardés dans Data/{serverMachine.id}/files/",
                    "Confirmer la collecte",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                {
                    kindOdextractionButton.Enabled = true;
                    kindOdextractionButton.Text = "Extract Data";
                    this.Cursor = Cursors.Default;
                    return;
                }

                // Lancer la collecte avec feedback
                kindOdextractionButton.Text = "Collecte en cours...";

                var report = collector.CollectAllFiles((status) =>
                {
                    // Mise à jour du status (peut être affiché dans un label)
                    Application.DoEvents();
                });

                // Réactiver le bouton
                kindOdextractionButton.Enabled = true;
                kindOdextractionButton.Text = "Extract Data";
                this.Cursor = Cursors.Default;

                // Afficher le rapport
                ShowFileCollectionReport(report);

                // Sauvegarder le rapport
                string reportPath = Path.Combine(baseDirectory, serverMachine.id, "files", "collection_report.json");
                collector.SaveReport(report, reportPath);

                // Ouvrir le dossier des résultats
                string resultsFolder = Path.Combine(baseDirectory, serverMachine.id, "files");
                if (Directory.Exists(resultsFolder))
                {
                    System.Diagnostics.Process.Start("explorer.exe", resultsFolder);
                }
            }
            catch (Exception ex)
            {
                kindOdextractionButton.Enabled = true;
                kindOdextractionButton.Text = "Extract Data";
                this.Cursor = Cursors.Default;

                MessageBox.Show(
                    $"Erreur lors de la collecte des fichiers :\n{ex.Message}",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Affiche un rapport de collecte de fichiers
        /// </summary>
        private void ShowFileCollectionReport(FileCollectionReport report)
        {
            var reportText = new StringBuilder();
            reportText.AppendLine("========== RAPPORT DE COLLECTE SSH/SFTP ==========");
            reportText.AppendLine($"Machine : {report.MachineId} ({report.MachineType})");
            reportText.AppendLine($"IP : {report.MachineIp}");
            reportText.AppendLine($"Date : {report.StartTime:yyyy-MM-dd HH:mm:ss}");
            reportText.AppendLine($"Durée : {report.Duration.TotalSeconds:F2} secondes");
            reportText.AppendLine();
            reportText.AppendLine($"Statut : {(report.Success ? "✓ RÉUSSI" : "✗ ÉCHEC")}");
            if (!string.IsNullOrEmpty(report.ErrorMessage))
            {
                reportText.AppendLine($"Erreur : {report.ErrorMessage}");
            }
            reportText.AppendLine();
            reportText.AppendLine("========== FICHIERS COLLECTÉS ==========");

            foreach (var kvp in report.FilesCollected)
            {
                reportText.AppendLine($"• .{kvp.Key,-10} : {kvp.Value,5} fichiers");
            }

            reportText.AppendLine();
            reportText.AppendLine($"TOTAL : {report.TotalFiles} fichiers");

            MessageBox.Show(
                reportText.ToString(),
                "Collecte terminée",
                MessageBoxButtons.OK,
                report.Success ? MessageBoxIcon.Information : MessageBoxIcon.Warning
            );
        }

        /// <summary>
        /// Extrait les données MES depuis une base de données et les sauvegarde en JSON
        /// </summary>
        private void ExtractDatabaseData(DataBase dbMachine)
        {
            try
            {
                // Créer la connexion
                Connection connectionInfo = new Connection(
                    dbMachine.ip,
                    dbMachine.instanceName,
                    dbMachine.username,
                    dbMachine.password,
                    dbMachine.sshPort.ToString()
                );

                DatabaseHelper dbHelper = new DatabaseHelper(connectionInfo);

                // Tester la connexion
                if (!dbHelper.TestConnection())
                {
                    MessageBox.Show("Impossible de se connecter à la base de données !", "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Afficher une boîte de dialogue de confirmation
                var result = MessageBox.Show(
                    $"Voulez-vous lancer l'extraction complète des données MES de la base '{dbMachine.id}' ?\n\n" +
                    "Cela va extraire :\n" +
                    "• Ordres de production (30 derniers jours)\n" +
                    "• Logs d'erreurs critiques\n" +
                    "• Tâches MES actives\n" +
                    "• Statistiques des machines\n" +
                    "• Événements d'arrêt\n" +
                    "• Métriques de qualité\n" +
                    "• Indicateurs de santé système\n\n" +
                    "Les résultats seront sauvegardés dans Data/{dbMachine.id}/database_results/",
                    "Confirmer l'extraction",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                // Désactiver le bouton pendant l'extraction
                kindOdextractionButton.Enabled = false;
                kindOdextractionButton.Text = "Extraction en cours...";
                this.Cursor = Cursors.WaitCursor;

                // Lancer l'extraction complète
                string baseDirectory = Path.Combine(
                    Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName,
                    "Data"
                );

                ExtractionReport report = dbHelper.ExecuteFullMESDataCollection(dbMachine.id, baseDirectory);

                // Réactiver le bouton
                kindOdextractionButton.Enabled = true;
                kindOdextractionButton.Text = "Extract Data";
                this.Cursor = Cursors.Default;

                // Afficher le rapport
                ShowExtractionReport(report);

                // Ouvrir le dossier des résultats
                string resultsFolder = Path.Combine(baseDirectory, dbMachine.id, "database_results");
                if (Directory.Exists(resultsFolder))
                {
                    System.Diagnostics.Process.Start("explorer.exe", resultsFolder);
                }
            }
            catch (Exception ex)
            {
                kindOdextractionButton.Enabled = true;
                kindOdextractionButton.Text = "Extract Data";
                this.Cursor = Cursors.Default;

                MessageBox.Show(
                    $"Erreur lors de l'extraction des données :\n{ex.Message}",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Affiche un rapport détaillé de l'extraction
        /// </summary>
        private void ShowExtractionReport(ExtractionReport report)
        {
            StringBuilder reportText = new StringBuilder();
            reportText.AppendLine("========== RAPPORT D'EXTRACTION ==========");
            reportText.AppendLine($"Base de données : {report.DatabaseId}");
            reportText.AppendLine($"Date d'extraction : {report.StartTime:yyyy-MM-dd HH:mm:ss}");
            reportText.AppendLine($"Durée totale : {report.TotalDuration.TotalSeconds:F2} secondes");
            reportText.AppendLine();
            reportText.AppendLine($"Total de requêtes : {report.TotalQueries}");
            reportText.AppendLine($"✓ Réussies : {report.SuccessfulQueries}");
            reportText.AppendLine($"✗ Échouées : {report.FailedQueries}");
            reportText.AppendLine();
            reportText.AppendLine("========== DÉTAILS DES REQUÊTES ==========");

            foreach (var query in report.Queries)
            {
                string status = query.Success ? "✓" : "✗";
                reportText.AppendLine($"{status} {query.QueryName}");
                reportText.AppendLine($"   Fichier: {query.OutputFile}");
                reportText.AppendLine($"   Durée: {query.Duration.TotalSeconds:F2}s");
                if (!query.Success && !string.IsNullOrEmpty(query.ErrorMessage))
                {
                    reportText.AppendLine($"   Erreur: {query.ErrorMessage}");
                }
                reportText.AppendLine();
            }

            // Afficher le rapport dans une MessageBox
            MessageBox.Show(
                reportText.ToString(),
                "Extraction terminée",
                MessageBoxButtons.OK,
                report.FailedQueries > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information
            );
        }

        /// <summary>
        /// Package et crypte les données collectées d'une machine
        /// </summary>
        private void packageButton_Click(object sender, EventArgs e)
        {
            if (machinesList.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une machine", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Machine selectedMachine = (Machine)machinesList.SelectedItem;

            try
            {
                // Chemin des données
                string baseDirectory = Path.Combine(
                    Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName,
                    "Data"
                );

                string machineDataPath = Path.Combine(baseDirectory, selectedMachine.id);

                // Vérifier que des données existent
                if (!Directory.Exists(machineDataPath))
                {
                    MessageBox.Show(
                        $"Aucune donnée trouvée pour la machine '{selectedMachine.id}'\n\n" +
                        "Veuillez d'abord effectuer une extraction de données.",
                        "Erreur",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Demander confirmation
                var result = MessageBox.Show(
                    $"Créer un package sécurisé pour '{selectedMachine.id}' ?\n\n" +
                    "Le package inclura :\n" +
                    "✓ Compression ZIP\n" +
                    "✓ Cryptage AES-256\n" +
                    "✓ Génération automatique de mot de passe\n\n" +
                    $"Format : Siemens_Debug_{selectedMachine.id}_[Date].zip.enc",
                    "Confirmer le packaging",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                // Demander si l'utilisateur veut fournir son propre mot de passe
                var pwdChoice = MessageBox.Show(
                    "Voulez-vous fournir votre propre mot de passe ?\n\n" +
                    "OUI : Vous saisissez un mot de passe\n" +
                    "NON : Génération automatique d'un mot de passe fort",
                    "Mot de passe de cryptage",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );

                if (pwdChoice == DialogResult.Cancel)
                    return;

                string password = null;
                if (pwdChoice == DialogResult.Yes)
                {
                    // Demander le mot de passe
                    password = Microsoft.VisualBasic.Interaction.InputBox(
                        "Entrez le mot de passe de cryptage (minimum 16 caractères) :",
                        "Mot de passe",
                        ""
                    );

                    if (string.IsNullOrEmpty(password) || password.Length < 16)
                    {
                        MessageBox.Show("Le mot de passe doit contenir au moins 16 caractères", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Désactiver le bouton pendant le traitement
                packageButton.Enabled = false;
                packageButton.Text = "Packaging...";
                this.Cursor = Cursors.WaitCursor;

                // Créer le répertoire de sortie
                string outputDirectory = Path.Combine(baseDirectory, "SecurePackages");
                Directory.CreateDirectory(outputDirectory);

                // Créer le package sécurisé
                var packageManager = new SecurePackageManager();
                var report = packageManager.CreateSecurePackage(
                    selectedMachine.id,
                    baseDirectory,
                    outputDirectory,
                    password
                );

                // Réactiver le bouton
                packageButton.Enabled = true;
                packageButton.Text = "📦 Package & Encrypt";
                this.Cursor = Cursors.Default;

                // Afficher le rapport
                MessageBox.Show(
                    report.GetSummary(),
                    report.Success ? "Package créé" : "Erreur",
                    MessageBoxButtons.OK,
                    report.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error
                );

                // Sauvegarder le rapport
                if (report.Success)
                {
                    string reportPath = Path.Combine(outputDirectory, $"{selectedMachine.id}_package_report.json");
                    packageManager.SaveReport(report, reportPath);

                    // Ouvrir le dossier
                    System.Diagnostics.Process.Start("explorer.exe", outputDirectory);
                }
            }
            catch (Exception ex)
            {
                packageButton.Enabled = true;
                packageButton.Text = "📦 Package & Encrypt";
                this.Cursor = Cursors.Default;

                MessageBox.Show(
                    $"Erreur lors du packaging :\n{ex.Message}",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
