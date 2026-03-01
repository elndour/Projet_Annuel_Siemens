namespace Projet_Siemens.Interface
{
    partial class FormFileExtraction
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            extractedFilesPanel = new FlowLayoutPanel();
            panel1 = new Panel();
            packageButton = new Button();
            sshServ = new CheckBox();
            kindOdextractionButton = new Button();
            label1 = new Label();
            machinesList = new ComboBox();
            panel2 = new Panel();
            extractionButton = new Button();
            pathToFolder = new TextBox();
            label3 = new Label();
            userName = new TextBox();
            label2 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1 (Colonne Gauche : Sélection)
            // 
            panel1.BackColor = Color.FromArgb(240, 242, 245);
            panel1.Controls.Add(packageButton);
            panel1.Controls.Add(sshServ);
            panel1.Controls.Add(kindOdextractionButton);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(machinesList);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(15);
            panel1.Size = new Size(223, 450);
            panel1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(0, 101, 110);
            label1.Location = new Point(15, 20);
            label1.Name = "label1";
            label1.Size = new Size(160, 23);
            label1.Text = "Available Machines";
            // 
            // machinesList
            // 
            machinesList.DropDownStyle = ComboBoxStyle.DropDownList;
            machinesList.FormattingEnabled = true;
            machinesList.Location = new Point(15, 55);
            machinesList.Name = "machinesList";
            machinesList.Size = new Size(190, 28);
            machinesList.TabIndex = 3;
            // 
            // sshServ
            // 
            sshServ.Font = new Font("Segoe UI", 9F);
            sshServ.Location = new Point(15, 120);
            sshServ.Name = "sshServ";
            sshServ.Size = new Size(190, 80);
            sshServ.TabIndex = 12;
            sshServ.Text = "Is a SSH server already set on the network ?";
            sshServ.UseVisualStyleBackColor = true;
            // 
            // kindOdextractionButton
            // 
            kindOdextractionButton.BackColor = Color.FromArgb(0, 101, 110);
            kindOdextractionButton.FlatStyle = FlatStyle.Flat;
            kindOdextractionButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            kindOdextractionButton.ForeColor = Color.White;
            kindOdextractionButton.Location = new Point(15, 220);
            kindOdextractionButton.Name = "kindOdextractionButton";
            kindOdextractionButton.Size = new Size(190, 35);
            kindOdextractionButton.TabIndex = 4;
            kindOdextractionButton.Text = "Validate Selection";
            kindOdextractionButton.UseVisualStyleBackColor = false;
            kindOdextractionButton.Click += kindOdextractionButton_Click;
            // 
            // packageButton
            // 
            packageButton.BackColor = Color.FromArgb(230, 126, 34);
            packageButton.FlatStyle = FlatStyle.Flat;
            packageButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            packageButton.ForeColor = Color.White;
            packageButton.Location = new Point(15, 270);
            packageButton.Name = "packageButton";
            packageButton.Size = new Size(190, 35);
            packageButton.TabIndex = 13;
            packageButton.Text = "📦 Package & Encrypt";
            packageButton.UseVisualStyleBackColor = false;
            packageButton.Click += packageButton_Click;
            // 
            // panel2 (Colonne Milieu : Paramètres)
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(extractionButton);
            panel2.Controls.Add(pathToFolder);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(userName);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(223, 0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(15);
            panel2.Size = new Size(223, 450);
            panel2.TabIndex = 3;
            panel2.Visible = false;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(15, 20);
            label2.Name = "label2";
            label2.Size = new Size(190, 60);
            label2.Text = "Remote machine username :";
            // 
            // userName
            // 
            userName.BorderStyle = BorderStyle.FixedSingle;
            userName.Location = new Point(15, 85);
            userName.Name = "userName";
            userName.Size = new Size(190, 27);
            userName.TabIndex = 1;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(15, 140);
            label3.Name = "label3";
            label3.Size = new Size(190, 60);
            label3.Text = "Storage folder path :";
            // 
            // pathToFolder
            // 
            pathToFolder.BorderStyle = BorderStyle.FixedSingle;
            pathToFolder.Location = new Point(15, 205);
            pathToFolder.Name = "pathToFolder";
            pathToFolder.Size = new Size(190, 27);
            pathToFolder.TabIndex = 3;
            // 
            // extractionButton
            // 
            extractionButton.BackColor = Color.FromArgb(0, 101, 110);
            extractionButton.FlatStyle = FlatStyle.Flat;
            extractionButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            extractionButton.ForeColor = Color.White;
            extractionButton.Location = new Point(15, 260);
            extractionButton.Name = "extractionButton";
            extractionButton.Size = new Size(190, 35);
            extractionButton.TabIndex = 5;
            extractionButton.Text = "Start Extraction";
            extractionButton.UseVisualStyleBackColor = false;
            // 
            // extractedFilesPanel (Colonne Droite : Résultats)
            // 
            extractedFilesPanel.AutoScroll = true;
            extractedFilesPanel.BackColor = Color.FromArgb(240, 242, 245);
            extractedFilesPanel.Dock = DockStyle.Fill;
            extractedFilesPanel.FlowDirection = FlowDirection.TopDown;
            extractedFilesPanel.Location = new Point(446, 0);
            extractedFilesPanel.Name = "extractedFilesPanel";
            extractedFilesPanel.Padding = new Padding(10);
            extractedFilesPanel.Size = new Size(224, 450);
            extractedFilesPanel.TabIndex = 1;
            extractedFilesPanel.WrapContents = false;
            // 
            // FormFileExtraction
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(670, 450);
            Controls.Add(extractedFilesPanel);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormFileExtraction";
            Text = "File Extraction Service";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private FlowLayoutPanel extractedFilesPanel;
        private Panel panel1;
        private Label label1;
        private ComboBox machinesList;
        private Button kindOdextractionButton;
        private Button packageButton;
        private Panel panel2;
        private CheckBox sshServ;
        private Label label2;
        private Label label3;
        private TextBox userName;
        private Button extractionButton;
        private TextBox pathToFolder;
    }
}