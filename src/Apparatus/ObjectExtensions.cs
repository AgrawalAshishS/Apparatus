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
        /// Gets the type name of this object with the assembly name, in <c>Namespace.Class, AssemblyName</c> format.
        /// </summary>
        /// <param name="src">The object. Must not be <c>null</c>.</param>
        /// <returns>For example <c>"System.String, System.Private.CoreLib"</c>.</returns>
        /// <exception cref="NullReferenceException"><paramref name="src"/> is <c>null</c>.</exception>
        /// <example>
        /// <code>
        /// string name = "text".FullNameWithAssembly();
        /// </code>
        /// </example>
        public static string FullNameWithAssembly([NotNull] this object src)
        {
            return src.GetType().FullNameWithAssembly();
        }

        /// <summary>
        /// Casts the object to <typeparamref name="T"/>. Useful when writing fluent code without extra brackets.
        /// </summary>
        /// <typeparam name="T">The reference type to cast to.</typeparam>
        /// <param name="src">The object to cast.</param>
        /// <returns>The same object as <typeparamref name="T"/>.</returns>
        /// <exception cref="InvalidCastException">The object is not a <typeparamref name="T"/>.</exception>
        /// <example>
        /// <code>
        /// object o = "text";
        /// int length = o.As&lt;string&gt;().Length;
        /// </code>
        /// </example>
        public static T As<T>(this object src) where T : class
        {
            return (T)src;
        }

        /// <summary>
        /// Converts the object to a value type such as <see cref="int"/>, <see cref="decimal"/>, <see cref="DateTime"/> or <see cref="Guid"/>.
        /// Uses the invariant culture, so results do not depend on the computer's language settings.
        /// </summary>
        /// <typeparam name="T">The value type to convert to.</typeparam>
        /// <param name="src">The value to convert, for example a string like <c>"42"</c>.</param>
        /// <returns>The converted value.</returns>
        /// <exception cref="FormatException">The value is not in a valid format.</exception>
        /// <exception cref="InvalidCastException">The conversion is not supported.</exception>
        /// <exception cref="OverflowException">The value is too big or too small for <typeparamref name="T"/>.</exception>
        /// <example>
        /// <code>
        /// int number = "42".To&lt;int&gt;();
        /// Guid id = "d3b07384-d9a1-4c3e-8b5a-2f6c5a1e9c00".To&lt;Guid&gt;();
        /// </code>
        /// </example>
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
        /// Checks whether this object can be cast to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The target type.</typeparam>
        /// <param name="src">The object to check. A <c>null</c> value returns <c>false</c>.</param>
        /// <returns><c>true</c> if the object's type is, inherits from or implements <typeparamref name="T"/>; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// "text".CanBeCastTo&lt;IEnumerable&lt;char&gt;&gt;(); // true
        /// </code>
        /// </example>
        public static bool CanBeCastTo<T>(this object src)
        {
            if (src == null) return false;
            var destinationType = typeof(T);

            return src.GetType().CanBeCastTo(destinationType);
        }

        /// <summary>
        /// Checks whether a value is between two limits. Both limits are included.
        /// </summary>
        /// <typeparam name="T">Any comparable type, for example numbers, dates or strings.</typeparam>
        /// <param name="src">The value to check.</param>
        /// <param name="from">The lower limit (included).</param>
        /// <param name="to">The upper limit (included).</param>
        /// <returns><c>true</c> if <paramref name="from"/> &lt;= <paramref name="src"/> &lt;= <paramref name="to"/>; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// 5.Between(1, 10);  // true
        /// 10.Between(1, 10); // true
        /// 11.Between(1, 10); // false
        /// </code>
        /// </example>
        public static bool Between<T>(this T src, T from, T to) where T : IComparable<T>
        {
            return src.CompareTo(from) >= 0 && src.CompareTo(to) <= 0;
        }

        /// <summary>
        /// Checks whether a value is one of the given values. Shorter than many <c>||</c> checks.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="src">The value to look for.</param>
        /// <param name="list">The allowed values.</param>
        /// <returns><c>true</c> if the value is in the list; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// "b".In("a", "b", "c"); // true
        /// </code>
        /// </example>
        public static bool In<T>(this T src, params T[] list)
        {
            return list.Contains(src);
        }

        /// <summary>
        /// Checks whether a value is in a collection.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="src">The value to look for.</param>
        /// <param name="list">The collection to search.</param>
        /// <returns><c>true</c> if the value is in the collection; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// var allowed = new List&lt;int&gt; { 1, 2, 3 };
        /// 2.In(allowed); // true
        /// </code>
        /// </example>
        public static bool In<T>(this T src, IEnumerable<T> list)
        {
            return list.Contains(src);
        }

        /// <summary>
        /// Applies a change to a value only when a condition is true, so a fluent chain does not need to break.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="src">The value to work on.</param>
        /// <param name="condition">When <c>false</c>, nothing happens and <paramref name="src"/> is returned as it is.</param>
        /// <param name="func">Function that takes the value and returns the new value.</param>
        /// <returns>The result of <paramref name="func"/> if <paramref name="condition"/> is <c>true</c>; otherwise <paramref name="src"/>.</returns>
        /// <example>
        /// <code>
        /// string name = "john".If(isFormal, s =&gt; s.ToUpper());
        /// </code>
        /// </example>
        public static T If<T>(this T src, bool condition, [NotNull] Func<T, T> func)
        {
            if (condition)
            {
                return func(src);
            }

            return src;
        }

        /// <summary>
        /// Runs an action on a value only when a condition is true, and always returns the same value so a fluent chain can continue.
        /// </summary>
        /// <typeparam name="T">The value type.</typeparam>
        /// <param name="src">The value to work on.</param>
        /// <param name="condition">When <c>false</c>, the action is not run.</param>
        /// <param name="action">Action that receives the value.</param>
        /// <returns>Always <paramref name="src"/>.</returns>
        /// <example>
        /// <code>
        /// list.If(log, l =&gt; Console.WriteLine(l.Count)).Add(5);
        /// </code>
        /// </example>
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
