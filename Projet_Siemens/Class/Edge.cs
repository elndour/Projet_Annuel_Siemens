using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Siemens.Class
{
    public class Edge
    {
        public Machine machineSource { get; set; }
        public Machine machineTarget { get; set; }
        public string protocol { get; set; }
        public bool birectional { get; set; }
        public int portSource { get; set; }
        public int portTarget { get; set; }
        public string ipSource { get; set; }
        public string ipTarget { get; set; }
        public bool firewall { get; set; }

        public Edge(Machine machineSource, Machine machineTarget, string protocol, bool birectional, int portSource, int portTarget, string ipSource, string ipTarget, bool firewall)
        {
            this.machineSource = machineSource;
            this.machineTarget = machineTarget;
            this.protocol = protocol;
            this.birectional = birectional;
            this.portSource = portSource;
            this.portTarget = portTarget;
            this.ipSource = ipSource;
            this.ipTarget = ipTarget;
            this.firewall = firewall;
        }

    }

}
