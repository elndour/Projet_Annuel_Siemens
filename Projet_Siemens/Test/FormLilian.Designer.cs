namespace Projet_Siemens
{
    partial class FormLilian
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLilian));
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            btnM = new PictureBox();
            sidebar = new FlowLayoutPanel();
            menuContainer = new FlowLayoutPanel();
            menu = new Button();
            button5 = new Button();
            button6 = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            menuTransition = new System.Windows.Forms.Timer(components);
            sidebarTransition = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnM).BeginInit();
            sidebar.SuspendLayout();
            menuContainer.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnM);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1418, 56);
            panel1.TabIndex = 0;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(1255, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(160, 40);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(59, 17);
            label1.Name = "label1";
            label1.Size = new Size(229, 23);
            label1.TabIndex = 1;
            label1.Text = "VISUALISATION I INTERFACE";
            // 
            // btnM
            // 
            btnM.Image = (Image)resources.GetObject("btnM.Image");
            btnM.Location = new Point(12, 12);
            btnM.Name = "btnM";
            btnM.Size = new Size(28, 30);
            btnM.TabIndex = 0;
            btnM.TabStop = false;
            btnM.Click += btnM_Click;
            // 
            // sidebar
            // 
            sidebar.BackColor = Color.Black;
            sidebar.Controls.Add(menuContainer);
            sidebar.Controls.Add(button1);
            sidebar.Controls.Add(button2);
            sidebar.Controls.Add(button3);
            sidebar.Dock = DockStyle.Left;
            sidebar.Location = new Point(0, 56);
            sidebar.Name = "sidebar";
            sidebar.Size = new Size(256, 525);
            sidebar.TabIndex = 1;
            // 
            // menuContainer
            // 
            menuContainer.BackColor = Color.Black;
            menuContainer.Controls.Add(menu);
            menuContainer.Controls.Add(button5);
            menuContainer.Controls.Add(button6);
            menuContainer.Location = new Point(3, 3);
            menuContainer.Name = "menuContainer";
            menuContainer.Size = new Size(250, 57);
            menuContainer.TabIndex = 6;
            // 
            // menu
            // 
            menu.BackColor = Color.Black;
            menu.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            menu.ForeColor = Color.White;
            menu.Image = (Image)resources.GetObject("menu.Image");
            menu.Location = new Point(0, 0);
            menu.Margin = new Padding(0);
            menu.Name = "menu";
            menu.Size = new Size(250, 57);
            menu.TabIndex = 5;
            menu.Text = "Menu";
            menu.UseVisualStyleBackColor = false;
            menu.Click += menu_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.Black;
            button5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button5.ForeColor = Color.White;
            button5.Image = (Image)resources.GetObject("button5.Image");
            button5.Location = new Point(0, 57);
            button5.Margin = new Padding(0);
            button5.Name = "button5";
            button5.Size = new Size(250, 57);
            button5.TabIndex = 6;
            button5.Text = "Visualize";
            button5.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            button6.BackColor = Color.Black;
            button6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button6.ForeColor = Color.White;
            button6.Image = (Image)resources.GetObject("button6.Image");
            button6.Location = new Point(0, 114);
            button6.Margin = new Padding(0);
            button6.Name = "button6";
            button6.Size = new Size(250, 57);
            button6.TabIndex = 7;
            button6.Text = "Add";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Black;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(3, 66);
            button1.Name = "button1";
            button1.Size = new Size(250, 57);
            button1.TabIndex = 2;
            button1.Text = "Settings";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Black;
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.Location = new Point(3, 129);
            button2.Name = "button2";
            button2.Size = new Size(250, 57);
            button2.TabIndex = 3;
            button2.Text = "About";
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.Black;
            button3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.White;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.Location = new Point(3, 192);
            button3.Name = "button3";
            button3.Size = new Size(250, 57);
            button3.TabIndex = 4;
            button3.Text = "Logout";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // sidebarTransition
            // 
            sidebarTransition.Interval = 10;
            sidebarTransition.Tick += sidebarTransition_Tick;
            // 
            // FormLilian
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1418, 581);
            Controls.Add(sidebar);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormLilian";
            Text = "Form2";
            Load += FormLilian_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnM).EndInit();
            sidebar.ResumeLayout(false);
            menuContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox btnM;
        private Label label1;
        private PictureBox pictureBox2;
        private FlowLayoutPanel sidebar;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button menu;
        private FlowLayoutPanel menuContainer;
        private Button button5;
        private Button button6;
        private System.Windows.Forms.Timer menuTransition;
        private System.Windows.Forms.Timer sidebarTransition;
    }
}