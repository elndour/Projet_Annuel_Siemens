using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Siemens.Class
{
    public class WebServer : Machine
    {
        public string endPoints { get; set; } 
        public string api { get; set; }

        public string repository { get; set; }

        public WebServer(string id, string ip, string endPoints, string api, string type, string repository) : base(ip, id, type)
        {
            this.endPoints = endPoints;
            this.api = api;
            this.repository = repository;
        }

        //public string getEndPoints() 
        //{
        //    return endPoints;
        //}
        //public string getApi()
        //{
        //    return api;
        //}

        //public void setEndPoints(string endPoints)
        //{
        //    this.endPoints = endPoints;
        //}

        //public void setApi(string api)
        //{
        //    this.api = api;
        //}
    }
}
