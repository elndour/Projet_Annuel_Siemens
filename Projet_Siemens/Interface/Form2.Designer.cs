namespace Projet_Siemens
{
    partial class Form2
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
            form2MainPanel = new Panel();
            extractFileButton = new Button();
            import = new Button();
            buttonConfirmJson = new Button();
            viewerPanel = new Panel();
            dragDropPanel = new Panel();
            dragDbButton = new Button();
            dragAppServerButton = new Button();
            dragWebServerButton = new Button();
            dragPresServerButton = new Button();
            dragDropLabel = new Label();
            edgeButton = new Button();
            subNetworkButton = new Button();
            form2MainPanel.SuspendLayout();
            dragDropPanel.SuspendLayout();
            SuspendLayout();

            // 1. ORIGINAL BUTTON PANEL SUPPRIMÉ - Tout est maintenant dans dragDropPanel

            // 1. FORM2 MAIN PANEL (Zone centrale agrandie)
            form2MainPanel.BackColor = Color.FromArgb(240, 242, 245);
            form2MainPanel.Controls.Add(extractFileButton);
            form2MainPanel.Controls.Add(import);
            form2MainPanel.Controls.Add(buttonConfirmJson);
            form2MainPanel.Controls.Add(viewerPanel);
            form2MainPanel.Dock = DockStyle.Fill;
            form2MainPanel.Location = new Point(0, 0);
            this.form2MainPanel.Name = "form2MainPanel";
            form2MainPanel.Size = new Size(800, 450);
            form2MainPanel.TabIndex = 0;

            viewerPanel.BackColor = Color.White;
            viewerPanel.BorderStyle = BorderStyle.FixedSingle;
            viewerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            viewerPanel.Location = new Point(155, 15);
            viewerPanel.Size = new Size(630, 350);
            viewerPanel.AllowDrop = true;

            void StyleActionBtn(Button btn, Color c, int x, string txt) {
                btn.BackColor = c;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                btn.Location = new Point(x, 385);
                btn.Size = new Size(115, 40);
                btn.Text = txt;
                btn.Anchor = AnchorStyles.Bottom;
            }

       // --- SECTION BOUTONS D'ACTION (MODIFIÉE) ---

// 1. On s'assure qu'ils sont bien dans le panel principal pour être visibles
this.form2MainPanel.Controls.Add(this.extractFileButton);
this.form2MainPanel.Controls.Add(this.import);
this.form2MainPanel.Controls.Add(this.buttonConfirmJson);

// 2. Bouton IMPORT (Positionné à gauche en bas)
this.import.BackColor = Color.FromArgb(0, 101, 110);
this.import.FlatStyle = FlatStyle.Flat;
this.import.FlatAppearance.BorderSize = 0;
this.import.ForeColor = Color.White;
this.import.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Pas de gras
this.import.Location = new Point(145, 385); 
this.import.Name = "import";
this.import.Size = new Size(110, 35);
this.import.TabIndex = 2;
this.import.Text = "IMPORT";
this.import.UseVisualStyleBackColor = false;
this.import.Anchor = AnchorStyles.Bottom;
this.import.Click += new System.EventHandler(this.import_Click);

// 3. Bouton CONFIRM (Positionné au milieu en bas)
this.buttonConfirmJson.BackColor = Color.FromArgb(0, 101, 110);
this.buttonConfirmJson.FlatStyle = FlatStyle.Flat;
this.buttonConfirmJson.FlatAppearance.BorderSize = 0;
this.buttonConfirmJson.ForeColor = Color.White;
this.buttonConfirmJson.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Pas de gras
this.buttonConfirmJson.Location = new Point(270, 385);
this.buttonConfirmJson.Name = "buttonConfirmJson";
this.buttonConfirmJson.Size = new Size(110, 35);
this.buttonConfirmJson.TabIndex = 1;
this.buttonConfirmJson.Text = "CONFIRM";
this.buttonConfirmJson.UseVisualStyleBackColor = false;
this.buttonConfirmJson.Anchor = AnchorStyles.Bottom;
this.buttonConfirmJson.Click += new System.EventHandler(this.buttonConfirmJson_Click);

// 4. Bouton EXTRACT (Positionné à droite en bas)
this.extractFileButton.BackColor = Color.FromArgb(0, 101, 110);
this.extractFileButton.FlatStyle = FlatStyle.Flat;
this.extractFileButton.FlatAppearance.BorderSize = 0;
this.extractFileButton.ForeColor = Color.White;
this.extractFileButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular); // Pas de gras
this.extractFileButton.Location = new Point(395, 385);
this.extractFileButton.Name = "extractFileButton";
this.extractFileButton.Size = new Size(110, 35);
this.extractFileButton.TabIndex = 3;
this.extractFileButton.Text = "EXTRACT FILE";
this.extractFileButton.UseVisualStyleBackColor = false;
this.extractFileButton.Anchor = AnchorStyles.Bottom;
this.extractFileButton.Click += new System.EventHandler(this.extractFileButton_Click);

            // 2. DRAG & DROP + MANAGEMENT PANEL (Panel à gauche agrandi)
            dragDropPanel.BackColor = Color.FromArgb(50, 50, 55);
            dragDropPanel.Location = new Point(15, 15);
            dragDropPanel.Size = new Size(130, 420);
            dragDropPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dragDropPanel.BorderStyle = BorderStyle.FixedSingle;

            dragDropLabel.Text = "🎯 Toolbox";
            dragDropLabel.ForeColor = Color.White;
            dragDropLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dragDropLabel.Location = new Point(5, 5);
            dragDropLabel.Size = new Size(120, 25);
            dragDropLabel.TextAlign = ContentAlignment.MiddleCenter;

            void StyleDragBtn(Button btn, int y, string txt, Color color) {
                btn.BackColor = color;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.White;
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                btn.Location = new Point(10, y);
                btn.Size = new Size(110, 50);
                btn.Text = txt;
                btn.AllowDrop = false;
                btn.Tag = txt;
                btn.Cursor = Cursors.Hand;
            }

            void StyleManagementBtn(Button btn, int y, string txt, Color color) {
                btn.BackColor = color;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                btn.Location = new Point(10, y);
                btn.Size = new Size(110, 40);
                btn.Text = txt;
                btn.Cursor = Cursors.Hand;
            }

            // Drag & Drop Buttons
            StyleDragBtn(dragDbButton, 40, "Database", Color.FromArgb(200, 50, 50));
            dragDbButton.MouseDown += DragButton_MouseDown;

            StyleDragBtn(dragAppServerButton, 100, "App Server", Color.FromArgb(200, 200, 50));
            dragAppServerButton.MouseDown += DragButton_MouseDown;

            StyleDragBtn(dragWebServerButton, 160, "Web Server", Color.FromArgb(200, 120, 50));
            dragWebServerButton.MouseDown += DragButton_MouseDown;

            StyleDragBtn(dragPresServerButton, 220, "Pres Server", Color.FromArgb(100, 150, 200));
            dragPresServerButton.MouseDown += DragButton_MouseDown;

            // Management Buttons
            StyleManagementBtn(edgeButton, 290, "⚡ Edges", Color.FromArgb(0, 101, 110));
            edgeButton.Click += edgeButton_Click;

            StyleManagementBtn(subNetworkButton, 340, "SubNetworks", Color.FromArgb(0, 101, 110));
            subNetworkButton.Click += subNetworkButton_Click;

            dragDropPanel.Controls.Add(dragDropLabel);
            dragDropPanel.Controls.Add(dragDbButton);
            dragDropPanel.Controls.Add(dragAppServerButton);
            dragDropPanel.Controls.Add(dragWebServerButton);
            dragDropPanel.Controls.Add(dragPresServerButton);
            dragDropPanel.Controls.Add(edgeButton);
            dragDropPanel.Controls.Add(subNetworkButton);

            form2MainPanel.Controls.Add(dragDropPanel);

            // 3. FORM2 CONFIGURATION FINALE
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);

            this.Controls.Add(form2MainPanel);
            
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;

            form2MainPanel.ResumeLayout(false);
            dragDropPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel form2MainPanel;
        private Panel viewerPanel;
        private Button buttonConfirmJson;
        private Button import;
        private Button extractFileButton;
        private Panel dragDropPanel;
        private Button dragDbButton;
        private Button dragAppServerButton;
        private Button dragWebServerButton;
        private Button dragPresServerButton;
        private Label dragDropLabel;
        private Button edgeButton;
        private Button subNetworkButton;
    }
}