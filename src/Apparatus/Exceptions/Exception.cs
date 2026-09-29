// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Exception.cs" company="Toshal Infotech">
//   Toshal - http://www.ToshalInfotech.com
//   Copyright (c) 2015-2016
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

namespace Apparatus.Exceptions
{
    /// <summary>
    /// Provides common exception that has Error code facility required for batter UI handling and processing.
    /// </summary>
    public class Exception : System.Exception
    {
        /// <summary>Creates an exception with no message and an empty <see cref="ErrorCode"/>.</summary>
        public Exception() { }
        /// <summary>Creates an exception with a message.</summary>
        /// <param name="message">The error message.</param>
        public Exception(string message) : base(message) { }
        /// <summary>Creates an exception with a message and the exception that caused it.</summary>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The exception that caused this one.</param>
        public Exception(string message, System.Exception inner) : base(message, inner) { }

        /// <summary>Creates an exception with an error code and a message.</summary>
        /// <param name="errorCode">A short code the UI or caller can use to decide what to show, for example <c>"USER_NOT_FOUND"</c>.</param>
        /// <param name="message">The error message.</param>
        public Exception(string errorCode, string message) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>Creates an exception with an error code, a message and the exception that caused it.</summary>
        /// <param name="errorCode">A short code the UI or caller can use to decide what to show.</param>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The exception that caused this one.</param>
        public Exception(string errorCode, string message, System.Exception inner) : base(message, inner)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Gets or sets a short machine readable code for this error. Empty when no code was given.
        /// </summary>
        public string ErrorCode { get; set; } = string.Empty;
    }
}
