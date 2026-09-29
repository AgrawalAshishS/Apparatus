using Apparatus;
using System;
using Xunit;

namespace ApparatusTests
{
    public class DateExtensionsTests
    {
        [Fact]
        public void WeekendCheckFor5DaysWeek()
        {
            Assert.True(DateTimeExtensions.IsWeekend(DayOfWeek.Saturday));
            Assert.False(DateTimeExtensions.IsWeekend(DayOfWeek.Saturday, 6));
            Assert.True(DateTimeExtensions.IsWeekend(DayOfWeek.Sunday, 6));
            Assert.True(DateTimeExtensions.IsWeekend(DayOfWeek.Friday, 4));
            Assert.False(DateTimeExtensions.IsWeekend(DayOfWeek.Friday, 5));
            Assert.False(DateTimeExtensions.IsWeekend(DayOfWeek.Thursday, 4));
        }

        [Fact]
        public void WeekdayCheckFor5DaysWeek()
        {
            Assert.False(DateTimeExtensions.IsWeekday(DayOfWeek.Saturday));
            Assert.True(DateTimeExtensions.IsWeekday(DayOfWeek.Saturday, 6));
            Assert.False(DateTimeExtensions.IsWeekday(DayOfWeek.Sunday, 6));
            Assert.False(DateTimeExtensions.IsWeekday(DayOfWeek.Friday, 4));
            Assert.True(DateTimeExtensions.IsWeekday(DayOfWeek.Friday, 5));
            Assert.True(DateTimeExtensions.IsWeekday(DayOfWeek.Thursday, 4));
        }
    }
}
