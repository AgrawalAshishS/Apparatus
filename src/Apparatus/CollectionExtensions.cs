// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionExtensions.cs" company="Toshal Infotech">
//   http://www.ToshalInfotech.com
//   Copyright (c) 2022-2023
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
    /// Extension methods for generic collections.
    /// </summary>
    public static class CollectionExtensions
    {

        /// <summary>
        /// Checks whether a collection is <c>null</c> or has no items.
        /// </summary>
        /// <typeparam name="T">Type of the items.</typeparam>
        /// <param name="source">The collection to check. May be <c>null</c>.</param>
        /// <returns><c>true</c> if <paramref name="source"/> is <c>null</c> or empty; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// List&lt;int&gt;? list = null;
        /// list.IsNullOrEmpty(); // true
        /// </code>
        /// </example>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// Removes from a collection every item that also appears in another list.
        /// Each matching item is removed once per occurrence in <paramref name="items"/>.
        /// </summary>
        /// <typeparam name="T">Type of the items.</typeparam>
        /// <param name="source">The collection to remove items from. Must not be <c>null</c>.</param>
        /// <param name="items">The items to remove. Must not be <c>null</c>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="items"/> is <c>null</c>.</exception>
        /// <example>
        /// <code>
        /// var numbers = new List&lt;int&gt; { 1, 2, 3, 4 };
        /// numbers.RemoveAll(new[] { 2, 4 }); // numbers is now { 1, 3 }
        /// </code>
        /// </example>
        public static void RemoveAll<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            source.ThrowIfNull(nameof(source));
            items.ThrowIfNull(nameof(items));

            foreach (var item in items)
            {
                source.Remove(item);
            }
        }

    }
}
