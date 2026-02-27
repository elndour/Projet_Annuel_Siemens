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
    public partial class FormSubNetwork : Form
    {
        private Form2 parentForm;

        public FormSubNetwork(Form2 parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            LoadSubNetworks();
        }

        private void LoadSubNetworks()
        {
            subNetworkListBox.Items.Clear();
            foreach (var subnet in parentForm.network.subNetworks)
            {
                subNetworkListBox.Items.Add(subnet);
            }
        }

        private void createSubNetworkButton_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(subNetworkIdTextBox.Text) ||
                string.IsNullOrWhiteSpace(subNetworkNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(subNetworkSubnetTextBox.Text))
            {
                MessageBox.Show("Please fill all required fields (ID, Name, Subnet)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check for duplicate ID
            if (parentForm.network.subNetworks.Any(sn => sn.id == subNetworkIdTextBox.Text))
            {
                MessageBox.Show("A subnet with this ID already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create new SubNetwork
            SubNetwork newSubNet = new SubNetwork(
                subNetworkIdTextBox.Text,
                subNetworkNameTextBox.Text,
                subNetworkSubnetTextBox.Text,
                subNetworkVlanTextBox.Text,
                subNetworkDescTextBox.Text
            );

            // Add to network
            parentForm.network.addSubNetwork(newSubNet);

            // Refresh the graph
            parentForm.RefreshGraphWithSubNetworks();

            MessageBox.Show($"SubNetwork '{newSubNet.name}' created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear fields
            subNetworkIdTextBox.Clear();
            subNetworkNameTextBox.Clear();
            subNetworkSubnetTextBox.Clear();
            subNetworkVlanTextBox.Clear();
            subNetworkDescTextBox.Clear();

            LoadSubNetworks();
        }

        private void assignMachinesButton_Click(object sender, EventArgs e)
        {
            if (subNetworkListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a subnet first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SubNetwork selectedSubNet = (SubNetwork)subNetworkListBox.SelectedItem;

            // Create a form to select machines
            Form machineSelectionForm = new Form
            {
                Text = $"Assign Machines to {selectedSubNet.name}",
                Size = new Size(400, 500),
                StartPosition = FormStartPosition.CenterParent
            };

            CheckedListBox machinesCheckedList = new CheckedListBox
            {
                Dock = DockStyle.Fill,
                CheckOnClick = true
            };

            // Add all machines
            foreach (var machine in parentForm.network.machines)
            {
                int index = machinesCheckedList.Items.Add(machine);
                // Check if already in this subnet
                if (selectedSubNet.ContainsMachine(machine))
                {
                    machinesCheckedList.SetItemChecked(index, true);
                }
            }

            Button confirmButton = new Button
            {
                Text = "Confirm",
                Dock = DockStyle.Bottom,
                Height = 40
            };

            confirmButton.Click += (s, ev) =>
            {
                // Clear current machines
                selectedSubNet.machines.Clear();

                // Add selected machines
                foreach (Machine machine in machinesCheckedList.CheckedItems)
                {
                    selectedSubNet.AddMachine(machine);
                }

                // Refresh graph
                parentForm.RefreshGraphWithSubNetworks();

                MessageBox.Show($"{machinesCheckedList.CheckedItems.Count} machines assigned to {selectedSubNet.name}", "Success");
                machineSelectionForm.Close();
                LoadSubNetworks();
            };

            machineSelectionForm.Controls.Add(machinesCheckedList);
            machineSelectionForm.Controls.Add(confirmButton);
            machineSelectionForm.ShowDialog();
        }

        private void deleteSubNetworkButton_Click(object sender, EventArgs e)
        {
            if (subNetworkListBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a subnet to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SubNetwork selectedSubNet = (SubNetwork)subNetworkListBox.SelectedItem;

            var result = MessageBox.Show($"Are you sure you want to delete '{selectedSubNet.name}'?", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                parentForm.network.removeSubNetwork(selectedSubNet);
                parentForm.RefreshGraphWithSubNetworks();
                LoadSubNetworks();
                MessageBox.Show("SubNetwork deleted successfully!", "Success");
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
            parentForm.setVisible();
        }
    }
}
