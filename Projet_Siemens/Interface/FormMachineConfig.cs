using System;
using System.Drawing;
using System.Windows.Forms;

namespace Projet_Siemens.Interface
{
    public partial class FormMachineConfig : Form
    {
        public string MachineId => machineIdText?.Text.Trim() ?? string.Empty;
        public string Ip => ipText?.Text.Trim() ?? string.Empty;

        // Database fields
        public string DbUsername => usernameText?.Text.Trim() ?? string.Empty;
        public string DbPassword => passwordText?.Text.Trim() ?? string.Empty;
        public string DbSshPort => sshPortText?.Text.Trim() ?? string.Empty;
        public string DbInstanceName => instanceNameText?.Text.Trim() ?? string.Empty;

        // App server fields
        public string AppDescription => descText?.Text.Trim() ?? string.Empty;
        public string AppRepository => repositoryText?.Text.Trim() ?? string.Empty;
        public string AppServicePort => servicePortText?.Text.Trim() ?? string.Empty;

        // Web server fields
        public string WebEndPoints => endpointsText?.Text.Trim() ?? string.Empty;
        public string WebApi => apiText?.Text.Trim() ?? string.Empty;
        public string WebRepository => webRepositoryText?.Text.Trim() ?? string.Empty;

        // Presentation server fields
        public string PresUrl => urlText?.Text.Trim() ?? string.Empty;
        public string PresServicePort => presServicePortText?.Text.Trim() ?? string.Empty;
        public string PresRepository => presRepositoryText?.Text.Trim() ?? string.Empty;

        private readonly string machineType;

        private TextBox? machineIdText;
        private TextBox? ipText;

        private TextBox? usernameText;
        private TextBox? passwordText;
        private TextBox? sshPortText;
        private TextBox? instanceNameText;

        private TextBox? descText;
        private TextBox? repositoryText;
        private TextBox? servicePortText;

        private TextBox? endpointsText;
        private TextBox? apiText;
        private TextBox? webRepositoryText;

        private TextBox? urlText;
        private TextBox? presServicePortText;
        private TextBox? presRepositoryText;

        public FormMachineConfig(string machineType, string defaultId, string defaultIp)
        {
            this.machineType = machineType;
            InitializeForm(defaultId, defaultIp);
        }

        private void InitializeForm(string defaultId, string defaultIp)
        {
            this.Text = $"Configure {machineType}";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ClientSize = new Size(420, 460);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            var titleLabel = new Label
            {
                Text = $"{machineType} Configuration",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 101, 110),
                Location = new Point(16, 10),
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(245, 248, 250),
                Padding = new Padding(12)
            };
            this.Controls.Add(mainPanel);

            var card = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(10, 50),
                Size = new Size(390, 350)
            };
            mainPanel.Controls.Add(card);

            int top = 20;
            int labelX = 16;
            int inputX = 145;
            int fieldHeight = 28;
            int spacing = 10;

            void AddRow(string label, TextBox input, string defaultValue = "")
            {
                var lbl = new Label
                {
                    Text = label,
                    Location = new Point(labelX, top + 4),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular)
                };
                card.Controls.Add(lbl);

                input.Location = new Point(inputX, top);
                input.Size = new Size(220, fieldHeight);
                input.Font = new Font("Segoe UI", 9F);
                input.Text = defaultValue;
                input.BackColor = Color.WhiteSmoke;
                card.Controls.Add(input);

                top += fieldHeight + spacing;
            }


            machineIdText = new TextBox();
            ipText = new TextBox();
            AddRow("Machine ID:", machineIdText, defaultId);
            AddRow("IP Address:", ipText, defaultIp);

            if (machineType == "Database")
            {
                usernameText = new TextBox();
                passwordText = new TextBox();
                passwordText.UseSystemPasswordChar = true;
                sshPortText = new TextBox();
                instanceNameText = new TextBox();

                AddRow("Username:", usernameText, "admin");
                AddRow("Password:", passwordText, "password");
                AddRow("SSH Port:", sshPortText, "22");
                AddRow("Instance Name:", instanceNameText, "ORCL");
            }
            else if (machineType == "App Server")
            {
                descText = new TextBox();
                repositoryText = new TextBox();
                servicePortText = new TextBox();

                AddRow("Description:", descText, "Application Server");
                AddRow("Repository:", repositoryText, "/opt/app");
                AddRow("Service Port:", servicePortText, "8080");
            }
            else if (machineType == "Web Server")
            {
                endpointsText = new TextBox();
                apiText = new TextBox();
                webRepositoryText = new TextBox();

                AddRow("Endpoints:", endpointsText, "/api/v1");
                AddRow("API Path:", apiText, "/api");
                AddRow("Repository:", webRepositoryText, "/var/www");
            }
            else if (machineType == "Pres Server")
            {
                urlText = new TextBox();
                presServicePortText = new TextBox();
                presRepositoryText = new TextBox();

                AddRow("URL:", urlText, "http://localhost");
                AddRow("Service Port:", presServicePortText, "3389");
                AddRow("Repository:", presRepositoryText, "/opt/pres");
            }

            var okBtn = new Button
            {
                Text = "OK",
                Location = new Point(152, top + 10),
                Size = new Size(90, 34),
                BackColor = Color.FromArgb(0, 101, 110),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK
            };
            okBtn.FlatAppearance.BorderSize = 0;
            okBtn.Click += OkBtn_Click;
            card.Controls.Add(okBtn);

            var cancelBtn = new Button
            {
                Text = "Cancel",
                Location = new Point(252, top + 10),
                Size = new Size(90, 34),
                BackColor = Color.FromArgb(220, 80, 70),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.Cancel
            };
            cancelBtn.FlatAppearance.BorderSize = 0;
            card.Controls.Add(cancelBtn);

            this.AcceptButton = okBtn;
            this.CancelButton = cancelBtn;
        }

        private void OkBtn_Click(object? sender, EventArgs e)
        {
            // Validate common fields
            if (string.IsNullOrWhiteSpace(MachineId))
            {
                MessageBox.Show("Machine ID is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            if (string.IsNullOrWhiteSpace(Ip))
            {
                MessageBox.Show("IP address is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            switch (machineType)
            {
                case "Database":
                    if (string.IsNullOrWhiteSpace(DbUsername) || string.IsNullOrWhiteSpace(DbPassword) || string.IsNullOrWhiteSpace(DbSshPort) || string.IsNullOrWhiteSpace(DbInstanceName))
                    {
                        MessageBox.Show("All database fields are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.None;
                    }
                    else if (!int.TryParse(DbSshPort, out _))
                    {
                        MessageBox.Show("SSH Port must be an integer.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.None;
                    }
                    break;
                case "App Server":
                    if (string.IsNullOrWhiteSpace(AppDescription) || string.IsNullOrWhiteSpace(AppRepository) || string.IsNullOrWhiteSpace(AppServicePort))
                    {
                        MessageBox.Show("All app server fields are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.None;
                    }
                    else if (!int.TryParse(AppServicePort, out _))
                    {
                        MessageBox.Show("Service Port must be an integer.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.None;
                    }
                    break;
                case "Web Server":
                    if (string.IsNullOrWhiteSpace(WebEndPoints) || string.IsNullOrWhiteSpace(WebApi) || string.IsNullOrWhiteSpace(WebRepository))
                    {
                        MessageBox.Show("All web server fields are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.None;
                    }
                    break;
                case "Pres Server":
                    if (string.IsNullOrWhiteSpace(PresUrl) || string.IsNullOrWhiteSpace(PresServicePort) || string.IsNullOrWhiteSpace(PresRepository))
                    {
                        MessageBox.Show("All presentation server fields are required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.None;
                    }
                    else if (!int.TryParse(PresServicePort, out _))
                    {
                        MessageBox.Show("Service Port must be an integer.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.None;
                    }
                    break;
            }
        }
    }
}
