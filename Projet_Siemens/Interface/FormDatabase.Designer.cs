namespace Projet_Siemens.Interface
{
    partial class FormDatabase
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
            dataBasePanel = new Panel();
            newDataBaseButton = new Button();
            label5 = new Label();
            dataBasemotdepasse = new TextBox();
            label4 = new Label();
            dataBaseusername = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            dataBaseSshPort = new TextBox();
            dataBaseIpAdress = new TextBox();
            dataBaseName = new TextBox();
            dataBasePanel.SuspendLayout();
            SuspendLayout();
            // 
            // dataBasePanel
            // 
            dataBasePanel.Controls.Add(newDataBaseButton);
            dataBasePanel.Controls.Add(label5);
            dataBasePanel.Controls.Add(dataBasemotdepasse);
            dataBasePanel.Controls.Add(label4);
            dataBasePanel.Controls.Add(dataBaseusername);
            dataBasePanel.Controls.Add(label3);
            dataBasePanel.Controls.Add(label2);
            dataBasePanel.Controls.Add(label1);
            dataBasePanel.Controls.Add(dataBaseSshPort);
            dataBasePanel.Controls.Add(dataBaseIpAdress);
            dataBasePanel.Controls.Add(dataBaseName);
            dataBasePanel.Location = new Point(255, 86);
            dataBasePanel.Name = "dataBasePanel";
            dataBasePanel.Size = new Size(311, 287);
            dataBasePanel.TabIndex = 4;
            // 
            // newDataBaseButton
            // 
            newDataBaseButton.Location = new Point(116, 255);
            newDataBaseButton.Name = "newDataBaseButton";
            newDataBaseButton.Size = new Size(94, 29);
            newDataBaseButton.TabIndex = 6;
            newDataBaseButton.Text = "Confirm";
            newDataBaseButton.UseVisualStyleBackColor = true;
            newDataBaseButton.Click += newDataBaseButton_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 202);
            label5.Name = "label5";
            label5.Size = new Size(79, 20);
            label5.TabIndex = 10;
            label5.Text = "password :";
            // 
            // dataBasemotdepasse
            // 
            dataBasemotdepasse.Location = new Point(99, 199);
            dataBasemotdepasse.Name = "dataBasemotdepasse";
            dataBasemotdepasse.PasswordChar = '*';
            dataBasemotdepasse.Size = new Size(132, 27);
            dataBasemotdepasse.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 153);
            label4.Name = "label4";
            label4.Size = new Size(80, 20);
            label4.TabIndex = 8;
            label4.Text = "username :";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // dataBaseusername
            // 
            dataBaseusername.Location = new Point(99, 149);
            dataBaseusername.Name = "dataBaseusername";
            dataBaseusername.Size = new Size(132, 27);
            dataBaseusername.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(19, 107);
            label3.Name = "label3";
            label3.Size = new Size(73, 20);
            label3.TabIndex = 5;
            label3.Text = "SSH Port :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 60);
            label2.Name = "label2";
            label2.Size = new Size(74, 20);
            label2.TabIndex = 4;
            label2.Text = "IP adress :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 15);
            label1.Name = "label1";
            label1.Size = new Size(56, 20);
            label1.TabIndex = 3;
            label1.Text = "Name :";
            // 
            // dataBaseSshPort
            // 
            dataBaseSshPort.Location = new Point(99, 104);
            dataBaseSshPort.Name = "dataBaseSshPort";
            dataBaseSshPort.Size = new Size(132, 27);
            dataBaseSshPort.TabIndex = 3;
            // 
            // dataBaseIpAdress
            // 
            dataBaseIpAdress.Location = new Point(99, 57);
            dataBaseIpAdress.Name = "dataBaseIpAdress";
            dataBaseIpAdress.Size = new Size(132, 27);
            dataBaseIpAdress.TabIndex = 2;
            // 
            // dataBaseName
            // 
            dataBaseName.Location = new Point(99, 12);
            dataBaseName.Name = "dataBaseName";
            dataBaseName.Size = new Size(132, 27);
            dataBaseName.TabIndex = 1;
            // 
            // FormDatabase
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataBasePanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormDatabase";
            Text = "FormDatabase";
            dataBasePanel.ResumeLayout(false);
            dataBasePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel dataBasePanel;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox dataBaseSshPort;
        private TextBox dataBaseIpAdress;
        private TextBox dataBaseName;
        private Label label4;
        private TextBox dataBaseusername;
        private Label label5;
        private TextBox dataBasemotdepasse;
        private Button newDataBaseButton;
    }
}