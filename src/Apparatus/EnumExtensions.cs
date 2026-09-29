// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="Toshal Infotech">
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
    /// Helpers for working with enumerations.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Joins a list of enum values into one comma separated string, either as numbers or as names.
        /// </summary>
        /// <typeparam name="TEnum">The enum type.</typeparam>
        /// <param name="items">The enum values to join.</param>
        /// <param name="useEnumValue"><c>true</c> (default) to write the numeric values; <c>false</c> to write the names.</param>
        /// <returns>A string like <c>"1,3"</c> or <c>"Red,Blue"</c>. Empty string for an empty list.</returns>
        /// <example>
        /// <code>
        /// var colors = new[] { Color.Red, Color.Blue };
        /// colors.ToCommaSeparatedList();      // "0,2"
        /// colors.ToCommaSeparatedList(false); // "Red,Blue"
        /// </code>
        /// </example>
        public static string ToCommaSeparatedList<TEnum>(this IEnumerable<TEnum> items, bool useEnumValue = true) where TEnum : struct, IComparable, IConvertible, IFormattable
        {
            var enumType = typeof(TEnum);
            return String.Join(",", items.Select(x => Enum.Format(enumType, x, useEnumValue ? "D" : "G")));
        }

        /// <summary>
        /// Converts an enumeration type into a dictionary of its names and numeric values.
        /// </summary>
        /// <param name="t">The enum type, for example <c>typeof(DayOfWeek)</c>.</param>
        /// <returns>A dictionary where the key is the enum member name and the value is its <see cref="int"/> value.</returns>
        /// <exception cref="NullReferenceException"><paramref name="t"/> is <c>null</c>.</exception>
        /// <example>
        /// <code>
        /// var map = typeof(DayOfWeek).EnumToDictionary(); // { "Sunday": 0, "Monday": 1, ... }
        /// </code>
        /// </example>
        public static IDictionary<string, int> EnumToDictionary(this Type t)
        {
            if (t == null) throw new NullReferenceException();
            //if (!t.IsEnum) throw new InvalidCastException("object is not an Enumeration");

            string[] names = Enum.GetNames(t);
            Array values = Enum.GetValues(t);

            return (from i in Enumerable.Range(0, names.Length)
                    select new { Key = names[i], Value = (int)values.GetValue(i) })
                        .ToDictionary(k => k.Key, k => k.Value);
        }
    }
}
