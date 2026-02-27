using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Siemens.Class
{
    public class SubNetwork
    {
        public string id { get; set; }
        public string name { get; set; }
        public string subnet { get; set; } // Format: 192.168.1.0/24
        public string vlan { get; set; }
        public string description { get; set; }
        public List<Machine> machines { get; set; }

        public SubNetwork(string id, string name, string subnet, string vlan, string description)
        {
            this.id = id;
            this.name = name;
            this.subnet = subnet;
            this.vlan = vlan;
            this.description = description;
            this.machines = new List<Machine>();
        }

        public void AddMachine(Machine machine)
        {
            if (!machines.Contains(machine))
            {
                machines.Add(machine);
            }
        }

        public void RemoveMachine(Machine machine)
        {
            machines.Remove(machine);
        }

        public bool ContainsMachine(Machine machine)
        {
            return machines.Contains(machine);
        }

        public override string ToString()
        {
            return $"{name} ({subnet}) - {machines.Count} machines";
        }
    }
}
