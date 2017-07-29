using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GitStatsApp.Helpers;

namespace GitStatsApp.Tests
{
    [TestClass]
    public class DateTimeExtensionTests
    {
        private DateTimeExtensionTestsFixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new DateTimeExtensionTestsFixture();
        }

        [TestMethod]
        public void Should_ParseDateToUnixTimestamp_When_DateIsValid()
        {
            // Arrange
            var validDate = _fixture.ValidUnixDate;

            // Act
            var unixTimestamp = validDate.ToUnixTimestamp();

            // Assert
            Assert.AreEqual(_fixture.ValidUnixDateTimestamp, unixTimestamp);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_ThrowException_When_DateIsBeforeUnixEpoch()
        {
            // Arrange
            var dateBeforeUnixEpoch = _fixture.DateBeforeUnixEpoch;

            // Act
            var unixTimestamp = dateBeforeUnixEpoch.ToUnixTimestamp();

            // Assert
        }
    }
}
