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

/// <summary>
/// NuGet Package: Apparatus
/// </summary>
namespace Apparatus
{
    using System.Collections;
    using System.Data;

    /// <summary>
    /// Common shorthand methods to make IDataReader code easy.
    /// </summary>
    public static class DataReaderExtension
    {
        /// <summary>
        /// Provide very fluent way to fill multiple collections / objects with chain of FillCollection, FillObject and NextResultSafely.
        /// 
        /// Example
        /// @code
        /// Tasks task;
        /// List<Comments> comments;
        /// cmd.ExecuteDataReader()
        ///     .FillObject<Task>(task)
        ///     .NextResultSafely()
        ///     .FillCollection<Comments>(comments);
        /// @endcode
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="listToFill"></param>
        /// <returns></returns>
        public static IDataReader FillCollection<T>(this IDataReader dr, IList listToFill) where T : IHydrator, new()
        {
            FillCollection<T>(dr, false, listToFill);
            return dr;
        }

        /// <summary>
        /// Takes IDataReader and provide shorthand to call FillCollection based on <see cref="IHydrator"/>.
        /// Code before this extension
        /// @code
        /// var dr = cmd.ExecuteReader();
        /// var dataList = MyType.FillCollection(dr);
        /// @endcode
        /// 
        /// After
        /// @code
        /// var dataList = cmd.ExecuteReader().FillCollection<MyType>();
        /// @endcode
        /// 
        /// <span class="attention" style="color:red">This method will close database connection.</span>
        /// 
        /// </summary>
        /// <param name="dr">
        /// Expect open data reader.
        /// </param>
        /// <typeparam name="T">
        /// Type that implements <see cref="IHydrator"/>.
        /// </typeparam>
        /// <returns>
        /// List filled with objects of given type.
        /// </returns>
        public static List<T> FillCollection<T>(this IDataReader dr) where T : IHydrator, new()
        {
            var retVal = new List<T>();
            FillCollection<T>(dr, true, retVal);
            return retVal;
        }

        /// <summary>
        /// Takes IDataReader and provide shorthand to call FillCollection based on <see cref="IHydrator"/>.
        /// For example see <see cref="FillCollection{T}(IDataReader)"/>.
        /// </summary>
        /// <param name="dr">
        /// Expect open data reader.
        /// </param>
        /// <param name="closeConnection">
        /// If true, after filling collection DataReader will be closed.
        /// </param>
        /// <typeparam name="T">
        /// Type that implements <see cref="IHydrator"/>.
        /// </typeparam>
        /// <returns>
        /// List filled with objects of given type.
        /// </returns>
        public static List<T> FillCollection<T>(this IDataReader dr, bool closeConnection)
            where T : IHydrator, new()
        {
            var retVal = new List<T>();
            FillCollection<T>(dr, closeConnection, retVal);
            return retVal;
        }

        /// \copydoc FillCollection<T>(IDataReader,bool)
        /// <param name="listToFill">
        /// Existing list to append into.
        /// </param>
        public static void FillCollection<T>(this IDataReader dr, bool closeConnection, IList listToFill)
            where T : IHydrator, new()
        {
            var obj = new T();
            obj.FillCollection(dr, closeConnection, listToFill);
        }

        /// <summary>
        /// Provide very fluent way to fill multiple collections / objects with chain of FillCollection, FillObject and NextResultSafely
        /// 
        /// <span class="attention" style="color:red">This method will do DataReader.Read() for fluent support.</span>
        /// 
        /// For example <see cref="FillCollection<T>(this IDataReader, IList)"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        public static IDataReader FillObject<T>([NotNull] this IDataReader dr, T src) where T : IHydrator, new()
        {
            if (dr.Read())
            {
                if (src == null) src = new T();
                src.FillObject(dr, false, false);
            }

            return dr;
        }

        /// <summary>
        /// The fill object based on current record that DataReader points to.
        /// Note: this method <b>does not call</b> DataReader.Read(), so please ensure you have performed DataReader.Read() before calling.
        /// 
        /// <span class="attention" style="color:red">This method will close database connection.</span>
        /// 
        /// </summary>
        /// <param name="dr">
        /// Object of IDataReader.
        /// </param>
        /// <typeparam name="T">
        /// Type that implement <see cref="IHydrator"/>
        /// </typeparam>
        /// <returns>
        /// New object with data filled from DataReader.
        /// </returns>
        public static T? FillObject<T>(this IDataReader dr) where T : IHydrator, new()
        {
            return FillObject<T>(dr, true);
        }

        /// <summary>
        /// The fill object based on current record that DataReader points to.
        /// Note: this method <b>does not call</b> DataReader.Read(), so please ensure you have performed DataReader.Read() before calling.
        /// 
        /// <span class="attention" style="color:red">This method will close database connection.</span>
        /// 
        /// </summary>
        /// <param name="dr">
        /// Object of IDataReader.
        /// </param>
        /// <param name="closeConnection">
        /// 
        /// </param>
        /// <typeparam name="T">
        /// Type that implement <see cref="IHydrator"/>
        /// </typeparam>
        /// <returns>
        /// New object with data filled from DataReader.
        /// </returns>
        public static T? FillObject<T>([NotNull] this IDataReader dr, bool closeConnection) where T : IHydrator, new()
        {
            if (dr.Read())
            {
                var retVal = new T();
                retVal.FillObject(dr, closeConnection, false);
                return retVal;
            }

            return default;
        }

        /// <summary>
        /// The fill object based on current record that DataReader points to.
        /// Note: this method <b>does not call</b> DataReader.Read(), so please ensure you have performed DataReader.Read() before calling.
        /// 
        /// <span class="attention" style="color:red">This method will close database connection.</span>
        /// 
        /// </summary>
        /// <param name="dr">
        /// Object of IDataReader.
        /// </param>
        /// <param name="closeConnection">
        /// 
        /// </param>
        /// <typeparam name="T">
        /// Type that implement <see cref="IHydrator"/>
        /// </typeparam>
        /// <returns>
        /// New object with data filled from DataReader.
        /// </returns>
        public static T? FillObject<T>([NotNull] this IDataReader dr, bool closeConnection, bool doDrRead) where T : IHydrator, new()
        {
            if (doDrRead)
            {
                if (dr.Read() == false) return default;
            }

            var retVal = new T();
            retVal.FillObject(dr, closeConnection, false);
            return retVal;
        }

        /// <summary>
        /// FillCollection and FillObject both need type to implement IHydrator.
        /// There are many cases where its not possible specifically if type is provided by 3rd party.
        /// 
        /// In such cases you can shorthand like
        /// @code
        /// var list = new List<MyType>();
        /// cmd.ExecuteReader().ForEachRecord( (dr) => {
        ///     var obj = new MyType();
        ///     obj.SomeProp = dr.GetInt32(0);
        ///     obj.Other = dr.GetString(1);
        ///     list.Add(obj);
        /// });
        /// @endcode
        /// 
        /// In contrast code before this extension
        /// @code
        /// var dr = cmd.ExecuteReader();
        /// var list = new List<MyType>();
        /// try
        /// {
        ///     while(dr.Read())
        ///     {
        ///         var obj = new MyType();
        ///         obj.SomeProp = dr.GetInt32(0);
        ///         obj.Other = dr.GetString(1);
        ///         list.Add(obj);
        ///     }
        /// }
        /// finally
        /// {
        ///     dr.CloseSafely();
        /// }
        /// @endcode
        /// 
        /// <span class="attention" style="color:red">This method will close database connection.</span>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="action">Probably a lambda method that takes one IDataReader parameter. (dr) => {};</param>
        public static void ForEachRecord(this IDataReader dr, Action<IDataReader> action)
        {
            ForEachRecord(dr, true, action);
        }

        /// <summary>
        /// This overload of <see cref="ForEachRecord(this IDataReader, Action<IDataReader>)"/> is useful if you want to have
        /// fluent chain of code. This works in same way as <see cref="ForEachRecord(this IDataReader, Action<IDataReader>)"/> with
        /// difference is, this allows you to keep DataReader open. It returns same DataReader for further fluent calls.
        /// 
        /// Fluent example
        /// @code
        /// var tasks = new List<Tasks>();
        /// var comments = new List<Comments>();
        /// cmd.ExecuteReader().ForEachRecord( false, (dr) => {
        ///         var obj = new Task();
        ///         obj.TaskId = dr.GetInt32(0);
        ///         obj.Title = dr.GetString(1);
        ///         tasks.Add(obj);
        ///     })
        ///     .NextResultSafely()
        ///     .ForEachRecord( false, (dr) => {
        ///         var obj = new Comment();
        ///         obj.CommentId = dr.GetInt32(0);
        ///         obj.Comment = dr.GetString(1);
        ///         comments.Add(obj);
        ///     });
        /// @endcode
        /// 
        /// <span class="attention" style="color:red">This method will close database connection.</span>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="action">Probably a lambda method that takes one IDataReader parameter. (dr) => {};</param>
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
        /// Close reader within try catch block.
        /// Code before extension.
        /// @code
        /// try
        /// {
        ///     while(dr.Read())
        ///     {
        ///         ......
        ///     }
        /// }
        /// finally
        /// {
        ///     try
        ///     {
        ///         dr.Close();
        ///     }
        ///     catch
        ///     {
        ///     }
        /// }
        /// @endcode
        /// 
        /// Code after extension.
        /// @code
        /// try
        /// {
        ///     while(dr.Read())
        ///     {
        ///         ......
        ///     }
        /// }
        /// finally
        /// {
        ///     dr.CloseSafely();
        /// }
        /// @endcode
        /// </summary>
        /// <param name="dr"></param>
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
        /// Move to next result set in DataReader, reduce try catch code.
        /// Code before extension.
        /// @code
        /// try
        /// {
        ///     while(dr.Read())
        ///     {
        ///         ......
        ///     }
        ///     try
        ///     {
        ///         dr.NextResult();
        ///         while(dr.Read())
        ///         {
        ///             ......
        ///         }
        ///     }
        ///     catch
        ///     {
        ///     }
        /// }
        /// finally
        /// {
        ///     try
        ///     {
        ///         dr.Close();
        ///     }
        ///     catch
        ///     {
        ///     }
        /// }
        /// @endcode
        /// 
        /// Code after extension.
        /// @code
        /// try
        /// {
        ///     while(dr.Read())
        ///     {
        ///         ......
        ///     }
        ///     dr.NextResultSafely();
        ///     while(dr.Read())
        ///     {
        ///         ......
        ///     }
        /// }
        /// finally
        /// {
        ///     dr.CloseSafely();
        /// }
        /// @endcode
        /// 
        /// For fluent examples
        /// + <see cref="FillCollection<T>(this IDataReader,IList)"/>
        /// + <see cref="ForEachRecord(this IDataReader,bool,Action<IDataReader>)"/> 
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns>Returns source DataReader for fluent calls.</returns>
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
        /// Similar to ForEachRecord but takes array of Actions. Each action is related to result set.
        /// Meaning it will call dr.NextResult() after each dr.Read() is false.
        /// 
        /// <span class="attention" style="color:red">This method will close database connection.</span>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="action"></param>
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
        /// Calls NoRecord function if there are no-record found in given DataReader.
        /// It checks records by checking FieldCount rather than Read function, otherwise it might mess up other read operations.
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="noRecordAction"></param>
        /// <returns></returns>
        public static System.Data.IDataReader NoRecord([NotNull] this System.Data.IDataReader dr, [NotNull] Action noRecordAction)
        {
            if (dr.FieldCount == 0) noRecordAction();
            return dr;
        }
    }
}
