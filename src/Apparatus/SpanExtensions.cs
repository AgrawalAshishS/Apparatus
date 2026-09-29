// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpanExtensions.cs" company="Toshal Infotech">
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

namespace Apparatus;

/// <summary>
/// Helpers for <see cref="ReadOnlySpan{T}"/>.
/// </summary>
public static class SpanExtensions
{
    /// <summary>
    /// Joins two spans into one new array. The first span comes first.
    /// </summary>
    /// <typeparam name="T">Item type.</typeparam>
    /// <param name="s1">The first span.</param>
    /// <param name="s2">The second span, added after the first.</param>
    /// <returns>A new array holding all items of <paramref name="s1"/> followed by all items of <paramref name="s2"/>.</returns>
    /// <example>
    /// <code>
    /// ReadOnlySpan&lt;int&gt; a = stackalloc int[] { 1, 2 };
    /// ReadOnlySpan&lt;int&gt; b = stackalloc int[] { 3 };
    /// int[] all = a.Concat(b); // { 1, 2, 3 }
    /// </code>
    /// </example>
    public static T[] Concat<T>(this ReadOnlySpan<T> s1, ReadOnlySpan<T> s2)
    {
        var array = new T[s1.Length + s2.Length];
        s1.CopyTo(array);
        s2.CopyTo(array.AsSpan(s1.Length));
        return array;
    }
}
