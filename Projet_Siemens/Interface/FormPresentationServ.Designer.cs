namespace Projet_Siemens.Interface
{
    partial class FormPresentationServ
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
            presServPanel = new Panel();
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
            presServRepositoryText = new TextBox();
            label5 = new Label();
            presServPanel.SuspendLayout();
            SuspendLayout();
            // 
            // presServPanel
            // 
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
            presServPanel.Location = new Point(167, 69);
            presServPanel.Name = "presServPanel";
            presServPanel.Size = new Size(467, 340);
            presServPanel.TabIndex = 2;
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
            ID.Location = new Point(95, 63);
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
            label4.Size = new Size(69, 20);
            label4.TabIndex = 6;
            label4.Text = "IP Adress";
            // 
            // newPresServButton
            // 
            newPresServButton.Location = new Point(207, 303);
            newPresServButton.Name = "newPresServButton";
            newPresServButton.Size = new Size(94, 29);
            newPresServButton.TabIndex = 5;
            newPresServButton.Text = "Submit";
            newPresServButton.UseVisualStyleBackColor = true;
            newPresServButton.Click += newPresServButton_Click;
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
            label3.Location = new Point(81, 152);
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
            label2.Location = new Point(77, 202);
            label2.Name = "label2";
            label2.Size = new Size(35, 20);
            label2.TabIndex = 1;
            label2.Text = "URL";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(192, 17);
            label1.Name = "label1";
            label1.Size = new Size(136, 20);
            label1.TabIndex = 0;
            label1.Text = "Presentation Server";
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
            // FormPresentationServ
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
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