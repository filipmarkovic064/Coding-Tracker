using Spectre.Console;
using Spectre.Console.Rendering;
using System.Runtime.InteropServices.ComTypes;
using static CodingTracker.Enums;

//Move to interface class
Style MenuStyle = new Style(
    foreground: Color.Green,
    decoration: Decoration.Bold
    );

AnsiConsole.Write(new Align(
            new Markup("Welcome to the Coding Tracker!\n", MenuStyle),
            HorizontalAlignment.Center));

var choice = AnsiConsole.Prompt(
    new SelectionPrompt<MenuChoice>()
    .Title("[green bold]What would you like to do?[/]")
    .AddChoices(Enum.GetValues<MenuChoice>())
    );
//Move to CRUD class
switch (choice)
{
    case MenuChoice.StartTime:
        //Start timer, make a live counter, add to DB
        AnsiConsole.Write(new Markup("StartTime", MenuStyle));
        break;

    case MenuChoice.CheckHistory:
        AnsiConsole.Write(new Markup("CheckHistory", MenuStyle));
        //Read all from DB, dapper
        break;

    case MenuChoice.DeleteHistory:
        AnsiConsole.Write(new Markup("Delete History", MenuStyle));
        //Drop table

        //This may output ??? depending on the character encoding in the terminal so i commented it out
        //AnsiConsole.MarkupLine("[green](╯°□°)╯[/]︵ [blue]┻━┻[/]");
        break;
}



Console.ReadLine();


