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

    using System.Text.RegularExpressions;

    /// <summary>
    /// Extensions for string.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly Regex PascalExpression = new Regex("([A-Z]+(?=$|[A-Z][a-z])|[A-Z]?[a-z]+)", RegexOptions.Compiled);


        /// <summary>
        /// Useful shorthand to find if string is present in given string.
        /// </summary>
        /// <example>
        /// @code
        /// if( "this is fine".IndexOfAny("is", "are", "f") > -1)
        /// @endcode
        /// </example>
        public static int IndexOfAny(this string src, params string[] values)
        {
            string pattern = string.Join("|", values.Select(w => Regex.Escape(w)));
            var m = Regex.Match(src, pattern);
            if (!m.Success) return -1;
            return m.Index;
        }

        /// <summary>
        /// Returns first string found from list.
        /// <example>
        /// @code
        /// var found = "This is subject".FindFirstIndexOfAny("is", "subject");
        /// Console.WriteLine("Index of item: " + found.Item1); //2 - is from th-is
        /// Console.WriteLine("Item found: " + found.Item2); //is
        /// @endcode
        /// </example>
        /// </summary>
        /// <returns>
        /// Tuple string that found with it's index in given source string.
        /// </returns>
        public static Tuple<int, string> FindFirstIndexOfAny(this string src, params string[] values)
        {
            string pattern = string.Join("|", values.Select(w => Regex.Escape(w)));
            var m = Regex.Match(src, pattern);
            if (!m.Success) return new Tuple<int, string>(-1, string.Empty);

            return new Tuple<int, string>(m.Index, m.Value);
        }

        /// <summary>
        /// Useful shorthand to find if string is present in given string.
        /// </summary>
        /// <example>
        /// @code
        /// if( "to".In("abc", "pqr", "to"))
        /// @endcode
        /// </example>
        [DebuggerStepThrough]
        public static bool In(this string src, params string[] values)
        {
            return src.In(StringComparison.Ordinal, values);
        }

        [DebuggerStepThrough]
        public static bool In(this string src, IEnumerable<string> values)
        {
            return src.In(StringComparison.Ordinal, values);
        }

        [DebuggerStepThrough]
        public static bool In(this string src, StringComparison comparison, params string[] values)
        {
            return values.Any(value => src.Equals(value, comparison));
        }

        [DebuggerStepThrough]
        public static bool In(this string src, StringComparison comparison, IEnumerable<string> values)
        {
            return values.Any(value => src.Equals(value, comparison));
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Check if string is Null, Empty or Whitespace.
        /// </summary>
        [DebuggerStepThrough]
        public static bool IsEmpty(this string src)
        {
            return IsNullOrEmpty(src) || IsNullOrWhiteSpace(src);
        }

        /// <summary>
        /// The is not empty. <see cref="IsEmpty(string)"/> 
        /// </summary>
        [DebuggerStepThrough]
        public static bool IsNotEmpty(this string src)
        {
            return !IsEmpty(src);
        }

        /// <summary>
        /// The not in. <see cref="In"/>
        /// </summary>
        [DebuggerStepThrough]
        public static bool NotIn(this string src, params string[] values)
        {
            return values.All(value => src != value);
        }

        [DebuggerStepThrough]
        public static bool NotIn(this string src, IEnumerable<string> values)
        {
            return values.Any(x => x != src);
        }

        /// <summary>
        /// Shorthand to compare strings ignoring case.
        /// @code
        /// if( "abc".EqualsIgnoreCase("ABC") )
        /// @endcode
        /// </summary>
        public static bool EqualsIgnoreCase(this string src, string otherString)
        {
            return src.Equals(otherString, StringComparison.CurrentCultureIgnoreCase);
        }

        [DebuggerStepThrough]
        public static bool StartsWithIgnoreCase(this string src, string find)
        {
            return src.StartsWith(find, StringComparison.CurrentCultureIgnoreCase);
        }

        [DebuggerStepThrough]
        public static bool EndsWithIgnoreCase(this string src, string find)
        {
            return src.EndsWith(find, StringComparison.CurrentCultureIgnoreCase);
        }

        #region " Various Sentance Changes "

        /// <summary>
        /// Shorthand for string formating.
        /// 
        /// For example
        /// @code
        /// var msg = string.Format("Hi {0}", name);
        /// @endcode
        /// 
        /// Can be written as
        /// @code
        /// var msg = "Hi {0}".ToFormat(name);
        /// @endcode
        /// </summary>
        /// <param name="src"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string ToFormat(this string src, params object[] args)
        {
            return string.Format(src, args);
        }

        /// <summary>
        /// Capitalize every letter after space.
        /// 
        /// @code
        /// "this is final" will become "This Is Final".
        /// @endcode
        /// </summary>
        [DebuggerStepThrough]
        public static string Capitalize(this string src)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src);
        }

        /// <summary>
        /// Capitalize every letter after space.
        /// 
        /// @code
        /// "this is final" will become "This Is Final".
        /// @endcode
        /// </summary>
        [DebuggerStepThrough]
        public static string ToTitleCase(this string src)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src);
        }


        /// <summary>
        /// Convert thisIs_it to ThisIsIt
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string ToPascalCase(this string src)
        {
            string convertedName = String.Empty;
            bool next2upper = true;
            bool allUpper = true;
            bool preserveUnderscores = false;
            bool useRawNames = false;

            // checks for names in all CAPS
            foreach (char c in src)
            {
                if (Char.IsLower(c))
                {
                    allUpper = false;
                    break;
                }
            }

            foreach (char c in src)
            {
                if (Char.IsLetterOrDigit(c))
                {
                    if (!useRawNames)
                    {
                        if (next2upper)
                        {
                            convertedName += c.ToString().ToUpper();
                            next2upper = false;
                        }
                        else if (allUpper)
                        {
                            convertedName += c.ToString().ToLower();
                        }
                        else
                        {
                            convertedName += c;
                        }
                    }
                    else
                    {
                        convertedName += c;
                    }
                }
                else if (c == '_' && (preserveUnderscores || useRawNames))
                {
                    convertedName += c;
                    next2upper = true;
                }
                else
                {
                    next2upper = true;
                }
            }

            if (Char.IsDigit(convertedName[0]))
            {
                convertedName = convertedName.Insert(0, "_");
            }

            return convertedName;
        }


        /// <summary>
        /// Convert ThisIs_it to thisIsIt
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
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

            return (useCurrentCulture ? char.ToLower(src[0]) : char.ToLowerInvariant(src[0])) + src.Substring(1);
        }

        /// <summary>
        /// Convert VeryLongName to very-long-name
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string ToKebabCase(this string src, bool useCurrentCulture = false)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                return src;
            }

            src = src.ToCamelCase();

            return useCurrentCulture
                ? Regex.Replace(src, "[a-z][A-Z]", m => m.Value[0] + "-" + char.ToLower(m.Value[1]))
                : Regex.Replace(src, "[a-z][A-Z]", m => m.Value[0] + "-" + char.ToLowerInvariant(m.Value[1]));
        }

        /// <summary>
        /// Convert VeryLongName to very_long_name. It lower case returned string.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string ToSnakeCase(this string src)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                return src;
            }

            var builder = new StringBuilder(src.Length + Math.Min(2, src.Length / 5));
            var previousCategory = default(UnicodeCategory?);

            for (var currentIndex = 0; currentIndex < src.Length; currentIndex++)
            {
                var currentChar = src[currentIndex];
                if (currentChar == '_')
                {
                    builder.Append('_');
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
                            builder.Append('_');
                        }

                        currentChar = char.ToLower(currentChar);
                        break;

                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.DecimalDigitNumber:
                        if (previousCategory == UnicodeCategory.SpaceSeparator)
                        {
                            builder.Append('_');
                        }
                        break;

                    default:
                        if (previousCategory != null)
                        {
                            previousCategory = UnicodeCategory.SpaceSeparator;
                        }
                        continue;
                }

                builder.Append(currentChar);
                previousCategory = currentCategory;
            }

            return builder.ToString();
        }

        #endregion

        /// <summary>
        /// Returns MD5 Hash for given string.
        /// </summary>
        [DebuggerStepThrough]
        public static string ToHash(this string src)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(src)).Select(b => b.ToString("x2")));
        }

        public static string ToMd5(this string src)
        {
            return ToHash(src);
        }

        /// <summary>
        /// Convert string to boolean. True, False, Y, N, Yes, No, T, F, 1, 0
        /// </summary>
        /// <param name="src"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
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

        public static string Left([NotNull] this string src, int len)
        {
            src.ThrowIfNull();
            src.Length.ThrowIfGreatorThan(len);

            return src.Substring(0, len);
        }

        public static string Right(this string src, int len)
        {
            src.ThrowIfNull();
            src.Length.ThrowIfGreatorThan(len);

            return src.Substring(src.Length - len, len);
        }

        /// <summary>
        /// Uses string.Split method to split given string by given separator.
        /// </summary>
        public static string[] Split([NotNull] this string src, string separator)
        {
            return src.Split(new[] { separator }, StringSplitOptions.None);
        }

        /// <summary>
        /// Uses string.Split method to split given string by given separator.
        /// </summary>
        public static string[] Split([NotNull] this string src, string separator, StringSplitOptions options)
        {
            return src.Split(new[] { separator }, options);
        }

        /// <summary>
        /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
        /// </summary>
        public static string[] SplitToLines([NotNull] this string src)
        {
            return src.Split(Environment.NewLine);
        }

        /// <summary>
        /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
        /// </summary>
        public static string[] SplitToLines([NotNull] this string src, StringSplitOptions options)
        {
            return src.Split(Environment.NewLine, options);
        }

        /// <summary>
        /// From camel case word to sentence.
        /// @code
        /// "thisIsFinal" will become "this Is Final"
        /// @endcode
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string SplitCamelCase(this string src)
        {
            return Regex.Replace(Regex.Replace(src, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        public static string SplitPascalCase(this string value)
        {
            return PascalExpression.Replace(value, " $1").Trim();
        }

        #endregion


        /// <summary>
        /// Ensure that string end with given value.
        /// @code
        /// "this is final" ends with "final" will return "this is final";
        /// but "this is " ends with "final" will append and return "this is final";
        /// @endcode
        /// </summary>
        /// <param name="src"></param>
        /// <param name="endsWith"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string EnsureEndsWith(this string src, string endsWith, StringComparison comparisonType = StringComparison.Ordinal)
        {
            if (src == null) return src;

            if (src.EndsWith(endsWith, comparisonType)) return src;

            return src + endsWith;
        }

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
        /// Returns reversed version of given string.
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static string Reverse([NotNull] this string src)
        {
            char[] charArray = src.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        [DebuggerStepThrough]
        public static string FindFirst(this string src, params string[] values)
        {
            return FindFirst(src, StringComparison.OrdinalIgnoreCase, values);
        }

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

        [DebuggerStepThrough]
        public static string Compress(this string src)
        {
            if (src == null)
                return null;
            byte[] binary = Encoding.UTF8.GetBytes(src);
            byte[] compressed;

            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress))
                {
                    zip.Write(binary, 0, binary.Length);
                }

                compressed = ms.ToArray();
            }

            byte[] compressedWithLength = new byte[compressed.Length + 4];

            Buffer.BlockCopy(compressed, 0, compressedWithLength, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(binary.Length), 0, compressedWithLength, 0, 4);

            return Convert.ToBase64String(compressedWithLength);
        }

        [DebuggerStepThrough]
        public static string Decompress(this string src)
        {
            if (src == null)
                return null;

            byte[] compressed = Convert.FromBase64String(src);
            byte[] binary;

            using (MemoryStream ms = new MemoryStream())
            {
                int length = BitConverter.ToInt32(compressed, 0);
                ms.Write(compressed, 4, compressed.Length - 4);

                binary = new byte[length];

                ms.Seek(0, SeekOrigin.Begin);

                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(binary, 0, binary.Length);
                }
            }

            return Encoding.UTF8.GetString(binary);
        }

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

        public static string Join(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }

    }
}
