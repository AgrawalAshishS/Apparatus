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
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System;

    using System.Text.RegularExpressions;
    using System.ComponentModel;

    /// <summary>
    /// Everyday helpers for <see cref="string"/>: checking, comparing, changing case style (Pascal, camel, snake, kebab),
    /// splitting, compressing and converting.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly Regex PascalExpression = new Regex("([A-Z]+(?=$|[A-Z][a-z])|[A-Z]?[a-z]+)", RegexOptions.Compiled);


        /// <summary>
        /// Finds the position of the first match of any of the given values in this string.
        /// </summary>
        /// <param name="src">The text to search.</param>
        /// <param name="values">
        /// The words to look for. Each value is used as a regular expression, so characters such as <c>.</c>, <c>(</c> or <c>+</c>
        /// have a special meaning. The search is case sensitive.
        /// </param>
        /// <returns>The zero-based index of the earliest match, or <c>-1</c> if nothing matches.</returns>
        /// <example>
        /// <code>
        /// int index = "this is fine".IndexOfAny("is", "are", "f"); // 2 (the "is" inside "this")
        /// </code>
        /// </example>
        public static int IndexOfAny(this string src, params string[] values)
        {
            string pattern = string.Join("|", values);
            var regex = new Regex(pattern, RegexOptions.Compiled);
            var match = regex.Match(src);
            return match.Success ? match.Index : -1;
        }

        /// <summary>
        /// Finds the first match of any of the given values and tells you both where it is and which value matched.
        /// </summary>
        /// <param name="src">The text to search. Must not be <c>null</c> or empty.</param>
        /// <param name="values">
        /// The words to look for. Each value is used as a regular expression, so characters such as <c>.</c> or <c>(</c> have a special meaning.
        /// The search is case sensitive.
        /// </param>
        /// <returns>
        /// A tuple of (index, matched text). When nothing matches, it is <c>(-1, "")</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> or <paramref name="src"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="values"/> is empty, or <paramref name="src"/> is an empty string.</exception>
        /// <example>
        /// <code>
        /// var found = "This is subject".FindFirstIndexOfAny("is", "subject");
        /// Console.WriteLine(found.Item1); // 2 (the "is" inside "This")
        /// Console.WriteLine(found.Item2); // "is"
        /// </code>
        /// </example>
        public static Tuple<int, string> FindFirstIndexOfAny(this string src, params string[] values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values), "Values cannot be null.");
            if (values.Length == 0) throw new ArgumentException("Values cannot be empty.", nameof(values));
            ArgumentException.ThrowIfNullOrEmpty(src, "Source cannot be null or empty.");


            string pattern = string.Join("|", values);
            var regex = new Regex(pattern, RegexOptions.Compiled);
            var match = regex.Match(src);
            return match.Success ? new Tuple<int, string>(match.Index, match.Value) : new Tuple<int, string>(-1, string.Empty);
        }

        /// <summary>
        /// Checks whether this string is equal to any of the given values. The comparison is case sensitive.
        /// </summary>
        /// <param name="src">The text to look for.</param>
        /// <param name="values">The allowed values.</param>
        /// <returns><c>true</c> if <paramref name="src"/> equals at least one value; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// if ("to".In("abc", "pqr", "to")) { /* runs */ }
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static bool In(this string src, params string[] values)
        {
            return src.In(StringComparison.Ordinal, values);
        }

        /// <summary>
        /// Checks whether this string is equal to any item in a collection. The comparison is case sensitive.
        /// </summary>
        /// <param name="src">The text to look for.</param>
        /// <param name="values">The allowed values.</param>
        /// <returns><c>true</c> if <paramref name="src"/> equals at least one item; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// var allowed = new List&lt;string&gt; { "red", "green" };
        /// "red".In(allowed); // true
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static bool In(this string src, IEnumerable<string> values)
        {
            return src.In(StringComparison.Ordinal, values);
        }

        /// <summary>
        /// Checks whether this string is equal to any of the given values, using the comparison rule you choose.
        /// </summary>
        /// <param name="src">The text to look for.</param>
        /// <param name="comparison">How to compare, for example <see cref="StringComparison.OrdinalIgnoreCase"/> to ignore upper and lower case.</param>
        /// <param name="values">The allowed values.</param>
        /// <returns><c>true</c> if <paramref name="src"/> equals at least one value; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// "RED".In(StringComparison.OrdinalIgnoreCase, "red", "green"); // true
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static bool In(this string src, StringComparison comparison, params string[] values)
        {
            return values.Any(value => src.Equals(value, comparison));
        }

        /// <summary>
        /// Checks whether this string is equal to any item in a collection, using the comparison rule you choose.
        /// </summary>
        /// <param name="src">The text to look for.</param>
        /// <param name="comparison">How to compare, for example <see cref="StringComparison.OrdinalIgnoreCase"/>.</param>
        /// <param name="values">The allowed values.</param>
        /// <returns><c>true</c> if <paramref name="src"/> equals at least one item; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// var allowed = new[] { "red", "green" };
        /// "GREEN".In(StringComparison.OrdinalIgnoreCase, allowed); // true
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static bool In(this string src, StringComparison comparison, IEnumerable<string> values)
        {
            return values.Any(value => src.Equals(value, comparison));
        }

        /// <summary>
        /// Checks whether the string is <c>null</c> or has no characters. Same as <see cref="string.IsNullOrEmpty(string)"/>, written as a method on the string.
        /// </summary>
        /// <param name="str">The string to check. May be <c>null</c>.</param>
        /// <returns><c>true</c> if <paramref name="str"/> is <c>null</c> or <c>""</c>; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// "".IsNullOrEmpty();  // true
        /// " ".IsNullOrEmpty(); // false
        /// </code>
        /// </example>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Checks whether the string is <c>null</c>, empty, or has only white space. Same as <see cref="string.IsNullOrWhiteSpace(string)"/>, written as a method on the string.
        /// </summary>
        /// <param name="str">The string to check. May be <c>null</c>.</param>
        /// <returns><c>true</c> if <paramref name="str"/> is <c>null</c>, empty or only white space; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// "  ".IsNullOrWhiteSpace(); // true
        /// </code>
        /// </example>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Checks whether the string is <c>null</c>, empty, or has only white space.
        /// </summary>
        /// <param name="src">The string to check. May be <c>null</c>.</param>
        /// <returns><c>true</c> if there is no visible text; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// "  ".IsEmpty();   // true
        /// "abc".IsEmpty();  // false
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static bool IsEmpty(this string src)
        {
            return IsNullOrEmpty(src) || IsNullOrWhiteSpace(src);
        }

        /// <summary>
        /// Checks whether the string has visible text. This is the opposite of <see cref="IsEmpty(string)"/>.
        /// </summary>
        /// <param name="src">The string to check. May be <c>null</c>.</param>
        /// <returns><c>true</c> if the string has at least one character that is not white space; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// "abc".IsNotEmpty(); // true
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static bool IsNotEmpty(this string src)
        {
            return !IsEmpty(src);
        }

        /// <summary>
        /// Checks that this string is different from every one of the given values. The comparison is case sensitive.
        /// </summary>
        /// <param name="src">The text to check.</param>
        /// <param name="values">The values it must not match.</param>
        /// <returns><c>true</c> if <paramref name="src"/> matches none of the values; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// "blue".NotIn("red", "green"); // true
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static bool NotIn(this string src, params string[] values)
        {
            return values.All(value => src != value);
        }

        /// <summary>
        /// Checks a string against a collection of values that it should not match.
        /// </summary>
        /// <param name="src">The text to check.</param>
        /// <param name="values">The values it should not match.</param>
        /// <returns>
        /// <c>true</c> if at least one item in <paramref name="values"/> is different from <paramref name="src"/>; otherwise <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Known issue: unlike <see cref="NotIn(string, string[])"/>, this overload returns <c>true</c> as soon as <em>one</em> item differs,
        /// so <c>"red".NotIn(new[] { "red", "green" })</c> returns <c>true</c>. Use <c>!src.In(values)</c> until this is fixed.
        /// </remarks>
        [DebuggerStepThrough]
        public static bool NotIn(this string src, IEnumerable<string> values)
        {
            return values.Any(x => x != src);
        }

        /// <summary>
        /// Compares two strings and ignores upper and lower case, using the current culture.
        /// </summary>
        /// <param name="src">The first string.</param>
        /// <param name="otherString">The string to compare with.</param>
        /// <returns><c>true</c> if both are equal apart from case; otherwise <c>false</c>.</returns>
        /// <exception cref="NullReferenceException"><paramref name="src"/> is <c>null</c>.</exception>
        /// <example>
        /// <code>
        /// "abc".EqualsIgnoreCase("ABC"); // true
        /// </code>
        /// </example>
        public static bool EqualsIgnoreCase(this string src, string otherString)
        {
            return src.Equals(otherString, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Checks whether the string starts with some text, ignoring upper and lower case (current culture).
        /// </summary>
        /// <param name="src">The string to check.</param>
        /// <param name="find">The text it should start with.</param>
        /// <returns><c>true</c> if <paramref name="src"/> starts with <paramref name="find"/>; otherwise <c>false</c>.</returns>
        /// <exception cref="NullReferenceException"><paramref name="src"/> is <c>null</c>.</exception>
        /// <example>
        /// <code>
        /// "Hello World".StartsWithIgnoreCase("hello"); // true
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static bool StartsWithIgnoreCase(this string src, string find)
        {
            return src.StartsWith(find, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Checks whether the string ends with some text, ignoring upper and lower case (current culture).
        /// </summary>
        /// <param name="src">The string to check.</param>
        /// <param name="find">The text it should end with.</param>
        /// <returns><c>true</c> if <paramref name="src"/> ends with <paramref name="find"/>; otherwise <c>false</c>.</returns>
        /// <exception cref="NullReferenceException"><paramref name="src"/> is <c>null</c>.</exception>
        /// <example>
        /// <code>
        /// "Hello World".EndsWithIgnoreCase("WORLD"); // true
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static bool EndsWithIgnoreCase(this string src, string find)
        {
            return src.EndsWith(find, StringComparison.CurrentCultureIgnoreCase);
        }

        #region " Various Sentance Changes "

        /// <summary>
        /// Shorthand for <see cref="string.Format(string, object[])"/>, written as a method on the format text.
        /// </summary>
        /// <param name="src">The format text, for example <c>"Hi {0}"</c>.</param>
        /// <param name="args">The values to put in place of <c>{0}</c>, <c>{1}</c> and so on.</param>
        /// <returns>The formatted text.</returns>
        /// <exception cref="FormatException">The format text is not valid or needs more arguments than given.</exception>
        /// <example>
        /// <code>
        /// string message = "Hi {0}, you have {1} messages".ToFormat("Sam", 3); // "Hi Sam, you have 3 messages"
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static string ToFormat(this string src, params object[] args)
        {
            return string.Format(src, args);
        }

        /// <summary>
        /// Makes the first letter of every word upper case and the rest lower case, using the current culture.
        /// Words that are fully in upper case (like <c>"USA"</c>) are left as they are. Same as <see cref="ToTitleCase(string)"/>.
        /// </summary>
        /// <param name="src">The text to change. The original string is not changed.</param>
        /// <returns>The text in title case.</returns>
        /// <example>
        /// <code>
        /// "this is final".Capitalize(); // "This Is Final"
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static string Capitalize(this string src)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src);
        }

        /// <summary>
        /// Makes the first letter of every word upper case and the rest lower case, using the current culture.
        /// Words that are fully in upper case (like <c>"USA"</c>) are left as they are. Same as <see cref="Capitalize(string)"/>.
        /// </summary>
        /// <param name="src">The text to change. The original string is not changed.</param>
        /// <returns>The text in title case.</returns>
        /// <example>
        /// <code>
        /// "this is final".ToTitleCase(); // "This Is Final"
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static string ToTitleCase(this string src)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src);
        }


        /// <summary>
        /// Converts text to PascalCase: no spaces or separators, and each word starts with a capital letter.
        /// Anything that is not a letter or digit (space, dash, dot, @ and so on) starts a new word and is removed.
        /// A text written fully in capitals is first changed to lower case, so <c>"HTTP_CODE"</c> becomes <c>"HttpCode"</c>.
        /// </summary>
        /// <param name="src">The text to change. The original string is not changed. <c>null</c>, empty or white space is returned as it is.</param>
        /// <param name="preserveUnderscores"><c>true</c> to keep underscores in the result (the next letter is still capitalized). Default is <c>false</c>.</param>
        /// <param name="useRawNames"><c>true</c> to keep the letters exactly as they are (no capitals are added), while still removing separators. Underscores are kept. Default is <c>false</c>.</param>
        /// <param name="handleDigitStart"><c>true</c> to put an underscore in front when the result starts with a digit, so it can be used as a C# name. Default is <c>false</c>. See the remarks: this option currently drops the last character.</param>
        /// <returns>The PascalCase text.</returns>
        /// <remarks>
        /// Known issue: with <paramref name="handleDigitStart"/> set to <c>true</c> and a result that starts with a digit, the underscore is added
        /// but the last character of the result is cut off (<c>"1st place"</c> gives <c>"_1stPlac"</c>). Leave the option off until this is fixed.
        /// </remarks>
        /// <example>
        /// <code>
        /// "hello world".ToPascalCase();               // "HelloWorld"
        /// "user_first-name".ToPascalCase();           // "UserFirstName"
        /// "hello_world".ToPascalCase(true);           // "Hello_World"
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static string ToPascalCase(this string src, bool preserveUnderscores = false, bool useRawNames = false, bool handleDigitStart = false)
        {
            if (string.IsNullOrWhiteSpace(src)) return src;

            Span<char> convertedName = stackalloc char[src.Length];
            int index = 0;
            bool next2upper = true;
            bool allUpper = true;

            // checks for names in all CAPS
            foreach (char c in src)
            {
                if (char.IsLower(c))
                {
                    allUpper = false;
                    break;
                }
            }

            foreach (char c in src)
            {
                if (char.IsLetterOrDigit(c))
                {
                    if (!useRawNames)
                    {
                        if (next2upper)
                        {
                            convertedName[index++] = char.ToUpper(c);
                            next2upper = false;
                        }
                        else if (allUpper)
                        {
                            convertedName[index++] = char.ToLower(c);
                        }
                        else
                        {
                            convertedName[index++] = c;
                        }
                    }
                    else
                    {
                        convertedName[index++] = c;
                    }
                }
                else if (c == '_' && (preserveUnderscores || useRawNames))
                {
                    convertedName[index++] = c;
                    next2upper = true;
                }
                else
                {
                    next2upper = true;
                }
            }

            if (handleDigitStart && char.IsDigit(convertedName[0]))
            {
                convertedName = "_".AsSpan().Concat(convertedName.Slice(0, index)).ToArray();
            }

            return new string(convertedName.Slice(0, index));
        }


        /// <summary>
        /// Converts text to camelCase: like PascalCase, but the first letter is lower case.
        /// </summary>
        /// <param name="src">The text to change. The original string is not changed. <c>null</c>, empty or white space is returned as it is.</param>
        /// <param name="useCurrentCulture"><c>true</c> to lower-case the first letter with the current culture rules. Default is <c>false</c>, which uses the invariant culture.</param>
        /// <returns>The camelCase text.</returns>
        /// <example>
        /// <code>
        /// "ThisIs_it".ToCamelCase();      // "thisIsIt"
        /// "hello world".ToCamelCase();    // "helloWorld"
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static string ToCamelCase(this string src, bool useCurrentCulture = false)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                return src;
            }

            if (src.Length == 1)
            {
                return useCurrentCulture ? src.ToLower() : src.ToLowerInvariant();
            }

            src = src.ToPascalCase();
            Span<char> result = stackalloc char[src.Length];
            result[0] = useCurrentCulture ? char.ToLower(src[0]) : char.ToLowerInvariant(src[0]);
            src.AsSpan(1).CopyTo(result.Slice(1));

            return new string(result);
        }

        /// <summary>
        /// Converts text to kebab-case: lower case words joined by single dashes.
        /// It understands PascalCase, camelCase, spaces, underscores, existing dashes, acronyms (<c>"HTTPRequest"</c> becomes <c>"http-request"</c>) and numbers.
        /// Other special characters are treated as word separators and removed.
        /// </summary>
        /// <param name="input">The text to convert.</param>
        /// <returns>The kebab-case text. An empty string is returned when the input is <c>null</c>, empty or only white space.</returns>
        /// <example>
        /// <code>
        /// "HelloWorld".ToKebabCase();      // "hello-world"
        /// "some_value here".ToKebabCase(); // "some-value-here"
        /// "HTTPRequest".ToKebabCase();     // "http-request"
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static string ToKebabCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                // Covers empty string and strings with only whitespace
                return string.Empty;
            }

            // --- Use StringBuilder for efficient string construction ---
            // Estimate capacity: input length is a good start, might need slightly more for hyphens.
            // Adding a small buffer can prevent some reallocations for hyphen insertions.
            var sb = new StringBuilder(input.Length + Math.Min(input.Length / 2, 10));

            // --- State variables for single-pass processing ---
            // Tracks if the last character appended to sb was a hyphen to prevent duplicates (--).
            bool lastAppendedWasHyphen = true; // Initialize to true to prevent leading hyphen

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                // --- Check if the character is a letter or digit ---
                if (char.IsLetterOrDigit(c))
                {
                    bool isBoundary = false;

                    // --- Determine if a boundary exists BEFORE this character ---
                    if (i > 0) // Need a previous character to check for boundaries
                    {
                        char prevChar = input[i - 1];

                        // Boundary conditions:
                        // 1. Lower to Upper case (e.g., "helloWorld")
                        if (char.IsLower(prevChar) && char.IsUpper(c)) isBoundary = true;
                        // 2. Letter to Digit (e.g., "Version1")
                        else if (char.IsLetter(prevChar) && char.IsDigit(c)) isBoundary = true;
                        // 3. Digit to Letter (e.g., "1Version")
                        else if (char.IsDigit(prevChar) && char.IsLetter(c)) isBoundary = true;
                        // 4. Acronym detection (e.g., "HTTPRequest" -> insert before 'R')
                        //    Checks for Upper -> Upper -> Lower sequence
                        else if (char.IsUpper(prevChar) && char.IsUpper(c))
                        {
                            // Check if there is a next character and if it's lowercase
                            if (i + 1 < input.Length && char.IsLower(input[i + 1])) isBoundary = true;
                        }
                        // 5. Previous character was effectively a delimiter (non-letter/digit)
                        //    We ensure sb is not empty to avoid issues at the very start if input begins with delimiter
                        else if (!char.IsLetterOrDigit(prevChar) && sb.Length > 0) isBoundary = true;
                    }

                    // --- Append hyphen if a boundary is detected AND last appended wasn't one ---
                    if (isBoundary && !lastAppendedWasHyphen)
                    {
                        sb.Append('-');
                        lastAppendedWasHyphen = true; // Mark that we just added a hyphen
                    }

                    // --- Append the character in lower case ---
                    sb.Append(char.ToLowerInvariant(c));
                    lastAppendedWasHyphen = false; // Mark that the last added char was not a hyphen
                }
                // --- Handle non-letter/digit characters (treat as potential delimiters) ---
                else
                {
                    // If we encounter a delimiter and the builder has content
                    // and we haven't just added a hyphen, mark that the next
                    // alphanumeric character should be preceded by a hyphen.
                    // This effectively consolidates multiple delimiters.
                    if (sb.Length > 0 && !lastAppendedWasHyphen)
                    {
                        // Don't append anything now, but signal that the *next*
                        // alphanumeric character should get a hyphen prepended.
                        // We achieve this by ensuring lastAppendedWasHyphen remains false
                        // and relying on the boundary check #5 in the next iteration (if any).
                        // A direct flag isn't strictly necessary with check #5 added above.

                        // *However*, to handle cases like "already-kebab-case" correctly,
                        // where an existing hyphen IS a delimiter but should be preserved
                        // *if* single, we need slightly different logic.
                        // Let's refine: if it's specifically a hyphen, treat it like a potential boundary.
                        // All *other* delimiters just get skipped but might trigger boundary #5 later.

                        if (c == '-')
                        {
                            // If it's a hyphen, and we haven't just added one, add it.
                            if (!lastAppendedWasHyphen)
                            {
                                sb.Append('-');
                                lastAppendedWasHyphen = true;
                            }
                            // If last was already a hyphen, we skip this one (consolidate).
                        }
                        else
                        {
                            // For any other delimiter (space, _, !, @ etc.), don't append it.
                            // Just ensure the next alphanumeric char will trigger boundary #5.
                            // No state change needed here due to boundary check #5.
                        }
                    }
                    // If sb.Length is 0 (start of string) or last appended WAS a hyphen,
                    // simply ignore this delimiter character.
                }
            }

            // --- Final Cleanup: Remove trailing hyphen if it exists ---
            // This can happen if the input string ends with delimiters.
            if (sb.Length > 0 && sb[sb.Length - 1] == '-')
            {
                sb.Length--; // Efficiently remove the last character
            }

            return sb.ToString();
        }


        /// <summary>
        /// Converts text to snake_case: lower case words joined by underscores.
        /// </summary>
        /// <param name="src">The text to convert. <c>null</c>, empty or white space is returned as it is.</param>
        /// <returns>The snake_case text.</returns>
        /// <example>
        /// <code>
        /// "VeryLongName".ToSnakeCase(); // "very_long_name"
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static string ToSnakeCase(this string src)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                return src;
            }

            Span<char> result = stackalloc char[src.Length * 2];
            int index = 0;
            var previousCategory = default(UnicodeCategory?);

            for (var currentIndex = 0; currentIndex < src.Length; currentIndex++)
            {
                var currentChar = src[currentIndex];
                if (currentChar == '_')
                {
                    result[index++] = '_';
                    previousCategory = null;
                    continue;
                }

                var currentCategory = char.GetUnicodeCategory(currentChar);
                switch (currentCategory)
                {
                    case UnicodeCategory.UppercaseLetter:
                    case UnicodeCategory.TitlecaseLetter:
                        if (previousCategory == UnicodeCategory.SpaceSeparator ||
                            previousCategory == UnicodeCategory.LowercaseLetter ||
                            previousCategory != UnicodeCategory.DecimalDigitNumber &&
                            previousCategory != null &&
                            currentIndex > 0 &&
                            currentIndex + 1 < src.Length &&
                            char.IsLower(src[currentIndex + 1]))
                        {
                            result[index++] = '_';
                        }

                        currentChar = char.ToLower(currentChar);
                        break;

                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.DecimalDigitNumber:
                        if (previousCategory == UnicodeCategory.SpaceSeparator)
                        {
                            result[index++] = '_';
                        }
                        break;

                    default:
                        if (previousCategory != null)
                        {
                            previousCategory = UnicodeCategory.SpaceSeparator;
                        }
                        continue;
                }

                result[index++] = currentChar;
                previousCategory = currentCategory;
            }

            return new string(result.Slice(0, index));
        }

        #endregion

        /// <summary>
        /// Gets the MD5 hash of the text (UTF-8 bytes) as 32 lower case hex characters.
        /// </summary>
        /// <param name="src">The text to hash. Must not be <c>null</c>.</param>
        /// <returns>The hash, for example <c>"5d41402abc4b2a76b9719d911017c592"</c> for <c>"hello"</c>.</returns>
        /// <remarks>
        /// MD5 is fine for cache keys or quick checks, but it is <b>not</b> safe for passwords or security decisions.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="src"/> is <c>null</c>.</exception>
        /// <example>
        /// <code>
        /// string hash = "hello".ToHash(); // "5d41402abc4b2a76b9719d911017c592"
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static string ToHash(this string src)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(src)).Select(b => b.ToString("x2")));
        }

        /// <summary>
        /// Gets the MD5 hash of the text. Same as <see cref="ToHash(string)"/>.
        /// </summary>
        /// <param name="src">The text to hash. Must not be <c>null</c>.</param>
        /// <returns>The hash as 32 lower case hex characters.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="src"/> is <c>null</c>.</exception>
        /// <example>
        /// <code>
        /// string hash = "hello".ToMd5(); // "5d41402abc4b2a76b9719d911017c592"
        /// </code>
        /// </example>
        public static string ToMd5(this string src)
        {
            return ToHash(src);
        }

        /// <summary>
        /// Reads a yes/no style text as a <see cref="bool"/>. Understands <c>True</c>, <c>False</c>, <c>Y</c>, <c>N</c>, <c>Yes</c>, <c>No</c>, <c>T</c>, <c>F</c>, <c>1</c> and <c>0</c>, in any case.
        /// </summary>
        /// <param name="src">The text to read. May be <c>null</c>.</param>
        /// <param name="defaultValue">Returned when the text is <c>null</c>, empty, or not understood.</param>
        /// <returns>
        /// The value that the text means. A whole number is <c>true</c> unless it is 0.
        /// Any other text is decided by its first letter: Y or T means <c>true</c>, N or F means <c>false</c>.
        /// </returns>
        /// <example>
        /// <code>
        /// "yes".ToBoolean(false);     // true
        /// "0".ToBoolean(true);        // false
        /// "maybe".ToBoolean(true);    // true  (not understood, so the default is used)
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static bool ToBoolean(this string src, bool defaultValue)
        {
            if (src.IsEmpty()) return defaultValue;
            var result = defaultValue;
            if (bool.TryParse(src, out result)) return result;

            src = src.Trim();
            var intResult = -1;
            if (int.TryParse(src, out intResult)) return Convert.ToBoolean(intResult);

            //this take cares of Y, N, Yes, No
            var c = char.ToUpperInvariant(src[0]);
            if (c == 'Y') return true;
            if (c == 'N') return false;

            if (c == 'T') return true;
            if (c == 'F') return false;

            return defaultValue;
        }

        #region " Various Cuts and Splits "

        /// <summary>
        /// Gets the first characters of the string.
        /// </summary>
        /// <param name="src">The string to cut. Must not be <c>null</c>.</param>
        /// <param name="len">The number of characters wanted.</param>
        /// <returns>The first <paramref name="len"/> characters.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="src"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">See the remarks.</exception>
        /// <remarks>
        /// Known issue: the length check is the wrong way round, so this method only works when <paramref name="len"/> is exactly the length of the string.
        /// For any other length it throws <see cref="ArgumentOutOfRangeException"/>. Use <c>src.Substring(0, len)</c> until this is fixed.
        /// </remarks>
        public static string Left([NotNull] this string src, int len)
        {
            src.ThrowIfNull();
            src.Length.ThrowIfGreatorThan(len);

            return src.Substring(0, len);
        }

        /// <summary>
        /// Gets the last characters of the string.
        /// </summary>
        /// <param name="src">The string to cut. Must not be <c>null</c>.</param>
        /// <param name="len">The number of characters wanted.</param>
        /// <returns>The last <paramref name="len"/> characters.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="src"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">See the remarks.</exception>
        /// <remarks>
        /// Known issue: the length check is the wrong way round, so this method only works when <paramref name="len"/> is exactly the length of the string.
        /// For any other length it throws <see cref="ArgumentOutOfRangeException"/>. Use <c>src.Substring(src.Length - len)</c> until this is fixed.
        /// </remarks>
        public static string Right(this string src, int len)
        {
            src.ThrowIfNull();
            src.Length.ThrowIfGreatorThan(len);

            return src.Substring(src.Length - len, len);
        }

        /// <summary>
        /// Splits the string by a text separator (not just a single character).
        /// </summary>
        /// <param name="src">The string to split.</param>
        /// <param name="separator">The text that separates the parts, for example <c>", "</c>.</param>
        /// <returns>The parts. Empty parts are kept.</returns>
        /// <example>
        /// <code>
        /// string[] parts = "a, b, c".Split(", "); // { "a", "b", "c" }
        /// </code>
        /// </example>
        public static string[] Split([NotNull] this string src, string separator)
        {
            return src.Split(new[] { separator }, StringSplitOptions.None);
        }

        /// <summary>
        /// Splits the string by a text separator (not just a single character), with options.
        /// </summary>
        /// <param name="src">The string to split.</param>
        /// <param name="separator">The text that separates the parts.</param>
        /// <param name="options">For example <see cref="StringSplitOptions.RemoveEmptyEntries"/> to drop empty parts.</param>
        /// <returns>The parts.</returns>
        /// <example>
        /// <code>
        /// "a,,b".Split(",", StringSplitOptions.RemoveEmptyEntries); // { "a", "b" }
        /// </code>
        /// </example>
        public static string[] Split([NotNull] this string src, string separator, StringSplitOptions options)
        {
            return src.Split(new[] { separator }, options);
        }

        /// <summary>
        /// Splits the string into lines, using <see cref="Environment.NewLine"/> as the separator.
        /// On Windows that is a CR+LF pair, so text with only LF line breaks is not split there.
        /// </summary>
        /// <param name="src">The string to split.</param>
        /// <returns>The lines. Empty lines are kept.</returns>
        /// <example>
        /// <code>
        /// string[] lines = ("a" + Environment.NewLine + "b").SplitToLines(); // { "a", "b" }
        /// </code>
        /// </example>
        public static string[] SplitToLines([NotNull] this string src)
        {
            return src.Split(Environment.NewLine);
        }

        /// <summary>
        /// Splits the string into lines, using <see cref="Environment.NewLine"/> as the separator, with options.
        /// </summary>
        /// <param name="src">The string to split.</param>
        /// <param name="options">For example <see cref="StringSplitOptions.RemoveEmptyEntries"/> to drop empty lines.</param>
        /// <returns>The lines.</returns>
        /// <example>
        /// <code>
        /// var text = "a" + Environment.NewLine + Environment.NewLine + "b";
        /// text.SplitToLines(StringSplitOptions.RemoveEmptyEntries); // { "a", "b" }
        /// </code>
        /// </example>
        public static string[] SplitToLines([NotNull] this string src, StringSplitOptions options)
        {
            return src.Split(Environment.NewLine, options);
        }

        /// <summary>
        /// Puts spaces between the words of a camelCase or PascalCase text.
        /// </summary>
        /// <param name="src">The text to split. Must not be <c>null</c>.</param>
        /// <returns>The text with spaces between words.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="src"/> is <c>null</c>.</exception>
        /// <example>
        /// <code>
        /// "thisIsFinal".SplitCamelCase(); // "this Is Final"
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static string SplitCamelCase(this string src)
        {
            return Regex.Replace(Regex.Replace(src, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        /// <summary>
        /// Splits a PascalCase text into separate words. Groups of capitals (like <c>"XML"</c>) stay together.
        /// </summary>
        /// <param name="value">The PascalCase text to split. Must not be <c>null</c>.</param>
        /// <returns>The words separated by single spaces, with no space at the start or end.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        /// <example>
        /// <code>
        /// "ParseXMLFile".SplitPascalCase(); // "Parse XML File"
        /// </code>
        /// </example>
        public static string SplitPascalCase(this string value)
        {
            return PascalExpression.Replace(value, " $1").Trim();
        }

        #endregion


        /// <summary>
        /// Makes sure the string ends with some text. If it does not, the text is added at the end.
        /// </summary>
        /// <param name="src">The string to check. A <c>null</c> value is returned as <c>null</c>.</param>
        /// <param name="endsWith">The ending that is required.</param>
        /// <param name="comparisonType">How to compare the ending. Default is <see cref="StringComparison.Ordinal"/> (case sensitive).</param>
        /// <returns>The same string if it already ends with <paramref name="endsWith"/>; otherwise the string with <paramref name="endsWith"/> added.</returns>
        /// <example>
        /// <code>
        /// "this is ".EnsureEndsWith("final");     // "this is final"
        /// "this is final".EnsureEndsWith("final"); // "this is final"
        /// "folder".EnsureEndsWith("/");            // "folder/"
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static string EnsureEndsWith(this string src, string endsWith, StringComparison comparisonType = StringComparison.Ordinal)
        {
            if (src == null) return src;

            if (src.EndsWith(endsWith, comparisonType)) return src;

            return src + endsWith;
        }

        /// <summary>
        /// Makes sure the string starts with some text. If it does not, the text is added at the start.
        /// </summary>
        /// <param name="src">The string to check. A <c>null</c> value is returned as <c>null</c>.</param>
        /// <param name="starstWith">The beginning that is required.</param>
        /// <param name="comparisonType">How to compare the beginning. Default is <see cref="StringComparison.Ordinal"/> (case sensitive).</param>
        /// <returns>The same string if it already starts with <paramref name="starstWith"/>; otherwise the string with <paramref name="starstWith"/> added in front.</returns>
        /// <example>
        /// <code>
        /// "path".EnsureStartsWith("/"); // "/path"
        /// </code>
        /// </example>
        public static string EnsureStartsWith(this string src, string starstWith, StringComparison comparisonType = StringComparison.Ordinal)
        {
            if (src == null) return src;

            if (src.StartsWith(starstWith, comparisonType))
            {
                return src;
            }

            return starstWith + src;
        }

        /// <summary>
        /// Returns the characters of the string in reverse order.
        /// </summary>
        /// <param name="src">The string to reverse. Must not be <c>null</c>.</param>
        /// <returns>The reversed string.</returns>
        /// <remarks>
        /// The reversal works on single UTF-16 characters. Emoji and other characters made of two parts (surrogate pairs)
        /// or combining marks will not stay correct.
        /// </remarks>
        /// <exception cref="NullReferenceException"><paramref name="src"/> is <c>null</c>.</exception>
        /// <example>
        /// <code>
        /// "abc".Reverse(); // "cba"
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static string Reverse([NotNull] this string src)
        {
            Span<char> charArray = stackalloc char[src.Length];
            for (int i = 0; i < src.Length; i++)
            {
                charArray[i] = src[src.Length - 1 - i];
            }
            return new string(charArray);
        }

        /// <summary>
        /// Finds which of the given words appears in the string. Upper and lower case are ignored.
        /// </summary>
        /// <param name="src">The text to search.</param>
        /// <param name="values">The words to look for, in order of priority.</param>
        /// <returns>The first word from <paramref name="values"/> (in the order you gave them) that is found in <paramref name="src"/>, or <c>null</c> if none is found.</returns>
        /// <example>
        /// <code>
        /// "Order shipped today".FindFirst("SHIPPED", "delivered"); // "SHIPPED"
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static string FindFirst(this string src, params string[] values)
        {
            return FindFirst(src, StringComparison.OrdinalIgnoreCase, values);
        }

        /// <summary>
        /// Finds which of the given words appears in the string, using the comparison rule you choose.
        /// </summary>
        /// <param name="src">The text to search.</param>
        /// <param name="comparer">How to compare, for example <see cref="StringComparison.Ordinal"/> for case sensitive.</param>
        /// <param name="values">The words to look for, in order of priority.</param>
        /// <returns>The first word from <paramref name="values"/> (in the order you gave them) that is found in <paramref name="src"/>, or <c>null</c> if none is found.</returns>
        /// <example>
        /// <code>
        /// "Order shipped".FindFirst(StringComparison.Ordinal, "SHIPPED", "shipped"); // "shipped"
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static string FindFirst(this string src, StringComparison comparer, params string[] values)
        {
            foreach (var s in values)
            {
                if (src.IndexOf(s, comparer) >= 0)
                    return s;
            }
            return null;
        }

        /// <summary>
        /// Compresses text with GZip and returns it as a Base64 string. Useful for storing or sending long text in less space.
        /// </summary>
        /// <param name="src">The text to compress. <c>null</c> returns <c>null</c>.</param>
        /// <returns>A Base64 string that <see cref="Decompress(string)"/> can turn back into the original text.</returns>
        /// <example>
        /// <code>
        /// string packed = longText.Compress();
        /// string original = packed.Decompress();
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static string Compress(this string src)
        {
            if (src == null)
                return null;
            if (src == null)
                return null;

            var binary = Encoding.UTF8.GetBytes(src);
            using var ms = new MemoryStream();
            using (var zip = new GZipStream(ms, CompressionMode.Compress))
            {
                zip.Write(binary, 0, binary.Length);
            }

            var compressed = ms.ToArray();
            var compressedWithLength = new byte[compressed.Length + 4];
            Buffer.BlockCopy(compressed, 0, compressedWithLength, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(binary.Length), 0, compressedWithLength, 0, 4);

            return Convert.ToBase64String(compressedWithLength);
        }

        /// <summary>
        /// Turns text made by <see cref="Compress(string)"/> back into the original text.
        /// </summary>
        /// <param name="src">The Base64 compressed text. <c>null</c> returns <c>null</c>.</param>
        /// <returns>The original text.</returns>
        /// <exception cref="FormatException"><paramref name="src"/> is not valid Base64.</exception>
        /// <example>
        /// <code>
        /// string original = packed.Decompress();
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static string Decompress(this string src)
        {
            if (src == null)
                return null;

            if (src == null)
                return null;

            var compressed = Convert.FromBase64String(src);
            using var ms = new MemoryStream();
            var length = BitConverter.ToInt32(compressed, 0);
            ms.Write(compressed, 4, compressed.Length - 4);

            var binary = new byte[length];
            ms.Seek(0, SeekOrigin.Begin);
            using (var zip = new GZipStream(ms, CompressionMode.Decompress))
            {
                zip.Read(binary, 0, binary.Length);
            }

            return Encoding.UTF8.GetString(binary);
        }

        /// <summary>
        /// Reads text as an enum value, ignoring upper and lower case. Gives a default value when the text is empty or not a valid name.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="src">The text to read, for example <c>"monday"</c>. Spaces around it are ignored.</param>
        /// <param name="defaultValue">Returned when <paramref name="src"/> is <c>null</c>, empty, or cannot be read.</param>
        /// <returns>The enum value, or <paramref name="defaultValue"/>.</returns>
        /// <example>
        /// <code>
        /// DayOfWeek day = "friday".ToEnum(DayOfWeek.Monday);  // DayOfWeek.Friday
        /// DayOfWeek other = "nope".ToEnum(DayOfWeek.Monday);  // DayOfWeek.Monday
        /// </code>
        /// </example>
        [DebuggerStepThrough]
        public static T ToEnum<T>(this string src, T defaultValue) where T : IComparable, IFormattable
        {
            T convertedValue = defaultValue;

            if (!string.IsNullOrEmpty(src))
            {
                try
                {
                    convertedValue = (T)Enum.Parse(typeof(T), src.Trim(), true);
                }
                catch (ArgumentException)
                {
                }
            }

            return convertedValue;
        }

        /// <summary>
        /// Joins a list of strings into one string with a separator between them. Same as <see cref="string.Join(string, IEnumerable{string})"/>, written as a method on the list.
        /// </summary>
        /// <param name="source">The strings to join.</param>
        /// <param name="separator">The text to put between the items.</param>
        /// <returns>The joined text. An empty list gives an empty string.</returns>
        /// <example>
        /// <code>
        /// new[] { "a", "b", "c" }.Join(", "); // "a, b, c"
        /// </code>
        /// </example>
        public static string Join(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }

    }
}
