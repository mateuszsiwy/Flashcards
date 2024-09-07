using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using Spectre.Console;

namespace Flashcards
{
    internal class StudySession
    {
        private static readonly FlashcardsService service = new FlashcardsService();

        public static void StartSession()
        {
            App.DisplayStackNames(service.GetStackNames());
            Console.WriteLine("Please select a Stack");
            string stackName = Console.ReadLine();
            List<FlashcardDTO> flashcards = service.GetFlashcards(null, stackName);
            int correct = 0;
            int total = 0;
            foreach (FlashcardDTO flashcard in flashcards)
            {
                Console.WriteLine(flashcard.Front);
                Console.WriteLine("Enter the back of the flashcard");
                string answer = Console.ReadLine();
                if (answer == flashcard.Back)
                {
                    Console.WriteLine("Correct answer!");
                    correct++;
                }
                else
                {
                    Console.WriteLine("Wrong answer");
                }
                total++;
                
            }
            service.AddSession(correct, stackName);
            Console.WriteLine($"Study session finished! You guessed {correct}/{total}, press any key to return");
            Console.ReadLine();

        }

        public static void DisplaySessions()
        {
            List<DataModels.StudySession> sessions = service.GetSessions();
            var table = new Table();
            table.AddColumn("Session Id");
            table.AddColumn("Date");
            table.AddColumn("Score");
            table.AddColumn("Stack Name");

            foreach (DataModels.StudySession session in sessions)
            {
                table.AddRow(session.SessionId.ToString(), session.Date, session.Score.ToString(), session.StackName);
            }
            AnsiConsole.Write(table);
            Console.WriteLine("Press any key to return");
            Console.ReadLine();
        }
    }
}
