using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    internal class CodingSession
    {
        internal int Id{set; get;}
        internal string StartTime { set; get; }
        internal string EndTime { set; get; }

        internal CodingSession(int Id, string startTime, string endTime, string Duration)
        {
            StartTime = startTime;
            EndTime = endTime;
            Duration = null;
        }
        public void DisplayDuration()
        {
            TimeSpan duration = HelperFunctions.CalculateDuration(StartTime,EndTime);
            string dur = duration.ToString();
            AnsiConsole.Write(new Markup(dur,UserInterface.MenuStyle));
        }
    }
}
