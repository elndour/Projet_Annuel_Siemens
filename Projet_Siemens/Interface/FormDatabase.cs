using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using Projet_Siemens.Class;
using Projet_Siemens.Interface;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Projet_Siemens.Interface
{
    public partial class FormDatabase : Form
    {
        private Form2 parentForm;
        public FormDatabase(Form2 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
        }

        private void newDataBaseButton_Click(object sender, EventArgs e)
        {
            // Check if the fields are empty
            if (string.IsNullOrWhiteSpace(dataBaseName.Text) ||
                string.IsNullOrWhiteSpace(dataBaseIpAdress.Text) ||
                string.IsNullOrWhiteSpace(dataBaseSshPort.Text) ||
                string.IsNullOrWhiteSpace(dataBaseusername.Text) ||
                string.IsNullOrWhiteSpace(dataBasemotdepasse.Text))

            {
                MessageBox.Show("Please fill all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if the SSH port is an integer
            if (!int.TryParse(dataBaseSshPort.Text, out int sshPort))
            {
                MessageBox.Show("Please enter a valid SSH port, which is an integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if there is no database with the same name in the network 
            if (parentForm.network.dataBases.Any(db => db.id.Equals(dataBaseName.Text)))
            {
                MessageBox.Show("There is already a database with the same name in the network, please change it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // There is no error, we can add the database to the network and display it on the graph
            DataBase dataBase = new DataBase(dataBaseName.Text, dataBaseIpAdress.Text, int.Parse(dataBaseSshPort.Text), dataBasemotdepasse.Text, dataBaseusername.Text, "DataBase");

            

            parentForm.addMachineToNetwork(dataBase);
            parentForm.addMachineToListOfMachines(dataBase);

            List<Machine> networkMachines = parentForm.network.machines;
            StringBuilder message = new StringBuilder();
            message.AppendLine("Machines du réseau:");
            foreach (Machine machine in networkMachines)
            {
                message.AppendLine($"- {machine.id} : {machine.ip} ({machine.type})");
            }

            // Chiffrer le mot de passe en utilisant MD5
            string encryptedPassword = EncryptPassword(dataBasemotdepasse.Text);

            // Création d'un dictionnaire avec les infos
            var dbInfo = new Dictionary<string, object>
            {
                { "Id DataBase", dataBaseName.Text },
                { "IpAddress", dataBaseIpAdress.Text },
                { "SshPort", dataBaseSshPort.Text },
                { "username", dataBaseusername.Text },
                { "motdepasse", encryptedPassword}
            };

            parentForm.AddNodeToGraph(dataBase);
            dataBaseName.Text = "";
            dataBaseIpAdress.Text = "";
            dataBaseSshPort.Text = "";

            // Rediriger vers Form2
            this.Close();
            parentForm.setVisible();

        }
        private string EncryptPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convertir le tableau de bytes en une chaîne hexadécimale
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

    }


}
