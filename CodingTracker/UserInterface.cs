using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Rendering;
using static CodingTracker.Enums;
namespace CodingTracker
{
    internal class UserInterface
    {
        public static Style MenuStyle = new Style(
        foreground: Color.Green,
        decoration: Decoration.Bold
        );

        internal void MainMenu()
        {
            TimeController.CreateDB();
            AnsiConsole.Write(new Align(
                              new Markup("Welcome to the Coding Tracker!\n", UserInterface.MenuStyle),
                              HorizontalAlignment.Center));

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuChoice>()
                .Title("[green bold]What would you like to do?[/]")
                .AddChoices(Enum.GetValues<MenuChoice>())
                );

            switch (choice)
            {
                case MenuChoice.StartTime:
                    //Start timer, make a live counter, add to DB
                    AnsiConsole.Write(new Markup("Start Time", UserInterface.MenuStyle));
                    break;

                case MenuChoice.InsertTime:
                    AnsiConsole.Write(new Markup("Inserting Time", UserInterface.MenuStyle));
                    break;

                case MenuChoice.CheckHistory:
                    AnsiConsole.Write(new Markup("Check History", UserInterface.MenuStyle));
                    //Read all from DB, dapper
                    break;

                case MenuChoice.DeleteHistory:
                    AnsiConsole.Write(new Markup("Delete History", UserInterface.MenuStyle));
                    //Drop table
                    break;

                case MenuChoice.Exit:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
