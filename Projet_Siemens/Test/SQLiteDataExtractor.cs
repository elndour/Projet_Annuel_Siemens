using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using Projet_Siemens.BDD;

namespace Projet_Siemens.Test
{
    /// <summary>
    /// Helper pour extraire les données MES depuis la base SQLite de test
    /// Démontre l'extraction complète avec sauvegarde JSON
    /// </summary>
    public class SQLiteDataExtractor
    {
        private string connectionString;
        private string databasePath;

        public SQLiteDataExtractor(string dbPath)
        {
            databasePath = dbPath;
            connectionString = $"Data Source={dbPath}";
        }

        /// <summary>
        /// Exécute une requête SQL et sauvegarde les résultats en JSON
        /// </summary>
        public bool ExecuteQueryAndSaveJson(string sqlQuery, string outputPath, string queryName)
        {
            try
            {
                var results = new List<Dictionary<string, object>>();

                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new SqliteCommand(sqlQuery, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var row = new Dictionary<string, object>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string columnName = reader.GetName(i);
                                    object value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                    row[columnName] = value;
                                }

                                results.Add(row);
                            }
                        }
                    }

                    connection.Close();
                }

                // Créer l'objet JSON avec métadonnées
                var jsonOutput = new
                {
                    metadata = new
                    {
                        queryName = queryName,
                        extractionDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        database = "SQLite MES Test",
                        server = "Local File",
                        rowCount = results.Count,
                        columnCount = results.Count > 0 ? results[0].Count : 0,
                        columns = results.Count > 0 ? new List<string>(results[0].Keys) : new List<string>()
                    },
                    data = results
                };

                // Créer le répertoire si nécessaire
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

                // Sauvegarder en JSON
                string json = JsonSerializer.Serialize(jsonOutput, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                File.WriteAllText(outputPath, json);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'extraction :\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Exécute l'extraction complète des données MES depuis SQLite
        /// Utilise les requêtes standard (adaptées pour SQLite)
        /// </summary>
        public ExtractionReport ExecuteFullExtraction(string outputBaseDirectory)
        {
            var report = new ExtractionReport
            {
                DatabaseId = "sqlite-test-db",
                StartTime = DateTime.Now,
                Queries = new List<QueryResult>()
            };

            string outputDir = Path.Combine(outputBaseDirectory, "sqlite-test-db", "database_results");
            Directory.CreateDirectory(outputDir);

            // Requêtes adaptées pour SQLite
            var queries = GetSQLiteQueries();

            foreach (var query in queries)
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
                    bool success = ExecuteQueryAndSaveJson(query.SqlQuery, outputPath, query.QueryName);

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
            report.SuccessfulQueries = report.Queries.FindAll(q => q.Success).Count;
            report.FailedQueries = report.Queries.FindAll(q => !q.Success).Count;

            // Sauvegarder le rapport
            SaveReport(report, Path.Combine(outputDir, "extraction_report.json"));

            return report;
        }

        /// <summary>
        /// Requêtes SQL adaptées pour SQLite (sans CURRENT_DATE qui n'existe pas en SQLite)
        /// </summary>
        private List<QueryConfig> GetSQLiteQueries()
        {
            return new List<QueryConfig>
            {
                new QueryConfig
                {
                    QueryName = "Production Orders",
                    OutputFileName = "production_orders.json",
                    SqlQuery = @"
                        SELECT 
                            order_id,
                            product_id,
                            product_name,
                            quantity_planned,
                            quantity_produced,
                            start_time,
                            end_time,
                            status,
                            efficiency_rate
                        FROM production_orders
                        WHERE start_time >= date('now', '-30 days')
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
                            severity_level,
                            stack_trace
                        FROM error_log
                        WHERE severity_level IN ('CRITICAL', 'ERROR', 'WARNING')
                        ORDER BY error_timestamp DESC
                        LIMIT 1000"
                },
                new QueryConfig
                {
                    QueryName = "MES Tasks",
                    OutputFileName = "mes_tasks.json",
                    SqlQuery = @"
                        SELECT 
                            task_id,
                            task_name,
                            task_type,
                            task_status,
                            assigned_machine,
                            assigned_user,
                            start_time,
                            estimated_end_time,
                            completion_percentage,
                            priority_level
                        FROM mes_tasks
                        WHERE task_status IN ('RUNNING', 'ERROR', 'PENDING', 'PAUSED')
                        ORDER BY priority_level DESC, start_time ASC"
                },
                new QueryConfig
                {
                    QueryName = "Downtime Events",
                    OutputFileName = "downtime_events.json",
                    SqlQuery = @"
                        SELECT 
                            downtime_id,
                            machine_id,
                            machine_name,
                            downtime_reason,
                            downtime_category,
                            downtime_start,
                            downtime_end,
                            duration_minutes,
                            reported_by
                        FROM downtime_log
                        WHERE downtime_start >= date('now', '-7 days')
                        ORDER BY downtime_start DESC"
                },
                new QueryConfig
                {
                    QueryName = "Machine Statistics",
                    OutputFileName = "machine_statistics.json",
                    SqlQuery = @"
                        SELECT 
                            machine_id,
                            machine_name,
                            COUNT(*) as total_operations,
                            AVG(processing_time) as avg_processing_time,
                            MIN(processing_time) as min_processing_time,
                            MAX(processing_time) as max_processing_time,
                            SUM(CASE WHEN status = 'SUCCESS' THEN 1 ELSE 0 END) as success_count,
                            SUM(CASE WHEN status = 'FAILED' THEN 1 ELSE 0 END) as failed_count,
                            SUM(CASE WHEN status = 'WARNING' THEN 1 ELSE 0 END) as warning_count
                        FROM operations_log
                        WHERE operation_date >= date('now', '-7 days')
                        GROUP BY machine_id, machine_name
                        ORDER BY failed_count DESC, machine_id"
                },
                new QueryConfig
                {
                    QueryName = "Inventory Levels",
                    OutputFileName = "inventory_levels.json",
                    SqlQuery = @"
                        SELECT 
                            material_id,
                            material_name,
                            material_type,
                            current_quantity,
                            min_quantity,
                            max_quantity,
                            unit_of_measure,
                            last_updated,
                            warehouse_location
                        FROM inventory
                        ORDER BY material_id"
                },
                new QueryConfig
                {
                    QueryName = "Batch Production",
                    OutputFileName = "batch_production.json",
                    SqlQuery = @"
                        SELECT 
                            batch_id,
                            batch_number,
                            product_id,
                            product_name,
                            batch_start_time,
                            batch_end_time,
                            total_quantity,
                            good_quantity,
                            rejected_quantity,
                            batch_status,
                            yield_percentage
                        FROM batch_production
                        WHERE batch_start_time >= date('now', '-30 days')
                        ORDER BY batch_start_time DESC"
                }
            };
        }

        private void SaveReport(ExtractionReport report, string path)
        {
            try
            {
                string json = JsonSerializer.Serialize(report, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la sauvegarde du rapport :\n{ex.Message}", "Erreur");
            }
        }
    }
}
