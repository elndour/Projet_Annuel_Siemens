using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client; // Bibliothèque Oracle
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace Projet_Siemens.BDD
{
    class DatabaseHelper
    {
        private Connection connectionInfo;

        public DatabaseHelper(Connection connectionInfo)
        {
            this.connectionInfo = connectionInfo;
        }

        public bool TestConnection()
        {
            try
            {

                using (OracleConnection con = new OracleConnection(connectionInfo.GetOracleConnectionString()))
                {
                    con.Open();

                    // ➤ Contournement : définir un identifiant de session "SQLDeveloper"
                    OracleCommand cmd = con.CreateCommand();
                    cmd.CommandText = "BEGIN DBMS_SESSION.SET_IDENTIFIER('SQLDeveloper'); END;";
                    cmd.ExecuteNonQuery();

                    con.Close();
                }

                return true;
            }
            catch (OracleException ex)
            {
                MessageBox.Show($"OracleException: {ex.Message}", "Erreur Oracle");
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Exception générale: {e.Message}", "Erreur .NET");
                return false;
            }
        }

        public bool ExecuteSqlFileInBlocks(string filePath)
        {
            try
            {
                string fullScript = File.ReadAllText(filePath);

                // Séparation en blocs SQL selon les fins d'instructions terminées par un point-virgule suivi d'un retour à la ligne
                string[] blocks = Regex.Split(fullScript, @"(?<=;)\s*\r?\n", RegexOptions.Multiline);

                using (OracleConnection con = new OracleConnection(GetConnectionString()))
                {
                    con.Open();

                    foreach (string rawBlock in blocks)
                    {
                        string block = rawBlock.Trim();

                        if (string.IsNullOrWhiteSpace(block))
                            continue;

                        using (OracleCommand cmd = new OracleCommand(block, con))
                        {
                            try
                            {
                                cmd.ExecuteNonQuery();
                            }
                            catch (OracleException ex)
                            {
                                ShowErrMessage($"Erreur dans le bloc :\n{block}\n\n{ex.Message}", "Erreur SQL Oracle");
                                return false;
                            }
                        }
                    }

                    con.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                ShowErrMessage(ex.Message, "Erreur lors de l'exécution du script SQL");
                return false;
            }
        }

        private string GetConnectionString()
        {
            // Construit la chaîne de connexion Oracle
            return $"Data Source={connectionInfo.Server}:{connectionInfo.Port}/{connectionInfo.Database};User Id={connectionInfo.Uid};Password={connectionInfo.Password};";
        }

        // Méthodes supplémentaires pour gérer les exceptions
        public void ShowErrMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowInfoMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Getters et setters
        public Connection GetConnectionInfo() { return this.connectionInfo; }
        public void SetConnectionInfo(Connection connectionInfo) { this.connectionInfo = connectionInfo; }

        // ========== COLLECTE DE DONNÉES AVEC SAUVEGARDE JSON ==========

        /// <summary>
        /// Exécute une requête SQL ANSI et sauvegarde les résultats dans un fichier JSON
        /// </summary>
        /// <param name="sqlQuery">Requête SQL ANSI (compatible Oracle/PostgreSQL)</param>
        /// <param name="outputPath">Chemin du fichier JSON de sortie</param>
        /// <param name="queryName">Nom descriptif de la requête pour les métadonnées</param>
        /// <returns>True si succès, False sinon</returns>
        public bool ExecuteSqlQueryAndSaveJson(string sqlQuery, string outputPath, string queryName = "Query")
        {
            try
            {
                var results = new List<Dictionary<string, object>>();

                using (OracleConnection con = new OracleConnection(GetConnectionString()))
                {
                    con.Open();

                    using (OracleCommand cmd = new OracleCommand(sqlQuery, con))
                    {
                        cmd.CommandTimeout = 300; // 5 minutes timeout

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            // Lire toutes les lignes
                            while (reader.Read())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string columnName = reader.GetName(i);
                                    object value = reader.IsDBNull(i) ? null : reader.GetValue(i);

                                    // Convertir les types Oracle en types .NET standard pour JSON
                                    if (value != null)
                                    {
                                        if (value is DateTime dt)
                                            value = dt.ToString("yyyy-MM-dd HH:mm:ss");
                                        else if (value is decimal dec)
                                            value = decimal.ToDouble(dec);
                                    }

                                    row[columnName] = value;
                                }

                                results.Add(row);
                            }
                        }
                    }

                    con.Close();
                }

                // Créer l'objet JSON avec métadonnées
                var jsonOutput = new
                {
                    metadata = new
                    {
                        queryName = queryName,
                        extractionDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        database = connectionInfo.Database,
                        server = connectionInfo.Server,
                        rowCount = results.Count,
                        columnCount = results.Count > 0 ? results[0].Count : 0,
                        columns = results.Count > 0 ? results[0].Keys.ToList() : new List<string>()
                    },
                    data = results
                };

                // Créer le répertoire si nécessaire
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

                // Sauvegarder en JSON avec indentation
                string json = JsonSerializer.Serialize(jsonOutput, new JsonSerializerOptions 
                { 
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                File.WriteAllText(outputPath, json);

                ShowInfoMessage($"Données extraites avec succès !\n\nNombre de lignes : {results.Count}\nFichier : {outputPath}", "Extraction réussie");
                return true;
            }
            catch (OracleException ex)
            {
                ShowErrMessage($"Erreur Oracle lors de l'exécution de la requête :\n{ex.Message}", "Erreur SQL");
                return false;
            }
            catch (Exception ex)
            {
                ShowErrMessage($"Erreur lors de la sauvegarde des données :\n{ex.Message}", "Erreur");
                return false;
            }
        }

        /// <summary>
        /// Exécute plusieurs requêtes SQL ANSI depuis un fichier de configuration et sauvegarde chaque résultat en JSON
        /// </summary>
        /// <param name="configFilePath">Chemin du fichier de configuration contenant les requêtes</param>
        /// <param name="outputDirectory">Répertoire de sortie pour les fichiers JSON</param>
        /// <returns>Dictionnaire des résultats (nom du fichier -> succès/échec)</returns>
        public Dictionary<string, bool> ExecuteMultipleQueriesAndSave(string configFilePath, string outputDirectory)
        {
            var results = new Dictionary<string, bool>();

            try
            {
                // Lire le fichier de configuration
                string configContent = File.ReadAllText(configFilePath);

                // Parser les requêtes (format : -- QUERY_NAME: nom_fichier.json suivi de la requête SQL)
                var queries = ParseQueryConfigFile(configContent);

                Directory.CreateDirectory(outputDirectory);

                foreach (var query in queries)
                {
                    string outputPath = Path.Combine(outputDirectory, query.OutputFileName);
                    bool success = ExecuteSqlQueryAndSaveJson(query.SqlQuery, outputPath, query.QueryName);
                    results[query.OutputFileName] = success;
                }

                return results;
            }
            catch (Exception ex)
            {
                ShowErrMessage($"Erreur lors de l'exécution multiple des requêtes :\n{ex.Message}", "Erreur");
                return results;
            }
        }

        /// <summary>
        /// Parse un fichier de configuration de requêtes SQL
        /// Format attendu :
        /// -- QUERY_NAME: Production Indicators
        /// -- OUTPUT_FILE: production_indicators.json
        /// SELECT * FROM production_orders WHERE order_date >= CURRENT_DATE - INTERVAL '30' DAY;
        /// </summary>
        private List<QueryConfig> ParseQueryConfigFile(string content)
        {
            var queries = new List<QueryConfig>();
            var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            QueryConfig currentQuery = null;
            var sqlLines = new List<string>();

            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("-- QUERY_NAME:"))
                {
                    // Sauvegarder la requête précédente si elle existe
                    if (currentQuery != null && sqlLines.Count > 0)
                    {
                        currentQuery.SqlQuery = string.Join("\n", sqlLines).Trim();
                        queries.Add(currentQuery);
                        sqlLines.Clear();
                    }

                    currentQuery = new QueryConfig
                    {
                        QueryName = line.Replace("-- QUERY_NAME:", "").Trim()
                    };
                }
                else if (line.Trim().StartsWith("-- OUTPUT_FILE:"))
                {
                    if (currentQuery != null)
                    {
                        currentQuery.OutputFileName = line.Replace("-- OUTPUT_FILE:", "").Trim();
                    }
                }
                else if (!line.Trim().StartsWith("--") && !string.IsNullOrWhiteSpace(line))
                {
                    // C'est une ligne SQL
                    sqlLines.Add(line);
                }
            }

            // Ajouter la dernière requête
            if (currentQuery != null && sqlLines.Count > 0)
            {
                currentQuery.SqlQuery = string.Join("\n", sqlLines).Trim();
                queries.Add(currentQuery);
            }

            return queries;
        }

        /// <summary>
        /// Exécute un ensemble de requêtes prédéfinies pour la collecte complète des données MES
        /// </summary>
        /// <param name="databaseId">ID de la base de données (utilisé pour le nom du répertoire)</param>
        /// <param name="outputBaseDirectory">Répertoire de base (ex: "Data/")</param>
        /// <returns>Rapport de l'extraction</returns>
        public ExtractionReport ExecuteFullMESDataCollection(string databaseId, string outputBaseDirectory)
        {
            var report = new ExtractionReport
            {
                DatabaseId = databaseId,
                StartTime = DateTime.Now,
                Queries = new List<QueryResult>()
            };

            string outputDir = Path.Combine(outputBaseDirectory, databaseId, "database_results");
            Directory.CreateDirectory(outputDir);

            // Liste des requêtes ANSI SQL pour l'extraction MES
            var mesQueries = GetStandardMESQueries();

            foreach (var query in mesQueries)
            {
                var queryResult = new QueryResult
                {
                    QueryName = query.QueryName,
                    OutputFile = query.OutputFileName,
                    StartTime = DateTime.Now
                };

                try
                {
                    string outputPath = Path.Combine(outputDir, query.OutputFileName);
                    bool success = ExecuteSqlQueryAndSaveJson(query.SqlQuery, outputPath, query.QueryName);

                    queryResult.Success = success;
                    queryResult.EndTime = DateTime.Now;
                    queryResult.ErrorMessage = success ? null : "Échec de l'extraction";
                }
                catch (Exception ex)
                {
                    queryResult.Success = false;
                    queryResult.EndTime = DateTime.Now;
                    queryResult.ErrorMessage = ex.Message;
                }

                report.Queries.Add(queryResult);
            }

            report.EndTime = DateTime.Now;
            report.TotalQueries = report.Queries.Count;
            report.SuccessfulQueries = report.Queries.Count(q => q.Success);
            report.FailedQueries = report.Queries.Count(q => !q.Success);

            // Sauvegarder le rapport d'extraction
            SaveExtractionReport(report, Path.Combine(outputDir, "extraction_report.json"));

            return report;
        }

        /// <summary>
        /// Retourne la liste des requêtes ANSI SQL standard pour l'extraction MES
        /// Ces requêtes sont compatibles Oracle et PostgreSQL
        /// </summary>
        private List<QueryConfig> GetStandardMESQueries()
        {
            return new List<QueryConfig>
            {
                // ANSI SQL - Compatible Oracle et PostgreSQL
                new QueryConfig
                {
                    QueryName = "Production Orders",
                    OutputFileName = "production_orders.json",
                    SqlQuery = @"
                        SELECT 
                            order_id,
                            product_id,
                            quantity_planned,
                            quantity_produced,
                            start_time,
                            end_time,
                            status
                        FROM production_orders
                        WHERE start_time >= CURRENT_DATE - 30
                        ORDER BY start_time DESC"
                },
                new QueryConfig
                {
                    QueryName = "Error Logs",
                    OutputFileName = "error_logs.json",
                    SqlQuery = @"
                        SELECT 
                            error_id,
                            error_code,
                            error_message,
                            error_timestamp,
                            module_name,
                            severity_level
                        FROM error_log
                        WHERE severity_level IN ('CRITICAL', 'ERROR', 'WARNING')
                        ORDER BY error_timestamp DESC
                        FETCH FIRST 1000 ROWS ONLY"
                },
                new QueryConfig
                {
                    QueryName = "Task Status",
                    OutputFileName = "task_status.json",
                    SqlQuery = @"
                        SELECT 
                            task_id,
                            task_name,
                            task_status,
                            assigned_machine,
                            start_time,
                            completion_percentage
                        FROM mes_tasks
                        WHERE task_status IN ('RUNNING', 'ERROR', 'PENDING')
                        ORDER BY start_time DESC"
                },
                new QueryConfig
                {
                    QueryName = "Machine Statistics",
                    OutputFileName = "machine_statistics.json",
                    SqlQuery = @"
                        SELECT 
                            machine_id,
                            COUNT(*) as total_operations,
                            AVG(processing_time) as avg_processing_time,
                            SUM(CASE WHEN status = 'SUCCESS' THEN 1 ELSE 0 END) as success_count,
                            SUM(CASE WHEN status = 'FAILED' THEN 1 ELSE 0 END) as failed_count
                        FROM operations_log
                        WHERE operation_date >= CURRENT_DATE - 7
                        GROUP BY machine_id
                        ORDER BY machine_id"
                }
            };
        }

        /// <summary>
        /// Sauvegarde le rapport d'extraction en JSON
        /// </summary>
        private void SaveExtractionReport(ExtractionReport report, string outputPath)
        {
            try
            {
                string json = JsonSerializer.Serialize(report, new JsonSerializerOptions 
                { 
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                File.WriteAllText(outputPath, json);
            }
            catch (Exception ex)
            {
                ShowErrMessage($"Erreur lors de la sauvegarde du rapport :\n{ex.Message}", "Erreur");
            }
        }
    }

    // ========== CLASSES DE CONFIGURATION ET RAPPORTS ==========

    public class QueryConfig
    {
        public string QueryName { get; set; }
        public string OutputFileName { get; set; }
        public string SqlQuery { get; set; }
    }

    public class QueryResult
    {
        public string QueryName { get; set; }
        public string OutputFile { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public TimeSpan Duration => EndTime - StartTime;
    }

    public class ExtractionReport
    {
        public string DatabaseId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalQueries { get; set; }
        public int SuccessfulQueries { get; set; }
        public int FailedQueries { get; set; }
        public List<QueryResult> Queries { get; set; }
        public TimeSpan TotalDuration => EndTime - StartTime;
    }

    [Serializable]
    public class DatabaseHelperException : Exception
    {
        public DatabaseHelperException(string message) : base(message) { }
        protected DatabaseHelperException(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt) { }
    }
}
