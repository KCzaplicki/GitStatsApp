using System;

namespace GitStatsApp.Consts
{
    public class DateTimeConsts
    {
        public static readonly DateTime Today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 23, 59, 59, DateTimeKind.Utc);
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static readonly DateTime Yesterday = DateTimeConsts.Today.AddDays(-2);
        public static readonly DateTime LastWeek = DateTimeConsts.Today.AddDays(-8);
        public static readonly DateTime LastMonth = DateTimeConsts.Today.AddMonths(-1).AddDays(-1);
        public static readonly DateTime LastYear = DateTimeConsts.Today.AddYears(-1).AddDays(-1);
        public static readonly string DateTimeFormat = "dd/MM/yyyy";
    }
}
