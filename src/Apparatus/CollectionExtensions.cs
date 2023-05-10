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
        /// Check if collection is null or empty (having no element).
        /// </summary>
        /// <typeparam name="T">Type of collection.</typeparam>
        /// <param name="source">Collection to check.</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// Remove all element from source matching with destination.
        /// </summary>
        /// <typeparam name="T">Type of collection.</typeparam>
        /// <param name="source">>Collection to check.</param>
        /// <param name="items">Items to remove from source.</param>
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
