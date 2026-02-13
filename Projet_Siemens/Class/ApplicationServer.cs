using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Siemens.Class
{
    public class ApplicationServer : Machine
    {
        public int servicePort { get; set; }
        public string description { get; set; }
        public string repository { get; set;}

        public ApplicationServer(string ip, int servicePort, string description, string id, string type, string repository) : base(ip, id, type)
        {
            this.servicePort = servicePort;
            this.description = description;
            this.repository = repository;
        }
    }
}
