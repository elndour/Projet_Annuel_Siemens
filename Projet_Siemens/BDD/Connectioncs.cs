using System;

namespace Projet_Siemens.BDD
{
    class Connection
    {
        private string server;
        public string Server
        {
            get { return server; }
            set { server = value; }
        }

        private string database;
        public string Database
        {
            get { return database; }
            set { database = value; }
        }

        private string uid;
        public string Uid
        {
            get { return uid; } // ❗Corrigé ici
            set { uid = value; } // ❗Corrigé ici
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string port;
        public string Port
        {
            get { return port; }
            set { port = value; }
        }

        public Connection(string ip, string instanceName, string username, string password, string port)
        {
            this.server = ip;
            this.database = instanceName;
            this.uid = username;
            this.password = password;
            this.port = port;
        }

        public string GetOracleConnectionString()
        {
            return
                $"User Id={uid};Password={password};" +
                $"Data Source=(DESCRIPTION=" +
                $"(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={server})(PORT={port})))" +
                $"(CONNECT_DATA=(SERVICE_NAME={database})))";
        }
    }
}
