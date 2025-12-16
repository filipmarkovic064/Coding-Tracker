using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    internal class HelperFunctions
    {
        internal static string UserInput()
        {

        }

        internal static DateTime StringToDateTime(string Time)
        {

        }

        internal static string DateTimeToString(DateTime Time)
        {

        }

        internal static TimeSpan CalculateDuration(string StartTime, string EndTime)
        {
            CultureInfo provider = new CultureInfo("en-150");

            DateTime startTime = DateTime.ParseExact(StartTime, "G", provider);
            DateTime endTime = DateTime.ParseExact(EndTime, "G", provider);

            TimeSpan duration = endTime - startTime;
            return duration;
        }
    }
}
