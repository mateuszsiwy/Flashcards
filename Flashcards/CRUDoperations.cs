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

    public List<FlashcardDTO> GetFlashcards(int? amount, string currentStack)
    {
        try
        {
            using(var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (amount.HasValue)
                {
                    string getFlashcards = $@"SELECT TOP {amount} f.FlashcardId, f.Front, f.Back FROM flashcards f " +
                       "INNER JOIN stacks s ON f.StackId = s.StackId WHERE s.StackName = @StackName";
                    var parameters = new { StackName = currentStack };
                    List<FlashcardDTO> flashcards = connection.Query<FlashcardDTO>(getFlashcards, parameters).ToList();
                    return flashcards;
                }
                else
                {
                    string getFlashcards = @"SELECT f.FlashcardId, f.Front, f.Back FROM flashcards f " + 
                       "INNER JOIN stacks s ON f.StackId = s.StackId WHERE s.StackName = @StackName";
                    var parameters = new { StackName = currentStack };
                    List<FlashcardDTO> flashcards = connection.Query<FlashcardDTO>(getFlashcards, parameters).ToList();
                    return flashcards;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
            return null;
        }
    }

    public void AddFlashcard(string front, string back, string StackName)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string getStackId = @"SELECT StackId from stacks WHERE StackName = @stackName";
                int? id = connection.QuerySingleOrDefault<int?>(getStackId, new { StackName = StackName });

                string insertFlashcard = @"INSERT into flashcards (Front, Back, StackId) VALUES(@Front, @Back, @StackId)";
                var parameters = new { Front = front, Back = back, StackId = id };
                connection.Execute(insertFlashcard, parameters);
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
            
        }
    }

    public void EditFlashcard(string front, string back, string id)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
  
                string updateFlashcard = @"UPDATE flashcards SET Front = @Front, Back = @Back WHERE FlashcardId = @FlashcardId ";
                var parameters = new { Front = front, Back = back, FlashcardId = id };
                connection.Execute(updateFlashcard, parameters);
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());

        }
    }

}