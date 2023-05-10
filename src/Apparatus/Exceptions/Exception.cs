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
        public Exception() { }
        public Exception(string message) : base(message) { }
        public Exception(string message, System.Exception inner) : base(message, inner) { }

        public Exception(string errorCode, string message) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public Exception(string errorCode, string message, System.Exception inner) : base(message, inner)
        {
            this.ErrorCode = errorCode;
        }

        public string ErrorCode { get; set; } = string.Empty;
    }
}
