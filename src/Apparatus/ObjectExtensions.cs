// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Toshal Infotech">
//   http://www.ToshalInfotech.com
//   Copyright (c) 2022-23
//   by Toshal Infotech
//   
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//   documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
//   the rights to use, copy, modify, merge, publish, distribute, sub-license, and/or sell copies of the Software, and 
//   to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//   
//   The above copyright notice and this permission notice shall be included in all copies or substantial portions 
//   of the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//   TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//   THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//   CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//   DEALINGS IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel;
using System.Globalization;

namespace Apparatus
{

    /// <summary>
    /// Extension methods of Object class; so for everything.
    /// </summary>
    public static class ObjectExtensions
    {

        /// <summary>
        /// Returns complete type name in "<Namespace>.<Class>, <Assembly-Name>" format.
        /// </summary>
        public static string FullNameWithAssembly([NotNull] this object src)
        {
            return src.GetType().FullNameWithAssembly();
        }

        /// <summary>
        /// Perform typecast. Useful for fluent code writing.
        /// </summary>
        public static T As<T>(this object src) where T : class
        {
            return (T)src;
        }

        public static T To<T>(this object src)
            where T : struct
        {
            if (typeof(T) == typeof(Guid))
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(src.ToString());
            }

            return (T)Convert.ChangeType(src, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Check if source object can e converted to given type or not.
        /// </summary>
        public static bool CanBeCastTo<T>(this object src)
        {
            if (src == null) return false;
            var destinationType = typeof(T);

            return src.GetType().CanBeCastTo(destinationType);
        }

        public static bool Between<T>(this T src, T from, T to) where T : IComparable<T>
        {
            return src.CompareTo(from) >= 0 && src.CompareTo(to) <= 0;
        }

        public static bool In<T>(this T src, params T[] list)
        {
            return list.Contains(src);
        }

        public static bool In<T>(this T src, IEnumerable<T> list)
        {
            return list.Contains(src);
        }

        public static T If<T>(this T src, bool condition, [NotNull] Func<T, T> func)
        {
            if (condition)
            {
                return func(src);
            }

            return src;
        }

        public static T If<T>(this T src, bool condition, [NotNull] Action<T> action)
        {
            if (condition)
            {
                action(src);
            }

            return src;
        }

    }
}
