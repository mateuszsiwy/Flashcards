using System;
using System.Collections.Generic;
using DataModels;
using Spectre.Console;

namespace Flashcards
{
    public class App
    {
        private static readonly FlashcardsService service = new FlashcardsService();

        public static void StartApp()
        {
            while (true)
            {
                DisplayMenu();
                string option = Console.ReadLine();
                ProcessMenuInput(option);
            }
        }

        private static void DisplayMenu()
        {
            string menuText = "0 - Exit\n" +
                              "1 - Manage stacks\n" +
                              "2 - Study\n" +
                              "3 - View study session data";

            var panel = new Panel(menuText) { Padding = new Padding(1, 1, 1, 1) };
            AnsiConsole.Write(panel);
        }

        private static void ProcessMenuInput(string option)
        {
            switch (option)
            {
                case "0":
                    Environment.Exit(0); // Exits the application
                    break;
                case "1":
                    ManageStacks();
                    break;
                case "2":
                    StudySession.StartSession();
                    break;
                case "3":
                    StudySession.DisplaySessions();
                    break;
                default:
                    Console.WriteLine("Please enter a valid option!");
                    break;
            }
        }

        private static void ManageStacks()
        {
            Console.WriteLine("Input a stack name");

            List<string> stackNames = service.GetStackNames();
            DisplayStackNames(stackNames);

            Console.WriteLine("Press 0 - to create a new stack");
            string option = Console.ReadLine();

            if (option == "0")
            {
                CreateNewStack(stackNames);
            }
            else if (stackNames.Contains(option))
            {
                ManageFlashcards(option);
            }
            else
            {
                Console.WriteLine("Please enter a valid stack name or 0 to create a new one");
            }
        }

        public static void DisplayStackNames(List<string> stackNames)
        {
            var table = new Table();
            table.AddColumn("Stack name");

            foreach (string stackName in stackNames)
            {
                table.AddRow(stackName);
            }

            AnsiConsole.Write(table);
        }

        private static void CreateNewStack(List<string> stackNames)
        {
            Console.WriteLine("Please enter a new stack name");
            string newStackName = Console.ReadLine();

            while (stackNames.Contains(newStackName))
            {
                Console.WriteLine("This name is already taken, enter a new one");
                newStackName = Console.ReadLine();
            }

            service.CreateStack(newStackName);
            Console.WriteLine("Stack created successfully.");
        }

        private static void ManageFlashcards(string currentStack)
        {
            while (true)
            {
                Console.WriteLine($"Current stack: {currentStack}");
                string flashcardMenu = "0 - return to main menu\n" +
                    "x - change current stack\n" +
                    "v - view all flashcards in stack\n" +
                    "a - view x amount of flashcards in stack\n" +
                    "c - create a flashcard in current stack\n" +
                    "e - edit a flashcard\n" +
                    "d - delete a flashcard\n" +
                    "s - delete current stack (all of the flashcards in the stack will disappear";

                var panel = new Panel(flashcardMenu) { Padding = new Padding(1, 1, 1, 1) };
                AnsiConsole.Write(panel);
                ProcessFlashcardsMenuInput(Console.ReadLine(), currentStack);   
            }
            
        }

        private static void ProcessFlashcardsMenuInput(string option, string currentStack)
        {
            switch (option)
            {
                case "0":
                    StartApp(); // Exits the application
                    break;
                case "x":
                    ManageStacks();
                    break;
                case "v":
                    ViewFlashcards(null, currentStack);
                    break;
                case "a":
                    ViewNumberOfFlashcards(currentStack);
                    break;
                case "c":
                    AddNewFlashcard(currentStack);
                    break;
                case "e":
                    EditFlashcard(currentStack);
                    break;
                case "d":
                    DeleteFlashcard(currentStack);
                    break;
                case "s":
                    DeleteStack(currentStack);
                    break;
                default:
                    Console.WriteLine("Please enter a valid option!");
                    break;
            }
        }

        private static void ViewFlashcards(int? amount, string currentStack)
        {
            List<FlashcardDTO> flashcards = service.GetFlashcards(amount, currentStack);
            DisplayFlashcards(flashcards);
        }

        private static void DisplayFlashcards(List<FlashcardDTO> flashcards)
        {
            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Front");
            table.AddColumn("Back");

            foreach (FlashcardDTO flashcard in flashcards)
            {
                table.AddRow(flashcard.FlashcardId.ToString(), flashcard.Front, flashcard.Back);
            }
            

            AnsiConsole.Write(table);
        }

        private static void AddNewFlashcard(string currentStack)
        {
            Console.WriteLine("Please enter the front of the flashcard");
            string front = Console.ReadLine();
            Console.WriteLine("Please enter the back of the flashcard");
            string back = Console.ReadLine();
            service.AddFlashcard(front, back, currentStack);
        }

        private static void ViewNumberOfFlashcards(string currentStack)
        {
            Console.WriteLine("Please enter the number of flashcards to be viewed");
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    int rows = int.Parse(input);
                    ViewFlashcards(rows, currentStack);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid number");
                }
            }
        }

        private static void EditFlashcard(string currentStack)
        {
            Console.WriteLine("Please enter the id of the flashcard");
            string id = Console.ReadLine();
            Console.WriteLine("Please enter the front of the flashcard");
            string front = Console.ReadLine();
            Console.WriteLine("Please enter the back of the flashcard");
            string back = Console.ReadLine();
            
            service.EditFlashcard(front, back, id);
        }

        private static void DeleteFlashcard(string currentStack)
        {
            Console.WriteLine("Please enter the id of the flashcard to be deleted");
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    int id = int.Parse(input);
                    service.DeleteFlashcard(id, currentStack);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid number");
                }
            }
        }
        private static void DeleteStack(string currentString)
        {
            service.DeleteStack(currentString);
            StartApp();
        }
    }
}
