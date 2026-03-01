using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Projet_Siemens.BDD;

namespace Projet_Siemens.Test
{
    /// <summary>
    /// Classe de test pour simuler l'extraction de données sans base de données réelle
    /// </summary>
    public class DatabaseHelperTester
    {
        /// <summary>
        /// Teste la sauvegarde JSON avec des données simulées
        /// </summary>
        public static void TestJsonSaving()
        {
            Console.WriteLine("========== TEST DE SAUVEGARDE JSON ==========");

            // Simuler des résultats de requête
            var mockData = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "order_id", 1 },
                    { "product_id", "PROD-001" },
                    { "product_name", "Widget A" },
                    { "quantity_planned", 1000 },
                    { "quantity_produced", 987 },
                    { "start_time", "2025-01-08 08:00:00" },
                    { "end_time", "2025-01-08 16:30:00" },
                    { "status", "COMPLETED" },
                    { "efficiency_rate", 98.7 }
                },
                new Dictionary<string, object>
                {
                    { "order_id", 2 },
                    { "product_id", "PROD-002" },
                    { "product_name", "Widget B" },
                    { "quantity_planned", 500 },
                    { "quantity_produced", 495 },
                    { "start_time", "2025-01-07 09:00:00" },
                    { "end_time", "2025-01-07 15:00:00" },
                    { "status", "COMPLETED" },
                    { "efficiency_rate", 99.0 }
                }
            };

            // Créer l'objet JSON avec métadonnées
            var jsonOutput = new
            {
                metadata = new
                {
                    queryName = "Production Orders Test",
                    extractionDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    database = "TEST_DB",
                    server = "localhost",
                    rowCount = mockData.Count,
                    columnCount = mockData[0].Count,
                    columns = new List<string>(mockData[0].Keys)
                },
                data = mockData
            };

            // Créer le répertoire de test
            string testDir = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName,
                "Data", "test-mock", "database_results"
            );
            Directory.CreateDirectory(testDir);

            // Sauvegarder en JSON
            string outputPath = Path.Combine(testDir, "test_production_orders.json");
            string json = JsonSerializer.Serialize(jsonOutput, new JsonSerializerOptions 
            { 
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            
            File.WriteAllText(outputPath, json);

            Console.WriteLine($"✓ Fichier JSON créé : {outputPath}");
            Console.WriteLine($"✓ Nombre de lignes : {mockData.Count}");
            Console.WriteLine($"✓ Taille du fichier : {new FileInfo(outputPath).Length} bytes");
            Console.WriteLine();
            Console.WriteLine("Contenu du fichier :");
            Console.WriteLine(json);
        }

        /// <summary>
        /// Teste le parsing du fichier de configuration SQL
        /// </summary>
        public static void TestQueryConfigParsing()
        {
            Console.WriteLine("========== TEST DU PARSING DE CONFIGURATION SQL ==========");

            string configPath = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName,
                "Data", "mes_queries_config.sql"
            );

            if (!File.Exists(configPath))
            {
                Console.WriteLine($"✗ Fichier de configuration introuvable : {configPath}");
                return;
            }

            string content = File.ReadAllText(configPath);
            Console.WriteLine($"✓ Fichier chargé : {configPath}");
            Console.WriteLine($"✓ Taille : {content.Length} caractères");
            
            // Compter les requêtes
            int queryCount = 0;
            foreach (string line in content.Split('\n'))
            {
                if (line.Trim().StartsWith("-- QUERY_NAME:"))
                {
                    queryCount++;
                    Console.WriteLine($"  {queryCount}. {line.Replace("-- QUERY_NAME:", "").Trim()}");
                }
            }

            Console.WriteLine($"\n✓ Total de requêtes trouvées : {queryCount}");
        }

        /// <summary>
        /// Simule un rapport d'extraction complet
        /// </summary>
        public static void TestExtractionReport()
        {
            Console.WriteLine("========== TEST DU RAPPORT D'EXTRACTION ==========");

            var report = new ExtractionReport
            {
                DatabaseId = "test-mock-db",
                StartTime = DateTime.Now.AddMinutes(-5),
                EndTime = DateTime.Now,
                TotalQueries = 10,
                SuccessfulQueries = 9,
                FailedQueries = 1,
                Queries = new List<QueryResult>
                {
                    new QueryResult
                    {
                        QueryName = "Production Orders",
                        OutputFile = "production_orders.json",
                        StartTime = DateTime.Now.AddMinutes(-5),
                        EndTime = DateTime.Now.AddMinutes(-4).AddSeconds(-45),
                        Success = true,
                        ErrorMessage = null
                    },
                    new QueryResult
                    {
                        QueryName = "Error Logs",
                        OutputFile = "error_logs.json",
                        StartTime = DateTime.Now.AddMinutes(-4).AddSeconds(-45),
                        EndTime = DateTime.Now.AddMinutes(-4).AddSeconds(-30),
                        Success = true,
                        ErrorMessage = null
                    },
                    new QueryResult
                    {
                        QueryName = "Invalid Table Test",
                        OutputFile = "invalid_table.json",
                        StartTime = DateTime.Now.AddMinutes(-4).AddSeconds(-30),
                        EndTime = DateTime.Now.AddMinutes(-4).AddSeconds(-25),
                        Success = false,
                        ErrorMessage = "Table 'invalid_table' does not exist"
                    }
                }
            };

            // Sauvegarder le rapport
            string testDir = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName,
                "Data", "test-mock", "database_results"
            );
            Directory.CreateDirectory(testDir);

            string reportPath = Path.Combine(testDir, "extraction_report.json");
            string json = JsonSerializer.Serialize(report, new JsonSerializerOptions 
            { 
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            
            File.WriteAllText(reportPath, json);

            Console.WriteLine($"✓ Rapport créé : {reportPath}");
            Console.WriteLine($"✓ Durée totale : {report.TotalDuration.TotalSeconds:F2} secondes");
            Console.WriteLine($"✓ Succès : {report.SuccessfulQueries}/{report.TotalQueries}");
            Console.WriteLine($"✓ Échecs : {report.FailedQueries}/{report.TotalQueries}");
            Console.WriteLine();
            Console.WriteLine("Contenu du rapport :");
            Console.WriteLine(json);
        }

        /// <summary>
        /// Exécute tous les tests
        /// </summary>
        public static void RunAllTests()
        {
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║  TEST SUITE - DATABASE HELPER & JSON EXTRACTION   ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.WriteLine();

            try
            {
                TestJsonSaving();
                Console.WriteLine();
                TestQueryConfigParsing();
                Console.WriteLine();
                TestExtractionReport();
                Console.WriteLine();
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║            ✓ TOUS LES TESTS RÉUSSIS !             ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║              ✗ ERREUR DANS LES TESTS              ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.WriteLine($"Erreur : {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
