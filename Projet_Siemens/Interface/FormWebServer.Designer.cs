namespace Projet_Siemens.Interface
{
    partial class FormWebServer
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
            appServPanel = new Panel();
            webServIDText = new TextBox();
            ID = new Label();
            webServIPText = new TextBox();
            label4 = new Label();
            newWebServButton = new Button();
            webServAPIText = new TextBox();
            label3 = new Label();
            webServAPIEndpointText = new TextBox();
            label2 = new Label();
            label1 = new Label();
            webServRepositoryText = new TextBox();
            label5 = new Label();
            appServPanel.SuspendLayout();
            SuspendLayout();
            // 
            // appServPanel
            // 
            appServPanel.Controls.Add(webServRepositoryText);
            appServPanel.Controls.Add(label5);
            appServPanel.Controls.Add(webServIDText);
            appServPanel.Controls.Add(ID);
            appServPanel.Controls.Add(webServIPText);
            appServPanel.Controls.Add(label4);
            appServPanel.Controls.Add(newWebServButton);
            appServPanel.Controls.Add(webServAPIText);
            appServPanel.Controls.Add(label3);
            appServPanel.Controls.Add(webServAPIEndpointText);
            appServPanel.Controls.Add(label2);
            appServPanel.Controls.Add(label1);
            appServPanel.Location = new Point(167, 69);
            appServPanel.Name = "appServPanel";
            appServPanel.Size = new Size(467, 348);
            appServPanel.TabIndex = 1;
            // 
            // webServIDText
            // 
            webServIDText.Location = new Point(176, 56);
            webServIDText.Name = "webServIDText";
            webServIDText.Size = new Size(236, 27);
            webServIDText.TabIndex = 9;
            // 
            // ID
            // 
            ID.AutoSize = true;
            ID.Location = new Point(95, 63);
            ID.Name = "ID";
            ID.Size = new Size(24, 20);
            ID.TabIndex = 8;
            ID.Text = "ID";
            // 
            // webServIPText
            // 
            webServIPText.Location = new Point(176, 101);
            webServIPText.Name = "webServIPText";
            webServIPText.Size = new Size(236, 27);
            webServIPText.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(68, 104);
            label4.Name = "label4";
            label4.Size = new Size(69, 20);
            label4.TabIndex = 6;
            label4.Text = "IP Adress";
            // 
            // newWebServButton
            // 
            newWebServButton.Location = new Point(207, 298);
            newWebServButton.Name = "newWebServButton";
            newWebServButton.Size = new Size(94, 29);
            newWebServButton.TabIndex = 5;
            newWebServButton.Text = "Submit";
            newWebServButton.UseVisualStyleBackColor = true;
            newWebServButton.Click += newWebServButton_Click;
            // 
            // webServAPIText
            // 
            webServAPIText.Location = new Point(176, 149);
            webServAPIText.Name = "webServAPIText";
            webServAPIText.Size = new Size(236, 27);
            webServAPIText.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(68, 152);
            label3.Name = "label3";
            label3.Size = new Size(84, 20);
            label3.TabIndex = 3;
            label3.Text = "Type of API";
            // 
            // webServAPIEndpointText
            // 
            webServAPIEndpointText.Location = new Point(176, 199);
            webServAPIEndpointText.Name = "webServAPIEndpointText";
            webServAPIEndpointText.Size = new Size(236, 27);
            webServAPIEndpointText.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(60, 202);
            label2.Name = "label2";
            label2.Size = new Size(101, 20);
            label2.TabIndex = 1;
            label2.Text = "API Endpoints";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(176, 14);
            label1.Name = "label1";
            label1.Size = new Size(84, 20);
            label1.TabIndex = 0;
            label1.Text = "Web Server";
            // 
            // webServRepositoryText
            // 
            webServRepositoryText.Location = new Point(176, 253);
            webServRepositoryText.Name = "webServRepositoryText";
            webServRepositoryText.Size = new Size(236, 27);
            webServRepositoryText.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(68, 256);
            label5.Name = "label5";
            label5.Size = new Size(80, 20);
            label5.TabIndex = 11;
            label5.Text = "Repository";
            // 
            // FormWebServer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(appServPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormWebServer";
            Text = "FormWebServer";
            appServPanel.ResumeLayout(false);
            appServPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel appServPanel;
        private TextBox webServIDText;
        private Label ID;
        private TextBox webServIPText;
        private Label label4;
        private Button newWebServButton;
        private TextBox webServAPIText;
        private Label label3;
        private TextBox webServAPIEndpointText;
        private Label label2;
        private Label label1;
        private TextBox webServRepositoryText;
        private Label label5;
    }
}