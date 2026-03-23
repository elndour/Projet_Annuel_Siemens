namespace Projet_Siemens.Interface
{
    partial class FormLocalTestSetup
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.pc1GroupBox = new System.Windows.Forms.GroupBox();
            this.pc1IpLabel = new System.Windows.Forms.Label();
            this.pc1StatusLabel = new System.Windows.Forms.Label();
            this.generateFilesPC1Button = new System.Windows.Forms.Button();
            this.createDbPC1Button = new System.Windows.Forms.Button();
            this.pc2GroupBox = new System.Windows.Forms.GroupBox();
            this.pc2IpTextBox = new System.Windows.Forms.TextBox();
            this.pc2IpInputLabel = new System.Windows.Forms.Label();
            this.pc2StatusLabel = new System.Windows.Forms.Label();
            this.testConnectionPC2Button = new System.Windows.Forms.Button();
            this.generateFilesPC2Button = new System.Windows.Forms.Button();
            this.setupGuideButton = new System.Windows.Forms.Button();
            this.reportTextBox = new System.Windows.Forms.TextBox();
            this.createMachinesButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.pc1GroupBox.SuspendLayout();
            this.pc2GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.titleLabel.Location = new System.Drawing.Point(12, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(328, 25);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "🧪 Configuration Test Local (2 PCs)";
            // 
            // pc1GroupBox
            // 
            this.pc1GroupBox.Controls.Add(this.pc1IpLabel);
            this.pc1GroupBox.Controls.Add(this.pc1StatusLabel);
            this.pc1GroupBox.Controls.Add(this.generateFilesPC1Button);
            this.pc1GroupBox.Controls.Add(this.createDbPC1Button);
            this.pc1GroupBox.Location = new System.Drawing.Point(12, 50);
            this.pc1GroupBox.Name = "pc1GroupBox";
            this.pc1GroupBox.Size = new System.Drawing.Size(380, 150);
            this.pc1GroupBox.TabIndex = 1;
            this.pc1GroupBox.TabStop = false;
            this.pc1GroupBox.Text = "💻 PC 1 - Cet ordinateur (localhost)";
            // 
            // pc1IpLabel
            // 
            this.pc1IpLabel.AutoSize = true;
            this.pc1IpLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.pc1IpLabel.Location = new System.Drawing.Point(15, 25);
            this.pc1IpLabel.Name = "pc1IpLabel";
            this.pc1IpLabel.Size = new System.Drawing.Size(89, 15);
            this.pc1IpLabel.TabIndex = 0;
            this.pc1IpLabel.Text = "IP: 127.0.0.1";
            // 
            // pc1StatusLabel
            // 
            this.pc1StatusLabel.AutoSize = true;
            this.pc1StatusLabel.ForeColor = System.Drawing.Color.Green;
            this.pc1StatusLabel.Location = new System.Drawing.Point(15, 45);
            this.pc1StatusLabel.Name = "pc1StatusLabel";
            this.pc1StatusLabel.Size = new System.Drawing.Size(98, 15);
            this.pc1StatusLabel.TabIndex = 1;
            this.pc1StatusLabel.Text = "✓ Disponible";
            // 
            // generateFilesPC1Button
            // 
            this.generateFilesPC1Button.Location = new System.Drawing.Point(15, 70);
            this.generateFilesPC1Button.Name = "generateFilesPC1Button";
            this.generateFilesPC1Button.Size = new System.Drawing.Size(350, 30);
            this.generateFilesPC1Button.TabIndex = 2;
            this.generateFilesPC1Button.Text = "📁 Générer fichiers de test (20 fichiers)";
            this.generateFilesPC1Button.UseVisualStyleBackColor = true;
            this.generateFilesPC1Button.Click += new System.EventHandler(this.generateFilesPC1Button_Click);
            // 
            // createDbPC1Button
            // 
            this.createDbPC1Button.Location = new System.Drawing.Point(15, 110);
            this.createDbPC1Button.Name = "createDbPC1Button";
            this.createDbPC1Button.Size = new System.Drawing.Size(350, 30);
            this.createDbPC1Button.TabIndex = 3;
            this.createDbPC1Button.Text = "🗄️ Créer base de données SQLite de test";
            this.createDbPC1Button.UseVisualStyleBackColor = true;
            this.createDbPC1Button.Click += new System.EventHandler(this.createDbPC1Button_Click);
            // 
            // pc2GroupBox
            // 
            this.pc2GroupBox.Controls.Add(this.pc2IpTextBox);
            this.pc2GroupBox.Controls.Add(this.pc2IpInputLabel);
            this.pc2GroupBox.Controls.Add(this.pc2StatusLabel);
            this.pc2GroupBox.Controls.Add(this.testConnectionPC2Button);
            this.pc2GroupBox.Controls.Add(this.generateFilesPC2Button);
            this.pc2GroupBox.Location = new System.Drawing.Point(410, 50);
            this.pc2GroupBox.Name = "pc2GroupBox";
            this.pc2GroupBox.Size = new System.Drawing.Size(380, 150);
            this.pc2GroupBox.TabIndex = 2;
            this.pc2GroupBox.TabStop = false;
            this.pc2GroupBox.Text = "💻 PC 2 - Deuxième ordinateur";
            // 
            // pc2IpTextBox
            // 
            this.pc2IpTextBox.Location = new System.Drawing.Point(70, 25);
            this.pc2IpTextBox.Name = "pc2IpTextBox";
            this.pc2IpTextBox.Size = new System.Drawing.Size(295, 23);
            this.pc2IpTextBox.TabIndex = 0;
            this.pc2IpTextBox.Text = "192.168.1.100";
            // 
            // pc2IpInputLabel
            // 
            this.pc2IpInputLabel.AutoSize = true;
            this.pc2IpInputLabel.Location = new System.Drawing.Point(15, 28);
            this.pc2IpInputLabel.Name = "pc2IpInputLabel";
            this.pc2IpInputLabel.Size = new System.Drawing.Size(20, 15);
            this.pc2IpInputLabel.TabIndex = 1;
            this.pc2IpInputLabel.Text = "IP:";
            // 
            // pc2StatusLabel
            // 
            this.pc2StatusLabel.AutoSize = true;
            this.pc2StatusLabel.ForeColor = System.Drawing.Color.Gray;
            this.pc2StatusLabel.Location = new System.Drawing.Point(15, 55);
            this.pc2StatusLabel.Name = "pc2StatusLabel";
            this.pc2StatusLabel.Size = new System.Drawing.Size(107, 15);
            this.pc2StatusLabel.TabIndex = 2;
            this.pc2StatusLabel.Text = "⚠ Non testé";
            // 
            // testConnectionPC2Button
            // 
            this.testConnectionPC2Button.Location = new System.Drawing.Point(15, 80);
            this.testConnectionPC2Button.Name = "testConnectionPC2Button";
            this.testConnectionPC2Button.Size = new System.Drawing.Size(350, 30);
            this.testConnectionPC2Button.TabIndex = 3;
            this.testConnectionPC2Button.Text = "🔌 Tester la connexion";
            this.testConnectionPC2Button.UseVisualStyleBackColor = true;
            this.testConnectionPC2Button.Click += new System.EventHandler(this.testConnectionPC2Button_Click);
            // 
            // generateFilesPC2Button
            // 
            this.generateFilesPC2Button.Enabled = false;
            this.generateFilesPC2Button.Location = new System.Drawing.Point(15, 115);
            this.generateFilesPC2Button.Name = "generateFilesPC2Button";
            this.generateFilesPC2Button.Size = new System.Drawing.Size(350, 30);
            this.generateFilesPC2Button.TabIndex = 4;
            this.generateFilesPC2Button.Text = "📁 Générer fichiers de test sur PC 2";
            this.generateFilesPC2Button.UseVisualStyleBackColor = true;
            this.generateFilesPC2Button.Click += new System.EventHandler(this.generateFilesPC2Button_Click);
            // 
            // setupGuideButton
            // 
            this.setupGuideButton.BackColor = System.Drawing.Color.LightBlue;
            this.setupGuideButton.Location = new System.Drawing.Point(12, 210);
            this.setupGuideButton.Name = "setupGuideButton";
            this.setupGuideButton.Size = new System.Drawing.Size(778, 40);
            this.setupGuideButton.TabIndex = 3;
            this.setupGuideButton.Text = "📖 GUIDE : Comment configurer SSH sur Windows 10/11";
            this.setupGuideButton.UseVisualStyleBackColor = false;
            this.setupGuideButton.Click += new System.EventHandler(this.setupGuideButton_Click);
            // 
            // reportTextBox
            // 
            this.reportTextBox.Font = new System.Drawing.Font("Consolas", 9F);
            this.reportTextBox.Location = new System.Drawing.Point(12, 260);
            this.reportTextBox.Multiline = true;
            this.reportTextBox.Name = "reportTextBox";
            this.reportTextBox.ReadOnly = true;
            this.reportTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.reportTextBox.Size = new System.Drawing.Size(778, 250);
            this.reportTextBox.TabIndex = 4;
            // 
            // createMachinesButton
            // 
            this.createMachinesButton.BackColor = System.Drawing.Color.LightGreen;
            this.createMachinesButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.createMachinesButton.Location = new System.Drawing.Point(12, 520);
            this.createMachinesButton.Name = "createMachinesButton";
            this.createMachinesButton.Size = new System.Drawing.Size(590, 45);
            this.createMachinesButton.TabIndex = 5;
            this.createMachinesButton.Text = "✅ Créer les machines de test dans le réseau";
            this.createMachinesButton.UseVisualStyleBackColor = false;
            this.createMachinesButton.Click += new System.EventHandler(this.createMachinesButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(610, 520);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(180, 45);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Fermer";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // FormLocalTestSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 577);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.createMachinesButton);
            this.Controls.Add(this.reportTextBox);
            this.Controls.Add(this.setupGuideButton);
            this.Controls.Add(this.pc2GroupBox);
            this.Controls.Add(this.pc1GroupBox);
            this.Controls.Add(this.titleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLocalTestSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuration Environnement de Test Local";
            this.Load += new System.EventHandler(this.FormLocalTestSetup_Load);
            this.pc1GroupBox.ResumeLayout(false);
            this.pc1GroupBox.PerformLayout();
            this.pc2GroupBox.ResumeLayout(false);
            this.pc2GroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.GroupBox pc1GroupBox;
        private System.Windows.Forms.Label pc1IpLabel;
        private System.Windows.Forms.Label pc1StatusLabel;
        private System.Windows.Forms.Button generateFilesPC1Button;
        private System.Windows.Forms.Button createDbPC1Button;
        private System.Windows.Forms.GroupBox pc2GroupBox;
        private System.Windows.Forms.TextBox pc2IpTextBox;
        private System.Windows.Forms.Label pc2IpInputLabel;
        private System.Windows.Forms.Label pc2StatusLabel;
        private System.Windows.Forms.Button testConnectionPC2Button;
        private System.Windows.Forms.Button generateFilesPC2Button;
        private System.Windows.Forms.Button setupGuideButton;
        private System.Windows.Forms.TextBox reportTextBox;
        private System.Windows.Forms.Button createMachinesButton;
        private System.Windows.Forms.Button closeButton;
    }
}
