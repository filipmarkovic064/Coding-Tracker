using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    internal class TimeController
    {
        private static SqliteConnection OpenConnection()
        {
            string? ConnectionString = ConfigurationManager.AppSettings["connectionString"];
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        internal static void CreateDB()
        {

            var connection = OpenConnection();
            var sql = @"CREATE TABLE IF NOT EXISTS CodingSessions(
                                                    Id        INTEGER PRIMARY KEY AUTOINCREMENT,
                                                    StartTime TEXT,
                                                    EndTime   TEXT,
                                                    Duration  TEXT);";
            connection.Execute(sql);
            connection.Close();
        }

        internal static void ViewHistory()
        {
            var connection = OpenConnection();
            var table = HelperFunctions.CreateTable();
            var sql = "SELECT * FROM CodingSessions";
            var History = connection.Query<CodingSession>(sql).ToList();

            if (History.Any() == false) AnsiConsole.Write(new Markup("[bold red]The list is empty [/]\n"));
            else
            {
                foreach (var session in History)     
                {
                    table.AddRow($"[bold blue]{session.Id}[/]",$"[bold blue]{session.Duration}[/]",$"[bold blue]{session.StartTime}[/]",$"[bold blue]{session.EndTime}[/]");
                }
                AnsiConsole.Write(table);
            }
            connection.Close();
            HelperFunctions.ContinueToMainMenu();
        }
    
        internal static void InsertTime()
        {
            var start = AnsiConsole.Prompt(
                new TextPrompt<DateTime>("When did you start the Coding Session? (yyyy-MM-dd HH:mm:ss)"));
            var end = AnsiConsole.Prompt(
                new TextPrompt<DateTime>("When did you finish the Coding Session? (yyyy-MM-dd HH:mm:ss)"));
            if(DateTime.Compare(start, end) >= 0) 
            {
                AnsiConsole.Write(new Markup("[bold red]\nError: Start time can't be equal to or later than end time. Returning to Main Menu.[/]"));
                Console.ReadKey();
                Console.Clear();
                UserInterface.MainMenu();
            }

            string startTime = start.ToString("yyyy-MM-dd HH:mm:ss");
            string endTime = end.ToString("yyyy-MM-dd HH:mm:ss");

            CodingSession UserInput = new CodingSession(startTime, endTime);
            var connection = OpenConnection();
            var sql = @$"INSERT INTO CodingSessions(StartTime, EndTime, Duration)
                         VALUES('{UserInput.StartTime}', '{UserInput.EndTime}', '{UserInput.Duration}')";
            var InsertStatus = connection.Execute(sql);
            if(InsertStatus > 0) AnsiConsole.Write(new Markup("Session inserted successfully", UserInterface.MenuStyle));
            connection.Close();
            HelperFunctions.ContinueToMainMenu();
        }

        internal static void DeleteSession()
        {
            var connection = OpenConnection();
            var sql = "SELECT * FROM CodingSessions";
            var SessionList = connection.Query<CodingSession>(sql).ToList();
            var Session = AnsiConsole.Prompt(
                new SelectionPrompt<CodingSession>()
                .UseConverter(sessionDisplay=> sessionDisplay.DisplaySession())
                .AddChoices(SessionList));
            var SessionId = Session.Id;

            var confirm = AnsiConsole.Prompt(
                (new TextPrompt<bool>($"Are you sure you want to delete session with ID: {SessionId}")
                .AddChoice(true)
                .AddChoice(false)
                .WithConverter(choice => choice? "y" : "n")));

            if (confirm)
            {
                var sqlDelete = $"DELETE FROM CodingSessions WHERE Id = {SessionId}";
                connection.Execute(sqlDelete);
                AnsiConsole.Write(new Markup($"Session with Id {SessionId} deleted", UserInterface.MenuStyle));
                HelperFunctions.ContinueToMainMenu();
            }
            else
            {
                AnsiConsole.Write(new Markup("Deletion cancelled", UserInterface.MenuStyle));
                HelperFunctions.ContinueToMainMenu();
            }

        }
        internal static void DeleteHistory()
        {
            var confirm = AnsiConsole.Prompt(
                new TextPrompt<bool>("Are you sure you want to delete the history (Wipes the table)?")
                .AddChoice(true)
                .AddChoice(false)
                .WithConverter(choice => choice? "y" : "n"));

            if (confirm)
            {
                var connection = OpenConnection();
                var sql = "DROP TABLE CodingSessions";
                connection.Execute(sql);
                AnsiConsole.Write(new Markup("History deletion successful", UserInterface.MenuStyle));
                connection.Close();
            }
            else
            {
                AnsiConsole.Write(new Markup("History deletion cancelled", UserInterface.MenuStyle));
            }
            HelperFunctions.ContinueToMainMenu();
        }
    
/*        internal static void StartTime()
        {

        }*/
    }
}