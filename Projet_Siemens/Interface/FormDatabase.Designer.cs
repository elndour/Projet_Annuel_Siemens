namespace Projet_Siemens.Interface
{
    partial class FormDatabase
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

        #region Windows Form Designer generated code

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
            titleLabel = new Label();
            dataBasePanel.SuspendLayout();
            SuspendLayout();
            // 
            // dataBasePanel
            // 
            // Anchor.None combiné au centrage manuel dans le constructeur ou le Resize assure le milieu
            dataBasePanel.Anchor = AnchorStyles.None;
            dataBasePanel.BackColor = Color.White;
            dataBasePanel.Controls.Add(titleLabel);
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
            dataBasePanel.Location = new Point(167, 45);
            dataBasePanel.Name = "dataBasePanel";
            dataBasePanel.Size = new Size(467, 360);
            dataBasePanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular); // Gras enlevé
            titleLabel.ForeColor = Color.FromArgb(0, 101, 110);
            titleLabel.Location = new Point(185, 10);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(93, 28);
            titleLabel.Text = "Database";
            // 
            // label1 (Name)
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Gras enlevé
            label1.Location = new Point(60, 63);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.Text = "Name";
            // 
            // dataBaseName
            // 
            dataBaseName.Location = new Point(176, 56);
            dataBaseName.Name = "dataBaseName";
            dataBaseName.Size = new Size(236, 27);
            dataBaseName.TabIndex = 1;
            // 
            // label2 (IP Address)
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Gras enlevé
            label2.Location = new Point(60, 104);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.Text = "IP Address";
            // 
            // dataBaseIpAdress
            // 
            dataBaseIpAdress.Location = new Point(176, 101);
            dataBaseIpAdress.Name = "dataBaseIpAdress";
            dataBaseIpAdress.Size = new Size(236, 27);
            dataBaseIpAdress.TabIndex = 2;
            // 
            // label3 (SSH Port)
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Gras enlevé
            label3.Location = new Point(60, 152);
            label3.Name = "label3";
            label3.Size = new Size(67, 20);
            label3.Text = "SSH Port";
            // 
            // dataBaseSshPort
            // 
            dataBaseSshPort.Location = new Point(176, 149);
            dataBaseSshPort.Name = "dataBaseSshPort";
            dataBaseSshPort.Size = new Size(236, 27);
            dataBaseSshPort.TabIndex = 3;
            // 
            // label4 (Username)
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Gras enlevé
            label4.Location = new Point(60, 202);
            label4.Name = "label4";
            label4.Size = new Size(75, 20);
            label4.Text = "Username";
            // 
            // dataBaseusername
            // 
            dataBaseusername.Location = new Point(176, 199);
            dataBaseusername.Name = "dataBaseusername";
            dataBaseusername.Size = new Size(236, 27);
            dataBaseusername.TabIndex = 4;
            // 
            // label5 (Password)
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Gras enlevé
            label5.Location = new Point(60, 256);
            label5.Name = "label5";
            label5.Size = new Size(70, 20);
            label5.Text = "Password";
            // 
            // dataBasemotdepasse
            // 
            dataBasemotdepasse.Location = new Point(176, 253);
            dataBasemotdepasse.Name = "dataBasemotdepasse";
            dataBasemotdepasse.PasswordChar = '●';
            dataBasemotdepasse.Size = new Size(236, 27);
            dataBasemotdepasse.TabIndex = 5;
            // 
            // newDataBaseButton
            // 
            newDataBaseButton.BackColor = Color.FromArgb(0, 101, 110);
            newDataBaseButton.FlatStyle = FlatStyle.Flat;
            newDataBaseButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Gras enlevé
            newDataBaseButton.ForeColor = Color.White;
            newDataBaseButton.Location = new Point(176, 305);
            newDataBaseButton.Name = "newDataBaseButton";
            newDataBaseButton.Size = new Size(236, 35);
            newDataBaseButton.TabIndex = 6;
            newDataBaseButton.Text = "SUBMIT";
            newDataBaseButton.UseVisualStyleBackColor = false;
            newDataBaseButton.Click += newDataBaseButton_Click;
            // 
            // FormDatabase
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
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
        private Label titleLabel;
        private Label label1;
        private TextBox dataBaseName;
        private Label label2;
        private TextBox dataBaseIpAdress;
        private Label label3;
        private TextBox dataBaseSshPort;
        private Label label4;
        private TextBox dataBaseusername;
        private Label label5;
        private TextBox dataBasemotdepasse;
        private Button newDataBaseButton;
    }
}