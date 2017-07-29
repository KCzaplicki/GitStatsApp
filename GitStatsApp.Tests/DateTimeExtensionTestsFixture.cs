using System;

namespace GitStatsApp.Tests
{
    public class DateTimeExtensionTestsFixture
    {
        public DateTime ValidUnixDate { get; }
        public long ValidUnixDateTimestamp { get; }
        public DateTime DateBeforeUnixEpoch { get; }

        public DateTimeExtensionTestsFixture()
        {
            ValidUnixDate = new DateTime(2010, 1, 1, 8, 0, 0, DateTimeKind.Utc);
            ValidUnixDateTimestamp = 1262332800000;
            DateBeforeUnixEpoch = new DateTime(1950, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }
    }
}
