using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Msagl.Layout.Layered;
using Projet_Siemens.Class;

namespace Projet_Siemens.Interface
{
    public partial class FormApplicationServer : Form
    {
        private Form2 parentForm;
        public FormApplicationServer(Form2 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
        }

        private void newAppServButton_Click(object sender, EventArgs e)
        {
            // Check if the fields are empty
            if (string.IsNullOrWhiteSpace(appServDescriptionText.Text) ||
                string.IsNullOrWhiteSpace(appServPortText.Text) ||
                string.IsNullOrWhiteSpace(appServIPText.Text) ||
                string.IsNullOrWhiteSpace(appServIDText.Text) ||
                string.IsNullOrWhiteSpace(appServRepositoryText.Text))

            {
                MessageBox.Show("Please fill all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if the port is an integer
            if (!int.TryParse(appServPortText.Text, out int sshPort))
            {
                MessageBox.Show("Please enter a valid SSH port, which is an integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if there is no database with the same name in the network 
            if (parentForm.network.applicationServers.Any(ap => ap.id.Equals(appServIDText.Text)))
            {
                MessageBox.Show("There is already an application server with the same name in the network, please change it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // There is no error, we can add the application server to the network and display it on the graph
            ApplicationServer appServ = new ApplicationServer(appServIPText.Text, int.Parse(appServPortText.Text), appServDescriptionText.Text, appServIDText.Text, "ApplicationServer", appServRepositoryText.Text);
            parentForm.addMachineToNetwork(appServ);
            parentForm.addMachineToListOfMachines(appServ);

            parentForm.AddNodeToGraph(appServ);
            appServDescriptionText.Text = "";
            appServIDText.Text = "";
            appServIPText.Text = "";
            appServPortText.Text = "";

            this.Close();
            parentForm.setVisible();

        }

      
    }
}
