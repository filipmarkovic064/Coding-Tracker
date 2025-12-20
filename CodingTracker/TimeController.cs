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
        static string? ConnectionString = ConfigurationManager.AppSettings["connectionString"];

        internal static void CreateDB()
        {

            var connection = new SqliteConnection(TimeController.ConnectionString);
            var sql = @"CREATE TABLE IF NOT EXISTS CodingSessions(
                                                    Id        INTEGER PRIMARY KEY AUTOINCREMENT,
                                                    StartTime STRING,
                                                    EndTime   STRING,
                                                    Duration  STRING)";
            connection.Open();
            connection.Execute(sql);
            connection.Close();
        }

        internal static void ViewHistory()
        {
            using (var connection = new SqliteConnection(TimeController.ConnectionString))
            {
                var sql = "SELECT * FROM CodingSessions";
                connection.Open();
                var History = connection.Query<CodingSession>(sql).ToList();
                if (History.Any() == false) AnsiConsole.Write(new Markup("The list is empty", UserInterface.MenuStyle));
                else
                    foreach (var session in History)
                        {
                            // Create a Panel where we will Display Coding Sessions
                            session.DisplaySession();
                        }
            }
        }
    
        internal static void InsertTime()
        {

        }

/*        internal static void DeleteHistory()
        {

        }
    
        internal static void StartTime()
        {

        }*/
    }
}