using System;
using System.Drawing;
using System.Windows.Forms;

namespace Projet_Siemens.Interface
{
    /// <summary>
    /// Formulaire pour saisir les credentials SSH/SFTP
    /// </summary>
    public class SSHCredentialsForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtPort;
        private Button btnOK;
        private Button btnCancel;
        private Label lblServer;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblPort;

        public string Username => txtUsername.Text;
        public string Password => txtPassword.Text;
        public int Port => int.TryParse(txtPort.Text, out int p) ? p : 22;

        public SSHCredentialsForm(string serverIp)
        {
            InitializeComponent(serverIp);
        }

        private void InitializeComponent(string serverIp)
        {
            this.Text = "Credentials SSH/SFTP";
            this.Size = new Size(450, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Server Label
            lblServer = new Label
            {
                Text = $"Serveur : {serverIp}",
                Location = new Point(20, 20),
                Size = new Size(400, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 101, 110)
            };

            // Username
            lblUsername = new Label
            {
                Text = "Nom d'utilisateur SSH :",
                Location = new Point(20, 60),
                Size = new Size(180, 20)
            };

            txtUsername = new TextBox
            {
                Location = new Point(210, 57),
                Size = new Size(200, 25),
                Text = "root"
            };

            // Password
            lblPassword = new Label
            {
                Text = "Mot de passe :",
                Location = new Point(20, 100),
                Size = new Size(180, 20)
            };

            txtPassword = new TextBox
            {
                Location = new Point(210, 97),
                Size = new Size(200, 25),
                UseSystemPasswordChar = true
            };

            // Port
            lblPort = new Label
            {
                Text = "Port SSH :",
                Location = new Point(20, 140),
                Size = new Size(180, 20)
            };

            txtPort = new TextBox
            {
                Location = new Point(210, 137),
                Size = new Size(200, 25),
                Text = "22"
            };

            // Buttons
            btnOK = new Button
            {
                Text = "OK",
                Location = new Point(210, 190),
                Size = new Size(90, 35),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };

            btnCancel = new Button
            {
                Text = "Annuler",
                Location = new Point(320, 190),
                Size = new Size(90, 35),
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel
            };

            // Add controls
            this.Controls.Add(lblServer);
            this.Controls.Add(lblUsername);
            this.Controls.Add(txtUsername);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(lblPort);
            this.Controls.Add(txtPort);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }
    }
}
