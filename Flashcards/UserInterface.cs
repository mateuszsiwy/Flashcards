using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
namespace Flashcards
{
    public class App
    {
        static public void StartApp()
        {
            string menuText = "0 - Exit\n" +
                "1 - Manage stacks\n" +
                "2 - Manage flashcards\n" +
                "3 - Study\n" +
                "4 - View study session data";

            var panel = new Panel(menuText);
            panel.Padding = new Padding(1, 1, 1, 1);
            AnsiConsole.Write(panel);
        }
    }
}
