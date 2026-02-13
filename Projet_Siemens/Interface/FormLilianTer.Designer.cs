namespace Projet_Siemens
{
    partial class FormLilianTer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLilianTer));
            panelside = new Panel();
            btnsettings = new Button();
            pictureBox1 = new PictureBox();
            btnvisu = new Button();
            btnconfig = new Button();
            panelheader = new Panel();
            btnclose = new Button();
            btnmaximize = new Button();
            btnminimize = new Button();
            menuStrip1 = new MenuStrip();
            fichierToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editionToolStripMenuItem = new ToolStripMenuItem();
            cancelToolStripMenuItem = new ToolStripMenuItem();
            retablishedToolStripMenuItem = new ToolStripMenuItem();
            displayToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            mainpanel = new Panel();
            panelside.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelheader.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panelside
            // 
            panelside.BackColor = Color.FromArgb(64, 64, 64);
            panelside.Controls.Add(btnsettings);
            panelside.Controls.Add(pictureBox1);
            panelside.Controls.Add(btnvisu);
            panelside.Controls.Add(btnconfig);
            panelside.Dock = DockStyle.Left;
            panelside.Location = new Point(0, 30);
            panelside.Name = "panelside";
            panelside.Size = new Size(200, 522);
            panelside.TabIndex = 0;
            // 
            // btnsettings
            // 
            btnsettings.BackColor = Color.Silver;
            btnsettings.FlatStyle = FlatStyle.Flat;
            btnsettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnsettings.ForeColor = Color.White;
            btnsettings.Image = (Image)resources.GetObject("btnsettings.Image");
            btnsettings.ImageAlign = ContentAlignment.MiddleLeft;
            btnsettings.Location = new Point(0, 131);
            btnsettings.Name = "btnsettings";
            btnsettings.Size = new Size(200, 30);
            btnsettings.TabIndex = 2;
            btnsettings.Text = "           SETTINGS";
            btnsettings.TextAlign = ContentAlignment.MiddleLeft;
            btnsettings.UseVisualStyleBackColor = false;
            btnsettings.Click += btnsettings_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(53, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(100, 21);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // btnvisu
            // 
            btnvisu.BackColor = Color.Silver;
            btnvisu.FlatStyle = FlatStyle.Flat;
            btnvisu.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnvisu.ForeColor = Color.White;
            btnvisu.Image = (Image)resources.GetObject("btnvisu.Image");
            btnvisu.ImageAlign = ContentAlignment.MiddleLeft;
            btnvisu.Location = new Point(0, 95);
            btnvisu.Name = "btnvisu";
            btnvisu.Size = new Size(200, 30);
            btnvisu.TabIndex = 1;
            btnvisu.Text = "     VISUALIZATION";
            btnvisu.UseVisualStyleBackColor = false;
            btnvisu.Click += btnvisu_Click;
            // 
            // btnconfig
            // 
            btnconfig.BackColor = Color.Silver;
            btnconfig.FlatStyle = FlatStyle.Flat;
            btnconfig.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnconfig.ForeColor = Color.White;
            btnconfig.Image = (Image)resources.GetObject("btnconfig.Image");
            btnconfig.ImageAlign = ContentAlignment.MiddleLeft;
            btnconfig.Location = new Point(0, 59);
            btnconfig.Name = "btnconfig";
            btnconfig.Size = new Size(200, 30);
            btnconfig.TabIndex = 0;
            btnconfig.Text = "      CONFIGURATION";
            btnconfig.UseVisualStyleBackColor = false;
            btnconfig.Click += btnconfig_Click;
            // 
            // panelheader
            // 
            panelheader.BackColor = Color.Gray;
            panelheader.Controls.Add(btnminimize);
            panelheader.Controls.Add(btnmaximize);
            panelheader.Controls.Add(btnclose);
            panelheader.Controls.Add(menuStrip1);
            panelheader.Dock = DockStyle.Top;
            panelheader.Location = new Point(0, 0);
            panelheader.Name = "panelheader";
            panelheader.Size = new Size(1386, 30);
            panelheader.TabIndex = 1;
            panelheader.Paint += panelheader_Paint;
            // 
            // btnclose
            // 
            btnclose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnclose.BackColor = Color.Gray;
            btnclose.FlatStyle = FlatStyle.Flat;
            btnclose.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnclose.ForeColor = Color.White;
            btnclose.Location = new Point(1341, 0);
            btnclose.Name = "btnclose";
            btnclose.Size = new Size(45, 30);
            btnclose.TabIndex = 4;
            btnclose.Text = "X";
            btnclose.UseVisualStyleBackColor = false;
            btnclose.Click += btnclose_Click;
            btnclose.MouseEnter += btnclose_MouseEnter;
            btnclose.MouseLeave += btnclose_MouseLeave;
            // 
            // btnmaximize
            // 
            btnmaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnmaximize.BackColor = Color.Gray;
            btnmaximize.FlatStyle = FlatStyle.Flat;
            btnmaximize.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnmaximize.ForeColor = Color.White;
            btnmaximize.Location = new Point(1296, 0);
            btnmaximize.Name = "btnmaximize";
            btnmaximize.Size = new Size(45, 30);
            btnmaximize.TabIndex = 6;
            btnmaximize.Text = "□";
            btnmaximize.UseVisualStyleBackColor = false;
            btnmaximize.Click += btnmaximize_Click;
            btnmaximize.MouseEnter += btnmaximize_MouseEnter;
            btnmaximize.MouseLeave += btnmaximize_MouseLeave;
            // 
            // btnminimize
            // 
            btnminimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnminimize.BackColor = Color.Gray;
            btnminimize.FlatStyle = FlatStyle.Flat;
            btnminimize.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnminimize.ForeColor = Color.White;
            btnminimize.Location = new Point(1251, 0);
            btnminimize.Name = "btnminimize";
            btnminimize.Size = new Size(45, 30);
            btnminimize.TabIndex = 7;
            btnminimize.Text = "_";
            btnminimize.UseVisualStyleBackColor = false;
            btnminimize.Click += btnminimize_Click;
            btnminimize.MouseEnter += btnminimize_MouseEnter;
            btnminimize.MouseLeave += btnminimize_MouseLeave;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fichierToolStripMenuItem, editionToolStripMenuItem, displayToolStripMenuItem, toolsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1386, 28);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            fichierToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, exitToolStripMenuItem });
            fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            fichierToolStripMenuItem.Size = new Size(66, 24);
            fichierToolStripMenuItem.Text = "Fichier";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(150, 26);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(150, 26);
            openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(150, 26);
            saveToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(150, 26);
            saveAsToolStripMenuItem.Text = "Save as...";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(150, 26);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // editionToolStripMenuItem
            // 
            editionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cancelToolStripMenuItem, retablishedToolStripMenuItem });
            editionToolStripMenuItem.Name = "editionToolStripMenuItem";
            editionToolStripMenuItem.Size = new Size(70, 24);
            editionToolStripMenuItem.Text = "Edition";
            // 
            // cancelToolStripMenuItem
            // 
            cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            cancelToolStripMenuItem.Size = new Size(170, 26);
            cancelToolStripMenuItem.Text = "Cancel";
            // 
            // retablishedToolStripMenuItem
            // 
            retablishedToolStripMenuItem.Name = "retablishedToolStripMenuItem";
            retablishedToolStripMenuItem.Size = new Size(170, 26);
            retablishedToolStripMenuItem.Text = "Retablished";
            // 
            // displayToolStripMenuItem
            // 
            displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            displayToolStripMenuItem.Size = new Size(72, 24);
            displayToolStripMenuItem.Text = "Display";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { optionsToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(58, 24);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(144, 26);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(55, 24);
            helpToolStripMenuItem.Text = "Help";
            // 
            // mainpanel
            // 
            mainpanel.Dock = DockStyle.Fill;
            mainpanel.Location = new Point(200, 30);
            mainpanel.Name = "mainpanel";
            mainpanel.Size = new Size(1186, 522);
            mainpanel.TabIndex = 2;
            // 
            // FormLilianTer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1386, 552);
            Controls.Add(mainpanel);
            Controls.Add(panelside);
            Controls.Add(panelheader);
            MainMenuStrip = menuStrip1;
            Name = "FormLilianTer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormLilianTer";
            panelside.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelheader.ResumeLayout(false);
            panelheader.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelside;
        private Panel panelheader;
        private Panel mainpanel;
        private Button btnconfig;
        private Button btnsettings;
        private Button button3;
        private Button btnvisu;
        private PictureBox pictureBox1;
        private Button btnclose;
        private Button btnmaximize;
        private Button btnminimize;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fichierToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editionToolStripMenuItem;
        private ToolStripMenuItem cancelToolStripMenuItem;
        private ToolStripMenuItem retablishedToolStripMenuItem;
        private ToolStripMenuItem displayToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
    }
}