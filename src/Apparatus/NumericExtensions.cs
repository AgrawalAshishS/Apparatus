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
    public static class NumericExtensions
    {
        /// <summary>
        /// Useful shorthand to find if given number is present in list saves lots of OR checking.
        /// </summary>
        /// <example>
        /// @code
        /// long i = 10
        /// if( i.In(10, 11, 12))
        /// @endcode
        /// </example>
        public static bool In(this long toCheck, params long[] values)
        {
            return values.Any(value => toCheck == value);
        }

        /// <summary>
        /// Useful shorthand to find if given number is present in list saves lots of OR checking.
        /// </summary>
        /// <example>
        /// @code
        /// int i = 10
        /// if( i.In(10, 11, 12))
        /// @endcode
        /// </example>
        public static bool In(this int toCheck, params int[] values)
        {
            return values.Any(value => toCheck == value);
        }

        /// <summary>
        /// Useful shorthand to find if given number is present in list saves lots of OR checking.
        /// </summary>
        /// <example>
        /// @code
        /// int i = 10
        /// if( i.In(10, 11, 12))
        /// @endcode
        /// </example>
        public static bool In(this double toCheck, params double[] values)
        {
            return values.Any(value => toCheck == value);
        }

        /// <summary>
        /// Useful shorthand to find if given number is present in list saves lots of OR checking.
        /// </summary>
        /// <example>
        /// @code
        /// int i = 10
        /// if( i.In(10, 11, 12))
        /// @endcode
        /// </example>
        public static bool In(this decimal toCheck, params decimal[] values)
        {
            return values.Any(value => toCheck == value);
        }

        /// <summary>
        /// Useful shorthand to find if given number is present in list saves lots of OR checking.
        /// </summary>
        /// <example>
        /// @code
        /// int i = 10
        /// if( i.In(10, 11, 12))
        /// @endcode
        /// </example>
        public static bool In(this float toCheck, params float[] values)
        {
            return values.Any(value => toCheck == value);
        }

        /// <summary>
        /// Useful shorthand to find if given number is present in list saves lots of OR checking.
        /// </summary>
        /// <example>
        /// @code
        /// int i = 10
        /// if( i.In(10, 11, 12))
        /// @endcode
        /// </example>
        public static bool In(this short toCheck, params short[] values)
        {
            return values.Any(value => toCheck == value);
        }

        /// <summary>
        /// Useful shorthand to find if given number is present in list saves lots of OR checking.
        /// </summary>
        /// <example>
        /// @code
        /// int i = 10
        /// if( i.In(10, 11, 12))
        /// @endcode
        /// </example>
        public static bool In(this byte toCheck, params byte[] values)
        {
            return values.Any(value => toCheck == value);
        }
    }
}
