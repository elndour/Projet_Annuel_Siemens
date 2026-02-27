namespace Projet_Siemens.Interface
{
    partial class FormApplicationServer
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
            appServPanel = new Panel();
            appServRepositoryText = new TextBox();
            label5 = new Label();
            appServIDText = new TextBox();
            ID = new Label();
            appServIPText = new TextBox();
            label4 = new Label();
            newAppServButton = new Button();
            appServPortText = new TextBox();
            label3 = new Label();
            appServDescriptionText = new TextBox();
            label2 = new Label();
            label1 = new Label();
            appServPanel.SuspendLayout();
            SuspendLayout();
            // 
            // appServPanel
            // 
            appServPanel.Anchor = AnchorStyles.None;
            appServPanel.BackColor = Color.White;
            appServPanel.Controls.Add(appServRepositoryText);
            appServPanel.Controls.Add(label5);
            appServPanel.Controls.Add(appServIDText);
            appServPanel.Controls.Add(ID);
            appServPanel.Controls.Add(appServIPText);
            appServPanel.Controls.Add(label4);
            appServPanel.Controls.Add(newAppServButton);
            appServPanel.Controls.Add(appServPortText);
            appServPanel.Controls.Add(label3);
            appServPanel.Controls.Add(appServDescriptionText);
            appServPanel.Controls.Add(label2);
            appServPanel.Controls.Add(label1);
            appServPanel.Location = new Point(167, 45);
            appServPanel.Name = "appServPanel";
            appServPanel.Size = new Size(467, 360);
            appServPanel.TabIndex = 0;
            // 
            // label1 (Titre)
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular); // Pas de gras
            label1.ForeColor = Color.FromArgb(0, 101, 110);
            label1.Location = new Point(141, 10);
            label1.Name = "label1";
            label1.Size = new Size(185, 28);
            label1.TabIndex = 0;
            label1.Text = "Application Server";
            // 
            // appServIDText
            // 
            appServIDText.Location = new Point(176, 56);
            appServIDText.Name = "appServIDText";
            appServIDText.Size = new Size(236, 27);
            appServIDText.TabIndex = 1;
            // 
            // ID
            // 
            ID.AutoSize = true;
            ID.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Pas de gras
            ID.Location = new Point(60, 63);
            ID.Name = "ID";
            ID.Size = new Size(24, 20);
            ID.TabIndex = 8;
            ID.Text = "ID";
            // 
            // appServIPText
            // 
            appServIPText.Location = new Point(176, 101);
            appServIPText.Name = "appServIPText";
            appServIPText.Size = new Size(236, 27);
            appServIPText.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Pas de gras
            label4.Location = new Point(60, 104);
            label4.Name = "label4";
            label4.Size = new Size(76, 20);
            label4.TabIndex = 6;
            label4.Text = "IP Address";
            // 
            // appServPortText
            // 
            appServPortText.Location = new Point(176, 149);
            appServPortText.Name = "appServPortText";
            appServPortText.Size = new Size(236, 27);
            appServPortText.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Pas de gras
            label3.Location = new Point(60, 152);
            label3.Name = "label3";
            label3.Size = new Size(86, 20);
            label3.TabIndex = 3;
            label3.Text = "Service Port";
            // 
            // appServDescriptionText
            // 
            appServDescriptionText.Location = new Point(176, 199);
            appServDescriptionText.Name = "appServDescriptionText";
            appServDescriptionText.Size = new Size(236, 27);
            appServDescriptionText.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Pas de gras
            label2.Location = new Point(60, 202);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 1;
            label2.Text = "Description";
            // 
            // appServRepositoryText
            // 
            appServRepositoryText.Location = new Point(176, 253);
            appServRepositoryText.Name = "appServRepositoryText";
            appServRepositoryText.Size = new Size(236, 27);
            appServRepositoryText.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Pas de gras
            label5.Location = new Point(60, 256);
            label5.Name = "label5";
            label5.Size = new Size(80, 20);
            label5.TabIndex = 9;
            label5.Text = "Repository";
            // 
            // newAppServButton (Submit)
            // 
            newAppServButton.BackColor = Color.FromArgb(0, 101, 110);
            newAppServButton.FlatStyle = FlatStyle.Flat;
            newAppServButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Pas de gras
            newAppServButton.ForeColor = Color.White;
            newAppServButton.Location = new Point(176, 305);
            newAppServButton.Name = "newAppServButton";
            newAppServButton.Size = new Size(236, 35);
            newAppServButton.TabIndex = 6;
            newAppServButton.Text = "SUBMIT";
            newAppServButton.UseVisualStyleBackColor = false;
            newAppServButton.Click += newAppServButton_Click;
            // 
            // FormApplicationServer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(800, 450);
            Controls.Add(appServPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormApplicationServer";
            Text = "FormApplicationServer";
            appServPanel.ResumeLayout(false);
            appServPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel appServPanel;
        private Label label1;
        private TextBox appServDescriptionText;
        private Label label2;
        private Button newAppServButton;
        private TextBox appServPortText;
        private Label label3;
        private TextBox appServIDText;
        private Label ID;
        private TextBox appServIPText;
        private Label label4;
        private TextBox appServRepositoryText;
        private Label label5;
    }
}