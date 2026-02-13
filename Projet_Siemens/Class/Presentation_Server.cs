using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Siemens.Class
{
    public class Presentation_Server : Machine
    {
        
        public int servicePort { get; set; }
        public string URL { get; set; }

        public string repository { get; set; }


        public Presentation_Server(string id, string ip, int servicePort, string URL, string type, string repository) : base(ip, id, type)
        {
            
            this.servicePort = servicePort;
            this.URL = URL;
            this.repository = repository;

        }
        
        //public int getServicePort()
        //{
        //    return servicePort;
        //}
        //public string getApi()
        //{
        //    return URL;
        //}

        //public void setServicePort(int servicePort)
        //{
        //    this.servicePort = servicePort;
        //}

        //public void setApi(string URL)
        //{
        //    this.URL = URL;
        //}

    }
}
