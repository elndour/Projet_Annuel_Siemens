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
            // extractedFilesPanel
            // 
            extractedFilesPanel.Location = new Point(543, 12);
            extractedFilesPanel.Name = "extractedFilesPanel";
            extractedFilesPanel.Size = new Size(250, 426);
            extractedFilesPanel.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Controls.Add(sshServ);
            panel1.Controls.Add(kindOdextractionButton);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(machinesList);
            panel1.Location = new Point(11, 16);
            panel1.Name = "panel1";
            panel1.Size = new Size(251, 422);
            panel1.TabIndex = 2;
            // 
            // sshServ
            // 
            sshServ.CheckAlign = ContentAlignment.BottomCenter;
            sshServ.Location = new Point(3, 158);
            sshServ.Name = "sshServ";
            sshServ.Size = new Size(245, 69);
            sshServ.TabIndex = 12;
            sshServ.Text = "Is a SSH serv has always been set on the network ?";
            sshServ.TextAlign = ContentAlignment.TopCenter;
            sshServ.UseVisualStyleBackColor = true;
            // 
            // kindOdextractionButton
            // 
            kindOdextractionButton.Location = new Point(79, 259);
            kindOdextractionButton.Name = "kindOdextractionButton";
            kindOdextractionButton.Size = new Size(94, 29);
            kindOdextractionButton.TabIndex = 4;
            kindOdextractionButton.Text = "Confirm";
            kindOdextractionButton.UseVisualStyleBackColor = true;
            kindOdextractionButton.Click += kindOdextractionButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 9);
            label1.Name = "label1";
            label1.Size = new Size(129, 20);
            label1.TabIndex = 2;
            label1.Text = "Machine available";
            // 
            // machinesList
            // 
            machinesList.FormattingEnabled = true;
            machinesList.Location = new Point(3, 32);
            machinesList.Name = "machinesList";
            machinesList.Size = new Size(151, 28);
            machinesList.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.Controls.Add(extractionButton);
            panel2.Controls.Add(pathToFolder);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(userName);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(277, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(250, 426);
            panel2.TabIndex = 3;
            panel2.Visible = false;
            // 
            // extractionButton
            // 
            extractionButton.Location = new Point(77, 312);
            extractionButton.Name = "extractionButton";
            extractionButton.Size = new Size(94, 29);
            extractionButton.TabIndex = 5;
            extractionButton.Text = "Confirm";
            extractionButton.UseVisualStyleBackColor = true;
            // 
            // pathToFolder
            // 
            pathToFolder.Location = new Point(43, 251);
            pathToFolder.Name = "pathToFolder";
            pathToFolder.Size = new Size(168, 27);
            pathToFolder.TabIndex = 3;
            // 
            // label3
            // 
            label3.Location = new Point(3, 180);
            label3.Name = "label3";
            label3.Size = new Size(244, 51);
            label3.TabIndex = 2;
            label3.Text = "Please, specify the path of the folder where files are stored ";
            // 
            // userName
            // 
            userName.Location = new Point(43, 103);
            userName.Name = "userName";
            userName.Size = new Size(168, 27);
            userName.TabIndex = 1;
            // 
            // label2
            // 
            label2.Location = new Point(3, 22);
            label2.Name = "label2";
            label2.Size = new Size(244, 67);
            label2.TabIndex = 0;
            label2.Text = "Provide us the username of the remote machines where interested files are stored :";
            // 
            // FormFileExtraction
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(extractedFilesPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormFileExtraction";
            Text = "FormFileExtraction";
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
        private Panel panel2;
        private CheckBox sshServ;
        private Label label2;
        private Label label3;
        private TextBox userName;
        private Button extractionButton;
        private TextBox pathToFolder;
    }
}