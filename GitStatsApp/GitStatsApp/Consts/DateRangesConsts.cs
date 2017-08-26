using System.Collections.Generic;

namespace GitStatsApp.Consts
{
    public class DateRangesConsts
    {
        public const string Day = "Day";
        public const string Week = "Week";
        public const string Month = "Month";
        public const string Year = "Year";
        public const string All = "All";

        public static readonly List<string> DateRanges = new List<string>
        {
            Day,
            Week,
            Month,
            Year,
            All
        };
    }
}
