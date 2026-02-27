namespace Projet_Siemens.Interface
{
    partial class FormSubNetwork
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.mainPanel = new System.Windows.Forms.Panel();
            this.createPanel = new System.Windows.Forms.Panel();
            this.subNetworkIdLabel = new System.Windows.Forms.Label();
            this.subNetworkIdTextBox = new System.Windows.Forms.TextBox();
            this.subNetworkNameLabel = new System.Windows.Forms.Label();
            this.subNetworkNameTextBox = new System.Windows.Forms.TextBox();
            this.subNetworkSubnetLabel = new System.Windows.Forms.Label();
            this.subNetworkSubnetTextBox = new System.Windows.Forms.TextBox();
            this.subNetworkVlanLabel = new System.Windows.Forms.Label();
            this.subNetworkVlanTextBox = new System.Windows.Forms.TextBox();
            this.subNetworkDescLabel = new System.Windows.Forms.Label();
            this.subNetworkDescTextBox = new System.Windows.Forms.TextBox();
            this.createSubNetworkButton = new System.Windows.Forms.Button();
            this.managePanel = new System.Windows.Forms.Panel();
            this.subNetworkListLabel = new System.Windows.Forms.Label();
            this.subNetworkListBox = new System.Windows.Forms.ListBox();
            this.assignMachinesButton = new System.Windows.Forms.Button();
            this.deleteSubNetworkButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.createPanel.SuspendLayout();
            this.managePanel.SuspendLayout();
            this.SuspendLayout();

            // Main Panel
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.mainPanel.Controls.Add(this.titleLabel);
            this.mainPanel.Controls.Add(this.createPanel);
            this.mainPanel.Controls.Add(this.managePanel);
            this.mainPanel.Controls.Add(this.closeButton);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(700, 500);
            this.mainPanel.TabIndex = 0;

            // Title Label
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(0, 101, 110);
            this.titleLabel.Location = new System.Drawing.Point(20, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(300, 40);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "🌐 SubNetwork Management";

            // Create Panel
            this.createPanel.BackColor = System.Drawing.Color.White;
            this.createPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.createPanel.Location = new System.Drawing.Point(20, 70);
            this.createPanel.Name = "createPanel";
            this.createPanel.Size = new System.Drawing.Size(320, 360);
            this.createPanel.TabIndex = 1;

            // ID Label & TextBox
            this.subNetworkIdLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.subNetworkIdLabel.Location = new System.Drawing.Point(15, 15);
            this.subNetworkIdLabel.Name = "subNetworkIdLabel";
            this.subNetworkIdLabel.Size = new System.Drawing.Size(100, 20);
            this.subNetworkIdLabel.TabIndex = 0;
            this.subNetworkIdLabel.Text = "ID *";

            this.subNetworkIdTextBox.Location = new System.Drawing.Point(15, 40);
            this.subNetworkIdTextBox.Name = "subNetworkIdTextBox";
            this.subNetworkIdTextBox.Size = new System.Drawing.Size(280, 25);
            this.subNetworkIdTextBox.TabIndex = 1;
            this.subNetworkIdTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            // Name Label & TextBox
            this.subNetworkNameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.subNetworkNameLabel.Location = new System.Drawing.Point(15, 75);
            this.subNetworkNameLabel.Name = "subNetworkNameLabel";
            this.subNetworkNameLabel.Size = new System.Drawing.Size(100, 20);
            this.subNetworkNameLabel.TabIndex = 2;
            this.subNetworkNameLabel.Text = "Name *";

            this.subNetworkNameTextBox.Location = new System.Drawing.Point(15, 100);
            this.subNetworkNameTextBox.Name = "subNetworkNameTextBox";
            this.subNetworkNameTextBox.Size = new System.Drawing.Size(280, 25);
            this.subNetworkNameTextBox.TabIndex = 3;
            this.subNetworkNameTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            // Subnet Label & TextBox
            this.subNetworkSubnetLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.subNetworkSubnetLabel.Location = new System.Drawing.Point(15, 135);
            this.subNetworkSubnetLabel.Name = "subNetworkSubnetLabel";
            this.subNetworkSubnetLabel.Size = new System.Drawing.Size(150, 20);
            this.subNetworkSubnetLabel.TabIndex = 4;
            this.subNetworkSubnetLabel.Text = "Subnet (CIDR) *";

            this.subNetworkSubnetTextBox.Location = new System.Drawing.Point(15, 160);
            this.subNetworkSubnetTextBox.Name = "subNetworkSubnetTextBox";
            this.subNetworkSubnetTextBox.Size = new System.Drawing.Size(280, 25);
            this.subNetworkSubnetTextBox.TabIndex = 5;
            this.subNetworkSubnetTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.subNetworkSubnetTextBox.Text = "192.168.1.0/24";

            // VLAN Label & TextBox
            this.subNetworkVlanLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.subNetworkVlanLabel.Location = new System.Drawing.Point(15, 195);
            this.subNetworkVlanLabel.Name = "subNetworkVlanLabel";
            this.subNetworkVlanLabel.Size = new System.Drawing.Size(100, 20);
            this.subNetworkVlanLabel.TabIndex = 6;
            this.subNetworkVlanLabel.Text = "VLAN";

            this.subNetworkVlanTextBox.Location = new System.Drawing.Point(15, 220);
            this.subNetworkVlanTextBox.Name = "subNetworkVlanTextBox";
            this.subNetworkVlanTextBox.Size = new System.Drawing.Size(280, 25);
            this.subNetworkVlanTextBox.TabIndex = 7;
            this.subNetworkVlanTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            // Description Label & TextBox
            this.subNetworkDescLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.subNetworkDescLabel.Location = new System.Drawing.Point(15, 255);
            this.subNetworkDescLabel.Name = "subNetworkDescLabel";
            this.subNetworkDescLabel.Size = new System.Drawing.Size(100, 20);
            this.subNetworkDescLabel.TabIndex = 8;
            this.subNetworkDescLabel.Text = "Description";

            this.subNetworkDescTextBox.Location = new System.Drawing.Point(15, 280);
            this.subNetworkDescTextBox.Name = "subNetworkDescTextBox";
            this.subNetworkDescTextBox.Size = new System.Drawing.Size(280, 25);
            this.subNetworkDescTextBox.TabIndex = 9;
            this.subNetworkDescTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            // Create Button
            this.createSubNetworkButton.BackColor = System.Drawing.Color.FromArgb(0, 101, 110);
            this.createSubNetworkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createSubNetworkButton.ForeColor = System.Drawing.Color.White;
            this.createSubNetworkButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.createSubNetworkButton.Location = new System.Drawing.Point(15, 315);
            this.createSubNetworkButton.Name = "createSubNetworkButton";
            this.createSubNetworkButton.Size = new System.Drawing.Size(280, 35);
            this.createSubNetworkButton.TabIndex = 10;
            this.createSubNetworkButton.Text = "✓ Create SubNetwork";
            this.createSubNetworkButton.UseVisualStyleBackColor = false;
            this.createSubNetworkButton.Click += new System.EventHandler(this.createSubNetworkButton_Click);

            this.createPanel.Controls.Add(this.subNetworkIdLabel);
            this.createPanel.Controls.Add(this.subNetworkIdTextBox);
            this.createPanel.Controls.Add(this.subNetworkNameLabel);
            this.createPanel.Controls.Add(this.subNetworkNameTextBox);
            this.createPanel.Controls.Add(this.subNetworkSubnetLabel);
            this.createPanel.Controls.Add(this.subNetworkSubnetTextBox);
            this.createPanel.Controls.Add(this.subNetworkVlanLabel);
            this.createPanel.Controls.Add(this.subNetworkVlanTextBox);
            this.createPanel.Controls.Add(this.subNetworkDescLabel);
            this.createPanel.Controls.Add(this.subNetworkDescTextBox);
            this.createPanel.Controls.Add(this.createSubNetworkButton);

            // Manage Panel
            this.managePanel.BackColor = System.Drawing.Color.White;
            this.managePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.managePanel.Location = new System.Drawing.Point(360, 70);
            this.managePanel.Name = "managePanel";
            this.managePanel.Size = new System.Drawing.Size(320, 360);
            this.managePanel.TabIndex = 2;

            // SubNetwork List Label
            this.subNetworkListLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.subNetworkListLabel.Location = new System.Drawing.Point(15, 15);
            this.subNetworkListLabel.Name = "subNetworkListLabel";
            this.subNetworkListLabel.Size = new System.Drawing.Size(200, 20);
            this.subNetworkListLabel.TabIndex = 0;
            this.subNetworkListLabel.Text = "Existing SubNetworks";

            // SubNetwork ListBox
            this.subNetworkListBox.FormattingEnabled = true;
            this.subNetworkListBox.Location = new System.Drawing.Point(15, 40);
            this.subNetworkListBox.Name = "subNetworkListBox";
            this.subNetworkListBox.Size = new System.Drawing.Size(280, 200);
            this.subNetworkListBox.TabIndex = 1;
            this.subNetworkListBox.Font = new System.Drawing.Font("Segoe UI", 9F);

            // Assign Machines Button
            this.assignMachinesButton.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.assignMachinesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.assignMachinesButton.ForeColor = System.Drawing.Color.White;
            this.assignMachinesButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.assignMachinesButton.Location = new System.Drawing.Point(15, 255);
            this.assignMachinesButton.Name = "assignMachinesButton";
            this.assignMachinesButton.Size = new System.Drawing.Size(280, 40);
            this.assignMachinesButton.TabIndex = 2;
            this.assignMachinesButton.Text = "📌 Assign Machines";
            this.assignMachinesButton.UseVisualStyleBackColor = false;
            this.assignMachinesButton.Click += new System.EventHandler(this.assignMachinesButton_Click);

            // Delete Button
            this.deleteSubNetworkButton.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.deleteSubNetworkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteSubNetworkButton.ForeColor = System.Drawing.Color.White;
            this.deleteSubNetworkButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.deleteSubNetworkButton.Location = new System.Drawing.Point(15, 305);
            this.deleteSubNetworkButton.Name = "deleteSubNetworkButton";
            this.deleteSubNetworkButton.Size = new System.Drawing.Size(280, 40);
            this.deleteSubNetworkButton.TabIndex = 3;
            this.deleteSubNetworkButton.Text = "🗑️ Delete SubNetwork";
            this.deleteSubNetworkButton.UseVisualStyleBackColor = false;
            this.deleteSubNetworkButton.Click += new System.EventHandler(this.deleteSubNetworkButton_Click);

            this.managePanel.Controls.Add(this.subNetworkListLabel);
            this.managePanel.Controls.Add(this.subNetworkListBox);
            this.managePanel.Controls.Add(this.assignMachinesButton);
            this.managePanel.Controls.Add(this.deleteSubNetworkButton);

            // Close Button
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(127, 140, 141);
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.closeButton.Location = new System.Drawing.Point(20, 445);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(660, 40);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "← Back to Network";
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);

            // FormSubNetwork
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSubNetwork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SubNetwork Management";
            this.mainPanel.ResumeLayout(false);
            this.createPanel.ResumeLayout(false);
            this.createPanel.PerformLayout();
            this.managePanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel createPanel;
        private System.Windows.Forms.Panel managePanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subNetworkIdLabel;
        private System.Windows.Forms.TextBox subNetworkIdTextBox;
        private System.Windows.Forms.Label subNetworkNameLabel;
        private System.Windows.Forms.TextBox subNetworkNameTextBox;
        private System.Windows.Forms.Label subNetworkSubnetLabel;
        private System.Windows.Forms.TextBox subNetworkSubnetTextBox;
        private System.Windows.Forms.Label subNetworkVlanLabel;
        private System.Windows.Forms.TextBox subNetworkVlanTextBox;
        private System.Windows.Forms.Label subNetworkDescLabel;
        private System.Windows.Forms.TextBox subNetworkDescTextBox;
        private System.Windows.Forms.Button createSubNetworkButton;
        private System.Windows.Forms.Label subNetworkListLabel;
        private System.Windows.Forms.ListBox subNetworkListBox;
        private System.Windows.Forms.Button assignMachinesButton;
        private System.Windows.Forms.Button deleteSubNetworkButton;
        private System.Windows.Forms.Button closeButton;
    }
}
