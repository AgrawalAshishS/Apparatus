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
    /// <summary>
    /// Helpers to run async code from normal (blocking) code.
    /// </summary>
    /// <remarks>
    /// Prefer <c>await</c> whenever you can. Use these only in places that cannot be async, such as a constructor or a <c>Main</c> method written without async.
    /// </remarks>
    public static class TaskExtensions
    {
        private static readonly TaskFactory _myTaskFactory = new TaskFactory(CancellationToken.None,
            TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        /// <summary>
        /// Waits for a task to finish and returns its result. Use it to call async code from non-async code.
        /// The task runs on the thread pool, so it does not deadlock on UI or ASP.NET classic synchronization contexts.
        /// If the task fails, the real exception is thrown, not an <see cref="AggregateException"/>.
        /// </summary>
        /// <typeparam name="T">Type of the task result.</typeparam>
        /// <param name="task">The task to wait for.</param>
        /// <returns>The result of the task.</returns>
        /// <exception cref="OperationCanceledException">The task was cancelled.</exception>
        /// <example>
        /// <code>
        /// string text = File.ReadAllTextAsync("a.txt").Await();
        /// </code>
        /// </example>
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

        /// <summary>
        /// Waits for a task to finish. Use it to call async code from non-async code.
        /// The task runs on the thread pool, so it does not deadlock on UI or ASP.NET classic synchronization contexts.
        /// If the task fails, the real exception is thrown, not an <see cref="AggregateException"/>.
        /// </summary>
        /// <param name="task">The task to wait for.</param>
        /// <exception cref="OperationCanceledException">The task was cancelled.</exception>
        /// <example>
        /// <code>
        /// Task.Delay(100).Await();
        /// </code>
        /// </example>
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

        /// <summary>
        /// Runs an async function on the thread pool, waits for it, and returns its result.
        /// </summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="func">The async function to run.</param>
        /// <returns>The result of the function.</returns>
        /// <example>
        /// <code>
        /// int value = TaskExtensions.RunSync(async () =&gt; { await Task.Delay(10); return 42; });
        /// </code>
        /// </example>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return _myTaskFactory.StartNew(() =>
            {
                return func();
            }).Unwrap().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Runs an async function on the thread pool and waits for it to finish.
        /// </summary>
        /// <param name="func">The async function to run.</param>
        /// <example>
        /// <code>
        /// TaskExtensions.RunSync(async () =&gt; await Task.Delay(10));
        /// </code>
        /// </example>
        public static void RunSync(Func<Task> func)
        {
            _myTaskFactory.StartNew(() =>
            {
                return func();
            }).Unwrap().GetAwaiter().GetResult();
        }
    }
}
