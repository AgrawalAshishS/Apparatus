// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumericExtensions.cs" company="Toshal Infotech">
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
    /// Helpers for numeric types such as <see cref="int"/>, <see cref="long"/> and <see cref="decimal"/>.
    /// </summary>
    public static class NumericExtensions
    {
        /// <summary>
        /// Checks whether this <see cref="long"/> value equals any of the given values.
        /// Shorter and easier to read than a chain of <c>||</c> comparisons.
        /// </summary>
        /// <param name="toCheck">The value to look for.</param>
        /// <param name="values">The values to compare against.</param>
        /// <returns><c>true</c> if <paramref name="toCheck"/> equals at least one of <paramref name="values"/>; otherwise <c>false</c>. Returns <c>false</c> when no values are given.</returns>
        /// <example>
        /// <code>
        /// long id = 10;
        /// if (id.In(10, 11, 12))
        /// {
        ///     // runs, because the value is in the list
        /// }
        /// </code>
        /// </example>
        public static bool In(this long toCheck, params long[] values)
        {
            return values.Any(value => toCheck == value);
        }

        /// <summary>
        /// Checks whether this <see cref="int"/> value equals any of the given values.
        /// Shorter and easier to read than a chain of <c>||</c> comparisons.
        /// </summary>
        /// <param name="toCheck">The value to look for.</param>
        /// <param name="values">The values to compare against.</param>
        /// <returns><c>true</c> if <paramref name="toCheck"/> equals at least one of <paramref name="values"/>; otherwise <c>false</c>. Returns <c>false</c> when no values are given.</returns>
        /// <example>
        /// <code>
        /// int id = 10;
        /// if (id.In(10, 11, 12))
        /// {
        ///     // runs, because the value is in the list
        /// }
        /// </code>
        /// </example>
        public static bool In(this int toCheck, params int[] values)
        {
            return values.Any(value => toCheck == value);
        }

        /// <summary>
        /// Checks whether this <see cref="double"/> value equals any of the given values.
        /// Shorter and easier to read than a chain of <c>||</c> comparisons.
        /// </summary>
        /// <param name="toCheck">The value to look for.</param>
        /// <param name="values">The values to compare against.</param>
        /// <returns><c>true</c> if <paramref name="toCheck"/> equals at least one of <paramref name="values"/>; otherwise <c>false</c>. Returns <c>false</c> when no values are given.</returns>
        /// <example>
        /// <code>
        /// double d = 2.5;
        /// if (d.In(2.5, 3.5))
        /// {
        ///     // runs, because the value is in the list
        /// }
        /// </code>
        /// </example>
        public static bool In(this double toCheck, params double[] values)
        {
            return values.Any(value => toCheck == value);
        }

        /// <summary>
        /// Checks whether this <see cref="decimal"/> value equals any of the given values.
        /// Shorter and easier to read than a chain of <c>||</c> comparisons.
        /// </summary>
        /// <param name="toCheck">The value to look for.</param>
        /// <param name="values">The values to compare against.</param>
        /// <returns><c>true</c> if <paramref name="toCheck"/> equals at least one of <paramref name="values"/>; otherwise <c>false</c>. Returns <c>false</c> when no values are given.</returns>
        /// <example>
        /// <code>
        /// decimal d = 2.5m;
        /// if (d.In(2.5m, 3.5m))
        /// {
        ///     // runs, because the value is in the list
        /// }
        /// </code>
        /// </example>
        public static bool In(this decimal toCheck, params decimal[] values)
        {
            return values.Any(value => toCheck == value);
        }

        /// <summary>
        /// Checks whether this <see cref="float"/> value equals any of the given values.
        /// Shorter and easier to read than a chain of <c>||</c> comparisons.
        /// </summary>
        /// <param name="toCheck">The value to look for.</param>
        /// <param name="values">The values to compare against.</param>
        /// <returns><c>true</c> if <paramref name="toCheck"/> equals at least one of <paramref name="values"/>; otherwise <c>false</c>. Returns <c>false</c> when no values are given.</returns>
        /// <example>
        /// <code>
        /// float f = 2.5f;
        /// if (f.In(2.5f, 3.5f))
        /// {
        ///     // runs, because the value is in the list
        /// }
        /// </code>
        /// </example>
        public static bool In(this float toCheck, params float[] values)
        {
            return values.Any(value => toCheck == value);
        }

        /// <summary>
        /// Checks whether this <see cref="short"/> value equals any of the given values.
        /// Shorter and easier to read than a chain of <c>||</c> comparisons.
        /// </summary>
        /// <param name="toCheck">The value to look for.</param>
        /// <param name="values">The values to compare against.</param>
        /// <returns><c>true</c> if <paramref name="toCheck"/> equals at least one of <paramref name="values"/>; otherwise <c>false</c>. Returns <c>false</c> when no values are given.</returns>
        /// <example>
        /// <code>
        /// short s = 10;
        /// if (s.In(10, 11, 12))
        /// {
        ///     // runs, because the value is in the list
        /// }
        /// </code>
        /// </example>
        public static bool In(this short toCheck, params short[] values)
        {
            return values.Any(value => toCheck == value);
        }

        /// <summary>
        /// Checks whether this <see cref="byte"/> value equals any of the given values.
        /// Shorter and easier to read than a chain of <c>||</c> comparisons.
        /// </summary>
        /// <param name="toCheck">The value to look for.</param>
        /// <param name="values">The values to compare against.</param>
        /// <returns><c>true</c> if <paramref name="toCheck"/> equals at least one of <paramref name="values"/>; otherwise <c>false</c>. Returns <c>false</c> when no values are given.</returns>
        /// <example>
        /// <code>
        /// byte b = 10;
        /// if (b.In(10, 11, 12))
        /// {
        ///     // runs, because the value is in the list
        /// }
        /// </code>
        /// </example>
        public static bool In(this byte toCheck, params byte[] values)
        {
            return values.Any(value => toCheck == value);
        }
    }
}
