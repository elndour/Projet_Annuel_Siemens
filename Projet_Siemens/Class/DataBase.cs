using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Projet_Siemens.Class
{

    public class DataBase : Machine
    /// <summary>
    /// public class dataBase with atributes stored for a new database which is used in the network
    /// </summary>
    {

        public int sshPort { get; set; }
        public string instanceName { get; set; }
        public  string username { get; set; }
        public string password { get; set; }


        /// <summary>
        /// Constructor of the class DataBase
        /// </summary>
        /// <param id="id">Name of the database.</param>
        /// <param name="ip">IP of the database.</param>
        /// <param name="sshPort">SSH port of the datbase</param>
        public DataBase(string id, string ip, int sshPort, string password, string username, string type) : base(ip, id, type)
        {

            this.sshPort = sshPort;
            this.password = password;
            this.username = username;
        }


        /// <summary>
        /// Getters and setters of the class DataBase
        /// </summary>
        /// <returns></returns>

        //public int getPortSSH()
        //{
        //    return sshPort;
        //}



        /// <summary>
        /// Set the SSH port of the database
        /// </summary>
        /// <param name="sshPort">Put the new SSH port of the dataBase</param>
        //public void setPortSSH(int sshPort)
        //{
        //    this.sshPort = sshPort;
        //}

        //public String getPassword()
        //{
        //    return password;
        //}

        //public String getUsername()
        //{
        //    return username;
        //}
    }
}
