// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Toshal Infotech">
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
    public static class TaskExtensions
    {
        private static readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None,
            TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        /// <summary>
        /// This shorthand method hides complexity involved in running async method as blocking method.
        /// Given task will be awaited and then result returned.
        /// In case of exception, it will throw inner exception instead of default AggregateException.
        /// If there are multiple inner exceptions, it will throw AggregateException.
        /// You can use this short hand or can simply put this in your code
        /// <code>
        /// try
        /// {
        ///     return Task.Run(async() => await [ your method ]).Result;
        /// }
        /// catch (AggregateException ae)
        /// {
        ///     ae.Flatten();
        ///     if (ae.InnerExceptions.Count > 1) throw ae;
        ///     throw ae.InnerException;
        /// }
        /// </code>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task">Function that should be awaited.</param>
        /// <returns></returns>
        public static T Await<T>(this Task<T> task)
        {
            try
            {
                return _myTaskFactory.StartNew(() =>
                {
                    return task;
                }).Unwrap().GetAwaiter().GetResult();

                //return Task.Run(async () => await task).Result;
            }
            catch (AggregateException ae)
            {
                ae.Flatten();
                if (ae.InnerExceptions.Count > 1) throw ae;
                throw ae.InnerException;
            }
        }

        public static void Await(this Task task)
        {
            try
            {
                _myTaskFactory.StartNew(() =>
                {
                    return task;
                }).Unwrap().GetAwaiter().GetResult();

                //Task.Run(async () => await task);
            }
            catch (AggregateException ae)
            {
                ae.Flatten();
                if (ae.InnerExceptions.Count > 1) throw ae;
                throw ae.InnerException;
            }
        }

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return _myTaskFactory.StartNew(() =>
            {
                return func();
            }).Unwrap().GetAwaiter().GetResult();
        }

        public static void RunSync(Func<Task> func)
        {
            _myTaskFactory.StartNew(() =>
            {
                return func();
            }).Unwrap().GetAwaiter().GetResult();
        }
    }
}
