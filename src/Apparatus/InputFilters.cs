// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputFilters.cs" company="Toshal Infotech">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The input filters. Copied from old DotNetNuke source code. Provide basic input text filters.
    /// </summary>
    public static class InputFilters
    {
        /// <summary>
        /// The filter flag.
        /// </summary>
        [Flags]
        public enum FilterFlag
        {
            /// <summary>
            /// The multi line.
            /// </summary>
            MultiLine = 1,

            /// <summary>
            /// The no markup.
            /// </summary>
            NoMarkup = 2,

            /// <summary>
            /// The no scripting.
            /// </summary>
            NoScripting = 4,

            /// <summary>
            /// The no sql.
            /// </summary>
            NoSQL = 8,

            /// <summary>
            /// The no angle brackets.
            /// </summary>
            NoAngleBrackets = 16
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// This function uses Regex search strings to remove HTML tags which are
        ///     targeted in Cross-site scripting (XSS) attacks.  This function will evolve
        ///     to provide more robust checking as additional holes are found.
        /// </summary>
        /// <param name="strInput">
        /// This is the string to be filtered
        /// </param>
        /// <returns>
        /// Filtered UserInput
        /// </returns>
        /// <remarks>
        /// This is a private function that is used internally by the FormatDisableScripting function
        /// </remarks>
        /// <history>
        ///     [cathal]        3/06/2007   Created
        /// </history>
        /// -----------------------------------------------------------------------------
        private static string FilterStrings(string strInput)
        {
            // setup up list of search terms as items may be used twice
            var TempInput = strInput;
            var listStrings = new List<string>
                                  {
                                      "<script[^>]*>.*?</script[^><]*>",
                                      "<script",
                                      "<input[^>]*>.*?</input[^><]*>",
                                      "<object[^>]*>.*?</object[^><]*>",
                                      "<embed[^>]*>.*?</embed[^><]*>",
                                      "<applet[^>]*>.*?</applet[^><]*>",
                                      "<form[^>]*>.*?</form[^><]*>",
                                      "<option[^>]*>.*?</option[^><]*>",
                                      "<select[^>]*>.*?</select[^><]*>",
                                      "<iframe[^>]*>.*?</iframe[^><]*>",
                                      "<iframe.*?<",
                                      "<iframe.*?",
                                      "<ilayer[^>]*>.*?</ilayer[^><]*>",
                                      "<form[^>]*>",
                                      "</form[^><]*>",
                                      "onerror",
                                      "onmouseover",
                                      "javascript:",
                                      "vbscript:",
                                      "unescape",
                                      "alert[\\s(&nbsp;)]*\\([\\s(&nbsp;)]*'?[\\s(&nbsp;)]*[\"(&quot;)]?"
                                  };

            const RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Singleline;
            const string replacement = " ";

            // check if text contains encoded angle brackets, if it does it we decode it to check the plain text
            if (TempInput.Contains("&gt;") && TempInput.Contains("&lt;"))
            {
                // text is encoded, so decode and try again
                TempInput = System.Net.WebUtility.HtmlDecode(TempInput);
                TempInput = listStrings.Aggregate(
                    TempInput,
                    (current, s) => Regex.Replace(current, s, replacement, options));

                // Re-encode
                TempInput = System.Net.WebUtility.HtmlEncode(TempInput);
            }
            else
            {
                TempInput = listStrings.Aggregate(
                    TempInput,
                    (current, s) => Regex.Replace(current, s, replacement, options));
            }

            return TempInput;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// This function uses Regex search strings to remove HTML tags which are
        ///     targeted in Cross-site scripting (XSS) attacks.  This function will evolve
        ///     to provide more robust checking as additional holes are found.
        /// </summary>
        /// <param name="strInput">
        /// This is the string to be filtered
        /// </param>
        /// <returns>
        /// Filtered UserInput
        /// </returns>
        /// <remarks>
        /// This is a private function that is used internally by the InputFilter function
        /// </remarks>
        /// -----------------------------------------------------------------------------
        private static string FormatDisableScripting(string strInput)
        {
            var TempInput = strInput;
            TempInput = FilterStrings(TempInput);
            return TempInput;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// This filter removes angle brackets i.e.
        /// </summary>
        /// <param name="strInput">
        /// This is the string to be filtered
        /// </param>
        /// <returns>
        /// Filtered UserInput
        /// </returns>
        /// <remarks>
        /// This is a private function that is used internally by the InputFilter function
        /// </remarks>
        /// <history>
        ///     [Cathal] 	6/1/2006	Created to fufill client request
        /// </history>
        /// -----------------------------------------------------------------------------
        private static string FormatAngleBrackets(string strInput)
        {
            var TempInput = strInput.Replace("<", string.Empty);
            TempInput = TempInput.Replace(">", string.Empty);
            return TempInput;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// This filter removes CrLf characters and inserts br
        /// </summary>
        /// <param name="strInput">
        /// This is the string to be filtered
        /// </param>
        /// <returns>
        /// Filtered UserInput
        /// </returns>
        /// <remarks>
        /// This is a private function that is used internally by the InputFilter function
        /// </remarks>
        /// -----------------------------------------------------------------------------
        private static string FormatMultiLine(string strInput)
        {
            var TempInput = strInput.Replace(Environment.NewLine, "<br />");
            return TempInput.Replace("\r", "<br />");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// This function verifies raw SQL statements to prevent SQL injection attacks
        ///     and replaces a similar function (PreventSQLInjection) from the Common.Globals.vb module
        /// </summary>
        /// <param name="strSQL">
        /// This is the string to be filtered
        /// </param>
        /// <returns>
        /// Filtered UserInput
        /// </returns>
        /// <remarks>
        /// This is a private function that is used internally by the InputFilter function
        /// </remarks>
        /// -----------------------------------------------------------------------------
        private static string FormatRemoveSQL(string strSQL)
        {
            const string BadStatementExpression =
                ";|--|create|drop|select|insert|delete|update|union|sp_|xp_|exec|/\\*.*\\*/|declare|waitfor|%|&";
            return
                Regex.Replace(strSQL, BadStatementExpression, " ", RegexOptions.IgnoreCase | RegexOptions.Compiled)
                    .Replace("'", "''");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// This function determines if the Input string contains any markup.
        /// </summary>
        /// <param name="strInput">
        /// This is the string to be checked
        /// </param>
        /// <returns>
        /// True if string contains Markup tag(s)
        /// </returns>
        /// <remarks>
        /// This is a private function that is used internally by the InputFilter function
        /// </remarks>
        /// -----------------------------------------------------------------------------
        private static bool IncludesMarkup(string strInput)
        {
            const RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Singleline;
            const string pattern = "<[^<>]*>";
            return Regex.IsMatch(strInput, pattern, options);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// This function applies security filtering to the UserInput string.
        /// </summary>
        /// <param name="userInput">
        /// This is the string to be filtered
        /// </param>
        /// <param name="filterType">
        /// Flags which designate the filters to be applied
        /// </param>
        /// <returns>
        /// Filtered UserInput
        /// </returns>
        /// -----------------------------------------------------------------------------
        public static string InputFilter(string userInput, FilterFlag filterType)
        {
            if (userInput == null)
            {
                return string.Empty;
            }

            var tempInput = userInput;
            if ((filterType & FilterFlag.NoAngleBrackets) == FilterFlag.NoAngleBrackets)
            {
                tempInput = FormatAngleBrackets(tempInput);
            }

            if ((filterType & FilterFlag.NoSQL) == FilterFlag.NoSQL)
            {
                tempInput = FormatRemoveSQL(tempInput);
            }
            else
            {
                if ((filterType & FilterFlag.NoMarkup) == FilterFlag.NoMarkup && IncludesMarkup(tempInput))
                {
                    tempInput = System.Net.WebUtility.HtmlEncode(tempInput);
                }

                if ((filterType & FilterFlag.NoScripting) == FilterFlag.NoScripting)
                {
                    tempInput = FormatDisableScripting(tempInput);
                }

                if ((filterType & FilterFlag.MultiLine) == FilterFlag.MultiLine)
                {
                    tempInput = FormatMultiLine(tempInput);
                }
            }

            return tempInput;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// This function applies security filtering to the UserInput string, and reports
        ///     whether the input string is valid.
        /// </summary>
        /// <param name="userInput">
        /// This is the string to be filtered
        /// </param>
        /// <param name="filterType">
        /// Flags which designate the filters to be applied
        /// </param>
        /// <returns>
        /// </returns>
        /// -----------------------------------------------------------------------------
        public static bool ValidateInput(string userInput, FilterFlag filterType)
        {
            var filteredInput = InputFilter(userInput, filterType);

            return userInput == filteredInput;
        }
    }
}
