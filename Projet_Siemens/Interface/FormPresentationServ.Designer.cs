namespace Projet_Siemens.Interface
{
    partial class FormPresentationServ
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
            presServPanel = new Panel();
            presServRepositoryText = new TextBox();
            label5 = new Label();
            presServIDText = new TextBox();
            ID = new Label();
            presServIPText = new TextBox();
            label4 = new Label();
            newPresServButton = new Button();
            presServPortText = new TextBox();
            label3 = new Label();
            presServURLText = new TextBox();
            label2 = new Label();
            label1 = new Label();
            presServPanel.SuspendLayout();
            SuspendLayout();
            // 
            // presServPanel
            // 
            // Anchor.None + calcul de position permet de garder le panel au centre
            presServPanel.Anchor = AnchorStyles.None; 
            presServPanel.BackColor = Color.White; // Fond blanc pour détacher du gris
            presServPanel.Controls.Add(presServRepositoryText);
            presServPanel.Controls.Add(label5);
            presServPanel.Controls.Add(presServIDText);
            presServPanel.Controls.Add(ID);
            presServPanel.Controls.Add(presServIPText);
            presServPanel.Controls.Add(label4);
            presServPanel.Controls.Add(newPresServButton);
            presServPanel.Controls.Add(presServPortText);
            presServPanel.Controls.Add(label3);
            presServPanel.Controls.Add(presServURLText);
            presServPanel.Controls.Add(label2);
            presServPanel.Controls.Add(label1);
            presServPanel.Location = new Point(167, 45);
            presServPanel.Name = "presServPanel";
            presServPanel.Size = new Size(467, 360);
            presServPanel.TabIndex = 2;
            // 
            // label1 (Titre du formulaire)
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(0, 101, 110); // Bleu Siemens
            label1.Location = new Point(138, 10);
            label1.Name = "label1";
            label1.Size = new Size(191, 28);
            label1.TabIndex = 0;
            label1.Text = "Presentation Server";
            // 
            // presServIDText
            // 
            presServIDText.Location = new Point(176, 56);
            presServIDText.Name = "presServIDText";
            presServIDText.Size = new Size(236, 27);
            presServIDText.TabIndex = 9;
            // 
            // ID
            // 
            ID.AutoSize = true;
            ID.Location = new Point(68, 63);
            ID.Name = "ID";
            ID.Size = new Size(24, 20);
            ID.TabIndex = 8;
            ID.Text = "ID";
            // 
            // presServIPText
            // 
            presServIPText.Location = new Point(176, 101);
            presServIPText.Name = "presServIPText";
            presServIPText.Size = new Size(236, 27);
            presServIPText.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(68, 104);
            label4.Name = "label4";
            label4.Size = new Size(76, 20);
            label4.TabIndex = 6;
            label4.Text = "IP Address";
            // 
            // presServPortText
            // 
            presServPortText.Location = new Point(176, 149);
            presServPortText.Name = "presServPortText";
            presServPortText.Size = new Size(236, 27);
            presServPortText.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(68, 152);
            label3.Name = "label3";
            label3.Size = new Size(35, 20);
            label3.TabIndex = 3;
            label3.Text = "Port";
            // 
            // presServURLText
            // 
            presServURLText.Location = new Point(176, 199);
            presServURLText.Name = "presServURLText";
            presServURLText.Size = new Size(236, 27);
            presServURLText.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(68, 202);
            label2.Name = "label2";
            label2.Size = new Size(35, 20);
            label2.TabIndex = 1;
            label2.Text = "URL";
            // 
            // presServRepositoryText
            // 
            presServRepositoryText.Location = new Point(176, 253);
            presServRepositoryText.Name = "presServRepositoryText";
            presServRepositoryText.Size = new Size(236, 27);
            presServRepositoryText.TabIndex = 12;
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
            // newPresServButton (Style Siemens)
            // 
            newPresServButton.BackColor = Color.FromArgb(0, 101, 110);
            newPresServButton.FlatStyle = FlatStyle.Flat;
            newPresServButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            newPresServButton.ForeColor = Color.White;
            newPresServButton.Location = new Point(176, 305);
            newPresServButton.Name = "newPresServButton";
            newPresServButton.Size = new Size(236, 35);
            newPresServButton.TabIndex = 5;
            newPresServButton.Text = "SUBMIT";
            newPresServButton.UseVisualStyleBackColor = false;
            newPresServButton.Click += newPresServButton_Click;
            // 
            // FormPresentationServ
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(800, 450);
            Controls.Add(presServPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormPresentationServ";
            Text = "FormPresentationServ";
            presServPanel.ResumeLayout(false);
            presServPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel presServPanel;
        private TextBox presServIDText;
        private Label ID;
        private TextBox presServIPText;
        private Label label4;
        private Button newPresServButton;
        private TextBox presServPortText;
        private Label label3;
        private TextBox presServURLText;
        private Label label2;
        private Label label1;
        private TextBox presServRepositoryText;
        private Label label5;
    }
}