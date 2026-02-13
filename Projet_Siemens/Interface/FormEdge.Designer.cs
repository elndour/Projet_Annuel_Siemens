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
            edgePanel.SuspendLayout();
            newEdgePanel.SuspendLayout();
            SuspendLayout();
            // 
            // edgePanel
            // 
            edgePanel.Controls.Add(edgeButton);
            edgePanel.Controls.Add(targetMachineList);
            edgePanel.Controls.Add(sourceMachineList);
            edgePanel.Location = new Point(12, 28);
            edgePanel.Name = "edgePanel";
            edgePanel.Size = new Size(430, 399);
            edgePanel.TabIndex = 0;
            // 
            // edgeButton
            // 
            edgeButton.Location = new Point(159, 341);
            edgeButton.Name = "edgeButton";
            edgeButton.Size = new Size(121, 39);
            edgeButton.TabIndex = 2;
            edgeButton.Text = "Create an edge";
            edgeButton.UseVisualStyleBackColor = true;
            edgeButton.Click += edgeButton_Click;
            // 
            // targetMachineList
            // 
            targetMachineList.DropDownStyle = ComboBoxStyle.DropDownList;
            targetMachineList.FormattingEnabled = true;
            targetMachineList.Location = new Point(244, 29);
            targetMachineList.Name = "targetMachineList";
            targetMachineList.Size = new Size(151, 28);
            targetMachineList.TabIndex = 1;
            // 
            // sourceMachineList
            // 
            sourceMachineList.FormattingEnabled = true;
            sourceMachineList.Location = new Point(28, 29);
            sourceMachineList.Name = "sourceMachineList";
            sourceMachineList.Size = new Size(151, 28);
            sourceMachineList.TabIndex = 0;
            // 
            // newEdgePanel
            // 
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
            newEdgePanel.Location = new Point(448, 28);
            newEdgePanel.Name = "newEdgePanel";
            newEdgePanel.Size = new Size(272, 399);
            newEdgePanel.TabIndex = 1;
            newEdgePanel.Visible = false;
            // 
            // newEdgeButton
            // 
            newEdgeButton.Location = new Point(54, 367);
            newEdgeButton.Name = "newEdgeButton";
            newEdgeButton.Size = new Size(94, 29);
            newEdgeButton.TabIndex = 13;
            newEdgeButton.Text = "Confirm";
            newEdgeButton.UseVisualStyleBackColor = true;
            newEdgeButton.Click += newEdgeButton_Click;
            // 
            // edgeHasFirewall
            // 
            edgeHasFirewall.AutoSize = true;
            edgeHasFirewall.Location = new Point(12, 328);
            edgeHasFirewall.Name = "edgeHasFirewall";
            edgeHasFirewall.Size = new Size(209, 24);
            edgeHasFirewall.TabIndex = 12;
            edgeHasFirewall.Text = "Firewall between machines";
            edgeHasFirewall.UseVisualStyleBackColor = true;
            // 
            // edgeIsBirectional
            // 
            edgeIsBirectional.AutoSize = true;
            edgeIsBirectional.Location = new Point(12, 298);
            edgeIsBirectional.Name = "edgeIsBirectional";
            edgeIsBirectional.Size = new Size(170, 24);
            edgeIsBirectional.TabIndex = 11;
            edgeIsBirectional.Text = "Bidirectional relation";
            edgeIsBirectional.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 100);
            label6.Name = "label6";
            label6.Size = new Size(62, 20);
            label6.TabIndex = 10;
            label6.Text = "IPTarget";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 55);
            label5.Name = "label5";
            label5.Size = new Size(66, 20);
            label5.TabIndex = 9;
            label5.Text = "IPSource";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 196);
            label4.Name = "label4";
            label4.Size = new Size(76, 20);
            label4.TabIndex = 8;
            label4.Text = "PortTarget";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 147);
            label3.Name = "label3";
            label3.Size = new Size(80, 20);
            label3.TabIndex = 7;
            label3.Text = "PortSource";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 247);
            label2.Name = "label2";
            label2.Size = new Size(65, 20);
            label2.TabIndex = 6;
            label2.Text = "Protocol";
            // 
            // edgeIPTarget
            // 
            edgeIPTarget.Location = new Point(98, 100);
            edgeIPTarget.Name = "edgeIPTarget";
            edgeIPTarget.Size = new Size(125, 27);
            edgeIPTarget.TabIndex = 5;
            // 
            // edgePortTarget
            // 
            edgePortTarget.Location = new Point(98, 196);
            edgePortTarget.Name = "edgePortTarget";
            edgePortTarget.Size = new Size(125, 27);
            edgePortTarget.TabIndex = 4;
            // 
            // edgeIPSource
            // 
            edgeIPSource.Location = new Point(98, 52);
            edgeIPSource.Name = "edgeIPSource";
            edgeIPSource.Size = new Size(125, 27);
            edgeIPSource.TabIndex = 3;
            edgeIPSource.Text = "\r\n";
            // 
            // edgePortSource
            // 
            edgePortSource.Location = new Point(98, 147);
            edgePortSource.Name = "edgePortSource";
            edgePortSource.Size = new Size(125, 27);
            edgePortSource.TabIndex = 2;
            // 
            // edgeProtocol
            // 
            edgeProtocol.Location = new Point(98, 244);
            edgeProtocol.Name = "edgeProtocol";
            edgeProtocol.Size = new Size(125, 27);
            edgeProtocol.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 14);
            label1.Name = "label1";
            label1.Size = new Size(125, 20);
            label1.TabIndex = 0;
            label1.Text = "Edge Information";
            // 
            // FormEdge
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(newEdgePanel);
            Controls.Add(edgePanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormEdge";
            Text = "FormEdge";
            edgePanel.ResumeLayout(false);
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