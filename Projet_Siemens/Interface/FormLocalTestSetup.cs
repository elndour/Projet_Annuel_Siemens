using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using Projet_Siemens.Class;
using Projet_Siemens.Test;

namespace Projet_Siemens.Interface
{
    public partial class FormLocalTestSetup : Form
    {
        private Form2 parentForm;
        private TestEnvironmentSetup testSetup;
        private List<Machine> createdMachines;

        public FormLocalTestSetup(Form2 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            this.testSetup = new TestEnvironmentSetup();
            this.createdMachines = new List<Machine>();
        }

        private void FormLocalTestSetup_Load(object sender, EventArgs e)
        {
            // Afficher l'IP locale
            string localIp = testSetup.GetLocalIPAddress();
            pc1IpLabel.Text = $"IP: {localIp} (localhost: 127.0.0.1)";

            // Message de bienvenue
            reportTextBox.Text = @"👋 BIENVENUE DANS LA CONFIGURATION DE TEST LOCAL

Ce formulaire vous aide à configurer vos 2 PCs personnels comme environnement de test pour le projet Siemens.

ÉTAPES RECOMMANDÉES :
1️⃣ Configurer le PC 1 (cet ordinateur)
   - Générer des fichiers de test
   - Créer une base de données SQLite

2️⃣ Configurer le PC 2
   - Installer OpenSSH Server (voir guide)
   - Entrer son adresse IP
   - Tester la connexion

3️⃣ Créer les machines de test
   - Ajouter les machines au réseau du projet

Cliquez sur le bouton 'GUIDE' pour voir comment installer SSH sur Windows.
";
        }

        private void generateFilesPC1Button_Click(object sender, EventArgs e)
        {
            try
            {
                generateFilesPC1Button.Enabled = false;
                generateFilesPC1Button.Text = "Génération...";
                this.Cursor = Cursors.WaitCursor;

                testSetup.GenerateTestFiles("PC1_TestServer", 20);

                reportTextBox.Text = $@"✅ FICHIERS DE TEST GÉNÉRÉS POUR PC 1

📁 Emplacement : {testSetup.BaseDirectory}\PC1_TestServer\test_files\

Fichiers créés :
• Fichiers .log - Journaux système simulés
• Fichiers .xml - Configurations XML
• Fichiers .config - Fichiers de paramètres
• Fichiers .nfo - Informations système

💡 Ces fichiers seront utilisés pour tester la collecte SSH/SFTP.

PROCHAINE ÉTAPE :
Vous pouvez maintenant créer une base de données de test ou configurer le PC 2.
";

                pc1StatusLabel.Text = "✓ Fichiers créés";
                pc1StatusLabel.ForeColor = Color.Green;

                MessageBox.Show(
                    "20 fichiers de test ont été générés avec succès !",
                    "Succès",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erreur lors de la génération des fichiers :\n{ex.Message}",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                generateFilesPC1Button.Enabled = true;
                generateFilesPC1Button.Text = "📁 Générer fichiers de test (20 fichiers)";
                this.Cursor = Cursors.Default;
            }
        }

        private void createDbPC1Button_Click(object sender, EventArgs e)
        {
            try
            {
                createDbPC1Button.Enabled = false;
                createDbPC1Button.Text = "Création...";
                this.Cursor = Cursors.WaitCursor;

                string dbPath = testSetup.CreateTestSQLiteDatabase("PC1_TestDatabase");

                if (!string.IsNullOrEmpty(dbPath))
                {
                    reportTextBox.Text = $@"✅ BASE DE DONNÉES SQLITE CRÉÉE

📁 Emplacement : {dbPath}

La base de données de test contient :
• Tables de production (ProductionOrders, Tasks)
• Logs système (SystemLogs)
• Données machines (MachineStats)
• Événements (StopEvents)
• Métriques de qualité (QualityMetrics)

💡 Vous pouvez maintenant tester l'extraction de données depuis cette base.

POUR TESTER :
1. Dans FormFileExtraction, sélectionnez 'PC1_TestDatabase'
2. Cliquez sur 'Extract Data'
3. Les données seront extraites au format JSON
";

                    MessageBox.Show(
                        "Base de données SQLite créée avec succès !",
                        "Succès",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    throw new Exception("La création de la base de données a échoué.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erreur lors de la création de la base :\n{ex.Message}",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                createDbPC1Button.Enabled = true;
                createDbPC1Button.Text = "🗄️ Créer base de données SQLite de test";
                this.Cursor = Cursors.Default;
            }
        }

        private void testConnectionPC2Button_Click(object sender, EventArgs e)
        {
            try
            {
                string pc2Ip = pc2IpTextBox.Text.Trim();

                if (string.IsNullOrEmpty(pc2Ip))
                {
                    MessageBox.Show(
                        "Veuillez entrer l'adresse IP du PC 2",
                        "IP manquante",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                testConnectionPC2Button.Enabled = false;
                testConnectionPC2Button.Text = "Test en cours...";
                this.Cursor = Cursors.WaitCursor;

                // Test de ping
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(pc2Ip, 3000);

                if (reply.Status == IPStatus.Success)
                {
                    pc2StatusLabel.Text = $"✓ Connecté (ping: {reply.RoundtripTime}ms)";
                    pc2StatusLabel.ForeColor = Color.Green;
                    generateFilesPC2Button.Enabled = true;

                    reportTextBox.Text = $@"✅ PC 2 ACCESSIBLE !

Adresse IP : {pc2Ip}
Temps de réponse : {reply.RoundtripTime} ms
Statut : Connecté

💡 PROCHAINES ÉTAPES :

1. Assurez-vous que SSH est installé sur PC 2
   - Voir le guide d'installation (bouton ci-dessus)

2. Créez un utilisateur de test :
   net user testuser test123 /add
   net localgroup Administrateurs testuser /add

3. Testez SSH depuis ce PC :
   ssh testuser@{pc2Ip}

4. (Optionnel) Générer des fichiers de test sur PC 2
   - Nécessite une connexion SSH active
";

                    MessageBox.Show(
                        $"✓ PC 2 est accessible !\n\nPing: {reply.RoundtripTime} ms\n\nVous pouvez maintenant créer les machines de test.",
                        "Connexion réussie",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    pc2StatusLabel.Text = $"✗ Échec : {reply.Status}";
                    pc2StatusLabel.ForeColor = Color.Red;

                    reportTextBox.Text = $@"❌ PC 2 NON ACCESSIBLE

Adresse IP : {pc2Ip}
Statut : {reply.Status}

CAUSES POSSIBLES :

1️⃣ L'adresse IP est incorrecte
   - Vérifiez avec 'ipconfig' sur le PC 2

2️⃣ Le PC 2 n'est pas sur le même réseau
   - Connectez les deux PCs au même réseau local

3️⃣ Le pare-feu bloque les pings
   - Désactivez temporairement le pare-feu pour tester
   - Ou ajoutez une règle pour autoriser ICMP

4️⃣ Le PC 2 est éteint
   - Assurez-vous que le PC 2 est allumé

SOLUTION ALTERNATIVE :
Vous pouvez quand même utiliser uniquement le PC 1 (localhost) pour vos tests.
";

                    MessageBox.Show(
                        $"✗ Impossible de contacter le PC 2\n\nStatut: {reply.Status}\n\nVérifiez l'adresse IP et la connectivité réseau.",
                        "Échec de connexion",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                pc2StatusLabel.Text = "✗ Erreur";
                pc2StatusLabel.ForeColor = Color.Red;

                MessageBox.Show(
                    $"Erreur lors du test de connexion :\n{ex.Message}",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                testConnectionPC2Button.Enabled = true;
                testConnectionPC2Button.Text = "🔌 Tester la connexion";
                this.Cursor = Cursors.Default;
            }
        }

        private void generateFilesPC2Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Cette fonctionnalité nécessite une connexion SSH active au PC 2.\n\n" +
                "Pour le moment, générez les fichiers manuellement sur le PC 2 en :\n" +
                "1. Copiant TestEnvironmentSetup.cs sur le PC 2\n" +
                "2. Exécutant la génération de fichiers localement\n\n" +
                "OU utilisez uniquement le PC 1 pour vos tests.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void setupGuideButton_Click(object sender, EventArgs e)
        {
            string guide = testSetup.GetSSHSetupGuide();
            reportTextBox.Text = guide;

            MessageBox.Show(
                "Le guide de configuration SSH a été affiché dans la zone de texte.\n\n" +
                "Suivez les étapes pour installer OpenSSH Server sur Windows 10/11.",
                "Guide SSH",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void createMachinesButton_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "Créer les machines de test et les ajouter au réseau ?\n\n" +
                    "Cela va créer :\n" +
                    "• PC1_TestServer (localhost)\n" +
                    "• PC1_TestDatabase (SQLite local)\n" +
                    "• PC2_TestServer (deuxième PC)\n\n" +
                    "Les machines apparaîtront dans la liste de FormFileExtraction.",
                    "Confirmer la création",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                createMachinesButton.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                // Créer les machines de test
                createdMachines = testSetup.CreateLocalTestMachines();

                // Mettre à jour l'IP du PC 2 si modifiée
                string pc2Ip = pc2IpTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(pc2Ip) && pc2Ip != "192.168.1.100")
                {
                    var pc2Machine = createdMachines.Find(m => m.id == "PC2_TestServer");
                    if (pc2Machine != null)
                    {
                        pc2Machine.ip = pc2Ip;
                    }
                }

                // Ajouter les machines au réseau du parent
                foreach (var machine in createdMachines)
                {
                    parentForm.network.machines.Add(machine);
                }

                // Générer et afficher le rapport
                string report = testSetup.GenerateTestEnvironmentReport();
                reportTextBox.Text = report;

                this.Cursor = Cursors.Default;

                MessageBox.Show(
                    $"✅ {createdMachines.Count} machines de test créées !\n\n" +
                    "Les machines sont maintenant disponibles dans FormFileExtraction.\n\n" +
                    "Vous pouvez fermer cette fenêtre et commencer vos tests.",
                    "Succès",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                createMachinesButton.Enabled = true;
                this.Cursor = Cursors.Default;

                MessageBox.Show(
                    $"Erreur lors de la création des machines :\n{ex.Message}",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
