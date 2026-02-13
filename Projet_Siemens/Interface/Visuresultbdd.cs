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
using Renci.SshNet;
using Renci.SshNet.Common;
using System.Net;
using System.Net.NetworkInformation;

using Oracle.ManagedDataAccess.Client;
using Microsoft.Msagl.Layout.Layered;
using Projet_Siemens.BDD;

namespace Projet_Siemens.Interface
{
    public partial class Visuresultbdd : Form
    {
       
        DatatbaseHelper helper;

        connection con2 = new Connection("10.16.140.39", "DataBaseName","DataBaseUsername","DatabasePassword");
        ServeurConnectionInfo info2 = new ServerConnectionInfo("SSHHostname",21098, "SSHUsername", "SSHPassword",30);
        //SSH port(de base port bdd = 1521 aucun rapport avec ssh) & time out 


        public Visuresultbdd()
        {
            
            InitializeComponent();
            helper = new DatatbaseHelper(con, info);
        }
    }
}

