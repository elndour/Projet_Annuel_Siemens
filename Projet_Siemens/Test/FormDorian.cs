using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Projet_Siemens.BDD;

namespace Projet_Siemens
{
    public partial class FormDorian : Form
    {
        private DatabaseHelper dbHelper;
        private DataGridView myDataGridView;

        public FormDorian()
        {
            InitializeComponent();

            // Ajout manuel du DataGridView
            myDataGridView = new DataGridView
            {
                Location = new Point(10, 10), // Position sur le formulaire
                Size = new Size(500, 300),   // Taille du DataGridView
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // Ajout du DataGridView au formulaire
            this.Controls.Add(myDataGridView);

            Connection connectionInfo = new Connection(
                "10.16.140.39",                    // Hôte
                "orclpdb.sfa.univ-poitiers.fr",    // Nom de service Oracle
                "GPHYSIEMENS",                     // Utilisateur Oracle
                "supreme",                         // Mot de passe
                "1521"                             // Port
            );

            dbHelper = new DatabaseHelper(connectionInfo);
        }

        private void FormDorian_Load(object sender, EventArgs e)
        {
            if (dbHelper.TestConnection())
            {
                MessageBox.Show("Connexion Oracle réussie !");

                string sqlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PA.sql");

                if (File.Exists(sqlPath))
                {
                    if (dbHelper.ExecuteSqlFileInBlocks(sqlPath))
                    {
                        MessageBox.Show("Script SQL exécuté avec succès !");
                    }
                    else
                    {
                        MessageBox.Show("Échec lors de l'exécution du script SQL.");
                    }
                }
                else
                {
                    MessageBox.Show("Fichier SQL introuvable.");
                }
            }
        }
    }
}
