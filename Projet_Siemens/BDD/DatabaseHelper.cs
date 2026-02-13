using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client; // Bibliothèque Oracle
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.IO;

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
    }

    [Serializable]
    public class DatabaseHelperException : Exception
    {
        public DatabaseHelperException(string message) : base(message) { }
        protected DatabaseHelperException(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt) { }
    }
}
