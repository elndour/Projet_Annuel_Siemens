using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Projet_Siemens.Class
{
    public class Machine
    {
        public string ip { get; set; }
        public string id { get; set; }
        public string type { get; set; }

        public string displayName { get; set; }

        public Machine(string ip, string id, string type)
        {
            this.ip = ip;
            this.id = id;
            this.type = type;
            displayName = type + " : " + id;
        }
        //public string getId()
        //{
         //   return id;
        //}
        //public string getIP()
        //{
        //    return ip;
        //}
        //public string getType()
        //{
        //    return type;
        //}

        // Propriétés publiques pour la sérialisation JSON
       // [JsonIgnore] // Ignore les champs privés
        //public string ID => getId();

        //[JsonIgnore]
        //public string IP => getIP();

       // [JsonIgnore]
        //public string Type => getType();
    }
}
