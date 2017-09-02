using GitStatsApp.Consts;
using System;

namespace GitStatsApp.Helpers
{
    public static class DateTimeExtension
    {
        private static readonly DateTime UnixEpoch = DateTimeConsts.UnixEpoch;

        public static long ToUnixTimestamp(this DateTime date)
        {
            if(date.CompareTo(UnixEpoch) < 0)
            {
                throw new ArgumentException("The date passed in the argument is invalid (before unix epoch).");
            }

            return (long)(date.ToUniversalTime() - UnixEpoch).TotalMilliseconds;
        }
    }
}
