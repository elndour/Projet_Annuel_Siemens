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

namespace Projet_Siemens.Interface
{
    public partial class FormFileExtraction : Form
    {
        private Form2 parentForm;
        public FormFileExtraction(Form2 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            machinesList.DataSource = new BindingList<Machine>(parentForm.network.machines);
            machinesList.DisplayMember = "displayName";


        }

        private void kindOdextractionButton_Click(object sender, EventArgs e)
        {
            if (machinesList.SelectedItem == null)
            {
                MessageBox.Show("Please select a machine", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }else
            {
                Machine selectedMachine = (Machine)machinesList.SelectedItem;

                if (selectedMachine.type == "DataBase")
                {
                    DataBase dbMachine = selectedMachine as DataBase;
                    // Utiliser les propriétés de dbMachine
                    Connection connectionInfo = new Connection(
                    dbMachine.ip,                    // Hôte
                    dbMachine.instanceName,    // Nom de service Oracle
                    dbMachine.username,                     // Utilisateur Oracle
                    dbMachine.password,                         // Mot de passe
                    dbMachine.sshPort.ToString()            // Port
                    );

                    DatabaseHelper dbHelper = new DatabaseHelper(connectionInfo);

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
                else
                {
                    panel2.Visible = true;
                }
            }
            
        }
    }
}
