using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_Siemens
{
    public partial class FormLilian : Form
    {
        public FormLilian()
        {
            InitializeComponent();
        }
        bool menuExpand = false;

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void FormLilian_Load(object sender, EventArgs e)
        {
            if (menuExpand == false)
            {
                menuContainer.Height += 10;
                if (menuContainer.Height >= 171)
                {
                    menuTransition.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                menuContainer.Height -= 10;
                if (menuContainer.Height <= 57)
                {
                    menuTransition.Stop();
                    menuExpand = false;
                }
            }
        }

        private void menu_Click(object sender, EventArgs e)
        {
            menuTransition.Start();
        }

        bool sidebarExpand = true;
        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= 50)
                {
                    sidebarTransition.Stop();
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                }
                else
                {
                    sidebar.Width += 10;
                    if (sidebar.Width >= 250)
                    {
                        sidebarExpand = true;
                        sidebarTransition.Stop();
                    }
                }
            }
        }

        private void btnM_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }
    }
}