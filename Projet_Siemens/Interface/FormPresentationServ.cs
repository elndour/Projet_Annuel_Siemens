using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projet_Siemens.Class;

namespace Projet_Siemens.Interface
{
    public partial class FormPresentationServ : Form
    {
        private Form2 parentForm;
        public FormPresentationServ(Form2 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
        }

        private void newPresServButton_Click(object sender, EventArgs e)
        {
            // Check if the fields are empty
            if (string.IsNullOrWhiteSpace(presServURLText.Text) ||
                string.IsNullOrWhiteSpace(presServPortText.Text) ||
                string.IsNullOrWhiteSpace(presServIPText.Text) ||
                string.IsNullOrWhiteSpace(presServIPText.Text) ||
                string.IsNullOrWhiteSpace(presServRepositoryText.Text))
            {
                MessageBox.Show("Please fill all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if the port is an integer
            if (!int.TryParse(presServPortText.Text, out int sshPort))
            {
                MessageBox.Show("Please enter a valid SSH port, which is an integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if there is no database with the same name in the network 
            if (parentForm.network.presentationServers.Any(ps => ps.id.Equals(presServIDText.Text)))
            {
                MessageBox.Show("There is already a presentation server with the same name in the network, please change it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // There is no error, we can add the application server to the network and display it on the graph
            Presentation_Server presServ = new Presentation_Server(presServIDText.Text, presServIPText.Text, int.Parse(presServPortText.Text), presServURLText.Text, "PresentationServer", presServRepositoryText.Text);
            parentForm.addMachineToNetwork(presServ);
            parentForm.addMachineToListOfMachines(presServ);

            parentForm.AddNodeToGraph(presServ);
            presServURLText.Text = "";
            presServIDText.Text = "";
            presServIPText.Text = "";
            presServPortText.Text = "";

            this.Close();
            parentForm.setVisible();

        }
    }
}
