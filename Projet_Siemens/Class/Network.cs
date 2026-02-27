using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projet_Siemens.Class;

namespace Projet_Siemens.Class
{
    public class Network
    {
        public List<DataBase> dataBases { get; set; }
        public  List<ApplicationServer> applicationServers { get; set; }
        public List<WebServer> webServers { get; set; }
        public List<Presentation_Server> presentationServers { get; set; }
        public List<Machine> machines { get; set; }
        public List<Edge> listOfEdges { get; set; }
        public List<SubNetwork> subNetworks { get; set; }

        public Network()
        {
            dataBases = new List<DataBase>();
            applicationServers = new List<ApplicationServer>();
            webServers = new List<WebServer>();
            presentationServers = new List<Presentation_Server>();
            listOfEdges = new List<Edge>();
            machines = new List<Machine>();
            subNetworks = new List<SubNetwork>();
        }

        public void addDataBase(DataBase dataBase)
        {
            dataBases.Add(dataBase);
        }

        public void removeDataBase(DataBase dataBase)
        {
            dataBases.Remove(dataBase);
        }

        //public List<DataBase> getDataBases()
        //{
        //    return dataBases;
        //}
        public void addApplicationServer(ApplicationServer applicationServer)
        {
            applicationServers.Add(applicationServer);
        }
        public void removeApplicationServer(ApplicationServer machineServer)
        {
            applicationServers.Remove(machineServer);
        }
        //public List<ApplicationServer> getApplicationServers()
        //{
        //    return applicationServers;
        //}
        public void addWebServer(WebServer webServer)
        {
            webServers.Add(webServer);
        }

        public void removeWebServer(WebServer webServer)
        {
            webServers.Remove(webServer);
        }

        //public List<WebServer> getWebServers()
        //{
        //    return webServers;
        //}

        public void addPresentationServer(Presentation_Server presentationServer)
        {
            presentationServers.Add(presentationServer);
        }
        public void removePresentationServer(Presentation_Server presentationServer)
        {
            presentationServers.Remove(presentationServer);
        }
        //public List<Presentation_Server> getPresentationServers()
        //{
        //    return presentationServers;
        //}
        //public List<Edge> getListOfEdges()
        //{
        //    return listOfEdges;
        //}
        public void addEdge(Edge edge)
        {
            listOfEdges.Add(edge);
        }
        public void removeEdge(Edge edge)
        {
            listOfEdges.Remove(edge);
        }

        public void addMachine(Machine machine)
        {
            machines.Add(machine);
        }

        public void removeMachine(Machine machine)
        {
            machines.Remove(machine);
        }

        // SubNetwork management methods
        public void addSubNetwork(SubNetwork subNetwork)
        {
            subNetworks.Add(subNetwork);
        }

        public void removeSubNetwork(SubNetwork subNetwork)
        {
            subNetworks.Remove(subNetwork);
        }

        public SubNetwork GetSubNetworkByMachine(Machine machine)
        {
            return subNetworks.FirstOrDefault(sn => sn.ContainsMachine(machine));
        }

        public void SaveMachinesToJson()
        {
            // Récupère le chemin du répertoire parent de bin/Debug (qui est le projet)
            string dataFolderPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "Data");

            // Vérifie si le dossier "Data" existe, sinon le crée
            if (!Directory.Exists(dataFolderPath))
            {
                Directory.CreateDirectory(dataFolderPath);
                MessageBox.Show("Dossier Data créé dans le projet !");
            }

            // Chemin du fichier JSON dans "Data"
            string jsonFilePath = Path.Combine(dataFolderPath, "network_data.json");

            // Crée un dictionnaire avec chaque liste sous une clé distincte
            var jsonData = new Dictionary<string, object>
            {
                { "DataBases", dataBases },
                { "ApplicationServers", applicationServers },
                { "WebServers", webServers },
                { "PresentationServers", presentationServers },
                { "ListOfEdges", listOfEdges }
            };

            // Sérialisation et sauvegarde du JSON
            string json = JsonSerializer.Serialize(jsonData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonFilePath, json);

            // Ouvre directement le dossier Data pour vérification
            Process.Start("explorer.exe", dataFolderPath);
        }

        public void LoadMachinesFromJson()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Sélectionner un fichier JSON",
                Filter = "Fichiers JSON (*.json)|*.json",
                InitialDirectory = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "Data")
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string jsonFilePath = openFileDialog.FileName;

                // Lire le fichier JSON
                string json = File.ReadAllText(jsonFilePath);
                var jsonData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);

                if (jsonData == null || jsonData.Count == 0)
                {
                    MessageBox.Show("Erreur : le fichier JSON est vide ou corrompu !");
                    return;
                }

                // Désérialisation de la liste des bases de données
                if (jsonData.ContainsKey("DataBases"))
                {
                    List<DataBase> loadedDataBases = JsonSerializer.Deserialize<List<DataBase>>(jsonData["DataBases"].GetRawText()) ?? new List<DataBase>();

                    foreach (var db in loadedDataBases)
                    {
                        // Ajout de la partie Machine dans la liste machines
                        Machine machine = new Machine(db.ip, db.id, db.type);
                        machines.Add(machine);

                        // Ajout de la partie DataBase dans dataBases
                        DataBase database = new DataBase(db.id, db.ip, db.sshPort, db.password, db.username, db.instanceName);
                        dataBases.Add(database);
                    }
                }

                // Désérialisation de la liste des serveurs applicatifs
                if (jsonData.ContainsKey("ApplicationServers"))
                {
                    List<ApplicationServer> loadedApplicationServers = JsonSerializer.Deserialize<List<ApplicationServer>>(jsonData["ApplicationServers"].GetRawText()) ?? new List<ApplicationServer>();

                    foreach (var appServer in loadedApplicationServers)
                    {
                        // Ajout de la partie Machine dans la liste machines
                        Machine machine = new Machine(appServer.ip, appServer.id, appServer.type);
                        machines.Add(machine);

                        // Ajout de la partie ApplicationServer dans applicationServers
                        ApplicationServer applicationServer = new ApplicationServer(appServer.ip, appServer.servicePort, appServer.description, appServer.id, appServer.type, appServer.repository);
                        applicationServers.Add(applicationServer);
                    }
                }

                // Désérialisation de la liste des serveurs web
                if (jsonData.ContainsKey("WebServers"))
                {
                    List<WebServer> loadedWebServers = JsonSerializer.Deserialize<List<WebServer>>(jsonData["WebServers"].GetRawText()) ?? new List<WebServer>();

                    foreach (var webServer in loadedWebServers)
                    {
                        // Ajout de la partie Machine dans la liste machines
                        Machine machine = new Machine(webServer.ip, webServer.id, webServer.type);
                        machines.Add(machine);

                        // Ajout de la partie WebServer dans webServers
                        WebServer webServerObj = new WebServer(webServer.id, webServer.ip, webServer.endPoints, webServer.api, webServer.type, webServer.repository);
                        webServers.Add(webServerObj);
                    }
                }

                // Désérialisation de la liste des serveurs de présentation
                if (jsonData.ContainsKey("PresentationServers"))
                {
                    List<Presentation_Server> loadedPresentationServers = JsonSerializer.Deserialize<List<Presentation_Server>>(jsonData["PresentationServers"].GetRawText()) ?? new List<Presentation_Server>();

                    foreach (var presentationServer in loadedPresentationServers)
                    {
                        // Ajout de la partie Machine dans la liste machines
                        Machine machine = new Machine(presentationServer.ip, presentationServer.id, presentationServer.type);
                        machines.Add(machine);

                        // Ajout de la partie Presentation_Server dans presentationServers
                        Presentation_Server presentationServerObj = new Presentation_Server(
                            presentationServer.id,
                            presentationServer.ip,
                            presentationServer.servicePort,
                            presentationServer.URL,
                            presentationServer.type,
                            presentationServer.repository
                        );

                        presentationServers.Add(presentationServerObj);
                    }
                }

                // Désérialisation de la liste des connexions réseau (Edges)
                if (jsonData.ContainsKey("ListOfEdges"))
                {
                    List<Edge> loadedEdges = JsonSerializer.Deserialize<List<Edge>>(jsonData["ListOfEdges"].GetRawText()) ?? new List<Edge>();

                    foreach (var edge in loadedEdges)
                    {
                        // Trouver les machines correspondantes dans la liste machines
                        Machine sourceMachine = machines.FirstOrDefault(m => m.id == edge.machineSource.id);
                        Machine targetMachine = machines.FirstOrDefault(m => m.id == edge.machineTarget.id);

                        // Si les machines n'existent pas encore, les créer
                        if (sourceMachine == null)
                        {
                            sourceMachine = new Machine(edge.ipSource, edge.machineSource.id, edge.machineSource.type);
                            machines.Add(sourceMachine);
                        }

                        if (targetMachine == null)
                        {
                            targetMachine = new Machine(edge.ipTarget, edge.machineTarget.id, edge.machineTarget.type);
                            machines.Add(targetMachine);
                        }

                        // Ajout de la partie Edge dans listOfEdges
                        Edge edgeObj = new Edge(
                            sourceMachine,
                            targetMachine,
                            edge.protocol,
                            edge.birectional,
                            edge.portSource,
                            edge.portTarget,
                            edge.ipSource,
                            edge.ipTarget,
                            edge.firewall
                        );

                        listOfEdges.Add(edgeObj);
                    }
                }

                MessageBox.Show("Données chargées avec succès !");
            }
            else
            {
                MessageBox.Show("Aucun fichier sélectionné.");
            }
        }

    }
}

