// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataReaderExtension.cs" company="Toshal Infotech">
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

// NuGet Package: Apparatus
namespace Apparatus
{
    using System.Collections;
    using System.Data;

    /// <summary>
    /// Short helpers for <see cref="System.Data.IDataReader"/>: loop over rows, move between result sets,
    /// close the reader without try/catch, and fill objects that implement <see cref="IHydrator"/>.
    /// </summary>
    public static class DataReaderExtension
    {
        /// <summary>
        /// Fills an existing list from the reader and returns the reader, so you can chain more calls
        /// (for example <see cref="NextResultSafely(IDataReader)"/>) to read several result sets. The reader stays open.
        /// </summary>
        /// <typeparam name="T">A type that implements <see cref="IHydrator"/> and has a public parameterless constructor.</typeparam>
        /// <param name="dr">An open data reader.</param>
        /// <param name="listToFill">The list that receives the new objects.</param>
        /// <returns>The same <paramref name="dr"/>, for chaining.</returns>
        /// <example>
        /// <code>
        /// var tasks = new List&lt;TaskItem&gt;();
        /// var comments = new List&lt;Comment&gt;();
        /// cmd.ExecuteReader()
        ///     .FillCollection&lt;TaskItem&gt;(tasks)
        ///     .NextResultSafely()
        ///     .FillCollection&lt;Comment&gt;(comments);
        /// </code>
        /// </example>
        public static IDataReader FillCollection<T>(this IDataReader dr, IList listToFill) where T : IHydrator, new()
        {
            FillCollection<T>(dr, false, listToFill);
            return dr;
        }

        /// <summary>
        /// Reads all rows of the reader into a new list of <typeparamref name="T"/>, and asks the type to close the reader.
        /// </summary>
        /// <typeparam name="T">A type that implements <see cref="IHydrator"/> and has a public parameterless constructor.</typeparam>
        /// <param name="dr">An open data reader.</param>
        /// <returns>A new list with one object per row.</returns>
        /// <remarks>The reader is closed afterwards (the <see cref="IHydrator"/> implementation must do it).</remarks>
        /// <example>
        /// <code>
        /// List&lt;Customer&gt; customers = cmd.ExecuteReader().FillCollection&lt;Customer&gt;();
        /// </code>
        /// </example>
        public static List<T> FillCollection<T>(this IDataReader dr) where T : IHydrator, new()
        {
            var retVal = new List<T>();
            FillCollection<T>(dr, true, retVal);
            return retVal;
        }

        /// <summary>
        /// Reads all rows of the reader into a new list of <typeparamref name="T"/>, and lets you choose whether the reader is closed.
        /// </summary>
        /// <typeparam name="T">A type that implements <see cref="IHydrator"/> and has a public parameterless constructor.</typeparam>
        /// <param name="dr">An open data reader.</param>
        /// <param name="closeConnection"><c>true</c> to close the reader after filling; <c>false</c> to keep it open.</param>
        /// <returns>A new list with one object per row.</returns>
        /// <example>
        /// <code>
        /// List&lt;Customer&gt; customers = cmd.ExecuteReader().FillCollection&lt;Customer&gt;(false);
        /// </code>
        /// </example>
        public static List<T> FillCollection<T>(this IDataReader dr, bool closeConnection)
            where T : IHydrator, new()
        {
            var retVal = new List<T>();
            FillCollection<T>(dr, closeConnection, retVal);
            return retVal;
        }

        /// <summary>
        /// Reads all rows of the reader and adds the new objects to a list you already have.
        /// </summary>
        /// <typeparam name="T">A type that implements <see cref="IHydrator"/> and has a public parameterless constructor.</typeparam>
        /// <param name="dr">An open data reader.</param>
        /// <param name="closeConnection"><c>true</c> to close the reader after filling; <c>false</c> to keep it open.</param>
        /// <param name="listToFill">The list that receives the new objects.</param>
        /// <example>
        /// <code>
        /// var customers = new List&lt;Customer&gt;();
        /// cmd.ExecuteReader().FillCollection&lt;Customer&gt;(true, customers);
        /// </code>
        /// </example>
        public static void FillCollection<T>(this IDataReader dr, bool closeConnection, IList listToFill)
            where T : IHydrator, new()
        {
            var hydrator = new T();
            hydrator.FillCollection(dr, closeConnection, listToFill);
        }

        /// <summary>
        /// Moves the reader to the next row (calls <c>Read()</c>) and, if there is a row, fills <paramref name="src"/> from it.
        /// Returns the reader so you can chain calls. The reader stays open.
        /// </summary>
        /// <typeparam name="T">A type that implements <see cref="IHydrator"/> and has a public parameterless constructor.</typeparam>
        /// <param name="dr">An open data reader.</param>
        /// <param name="src">The object to fill. Pass an existing object.</param>
        /// <returns>The same <paramref name="dr"/>, for chaining.</returns>
        /// <remarks>
        /// Known issue: when <paramref name="src"/> is <c>null</c>, a new object is created inside the method but the caller never receives it.
        /// Always pass an existing object.
        /// </remarks>
        /// <example>
        /// <code>
        /// var task = new TaskItem();
        /// var comments = new List&lt;Comment&gt;();
        /// cmd.ExecuteReader()
        ///     .FillObject(task)
        ///     .NextResultSafely()
        ///     .FillCollection&lt;Comment&gt;(comments);
        /// </code>
        /// </example>
        public static IDataReader FillObject<T>([NotNull] this IDataReader dr, T src) where T : IHydrator, new()
        {
            if (dr.Read())
            {
                src ??= new T();
                src.FillObject(dr, false, false);
            }

            return dr;
        }

        /// <summary>
        /// Reads the next row (calls <c>Read()</c>) into a new object of type <typeparamref name="T"/> and asks the type to close the reader.
        /// </summary>
        /// <typeparam name="T">A type that implements <see cref="IHydrator"/> and has a public parameterless constructor.</typeparam>
        /// <param name="dr">An open data reader.</param>
        /// <returns>A new object filled from the row, or <c>null</c> when there is no row.</returns>
        /// <example>
        /// <code>
        /// Customer? customer = cmd.ExecuteReader().FillObject&lt;Customer&gt;();
        /// </code>
        /// </example>
        public static T? FillObject<T>(this IDataReader dr) where T : IHydrator, new()
        {
            return FillObject<T>(dr, true);
        }

        /// <summary>
        /// Reads the next row (calls <c>Read()</c>) into a new object of type <typeparamref name="T"/>, and lets you choose whether the reader is closed.
        /// </summary>
        /// <typeparam name="T">A type that implements <see cref="IHydrator"/> and has a public parameterless constructor.</typeparam>
        /// <param name="dr">An open data reader.</param>
        /// <param name="closeConnection"><c>true</c> to ask the type to close the reader; <c>false</c> to keep it open.</param>
        /// <returns>A new object filled from the row, or <c>null</c> when there is no row.</returns>
        /// <example>
        /// <code>
        /// Customer? customer = cmd.ExecuteReader().FillObject&lt;Customer&gt;(false);
        /// </code>
        /// </example>
        public static T? FillObject<T>([NotNull] this IDataReader dr, bool closeConnection) where T : IHydrator, new()
        {
            if (dr.Read())
            {
                var hydrator = new T();
                hydrator.FillObject(dr, closeConnection, false);
                return hydrator;
            }

            return default;
        }

        /// <summary>
        /// Fills a new object from the current row, and lets you decide whether <c>Read()</c> is called first.
        /// </summary>
        /// <typeparam name="T">A type that implements <see cref="IHydrator"/> and has a public parameterless constructor.</typeparam>
        /// <param name="dr">An open data reader.</param>
        /// <param name="closeConnection"><c>true</c> to ask the type to close the reader; <c>false</c> to keep it open.</param>
        /// <param name="doDrRead">
        /// <c>true</c> to call <c>Read()</c> first. <c>false</c> to use the row the reader is already on,
        /// which is needed when several different types are built from the same row.
        /// </param>
        /// <returns>A new object filled from the row, or <c>null</c> when <paramref name="doDrRead"/> is <c>true</c> and there is no row.</returns>
        /// <example>
        /// <code>
        /// while (dr.Read())
        /// {
        ///     var customer = dr.FillObject&lt;Customer&gt;(false, false);
        ///     var address = dr.FillObject&lt;Address&gt;(false, false);
        /// }
        /// </code>
        /// </example>
        public static T? FillObject<T>([NotNull] this IDataReader dr, bool closeConnection, bool doDrRead) where T : IHydrator, new()
        {
            if (doDrRead)
            {
                if (dr.Read() == false) return default;
            }

            var hydrator = new T();
            hydrator.FillObject(dr, closeConnection, false);
            return hydrator;
        }

        /// <summary>
        /// Runs an action for every row of the reader, then closes the reader. Use it when the row type does not implement <see cref="IHydrator"/>.
        /// </summary>
        /// <param name="dr">An open data reader.</param>
        /// <param name="action">Code to run for each row. It receives the reader positioned on that row.</param>
        /// <remarks>The reader is closed afterwards, even if the action throws.</remarks>
        /// <exception cref="Exception">Any exception thrown by <paramref name="action"/> is thrown again after the reader is closed.</exception>
        /// <example>
        /// <code>
        /// var list = new List&lt;MyType&gt;();
        /// cmd.ExecuteReader().ForEachRecord(dr =&gt;
        /// {
        ///     list.Add(new MyType { Id = dr.GetInt32(0), Name = dr.GetString(1) });
        /// });
        /// </code>
        /// </example>
        public static void ForEachRecord(this IDataReader dr, Action<IDataReader> action)
        {
            ForEachRecord(dr, true, action);
        }

        /// <summary>
        /// Runs an action for every row of the reader, and lets you choose whether the reader is closed.
        /// Keep it open to read the next result set in the same chain.
        /// </summary>
        /// <param name="dr">An open data reader.</param>
        /// <param name="closeConnection"><c>true</c> to close the reader after the last row; <c>false</c> to keep it open.</param>
        /// <param name="action">Code to run for each row. It receives the reader positioned on that row.</param>
        /// <returns>The same <paramref name="dr"/>, for chaining.</returns>
        /// <remarks>If the action throws, the reader is closed and the exception is thrown again.</remarks>
        /// <example>
        /// <code>
        /// var tasks = new List&lt;TaskItem&gt;();
        /// var comments = new List&lt;Comment&gt;();
        /// cmd.ExecuteReader()
        ///     .ForEachRecord(false, dr =&gt; tasks.Add(new TaskItem { Id = dr.GetInt32(0) }))
        ///     .NextResultSafely()
        ///     .ForEachRecord(true, dr =&gt; comments.Add(new Comment { Id = dr.GetInt32(0) }));
        /// </code>
        /// </example>
        public static IDataReader ForEachRecord([NotNull] this IDataReader dr, bool closeConnection, [NotNull] Action<IDataReader> action)
        {
            try
            {
                if (closeConnection)
                {
                    using (dr)
                    {
                        while (dr.Read())
                        {
                            action(dr);
                        }
                    }
                    dr.CloseSafely();
                }
                else
                {
                    while (dr.Read())
                    {
                        action(dr);
                    }
                }
            }
            catch
            {
                dr.CloseSafely();
                throw;
            }

            return dr;
        }

        /// <summary>
        /// Closes the reader and ignores any error, so it is safe to call inside <c>finally</c>.
        /// </summary>
        /// <param name="dr">The reader to close. Already closed or disposed readers are fine.</param>
        /// <example>
        /// <code>
        /// try
        /// {
        ///     while (dr.Read()) { /* ... */ }
        /// }
        /// finally
        /// {
        ///     dr.CloseSafely();
        /// }
        /// </code>
        /// </example>
        public static void CloseSafely(this IDataReader dr)
        {
            try
            {
                using (dr) { }//forcing a close for .NET core version.
                dr.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Moves to the next result set of the reader and ignores any error, so you do not need a try/catch.
        /// </summary>
        /// <param name="dr">An open data reader.</param>
        /// <returns>The same <paramref name="dr"/>, for chaining.</returns>
        /// <example>
        /// <code>
        /// while (dr.Read()) { /* first result */ }
        /// dr.NextResultSafely();
        /// while (dr.Read()) { /* second result */ }
        /// </code>
        /// </example>
        public static IDataReader NextResultSafely([NotNull] this IDataReader dr)
        {
            try
            {
                dr.NextResult();
            }
            catch
            {
            }
            return dr;
        }

        /// <summary>
        /// Reads several result sets. The first action runs for every row of the first result set, the second action for the second result set, and so on.
        /// The reader is disposed at the end.
        /// </summary>
        /// <param name="dr">An open data reader.</param>
        /// <param name="action">One action per result set, in order.</param>
        /// <remarks>
        /// Known issue: calling this without any action fails with an <see cref="IndexOutOfRangeException"/> (or a <see cref="NullReferenceException"/> for <c>null</c>) instead of just closing the reader.
        /// </remarks>
        /// <example>
        /// <code>
        /// cmd.ExecuteReader().ForEachResult(
        ///     dr =&gt; tasks.Add(new TaskItem { Id = dr.GetInt32(0) }),
        ///     dr =&gt; comments.Add(new Comment { Id = dr.GetInt32(0) }));
        /// </code>
        /// </example>
        public static void ForEachResult(this IDataReader dr, params Action<IDataReader>[] action)
        {
            using (dr)
            {
                if (action == null || action.Length == 0) dr.CloseSafely();

                var i = 0;
                do
                {
                    dr.ForEachRecord(false, action[i]);
                    dr.NextResultSafely();
                    i++;
                } while (i < action.Length);

                //dr.CloseSafely();
            }
        }

        /// <summary>
        /// Runs an action when the reader has no columns, which is how an empty result is detected without calling <c>Read()</c> (so other reading code is not disturbed).
        /// </summary>
        /// <param name="dr">An open data reader.</param>
        /// <param name="noRecordAction">Code to run when <c>FieldCount</c> is 0.</param>
        /// <returns>The same <paramref name="dr"/>, for chaining.</returns>
        /// <example>
        /// <code>
        /// cmd.ExecuteReader().NoRecord(() =&gt; Console.WriteLine("Nothing found"));
        /// </code>
        /// </example>
        public static System.Data.IDataReader NoRecord([NotNull] this System.Data.IDataReader dr, [NotNull] Action noRecordAction)
        {
            if (dr.FieldCount == 0) noRecordAction();
            return dr;
        }
    }
}
