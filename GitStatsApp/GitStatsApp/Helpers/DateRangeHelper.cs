using GitStatsApp.Consts;
using System;

namespace GitStatsApp.Helpers
{
    public static class DateRangeHelper
    {
        public static DateTime GetStartRangeDateTime(string dateRange)
        {
            DateTime startDateRange = DateTimeConsts.UnixEpoch;

            switch (dateRange)
            {
                case DateRangesConsts.Day:
                    startDateRange = DateTimeConsts.Yesterday;
                    break;
                case DateRangesConsts.Week:
                    startDateRange = DateTimeConsts.LastWeek;
                    break;
                case DateRangesConsts.Month:
                    startDateRange = DateTimeConsts.LastMonth;
                    break;
                case DateRangesConsts.Year:
                    startDateRange = DateTimeConsts.LastYear;
                    break;
                case DateRangesConsts.All:
                default:
                    break;
            }

            return startDateRange;
        }
    }
}
