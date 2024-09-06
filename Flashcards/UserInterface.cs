using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;

namespace Flashcards
{
    public class App
    {
        static public void StartApp()
        {
            while (true)
            {
                string menuText = "0 - Exit\n" +
                    "1 - Manage stacks\n" +
                    "2 - Manage flashcards\n" +
                    "3 - Study\n" +
                    "4 - View study session data";

                var panel = new Panel(menuText);
                panel.Padding = new Padding(1, 1, 1, 1);
                AnsiConsole.Write(panel);
                ProcessMenuInput();
            }
        }

        static public void ProcessMenuInput()
        {
            
            string option = Console.ReadLine();
            switch (option)
            {
                case "0":
                    
                    break;
                case "1":
                    ManageStacks();
                    break;
                case "2":
                    ManageFlashcards();
                    break;
                case "3":

                    break;
                case "4":

                    break;
                default:
                    Console.WriteLine("Please enter a valid option!");
                    ProcessMenuInput();
                    break;
            }
        }

        static public void ManageStacks()
        {
            Console.WriteLine("Input a stack name");
            FlashcardsService service = new FlashcardsService();
            List<string> stackNames = service.GetStackNames();
            var table = new Table();
            table.AddColumn("Stack name");
            foreach (string stackName in stackNames)
            {
                table.AddRow(stackName);
            }
            AnsiConsole.Write(table);
            Console.WriteLine("Press 0 - to create a new stack");
            string option = Console.ReadLine();
            if (option == "0")
            {
                Console.WriteLine("Please enter a new stack name");
                string newStackName = Console.ReadLine();
                while (stackNames.Contains(newStackName))
                {
                    Console.WriteLine("This name is already taken, enter a new one");
                    newStackName = Console.ReadLine();
                }
                service.CreateStack(newStackName);
            }
            else if (stackNames.Contains(option))
            {
                ManageFlashcards();
            }
            else
            {
                Console.WriteLine("Please enter a valid stack name or 0 to create a new one");
                
            }
            ManageStacks();
        }

        static public void ManageFlashcards()
        {

        }
    }
}
