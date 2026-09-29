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
    /// Basic clean-up of text typed by users before it is shown or stored.
    /// Choose what to clean with <see cref="FilterFlag"/>, then call <see cref="InputFilter"/> or <see cref="ValidateInput"/>.
    /// </summary>
    /// <remarks>
    /// These filters are simple text replacements. They help, but they are <b>not</b> a full security solution.
    /// Always also use proper HTML encoding when writing user text into a page, and parameterized queries for SQL.
    /// The code is based on the input filters of the old DotNetNuke project.
    /// </remarks>
    public static class InputFilters
    {
        /// <summary>
        /// Options for <see cref="InputFilter"/>. Combine several with <c>|</c>, for example <c>FilterFlag.NoMarkup | FilterFlag.NoScripting</c>.
        /// </summary>
        [Flags]
        public enum FilterFlag
        {
            /// <summary>
            /// Replaces line breaks with <c>&lt;br /&gt;</c> so multi-line text keeps its shape in HTML. Ignored when <see cref="NoSQL"/> is also set.
            /// </summary>
            MultiLine = 1,

            /// <summary>
            /// If the text contains HTML tags, HTML-encodes the whole text so the tags are shown as plain text. Ignored when <see cref="NoSQL"/> is also set.
            /// </summary>
            NoMarkup = 2,

            /// <summary>
            /// Replaces risky parts such as <c>&lt;script&gt;</c>, <c>&lt;iframe&gt;</c>, <c>javascript:</c> and <c>onerror</c> with a space. Ignored when <see cref="NoSQL"/> is also set.
            /// </summary>
            NoScripting = 4,

            /// <summary>
            /// Replaces common SQL words and symbols (such as <c>select</c>, <c>drop</c>, <c>--</c>, <c>;</c>) with a space and doubles single quotes.
            /// When set, the <see cref="MultiLine"/>, <see cref="NoMarkup"/> and <see cref="NoScripting"/> filters are skipped.
            /// </summary>
            NoSQL = 8,

            /// <summary>
            /// Removes all <c>&lt;</c> and <c>&gt;</c> characters.
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

        /// <summary>
        /// Cleans user text by applying the filters you choose.
        /// </summary>
        /// <param name="userInput">The text to clean. A <c>null</c> value gives an empty string.</param>
        /// <param name="filterType">The filters to apply. Combine several with <c>|</c>.</param>
        /// <returns>The cleaned text, or <see cref="string.Empty"/> when <paramref name="userInput"/> is <c>null</c>.</returns>
        /// <example>
        /// <code>
        /// string safe = InputFilters.InputFilter("&lt;b&gt;hi&lt;/b&gt;", InputFilters.FilterFlag.NoMarkup);
        /// // "&amp;lt;b&amp;gt;hi&amp;lt;/b&amp;gt;"
        /// </code>
        /// </example>
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

        /// <summary>
        /// Checks whether text is already clean, which means the chosen filters would not change it.
        /// </summary>
        /// <param name="userInput">The text to check.</param>
        /// <param name="filterType">The filters to apply. Combine several with <c>|</c>.</param>
        /// <returns><c>true</c> if the filtered text is the same as the input; otherwise <c>false</c>. A <c>null</c> input returns <c>false</c> because it is filtered to an empty string.</returns>
        /// <example>
        /// <code>
        /// InputFilters.ValidateInput("hello", InputFilters.FilterFlag.NoMarkup);     // true
        /// InputFilters.ValidateInput("&lt;b&gt;hi&lt;/b&gt;", InputFilters.FilterFlag.NoMarkup); // false
        /// </code>
        /// </example>
        public static bool ValidateInput(string userInput, FilterFlag filterType)
        {
            var filteredInput = InputFilter(userInput, filterType);

            return userInput == filteredInput;
        }
    }
}
