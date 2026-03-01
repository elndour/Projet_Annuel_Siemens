using System;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Windows.Forms;

namespace Projet_Siemens.Test
{
    /// <summary>
    /// Classe pour créer et initialiser une base de données SQLite de test
    /// Permet de tester la collecte de données SANS installer Oracle
    /// </summary>
    public class SQLiteTestDatabaseCreator
    {
        private string databasePath;
        private string connectionString;

        /// <summary>
        /// Crée une base de données SQLite MES complète pour les tests
        /// </summary>
        /// <param name="dbFileName">Nom du fichier de base de données (ex: "mes_test.db")</param>
        /// <returns>Chemin complet vers la base de données créée</returns>
        public string CreateTestDatabase(string dbFileName = "mes_test.db")
        {
            try
            {
                // Définir le chemin de la base de données dans le dossier Data
                string dataFolder = Path.Combine(
                    Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName,
                    "Data"
                );
                Directory.CreateDirectory(dataFolder);

                databasePath = Path.Combine(dataFolder, dbFileName);
                connectionString = $"Data Source={databasePath}";

                // Supprimer la base existante si elle existe
                if (File.Exists(databasePath))
                {
                    File.Delete(databasePath);
                }

                // Créer la base de données (Microsoft.Data.Sqlite crée automatiquement le fichier)
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                }

                // Lire et exécuter le script SQL
                string sqlScriptPath = Path.Combine(dataFolder, "create_sqlite_test_db.sql");
                
                if (!File.Exists(sqlScriptPath))
                {
                    MessageBox.Show($"Fichier SQL introuvable : {sqlScriptPath}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                string sqlScript = File.ReadAllText(sqlScriptPath);

                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    // Exécuter le script
                    using (var command = new SqliteCommand(sqlScript, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }

                MessageBox.Show(
                    $"Base de données SQLite créée avec succès !\n\n" +
                    $"Emplacement : {databasePath}\n" +
                    $"Taille : {new FileInfo(databasePath).Length / 1024} KB\n\n" +
                    "Vous pouvez maintenant tester l'extraction de données avec cette base locale !",
                    "Base de données créée",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return databasePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la création de la base SQLite :\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Teste la connexion à la base SQLite
        /// </summary>
        public bool TestConnection()
        {
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    // Compter les lignes dans une table
                    using (var command = new SqliteCommand("SELECT COUNT(*) FROM production_orders", connection))
                    {
                        var count = command.ExecuteScalar();
                        MessageBox.Show($"Connexion réussie !\n\nNombre d'ordres de production : {count}", "Test de connexion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion :\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Retourne les statistiques de la base de données
        /// </summary>
        public string GetDatabaseStats()
        {
            if (string.IsNullOrEmpty(connectionString))
                return "Base de données non créée.";

            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    var stats = "========== STATISTIQUES DE LA BASE ==========\n\n";

                    string[] tables = {
                        "production_orders",
                        "error_log",
                        "mes_tasks",
                        "downtime_log",
                        "operations_log",
                        "quality_control",
                        "inventory",
                        "user_activity_log",
                        "system_health_indicators",
                        "batch_production"
                    };

                    foreach (var table in tables)
                    {
                        using (var command = new SqliteCommand($"SELECT COUNT(*) FROM {table}", connection))
                        {
                            var count = command.ExecuteScalar();
                            stats += $"{table,-30} : {count,5} lignes\n";
                        }
                    }

                    connection.Close();

                    stats += $"\nFichier : {databasePath}";
                    stats += $"\nTaille  : {new FileInfo(databasePath).Length / 1024} KB";

                    return stats;
                }
            }
            catch (Exception ex)
            {
                return $"Erreur : {ex.Message}";
            }
        }
    }
}
