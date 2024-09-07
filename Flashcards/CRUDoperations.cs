using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using System.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

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

    public void DeleteFlashcard(int id, string stackName)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {

                string deleteFlashcard = @"DELETE FROM flashcards WHERE FlashcardId = @FlashcardId";
                var parameters = new { FlashcardId = id };
                connection.Execute(deleteFlashcard, parameters);
                Console.WriteLine("Flashcard deleted succesfully");
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());

        }
    }
    public void DeleteStack(string stackName)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {

                string getStackId = @"SELECT StackId from stacks WHERE StackName = @stackName";
                int? id = connection.QuerySingleOrDefault<int?>(getStackId, new { StackName = stackName });

                string deleteFlashcardsInStack = @"DELETE FROM flashcards WHERE StackId = @StackId";
                var parameters = new {StackId =  id};
                connection.Execute(deleteFlashcardsInStack, parameters);

                string deleteSessions = @"DELETE FROM sessions WHERE StackId = @StackId";
                parameters = new { StackId = id };
                connection.Execute(deleteSessions, parameters);

                string deleteStack = @"DELETE FROM stacks WHERE StackId = @StackId";
                connection.Execute(deleteStack, parameters);
                Console.WriteLine("\nStack deleted!\n");
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());

        }
    }

    public void AddSession(int score, string stackName)
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {

                string getStackId = @"SELECT StackId from stacks WHERE StackName = @stackName";
                int? id = connection.QuerySingleOrDefault<int?>(getStackId, new { StackName = stackName });

                string addSession = @"INSERT INTO sessions (Date, Score, StackId, StackName) VALUES (@Date, @Score, @StackId, @StackName)";
                var parameters = new {Date = DateTime.Now.ToString(), Score = score, StackId = id, StackName = stackName};
                connection.Execute(addSession, parameters);
                Console.WriteLine($"Added session to database");
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());

        }
    }
    public List<StudySession> GetSessions()
    {
        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string getSessions = @"SELECT * FROM sessions";
                List<StudySession> sessions = connection.Query<StudySession>(getSessions).ToList();

                return sessions;
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
            return null;
        }
    }

}