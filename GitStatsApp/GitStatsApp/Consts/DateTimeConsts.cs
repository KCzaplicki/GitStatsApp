using System;

namespace GitStatsApp.Consts
{
    public class DateTimeConsts
    {
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static readonly string DateTimeFormat = "dd/MM/yyyy";
    }
}
