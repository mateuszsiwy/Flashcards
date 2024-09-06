using Spectre.Console;
using Flashcards;

using System.Configuration;

AnsiConsole.MarkupLine("[green]Welcome to Flashcards!!![/]");

var DataAccess = new DataAccess();
DataAccess.CreateTables();

App.StartApp();