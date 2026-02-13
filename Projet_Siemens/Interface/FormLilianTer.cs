using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projet_Siemens.Class;
using Projet_Siemens.Interface;
using Edge = Projet_Siemens.Class.Edge;

namespace Projet_Siemens
{
    public partial class FormLilianTer : Form
    {
        private Network appNetwork = new Network();
        public FormLilianTer()
        {
            InitializeComponent();
        }
        public void loadform(object Form)
        {
            if (this.mainpanel.Controls.Count > 0)
                this.mainpanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.mainpanel.Controls.Add(f);
            this.mainpanel.Tag = f;
            f.Show();
        }

        private void btnconfig_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(appNetwork);
            loadform(form2);
        }

        private void btnvisu_Click(object sender, EventArgs e)
        {
            loadform(new Visualization());
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {
            loadform(new Settings());
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panelheader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnclose_MouseEnter(object sender, EventArgs e)
        {
            btnclose.BackColor = Color.Red;
        }

        private void btnclose_MouseLeave(object sender, EventArgs e)
        {
            btnclose.BackColor = Color.Gray;
        }

        private void btnmaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnmaximize_MouseEnter(object sender, EventArgs e)
        {
            btnmaximize.BackColor = Color.DarkGray;
        }

        private void btnmaximize_MouseLeave(object sender, EventArgs e)
        {
            btnmaximize.BackColor = Color.Gray;
        }

        private void btnminimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnminimize_MouseEnter(object sender, EventArgs e)
        {
            btnminimize.BackColor = Color.DarkGray;
        }

        private void btnminimize_MouseLeave(object sender, EventArgs e)
        {
            btnminimize.BackColor = Color.Gray;
        }
    }
}
