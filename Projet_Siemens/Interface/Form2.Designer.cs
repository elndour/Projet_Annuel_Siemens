namespace Projet_Siemens
{
    partial class Form2
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
            dataBaseButton = new Button();
            machineButton = new Button();
            edgeButton = new Button();
            form2MainPanel = new Panel();
            import = new Button();
            buttonConfirmJson = new Button();
            viewerPanel = new Panel();
            originalButtonPanel = new Panel();
            serverButtonPanel = new Panel();
            presServButton = new Button();
            webSerButton = new Button();
            appServerButton = new Button();
            label1 = new Label();
            extractFileButton = new Button();
            form2MainPanel.SuspendLayout();
            originalButtonPanel.SuspendLayout();
            serverButtonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // dataBaseButton
            // 
            dataBaseButton.Location = new Point(9, 16);
            dataBaseButton.Name = "dataBaseButton";
            dataBaseButton.Size = new Size(94, 29);
            dataBaseButton.TabIndex = 1;
            dataBaseButton.Text = "DataBase";
            dataBaseButton.UseVisualStyleBackColor = true;
            dataBaseButton.Click += dataBaseButton_Click;
            // 
            // machineButton
            // 
            machineButton.Location = new Point(9, 68);
            machineButton.Name = "machineButton";
            machineButton.Size = new Size(94, 29);
            machineButton.TabIndex = 2;
            machineButton.Text = "Server";
            machineButton.UseVisualStyleBackColor = true;
            machineButton.Click += machineButton_Click;
            // 
            // edgeButton
            // 
            edgeButton.Location = new Point(9, 120);
            edgeButton.Name = "edgeButton";
            edgeButton.Size = new Size(94, 29);
            edgeButton.TabIndex = 3;
            edgeButton.Text = "Edge";
            edgeButton.UseVisualStyleBackColor = true;
            edgeButton.Click += edgeButton_Click;
            // 
            // form2MainPanel
            // 
            form2MainPanel.Controls.Add(extractFileButton);
            form2MainPanel.Controls.Add(import);
            form2MainPanel.Controls.Add(buttonConfirmJson);
            form2MainPanel.Controls.Add(viewerPanel);
            form2MainPanel.Location = new Point(1, 1);
            form2MainPanel.Name = "form2MainPanel";
            form2MainPanel.Size = new Size(664, 447);
            form2MainPanel.TabIndex = 4;
            // 
            // import
            // 
            import.Location = new Point(155, 384);
            import.Name = "import";
            import.Size = new Size(95, 29);
            import.TabIndex = 2;
            import.Text = "Import";
            import.UseVisualStyleBackColor = true;
            import.Click += import_Click;
            // 
            // buttonConfirmJson
            // 
            buttonConfirmJson.Location = new Point(269, 384);
            buttonConfirmJson.Name = "buttonConfirmJson";
            buttonConfirmJson.Size = new Size(94, 29);
            buttonConfirmJson.TabIndex = 1;
            buttonConfirmJson.Text = "Confirm";
            buttonConfirmJson.UseVisualStyleBackColor = true;
            buttonConfirmJson.Click += buttonConfirmJson_Click;
            // 
            // viewerPanel
            // 
            viewerPanel.Location = new Point(73, 48);
            viewerPanel.Name = "viewerPanel";
            viewerPanel.Size = new Size(483, 317);
            viewerPanel.TabIndex = 0;
            // 
            // originalButtonPanel
            // 
            originalButtonPanel.Controls.Add(dataBaseButton);
            originalButtonPanel.Controls.Add(machineButton);
            originalButtonPanel.Controls.Add(edgeButton);
            originalButtonPanel.Location = new Point(676, 12);
            originalButtonPanel.Name = "originalButtonPanel";
            originalButtonPanel.Size = new Size(112, 185);
            originalButtonPanel.TabIndex = 5;
            // 
            // serverButtonPanel
            // 
            serverButtonPanel.Controls.Add(presServButton);
            serverButtonPanel.Controls.Add(webSerButton);
            serverButtonPanel.Controls.Add(appServerButton);
            serverButtonPanel.Controls.Add(label1);
            serverButtonPanel.Location = new Point(676, 203);
            serverButtonPanel.Name = "serverButtonPanel";
            serverButtonPanel.Size = new Size(112, 235);
            serverButtonPanel.TabIndex = 6;
            serverButtonPanel.Visible = false;
            // 
            // presServButton
            // 
            presServButton.Font = new Font("Segoe UI", 9F);
            presServButton.Location = new Point(6, 170);
            presServButton.Name = "presServButton";
            presServButton.Size = new Size(103, 52);
            presServButton.TabIndex = 3;
            presServButton.Text = "Presentation Server";
            presServButton.UseVisualStyleBackColor = true;
            presServButton.Click += presServButton_Click;
            // 
            // webSerButton
            // 
            webSerButton.Font = new Font("Segoe UI", 9F);
            webSerButton.Location = new Point(6, 107);
            webSerButton.Name = "webSerButton";
            webSerButton.Size = new Size(103, 42);
            webSerButton.TabIndex = 2;
            webSerButton.Text = "Web Server";
            webSerButton.UseVisualStyleBackColor = true;
            webSerButton.Click += webSerButton_Click;
            // 
            // appServerButton
            // 
            appServerButton.Font = new Font("Segoe UI", 9F);
            appServerButton.Location = new Point(6, 39);
            appServerButton.Name = "appServerButton";
            appServerButton.Size = new Size(103, 48);
            appServerButton.TabIndex = 1;
            appServerButton.Text = "Application Server";
            appServerButton.UseVisualStyleBackColor = true;
            appServerButton.Click += appServerButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 6);
            label1.Name = "label1";
            label1.Size = new Size(61, 20);
            label1.TabIndex = 0;
            label1.Text = "Server : ";
            // 
            // extractFileButton
            // 
            extractFileButton.Location = new Point(403, 384);
            extractFileButton.Name = "extractFileButton";
            extractFileButton.Size = new Size(94, 29);
            extractFileButton.TabIndex = 3;
            extractFileButton.Text = "Extract File";
            extractFileButton.UseVisualStyleBackColor = true;
            extractFileButton.Click += extractFileButton_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(serverButtonPanel);
            Controls.Add(originalButtonPanel);
            Controls.Add(form2MainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form2";
            Text = "Form2";
            form2MainPanel.ResumeLayout(false);
            originalButtonPanel.ResumeLayout(false);
            serverButtonPanel.ResumeLayout(false);
            serverButtonPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button dataBaseButton;
        private Button machineButton;
        private Button edgeButton;
        private Panel form2MainPanel;
        private Panel viewerPanel;
        private Panel originalButtonPanel;
        private Panel serverButtonPanel;
        private Label label1;
        private Button appServerButton;
        private Button presServButton;
        private Button webSerButton;
        private Button buttonConfirmJson;
        private Button import;
        private Button extractFileButton;
    }
}