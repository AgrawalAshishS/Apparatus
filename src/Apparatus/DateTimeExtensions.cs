// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataReaderExtension.cs" company="Toshal Infotech">
//   http://www.ToshalInfotech.com
//   Copyright (c) 2022-23
//   by Toshal Infotech
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//   documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
//   the rights to use, copy, modify, merge, publish, distribute, sub-license, and/or sell copies of the Software, and 
//   to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//   The above copyright notice and this permission notice shall be included in all copies or substantial portions 
//   of the Software.
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//   TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//   THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//   CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//   DEALINGS IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Apparatus
{
    /// <summary>
    /// Helpers for <see cref="DateTime"/>, <see cref="DateTimeOffset"/>, <see cref="DayOfWeek"/> and Unix epoch seconds.
    /// </summary>
    public static class DateTimeExtensions
    {
        static readonly DateTime s_baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts a <see cref="DateTime"/> to Unix epoch time (whole seconds since 1970-01-01 00:00:00 UTC).
        /// </summary>
        /// <param name="dateTime">The date to convert. It is converted to UTC first, so <see cref="DateTimeKind.Local"/> values are handled correctly.</param>
        /// <returns>Seconds since the Unix epoch. Negative for dates before 1970. Fractions of a second are dropped.</returns>
        /// <example>
        /// <code>
        /// long epoch = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToEpoch(); // 1577836800
        /// </code>
        /// </example>
        public static long ToEpoch(this DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - s_baseDate).TotalSeconds;
        }

        /// <summary>
        /// Converts a <see cref="DateTimeOffset"/> to Unix epoch time (whole seconds since 1970-01-01 00:00:00 UTC).
        /// </summary>
        /// <param name="dateTime">The date to convert. The offset is taken into account.</param>
        /// <returns>Seconds since the Unix epoch. Negative for dates before 1970. Fractions of a second are dropped.</returns>
        /// <example>
        /// <code>
        /// long epoch = new DateTimeOffset(2020, 1, 1, 5, 30, 0, TimeSpan.FromHours(5.5)).ToEpoch(); // 1577836800
        /// </code>
        /// </example>
        public static long ToEpoch(this DateTimeOffset dateTime)
        {
            return (long)(dateTime.UtcDateTime - s_baseDate).TotalSeconds;
        }

        /// <summary>
        /// Converts Unix epoch seconds back to a UTC <see cref="DateTime"/>.
        /// </summary>
        /// <param name="epoch">Seconds since 1970-01-01 00:00:00 UTC.</param>
        /// <returns>The matching date and time, with <see cref="DateTimeKind.Utc"/>.</returns>
        /// <example>
        /// <code>
        /// DateTime date = 1577836800L.EpochToUtcDate(); // 2020-01-01 00:00:00 UTC
        /// </code>
        /// </example>
        public static DateTime EpochToUtcDate(this long epoch)
        {
            return s_baseDate.AddSeconds(epoch);
        }

        /// <summary>
        /// Gets the time between two Unix epoch values.
        /// </summary>
        /// <param name="startEpoch">Start time in epoch seconds.</param>
        /// <param name="endEpoch">End time in epoch seconds.</param>
        /// <returns><paramref name="endEpoch"/> minus <paramref name="startEpoch"/>. Negative when the end is before the start.</returns>
        /// <example>
        /// <code>
        /// TimeSpan diff = 1000L.EpochDiff(4600L); // 1 hour
        /// </code>
        /// </example>
        public static TimeSpan EpochDiff(this long startEpoch, long endEpoch)
        {
            return TimeSpan.FromSeconds(endEpoch) - TimeSpan.FromSeconds(startEpoch);
        }

        /// <summary>
        /// Gets the number of whole minutes between two Unix epoch values.
        /// </summary>
        /// <param name="startEpoch">Start time in epoch seconds.</param>
        /// <param name="endEpoch">End time in epoch seconds.</param>
        /// <returns>Whole minutes between the two values. Partial minutes are dropped (rounded toward zero).</returns>
        /// <example>
        /// <code>
        /// long minutes = 0L.EpochDiffInMinutes(150L); // 2
        /// </code>
        /// </example>
        public static long EpochDiffInMinutes(this long startEpoch, long endEpoch)
        {
            return (long)(startEpoch.EpochDiff(endEpoch)).TotalMinutes;
        }

        /// <summary>
        /// Removes the time part of a date (hours, minutes, seconds and milliseconds), keeping only the day.
        /// </summary>
        /// <param name="dateTime">The date to clear.</param>
        /// <returns>The same day at 00:00:00.000. The <see cref="DateTime.Kind"/> is kept.</returns>
        /// <example>
        /// <code>
        /// DateTime day = new DateTime(2024, 5, 17, 14, 45, 30).ClearTime(); // 2024-05-17 00:00:00
        /// </code>
        /// </example>
        public static DateTime ClearTime(this DateTime dateTime)
        {
            return dateTime.Subtract(
                new TimeSpan(
                    0,
                    dateTime.Hour,
                    dateTime.Minute,
                    dateTime.Second,
                    dateTime.Millisecond
                )
            );
        }

        /// <summary>
        /// Checks whether a <see cref="DayOfWeek"/> is a weekend day.
        /// Sunday is always a weekend. Any day with a number greater than <paramref name="numberOfDaysInWeek"/> is also a weekend (Monday = 1 ... Saturday = 6).
        /// </summary>
        /// <param name="dayOfWeek">The day to check.</param>
        /// <param name="numberOfDaysInWeek">
        /// Number of working days counted from Monday. Default is 5 (Monday to Friday work, Saturday and Sunday off).
        /// Use 6 for a Monday to Saturday work week.
        /// </param>
        /// <returns><c>true</c> if the day is a weekend; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// DayOfWeek.Saturday.IsWeekend();  // true
        /// DayOfWeek.Saturday.IsWeekend(6); // false, Saturday is a working day
        /// DayOfWeek.Monday.IsWeekend();    // false
        /// </code>
        /// </example>
        public static bool IsWeekend(this DayOfWeek dayOfWeek, short numberOfDaysInWeek = 5)
        {
            if (dayOfWeek == DayOfWeek.Sunday) return true;

            if ((int)dayOfWeek > numberOfDaysInWeek) return true;

            return false;
        }

        /// <summary>
        /// Checks whether a <see cref="DayOfWeek"/> is a working day. This is the opposite of <see cref="IsWeekend"/>.
        /// </summary>
        /// <param name="dayOfWeek">The day to check.</param>
        /// <param name="numberOfDaysInWeek">Number of working days counted from Monday. Default is 5.</param>
        /// <returns><c>true</c> if the day is a working day; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// DayOfWeek.Wednesday.IsWeekday(); // true
        /// </code>
        /// </example>
        public static bool IsWeekday(this DayOfWeek dayOfWeek, short numberOfDaysInWeek = 5)
        {
            return !IsWeekend(dayOfWeek, numberOfDaysInWeek);
        }

    }
}
