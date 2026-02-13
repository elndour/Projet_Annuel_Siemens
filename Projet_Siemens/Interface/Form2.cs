using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Projet_Siemens.Class;
using Projet_Siemens.Interface;
using Edge = Projet_Siemens.Class.Edge;

namespace Projet_Siemens
{
    public partial class Form2 : Form
    {
        public GViewer viewer { get; set; }
        public Graph graph { get; set; }
        public Network network { get; set; }
        public Form2(Network sharedNetwork)
        {
            InitializeComponent();
            network = sharedNetwork;

            // Initialiser MSAGL
            graph = new Graph("NetworkGraph");
            viewer = new GViewer();
            viewer.Dock = DockStyle.Fill;
            viewerPanel.Controls.Add(viewer);
            form2MainPanel.Controls.Add(viewerPanel);

            // Reconstruction du graphe complet
            AddAllNodesToGraph(network.machines);
            AddAllEdgesToGraph(network.listOfEdges);

            viewer.Graph = graph;
            viewer.Refresh();
        }

        //public Network getNetwork()
        //{
        //    return network;
        //}
        public void loadform(object Form)
        {
            //if (this.form2MainPanel.Controls.Count > 0)
            //this.form2MainPanel.Controls.RemoveAt(0);
            foreach (Control control in form2MainPanel.Controls)
            {
                control.Visible = false;
            }
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.form2MainPanel.Controls.Add(f);
            this.form2MainPanel.Tag = f;
            f.Show();
        }

        private void dataBaseButton_Click(object sender, EventArgs e)
        {
            loadform(new FormDatabase(this));
        }

        public void AddNodeToGraph(Machine machine)
        {
            Node node = graph.AddNode(machine.id);

            // Attribution d'une couleur par type de machine
            switch (machine.type)
            {
                case "PresentationServer":
                    node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightBlue;
                    break;
                case "MachineServer":
                    node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightGreen;
                    break;
                case "WebServer":
                    node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Orange;
                    break;
                case "DataBase":
                    node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Red;
                    break;
                case "ApplicationServer":
                    node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Yellow;
                    break;
                default:
                    node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Gray; // Couleur par défaut
                    break;
            }

            viewer.Graph = graph;
            viewer.Refresh();
        }

        public void AddAllNodesToGraph(List<Machine> machines)
        {
            foreach (var machine in machines)
            {
                AddNodeToGraph(machine);
            }
        }


        public void addEdgeToGraph(Edge edge)
        {
            // Add the edge directly to the graph and get the MSAGL Edge object
            Microsoft.Msagl.Drawing.Edge msaglEdge = graph.AddEdge(edge.machineSource.id, edge.machineTarget.id);

            // Set color based on firewall presence
            msaglEdge.Attr.Color = edge.firewall ? Microsoft.Msagl.Drawing.Color.Red : Microsoft.Msagl.Drawing.Color.Black;

            // If the edge is bidirectional, add an arrow at the source as well
            if (edge.birectional)
            {
                msaglEdge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.Normal; // Add an arrow at the source
            }

            // Refresh the viewer
            viewer.Graph = graph;
            viewer.Refresh();
        }

        public void AddAllEdgesToGraph(List<Edge> edges)
        {
            foreach (var edge in edges)
            {
                addEdgeToGraph(edge); // Pareil, tu réutilises ta méthode
            }
        }

        public void setVisible()
        {
            foreach (Control control in form2MainPanel.Controls)
            {
                control.Visible = true;
            }
        }

        public void addMachineToNetwork(Machine machine)
        {
            if (machine is DataBase)
            {
                network.addDataBase(machine as DataBase);
            }
            else if (machine is ApplicationServer)
            {
                network.addApplicationServer(machine as ApplicationServer);
            }
            else if (machine is Presentation_Server)
            {
                network.addPresentationServer(machine as Presentation_Server);
            }
            else if (machine is WebServer)
            {
                network.addWebServer(machine as WebServer);
            }
        }

        public void addMachineToListOfMachines(Machine machine)
        {
            network.addMachine(machine);
        }


        private void machineButton_Click(object sender, EventArgs e)
        {
            serverButtonPanel.Visible = true;
        }

        private void appServerButton_Click(object sender, EventArgs e)
        {
            loadform(new FormApplicationServer(this));
            serverButtonPanel.Visible = false;
        }

        private void webSerButton_Click(object sender, EventArgs e)
        {
            loadform(new FormWebServer(this));
            serverButtonPanel.Visible = false;
        }

        private void presServButton_Click(object sender, EventArgs e)
        {
            loadform(new FormPresentationServ(this));
            serverButtonPanel.Visible = false;
        }

        private void edgeButton_Click(object sender, EventArgs e)
        {
            loadform(new FormEdge(this));
        }

        private void buttonConfirmJson_Click(object sender, EventArgs e)
        {
            network.SaveMachinesToJson();
        }

        private void import_Click(object sender, EventArgs e)
        {
            // Charger les données depuis le fichier JSON
            network.LoadMachinesFromJson();

            // Ajouter toutes les machines du réseau dans le graphe
            foreach (Machine machine in network.machines)
            {
                AddNodeToGraph(machine);
            }

            // Ajouter les connexions entre machines (arêtes) dans le graphe
            foreach (Edge edge in network.listOfEdges)
            {
                addEdgeToGraph(edge);
            }

            MessageBox.Show("Importation terminée et graph mis à jour !");
        }

        private void extractFileButton_Click(object sender, EventArgs e)

        {
            //we want to verify if the list of machines is not empty
            if(network.machines.Count == 0)
            {
                MessageBox.Show("Please add machines to the network before extracting the file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            loadform(new FormFileExtraction(this));
        }
    }
}
