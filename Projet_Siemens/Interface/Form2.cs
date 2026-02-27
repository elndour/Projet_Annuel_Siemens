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

            // Add event handler for edge/node clicks
            viewer.Click += Viewer_Click;

            // Add drag & drop event handlers
            viewerPanel.DragEnter += ViewerPanel_DragEnter;
            viewerPanel.DragDrop += ViewerPanel_DragDrop;

            viewerPanel.Controls.Add(viewer);
            form2MainPanel.Controls.Add(viewerPanel);

            // Reconstruction du graphe complet
            AddAllNodesToGraph(network.machines);
            AddAllEdgesToGraph(network.listOfEdges);

            viewer.Graph = graph;
            viewer.Refresh();
        }

        private void Viewer_Click(object sender, EventArgs e)
        {
            if (viewer.SelectedObject is Microsoft.Msagl.Drawing.Edge selectedEdge)
            {
                // Retrieve the Edge object stored in UserData
                if (selectedEdge.UserData is Edge edge)
                {
                    string details = $"📊 Configuration de la connexion\n\n" +
                                   $"Source: {edge.machineSource.id} ({edge.ipSource}:{edge.portSource})\n" +
                                   $"Cible: {edge.machineTarget.id} ({edge.ipTarget}:{edge.portTarget})\n\n" +
                                   $"Protocole: {edge.protocol}\n" +
                                   $"Direction: {(edge.birectional ? "Bidirectionnelle ↔" : "Unidirectionnelle →")}\n" +
                                   $"Firewall: {(edge.firewall ? "OUI ⚠️" : "NON ✓")}\n";

                    MessageBox.Show(details, "Détails de la connexion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

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

            // Add label with configuration details
            string firewallText = edge.firewall ? " [FW]" : "";
            string directionText = edge.birectional ? "↔" : "→";
            msaglEdge.LabelText = $"{edge.protocol} :{edge.portTarget}{firewallText} {directionText}";

            // Optional: Store the Edge object in UserData for later access
            msaglEdge.UserData = edge;

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

        private void edgeButton_Click(object sender, EventArgs e)
        {
            loadform(new FormEdge(this));
        }

        private void subNetworkButton_Click(object sender, EventArgs e)
        {
            loadform(new FormSubNetwork(this));
        }

        public void RefreshGraphWithSubNetworks()
        {
            // Clear the current graph
            graph = new Graph("NetworkGraph");

            // If there are subnets, create subgraphs (clusters)
            if (network.subNetworks.Count > 0)
            {
                foreach (var subnet in network.subNetworks)
                {
                    // Create a subgraph for each subnet
                    Subgraph subgraph = new Subgraph(subnet.id);
                    subgraph.Label = new Microsoft.Msagl.Drawing.Label($"{subnet.name}\n{subnet.subnet}");
                    subgraph.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightGray;
                    subgraph.Attr.Color = Microsoft.Msagl.Drawing.Color.DarkBlue;
                    subgraph.Attr.LineWidth = 2;

                    // Add machines that belong to this subnet
                    foreach (var machine in subnet.machines)
                    {
                        Node node = graph.AddNode(machine.id);
                        SetNodeColor(node, machine);
                        subgraph.AddNode(node);
                    }

                    graph.RootSubgraph.AddSubgraph(subgraph);
                }

                // Add machines that don't belong to any subnet
                foreach (var machine in network.machines)
                {
                    var subnet = network.GetSubNetworkByMachine(machine);
                    if (subnet == null)
                    {
                        Node node = graph.AddNode(machine.id);
                        SetNodeColor(node, machine);
                    }
                }
            }
            else
            {
                // No subnets, just add all machines normally
                AddAllNodesToGraph(network.machines);
            }

            // Add all edges
            AddAllEdgesToGraph(network.listOfEdges);

            // Refresh the viewer
            viewer.Graph = graph;
            viewer.Refresh();
        }

        private void SetNodeColor(Node node, Machine machine)
        {
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
                    node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Gray;
                    break;
            }
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

        // ========== DRAG & DROP FUNCTIONALITY ==========

        private void DragButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Button btn)
            {
                // Start drag operation with the button's tag (machine type)
                btn.DoDragDrop(btn.Tag.ToString(), DragDropEffects.Copy);
            }
        }

        private void ViewerPanel_DragEnter(object sender, DragEventArgs e)
        {
            // Allow drop if data is text (machine type)
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void ViewerPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string machineType = e.Data.GetData(DataFormats.Text).ToString();

                // Create a unique ID for the new machine
                string uniqueId = $"{machineType.Replace(" ", "")}-{DateTime.Now:HHmmss}";

                // Prompt user for basic information
                string machineId = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Enter ID for the new {machineType}:", 
                    "Machine ID", 
                    uniqueId);

                if (string.IsNullOrWhiteSpace(machineId))
                    return;

                // Check if machine with same ID already exists
                if (network.machines.Any(m => m.id.Equals(machineId)))
                {
                    MessageBox.Show("A machine with this ID already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string ip = Microsoft.VisualBasic.Interaction.InputBox(
                    "Enter IP address:", 
                    "IP Address", 
                    "192.168.1.1");

                if (string.IsNullOrWhiteSpace(ip))
                    return;

                // Create the appropriate machine type
                Machine newMachine = null;

                switch (machineType)
                {
                    case "Database":
                        string username = Microsoft.VisualBasic.Interaction.InputBox(
                            "Enter username:", 
                            "Username", 
                            "admin");
                        string password = Microsoft.VisualBasic.Interaction.InputBox(
                            "Enter password:", 
                            "Password", 
                            "password");
                        string sshPortStr = Microsoft.VisualBasic.Interaction.InputBox(
                            "Enter SSH port:", 
                            "SSH Port", 
                            "22");

                        if (int.TryParse(sshPortStr, out int sshPort))
                        {
                            // Constructor: DataBase(string id, string ip, int sshPort, string password, string username, string type)
                            newMachine = new DataBase(machineId, ip, sshPort, password, username, "DataBase");
                        }
                        else
                        {
                            MessageBox.Show("Invalid SSH port!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;

                    case "App Server":
                        string appDescription = Microsoft.VisualBasic.Interaction.InputBox(
                            "Enter description:", 
                            "Description", 
                            "Application Server");
                        string repository = Microsoft.VisualBasic.Interaction.InputBox(
                            "Enter repository path:", 
                            "Repository", 
                            "/opt/app");
                        string servicePortStr = Microsoft.VisualBasic.Interaction.InputBox(
                            "Enter service port:", 
                            "Service Port", 
                            "8080");

                        if (int.TryParse(servicePortStr, out int servicePort))
                        {
                            // Constructor: ApplicationServer(string ip, int servicePort, string description, string id, string type, string repository)
                            newMachine = new ApplicationServer(ip, servicePort, appDescription, machineId, "ApplicationServer", repository);
                        }
                        else
                        {
                            MessageBox.Show("Invalid service port!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;

                    case "Web Server":
                        string endPoints = Microsoft.VisualBasic.Interaction.InputBox(
                            "Enter endpoints:", 
                            "Endpoints", 
                            "/api/v1");
                        string api = Microsoft.VisualBasic.Interaction.InputBox(
                            "Enter API path:", 
                            "API", 
                            "/api");
                        string webRepository = Microsoft.VisualBasic.Interaction.InputBox(
                            "Enter repository:", 
                            "Repository", 
                            "/var/www");

                        // Constructor: WebServer(string id, string ip, string endPoints, string api, string type, string repository)
                        newMachine = new WebServer(machineId, ip, endPoints, api, "WebServer", webRepository);
                        break;

                    case "Pres Server":
                        string url = Microsoft.VisualBasic.Interaction.InputBox(
                            "Enter URL:", 
                            "URL", 
                            "http://localhost");
                        string presServicePortStr = Microsoft.VisualBasic.Interaction.InputBox(
                            "Enter service port:", 
                            "Service Port", 
                            "3389");
                        string presRepository = Microsoft.VisualBasic.Interaction.InputBox(
                            "Enter repository:", 
                            "Repository", 
                            "/opt/pres");

                        if (int.TryParse(presServicePortStr, out int presServicePort))
                        {
                            // Constructor: Presentation_Server(string id, string ip, int servicePort, string URL, string type, string repository)
                            newMachine = new Presentation_Server(machineId, ip, presServicePort, url, "PresentationServer", presRepository);
                        }
                        else
                        {
                            MessageBox.Show("Invalid service port!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        break;
                }

                if (newMachine != null)
                {
                    // Add to network
                    addMachineToNetwork(newMachine);
                    addMachineToListOfMachines(newMachine);

                    // Add to graph
                    AddNodeToGraph(newMachine);

                    MessageBox.Show($"{machineType} '{machineId}' created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
