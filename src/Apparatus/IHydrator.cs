// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IHydrator.cs" company="Toshal Infotech">
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
    using System.Collections;
    using System.Data;

    /// <summary>
    /// A contract for types that know how to fill themselves from an <see cref="IDataReader"/>.
    /// Implement it on your data classes, then use <see cref="DataReaderExtension"/> to fill one object or a whole list in a single call.
    /// </summary>
    /// <example>
    /// <code>
    /// public class Customer : IHydrator
    /// {
    ///     public int Id { get; set; }
    ///     public string Name { get; set; } = "";
    ///
    ///     public void FillObject(IDataReader dr, bool manageDataReader, bool doDrRead)
    ///     {
    ///         if (doDrRead &amp;&amp; !dr.Read()) return;
    ///         Id = dr.GetInt32(0);
    ///         Name = dr.GetString(1);
    ///         if (manageDataReader) dr.Close();
    ///     }
    ///
    ///     public void FillCollection(IDataReader dr, bool manageDataReader, IList listToFill)
    ///     {
    ///         while (dr.Read())
    ///         {
    ///             var item = new Customer();
    ///             item.FillObject(dr, false, false);
    ///             listToFill.Add(item);
    ///         }
    ///         if (manageDataReader) dr.Close();
    ///     }
    /// }
    ///
    /// // usage
    /// List&lt;Customer&gt; customers = cmd.ExecuteReader().FillCollection&lt;Customer&gt;();
    /// </code>
    /// </example>
    public interface IHydrator
    {
        /// <summary>
        /// Called when collection filling is expected from data reader.
        /// </summary>
        /// <param name="dr">Object of data reader class, mostly connecting data from database.</param>
        /// <param name="manageDataReader">If true, the implementing method MUST close data reader. Otherwise not.</param>
        /// <param name="listToFill">List that to be filled.</param>
        void FillCollection(IDataReader dr, bool manageDataReader, IList listToFill);

        /// <summary>
        /// Called when single object filling is expected.
        /// </summary>
        /// <param name="dr">Object of data reader class, mostly connecting data from database.</param>
        /// <param name="manageDataReader">If true, the implementing method MUST close data reader. Otherwise not.</param>
        /// <param name="doDrRead">If false, dr.read() should not be called. This is important when multiple different types objects to be created using single DR.</param>
        void FillObject(IDataReader dr, bool manageDataReader, bool doDrRead);
    }
}
