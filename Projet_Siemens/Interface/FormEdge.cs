using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Machine = Projet_Siemens.Class.Machine;
using Projet_Siemens.Class;

namespace Projet_Siemens.Interface
{
    public partial class FormEdge : Form
    {
        private Form2 parentForm;
        public FormEdge(Form2 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            sourceMachineList.DataSource = new BindingList<Machine>(parentForm.network.machines);
            sourceMachineList.DisplayMember = "displayName";
            targetMachineList.DataSource = new BindingList<Machine>(parentForm.network.machines);
            targetMachineList.DisplayMember = "displayName";

        }

        private void edgeButton_Click(object sender, EventArgs e)
        {
            // both machines (source and target) must be selected
            if (sourceMachineList.SelectedItem == null || targetMachineList.SelectedItem == null)
            {
                MessageBox.Show("Please select a source and a target machine");
                return;
            }

            // both machines must be different
            if (sourceMachineList.SelectedItem == targetMachineList.SelectedItem)
            {
                MessageBox.Show("Source and target machines must be different");
                return;
            }

            //check if the source machine is already connected to the target machine
            if (parentForm.network.listOfEdges.Any(edge => edge.machineSource == sourceMachineList.SelectedItem && edge.machineTarget == targetMachineList.SelectedItem))
            {
                MessageBox.Show("The source machine is already connected to the target machine");
                return;
            }


            // we don't know yet if we will allow the user to create an edge between two machines that are already connected
            // we will allow it for now
            edgeIPSource.Text = (sourceMachineList.SelectedItem as Machine).ip;
            edgeIPTarget.Text = (targetMachineList.SelectedItem as Machine).ip;
            //edgePortSource.Text = (sourceMachineList.SelectedItem as Machine).port.ToString();
            newEdgePanel.Visible = true;

        }

        private void newEdgeButton_Click(object sender, EventArgs e)
        {
            bool birectional = false;
            bool firewall = false;
            // check there is no empty fields
            if (string.IsNullOrWhiteSpace(edgeProtocol.Text) ||
                string.IsNullOrWhiteSpace(edgePortSource.Text) ||
                string.IsNullOrWhiteSpace(edgePortTarget.Text) ||
                string.IsNullOrWhiteSpace(edgeIPSource.Text) ||
                string.IsNullOrWhiteSpace(edgeIPTarget.Text))
            {
                MessageBox.Show("Please fill all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // check if source port is an integer
            if (!int.TryParse(edgePortSource.Text, out _))
            {
                MessageBox.Show("Source port must be an integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // check if target port is an integer
            if (!int.TryParse(edgePortTarget.Text, out _))
            {
                MessageBox.Show("Target port must be an integer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // check if the edge is birectional 
            if (edgeIsBirectional.Checked)
            {
                birectional = true;
            }


            // check if there is a firewall
            if (edgeHasFirewall.Checked)
            {
                firewall = true;
            }


            // storage of source and target machines
            Machine sourceMachine = sourceMachineList.SelectedItem as Machine;
            Machine targetMachine = targetMachineList.SelectedItem as Machine;

            // create the edge
            Edge edge = new Edge(sourceMachine, targetMachine, edgeProtocol.Text, birectional, int.Parse(edgePortSource.Text), int.Parse(edgePortTarget.Text), edgeIPSource.Text, edgeIPTarget.Text, firewall);
            parentForm.network.addEdge(edge);

            // we want to modify it to display a window to specify the new edge to the user
            //List<Edge> networkEdges = parentForm.getNetwork().getListOfEdges();
            //StringBuilder message = new StringBuilder();
            //message.AppendLine("Machines du réseau:");
            //foreach (Edge edges in networkEdges)
            //{
            //    message.AppendLine($"- {edges.machineTarget.id} : {edges.machineSource.id}");
            //}

            //MessageBox.Show(message.ToString(), "Machines connectées", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // we have to empty the fields
            edgeProtocol.Text = "";
            edgePortSource.Text = "";
            edgePortTarget.Text = "";
            edgeIPSource.Text = "";
            edgeIPTarget.Text = "";
            edgeIsBirectional.Checked = false;
            edgeHasFirewall.Checked = false;
            sourceMachineList.SelectedItem = null;
            targetMachineList.SelectedItem = null;
            newEdgeButton.Visible = false;

            parentForm.addEdgeToGraph(edge);

            // Rediriger vers Form2
            this.Close();
            parentForm.setVisible();
        }


    }
}
