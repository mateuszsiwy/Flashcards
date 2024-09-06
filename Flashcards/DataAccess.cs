using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Flashcards
{
    internal class DataAccess
    {
        private string? connectionString;
        public DataAccess() 
        {
            connectionString = ConfigurationManager.AppSettings.Get("connectionString");

        }

        public void CreateTables()
        {
            try
            {
                using (var connection =  new SqlConnection(connectionString))
                {
                    connection.Open();

                    string createStackTable = @"IF NOT EXISTS" + "(SELECT * FROM sysobjects WHERE name = 'stacks') BEGIN " +
                        "CREATE TABLE stacks (" +
                        "StackId INT PRIMARY KEY IDENTITY(1,1)," +
                        "StackName NVARCHAR(50))" +
                        "END";

                    string createFlashcardTable = @"IF NOT EXISTS" + "(SELECT * FROM sysobjects WHERE name = 'flashcards') BEGIN " +
                        "CREATE TABLE flashcards (" +
                        "FlashcardId INT PRIMARY KEY IDENTITY(1,1)," +
                        "Front NVARCHAR(100)," +
                        "Back NVARCHAR(100)," +
                        "StackId INT," +
                        "FOREIGN KEY (StackId) REFERENCES stacks(StackId)) " +
                        "END";
                    connection.Execute(createStackTable);
                    connection.Execute(createFlashcardTable);
                }
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error creating the tables: {ex.Message}");
            }
        }
    }
}
