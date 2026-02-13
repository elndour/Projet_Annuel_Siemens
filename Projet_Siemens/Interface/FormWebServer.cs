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
    public partial class FormWebServer : Form
    {
        private Form2 parentForm;
        public FormWebServer(Form2 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
        }

        private void newWebServButton_Click(object sender, EventArgs e)
        {
            // Check if the fields are empty
            if (string.IsNullOrWhiteSpace(webServAPIText.Text) ||
                string.IsNullOrWhiteSpace(webServAPIEndpointText.Text) ||
                string.IsNullOrWhiteSpace(webServIDText.Text) ||
                string.IsNullOrWhiteSpace(webServIPText.Text) ||
                string.IsNullOrWhiteSpace(webServRepositoryText.Text))
            {
                MessageBox.Show("Please fill all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if there is no database with the same name in the network 
            if (parentForm.network.webServers.Any(ws => ws.id.Equals(webServIDText.Text)))
            {
                MessageBox.Show("There is already a web server with the same name in the network, please change it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // There is no error, we can add the application server to the network and display it on the graph
            WebServer webServ = new WebServer(webServIDText.Text, webServIDText.Text, webServAPIEndpointText.Text, webServAPIText.Text, "WebServer", webServRepositoryText.Text);
            parentForm.addMachineToNetwork(webServ);
            parentForm.addMachineToListOfMachines(webServ);

            parentForm.AddNodeToGraph(webServ);
            webServAPIEndpointText.Text = "";
            webServIDText.Text = "";
            webServIPText.Text = "";
            webServAPIText.Text = "";

            this.Close();
            parentForm.setVisible();
        }
    }
}
