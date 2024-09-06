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

                    break;
                case "2":
                    
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
    }
}
