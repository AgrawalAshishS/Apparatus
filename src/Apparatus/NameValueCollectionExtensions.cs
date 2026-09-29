// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NameValueCollectionExtensions.cs" company="Toshal Infotech">
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

namespace Apparatus
{
    using System.Collections.Specialized;
    using System.Linq;

    /// <summary>
    /// Extension methods for NameValueCollection.
    /// </summary>
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// Converts a <see cref="NameValueCollection"/> (for example query string values) to a <see cref="Dictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="source">The collection to convert.</param>
        /// <returns>A dictionary with one entry per key. If a key has several values, they are joined with a comma (standard <see cref="NameValueCollection"/> behavior).</returns>
        /// <exception cref="NullReferenceException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <example>
        /// <code>
        /// var query = HttpUtility.ParseQueryString("a=1&amp;b=2");
        /// Dictionary&lt;string, string&gt; map = query.ToDictionary(); // { "a": "1", "b": "2" }
        /// </code>
        /// </example>
        public static Dictionary<string, string> ToDictionary([NotNull] this NameValueCollection source)
        {
            return source.AllKeys.ToDictionary(k => k, k => source[k]);
        }
    }
}
