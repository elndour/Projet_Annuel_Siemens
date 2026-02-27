namespace Projet_Siemens.Interface
{
    partial class FormEdge
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
            edgePanel = new Panel();
            edgeButton = new Button();
            targetMachineList = new ComboBox();
            sourceMachineList = new ComboBox();
            newEdgePanel = new Panel();
            newEdgeButton = new Button();
            edgeHasFirewall = new CheckBox();
            edgeIsBirectional = new CheckBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            edgeIPTarget = new TextBox();
            edgePortTarget = new TextBox();
            edgeIPSource = new TextBox();
            edgePortSource = new TextBox();
            edgeProtocol = new TextBox();
            label1 = new Label();
            Label labelTitleSelection = new Label();
            edgePanel.SuspendLayout();
            newEdgePanel.SuspendLayout();
            SuspendLayout();
            // 
            // edgePanel (Panneau de Sélection Gauche)
            // 
            edgePanel.BackColor = Color.FromArgb(240, 242, 245);
            edgePanel.Controls.Add(labelTitleSelection);
            edgePanel.Controls.Add(edgeButton);
            edgePanel.Controls.Add(targetMachineList);
            edgePanel.Controls.Add(sourceMachineList);
            edgePanel.Dock = DockStyle.Left; // Prend la moitié gauche
            edgePanel.Location = new Point(0, 0);
            edgePanel.Name = "edgePanel";
            edgePanel.Size = new Size(335, 450);
            edgePanel.TabIndex = 0;
            // 
            // labelTitleSelection
            // 
            labelTitleSelection.AutoSize = true;
            labelTitleSelection.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelTitleSelection.ForeColor = Color.FromArgb(0, 101, 110);
            labelTitleSelection.Location = new Point(40, 40);
            labelTitleSelection.Text = "Machine Selection";
            // 
            // sourceMachineList
            // 
            sourceMachineList.DropDownStyle = ComboBoxStyle.DropDownList;
            sourceMachineList.FormattingEnabled = true;
            sourceMachineList.Location = new Point(40, 100);
            sourceMachineList.Name = "sourceMachineList";
            sourceMachineList.Size = new Size(250, 28);
            sourceMachineList.TabIndex = 0;
            // 
            // targetMachineList
            // 
            targetMachineList.DropDownStyle = ComboBoxStyle.DropDownList;
            targetMachineList.FormattingEnabled = true;
            targetMachineList.Location = new Point(40, 160);
            targetMachineList.Name = "targetMachineList";
            targetMachineList.Size = new Size(250, 28);
            targetMachineList.TabIndex = 1;
            // 
            // edgeButton
            // 
            edgeButton.BackColor = Color.FromArgb(0, 101, 110);
            edgeButton.FlatStyle = FlatStyle.Flat;
            edgeButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            edgeButton.ForeColor = Color.White;
            edgeButton.Location = new Point(40, 230);
            edgeButton.Name = "edgeButton";
            edgeButton.Size = new Size(250, 40);
            edgeButton.TabIndex = 2;
            edgeButton.Text = "Configure Relation";
            edgeButton.UseVisualStyleBackColor = false;
            edgeButton.Click += edgeButton_Click;
            // 
            // newEdgePanel (Panneau de Configuration Droite)
            // 
            newEdgePanel.BackColor = Color.White;
            newEdgePanel.Controls.Add(newEdgeButton);
            newEdgePanel.Controls.Add(edgeHasFirewall);
            newEdgePanel.Controls.Add(edgeIsBirectional);
            newEdgePanel.Controls.Add(label6);
            newEdgePanel.Controls.Add(label5);
            newEdgePanel.Controls.Add(label4);
            newEdgePanel.Controls.Add(label3);
            newEdgePanel.Controls.Add(label2);
            newEdgePanel.Controls.Add(edgeIPTarget);
            newEdgePanel.Controls.Add(edgePortTarget);
            newEdgePanel.Controls.Add(edgeIPSource);
            newEdgePanel.Controls.Add(edgePortSource);
            newEdgePanel.Controls.Add(edgeProtocol);
            newEdgePanel.Controls.Add(label1);
            newEdgePanel.Dock = DockStyle.Fill; // Remplit le reste (droite)
            newEdgePanel.Location = new Point(335, 0);
            newEdgePanel.Name = "newEdgePanel";
            newEdgePanel.Size = new Size(335, 450);
            newEdgePanel.TabIndex = 1;
            newEdgePanel.Visible = false; // Caché par défaut jusqu'au clic
            // 
            // label1 (Titre Info)
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(0, 101, 110);
            label1.Location = new Point(30, 25);
            label1.Name = "label1";
            label1.Size = new Size(174, 28);
            label1.Text = "Edge Properties";
            // 
            // Labels (IP/Port)
            // 
            label5.Location = new Point(30, 70); label5.Text = "IP Source:"; label5.AutoSize = true;
            label6.Location = new Point(30, 110); label6.Text = "IP Target:"; label6.AutoSize = true;
            label3.Location = new Point(30, 150); label3.Text = "Port Source:"; label3.AutoSize = true;
            label4.Location = new Point(30, 190); label4.Text = "Port Target:"; label4.AutoSize = true;
            label2.Location = new Point(30, 230); label2.Text = "Protocol:"; label2.AutoSize = true;
            // 
            // TextBoxes
            // 
            edgeIPSource.Location = new Point(140, 67); edgeIPSource.Size = new Size(160, 27);
            edgeIPTarget.Location = new Point(140, 107); edgeIPTarget.Size = new Size(160, 27);
            edgePortSource.Location = new Point(140, 147); edgePortSource.Size = new Size(160, 27);
            edgePortTarget.Location = new Point(140, 187); edgePortTarget.Size = new Size(160, 27);
            edgeProtocol.Location = new Point(140, 227); edgeProtocol.Size = new Size(160, 27);
            // 
            // edgeIsBirectional
            // 
            edgeIsBirectional.AutoSize = true;
            edgeIsBirectional.Location = new Point(30, 275);
            edgeIsBirectional.Name = "edgeIsBirectional";
            edgeIsBirectional.Size = new Size(170, 24);
            edgeIsBirectional.Text = "Bidirectional relation";
            // 
            // edgeHasFirewall
            // 
            edgeHasFirewall.AutoSize = true;
            edgeHasFirewall.Location = new Point(30, 305);
            edgeHasFirewall.Name = "edgeHasFirewall";
            edgeHasFirewall.Size = new Size(209, 24);
            edgeHasFirewall.Text = "Firewall protection";
            // 
            // newEdgeButton (Confirm)
            // 
            newEdgeButton.BackColor = Color.FromArgb(0, 101, 110);
            newEdgeButton.FlatStyle = FlatStyle.Flat;
            newEdgeButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            newEdgeButton.ForeColor = Color.White;
            newEdgeButton.Location = new Point(30, 350);
            newEdgeButton.Name = "newEdgeButton";
            newEdgeButton.Size = new Size(270, 40);
            newEdgeButton.TabIndex = 13;
            newEdgeButton.Text = "Confirm and Add";
            newEdgeButton.UseVisualStyleBackColor = false;
            newEdgeButton.Click += newEdgeButton_Click;
            // 
            // FormEdge
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(670, 450);
            Controls.Add(newEdgePanel);
            Controls.Add(edgePanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormEdge";
            Text = "Network Edge Configuration";
            edgePanel.ResumeLayout(false);
            edgePanel.PerformLayout();
            newEdgePanel.ResumeLayout(false);
            newEdgePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel edgePanel;
        private ComboBox targetMachineList;
        private ComboBox sourceMachineList;
        private Button edgeButton;
        private Panel newEdgePanel;
        private Label label1;
        private TextBox edgeIPTarget;
        private TextBox edgePortTarget;
        private TextBox edgeIPSource;
        private TextBox edgePortSource;
        private TextBox edgeProtocol;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private CheckBox edgeIsBirectional;
        private CheckBox edgeHasFirewall;
        private Button newEdgeButton;
    }
}