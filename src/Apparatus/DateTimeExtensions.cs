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
    public static class DateTimeExtensions
    {
        static readonly DateTime s_baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToEpoch(this DateTime dateTime)
        {
            return (long)(dateTime.ToUniversalTime() - s_baseDate).TotalSeconds;
        }

        public static long ToEpoch(this DateTimeOffset dateTime)
        {
            return (long)(dateTime.UtcDateTime - s_baseDate).TotalSeconds;
        }

        public static DateTime EpochToUtcDate(this long epoch)
        {
            return s_baseDate.AddSeconds(epoch);
        }

        public static TimeSpan EpochDiff(this long startEpoch, long endEpoch)
        {
            return TimeSpan.FromSeconds(endEpoch) - TimeSpan.FromSeconds(startEpoch);
        }

        public static long EpochDiffInMinutes(this long startEpoch, long endEpoch)
        {
            return (long)(startEpoch.EpochDiff(endEpoch)).TotalMinutes;
        }

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
        /// Check if given <see cref="DayOfWeek"/> value is weekend.
        /// </summary>
        public static bool IsWeekend(this DayOfWeek dayOfWeek, short numberOfDaysInWeek = 5)
        {
            if (dayOfWeek == DayOfWeek.Sunday) return true;

            if ((int)dayOfWeek > numberOfDaysInWeek) return true;

            return false;
        }

        /// <summary>
        /// Check if given <see cref="DayOfWeek"/> value is weekday.
        /// </summary>
        public static bool IsWeekday(this DayOfWeek dayOfWeek, short numberOfDaysInWeek = 5)
        {
            return !IsWeekend(dayOfWeek, numberOfDaysInWeek);
        }

    }
}
