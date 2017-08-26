using System;

namespace GitStatsApp.Consts
{
    public class DateTimeConsts
    {
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static readonly DateTime Yesterday = DateTime.UtcNow.AddDays(-1);
        public static readonly DateTime LastWeek = DateTime.UtcNow.AddDays(-7);
        public static readonly DateTime LastMonth = DateTime.UtcNow.AddMonths(-1);
        public static readonly DateTime LastYear = DateTime.UtcNow.AddYears(-1);
        public static readonly string DateTimeFormat = "dd/MM/yyyy";
    }
}
