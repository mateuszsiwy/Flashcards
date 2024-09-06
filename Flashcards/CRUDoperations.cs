using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using System.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;

public class FlashcardsService
{
    private string? connectionString;
    public FlashcardsService()
    {
        connectionString = ConfigurationManager.AppSettings.Get("connectionString");

    }

    public List<string> GetStackNames()
    {
        try
        {
            using(var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string namesCommand = @"SELECT StackName FROM stacks";
                List<string> stackNames = connection.Query<string>(namesCommand).ToList();
                return stackNames;
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
            return null;
        }
    }

    public void CreateStack(string newStackName)
    {
        try
        {
            using(var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                string createStackCommand = @"INSERT INTO stacks (StackName) VALUES(@StackName)";
                var parameters = new { StackName = newStackName };
                connection.Execute(createStackCommand, parameters);
            }
        }
        catch(Exception ex)
        {
            Console.Write(ex.ToString());
        }
    }

}