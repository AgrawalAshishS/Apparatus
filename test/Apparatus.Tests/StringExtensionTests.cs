// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensionTestss.cs" company="Toshal Infotech">
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

using Apparatus;
using System;
using Xunit;

namespace ApparatusTests
{
    public class StringExtensionTests
    {
        [Fact]
        public void FindFirstIndexOfAny()
        {
            var abc = "this is test sentence";

            var result = abc.FindFirstIndexOfAny("test");
            Assert.Equal(8, result.Item1);
            Assert.Equal("test", result.Item2);
        }

        [Fact]
        public void FindFirstIndexOfAny2()
        {
            var abc = "this is test sentence";

            var result = abc.FindFirstIndexOfAny("is", "test");
            Assert.Equal(2, result.Item1);
            Assert.Equal("is", result.Item2);
        }

        [Fact]
        public void IndexOfAny()
        {
            var abc = "this is test sentence";

            var result = abc.IndexOfAny("test");
            Assert.Equal(8, result);
        }

        [Fact]
        public void EnsureEndsWithIgnoreNullString()
        {
            Assert.Null(StringExtensions.EnsureEndsWith(null, "."));
        }

        [Fact]
        public void StartsWithIgoreCase()
        {
            Assert.True(StringExtensions.StartsWithIgnoreCase("abc", "ABC"));
            Assert.True(StringExtensions.StartsWithIgnoreCase("abc", "abc"));
            Assert.True(StringExtensions.StartsWithIgnoreCase("ABC", "abc"));
            Assert.Throws<NullReferenceException>(() => { Assert.False(StringExtensions.StartsWithIgnoreCase(null, "abc")); });

            Assert.True(StringExtensions.StartsWithIgnoreCase("abc x", "ABC"));
            Assert.True(StringExtensions.StartsWithIgnoreCase("Abc x", "a"));
        }

        [Fact]
        public void EndsWithIgoreCase()
        {
            Assert.True(StringExtensions.EndsWithIgnoreCase("abc", "ABC"));
            Assert.True(StringExtensions.EndsWithIgnoreCase("abc", "abc"));
            Assert.True(StringExtensions.EndsWithIgnoreCase("ABC", "abc"));
            Assert.Throws<NullReferenceException>(() => { Assert.False(StringExtensions.EndsWithIgnoreCase(null, "abc")); });

            Assert.True(StringExtensions.EndsWithIgnoreCase("x abc", "ABC"));
            Assert.True(StringExtensions.EndsWithIgnoreCase("X Abc", "c"));
        }

        [Fact]
        public void ToPascalCaseTests()
        {
            Assert.Equal("", "".ToPascalCase());
            Assert.Equal("Hello", "hello".ToPascalCase());
            Assert.Equal("HelloWorld", "hello world".ToPascalCase());
            Assert.Equal("HelloWorld", "hello_world".ToPascalCase());
            Assert.Equal("Hello_World", "hello_world".ToPascalCase(true));
            Assert.Equal("AbcXyz", "ABC_XYZ".ToPascalCase());
            Assert.Equal("ABcXYZ", "aBc_XYZ".ToPascalCase());
            Assert.Equal("AbcXYZ", "Abc_XYZ".ToPascalCase());
        }

        [Fact]
        public void ToCamelCaseRemoves_()
        {
            Assert.Equal("", StringExtensions.ToCamelCase(""));
            Assert.Equal("abcXYZ", StringExtensions.ToCamelCase("abc_XYZ"));
            Assert.Equal("aBcXYZ", StringExtensions.ToCamelCase("aBc_XYZ"));
            Assert.Equal("abcXyz", StringExtensions.ToCamelCase("ABC_XYZ"));
            Assert.Equal("thisIsIt", StringExtensions.ToCamelCase("ThisIs_it"));
        }

        // --- Regular Cases ---

        [Theory] // Changed from DataTestMethod
        [InlineData("HelloWorld", "hello-world")] // Changed from DataRow
        [InlineData("helloWorld", "hello-world")]
        [InlineData("Hello World", "hello-world")]
        [InlineData("Hello_World", "hello-world")]
        [InlineData("Hello World Again", "hello-world-again")]
        [InlineData("hello_world_again", "hello-world-again")]
        [InlineData("HelloWorldAgain", "hello-world-again")]
        public void ToKebabCase_RegularInputs_ReturnsCorrectKebabCase(string input, string expected)
        {
            // Act
            string actual = input.ToKebabCase();

            // Assert
            Assert.Equal(expected, actual); // Changed from Assert.AreEqual
        }

        // --- Edge Cases ---

        [Fact] // Changed from TestMethod
        public void ToKebabCase_NullInput_ReturnsNullOrEmpty()
        {
            // Arrange
            string input = null;
            
            // Act
            string actual = input.ToKebabCase();

            // Assert
            Assert.Null(actual); // Changed for null check
            // Or if you expect empty: Assert.Empty(actual);
            // Or if you expect an exception:
            // Assert.Throws<ArgumentNullException>(() => input.ToKebabCase()); // Changed from ThrowsException
        }

        [Fact] // Changed from TestMethod
        public void ToKebabCase_EmptyInput_ReturnsEmpty()
        {
            // Arrange
            string input = "";
            //string expected = ""; // Not needed for Assert.Empty

            // Act
            string actual = input.ToKebabCase();

            // Assert
            Assert.Empty(actual); // Changed for empty string check

            Assert.Equal("", StringExtensions.ToKebabCase(""));
            Assert.Equal("", StringExtensions.ToKebabCase(" "));
            Assert.Equal("", StringExtensions.ToKebabCase("     "));
            Assert.Equal("123", StringExtensions.ToKebabCase("123"));
            Assert.Equal("", StringExtensions.ToKebabCase(null));
             // More Complex Cases
            Assert.Equal("this-is-a-test", StringExtensions.ToKebabCase("ThisIsATest"));
            Assert.Equal("this-is-another-test", StringExtensions.ToKebabCase("thisIsAnotherTest"));
            Assert.Equal("some-number-123", StringExtensions.ToKebabCase("SomeNumber123"));
            Assert.Equal("acronyms-like-http", StringExtensions.ToKebabCase("AcronymsLikeHTTP"));
            Assert.Equal("html-5", StringExtensions.ToKebabCase("HTML5"));
            Assert.Equal("hello--world", StringExtensions.ToKebabCase("hello world"));
        }

        [Fact] // Changed from TestMethod
        public void ToKebabCase_WhitespaceInput_ReturnsEmpty()
        {
            // Arrange
            string input = "   \t \n ";
            //string expected = ""; // Not needed for Assert.Empty

            // Act
            string actual = input.ToKebabCase();

            // Assert
            Assert.Empty(actual); // Assuming whitespace is trimmed and results in empty
        }

        [Fact] // Changed from TestMethod
        public void ToKebabCase_SingleWordLower_ReturnsSame()
        {
            // Arrange
            string input = "word";
            string expected = "word";

            // Act
            string actual = input.ToKebabCase();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact] // Changed from TestMethod
        public void ToKebabCase_SingleWordUpper_ReturnsLower()
        {
            // Arrange
            string input = "WORD";
            string expected = "word";

            // Act
            string actual = input.ToKebabCase();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact] // Changed from TestMethod
        public void ToKebabCase_SingleWordPascal_ReturnsLower()
        {
            // Arrange
            string input = "Word";
            string expected = "word";

            // Act
            string actual = input.ToKebabCase();

            // Assert
            Assert.Equal(expected, actual);
        }

        // --- Numeric and Text Variations ---

        [Theory] // Changed from DataTestMethod
        [InlineData("Version1", "version-1")] // Changed from DataRow
        [InlineData("Version 1", "version-1")]
        [InlineData("Version_1", "version-1")]
        [InlineData("1Version", "1-version")]              // Number preceding text (Pascal)
        [InlineData("1 version", "1-version")]             // Number preceding text (space)
        [InlineData("1_version", "1-version")]             // Number preceding text (underscore)
        [InlineData("Version1Update", "version-1-update")]
        [InlineData("Version1Update2", "version-1-update-2")]
        [InlineData("Version1_Update2", "version-1-update-2")]
        [InlineData("File123Name", "file-123-name")]       // Multiple digits in middle
        [InlineData("Player1Score", "player-1-score")]
        [InlineData("GL11Version", "gl-11-version")]       // Acronym-like before number
        [InlineData("MyV1Api", "my-v1-api")]               // Number within mixed case
        [InlineData("ValueEndsWith9", "value-ends-with-9")]// Ends with single digit
        [InlineData("ValueEndsWith123", "value-ends-with-123")]// Ends with multiple digits
        [InlineData("Value Ends With 456", "value-ends-with-456")]// Ends with digits after space
        [InlineData("Value_Ends_With_789", "value-ends-with-789")]// Ends with digits after underscore
        [InlineData("123StartsWith", "123-starts-with")]  // Starts with multiple digits (Pascal)
        [InlineData("456 Starts With", "456-starts-with")] // Starts with multiple digits (space)
        [InlineData("789_Starts_With", "789-starts-with")] // Starts with multiple digits (underscore)
        [InlineData("V1StartsWithDigit", "v1-starts-with-digit")] // Mix starting with letter then digit
        [InlineData("EndsWithDigitV1", "ends-with-digit-v1")] // Mix ending with letter then digit
        public void ToKebabCase_NumericTextVariations_ReturnsCorrectKebabCase(string input, string expected)
        {
            // Act
            string actual = input.ToKebabCase();

            // Assert
            // xUnit doesn't have a built-in message parameter like MSTest's Assert.AreEqual
            // If a test fails, the runner will show the input parameters.
            // For more complex debugging messages, you might use ITestOutputHelper
            // or simply format the message in Assert.True/False.
            Assert.Equal(expected, actual);
        }

        [Fact] // Changed from TestMethod
        public void ToKebabCase_OnlyNumbers_ReturnsSame()
        {
            // Arrange
            string input = "123456";
            string expected = "123456";

            // Act
            string actual = input.ToKebabCase();

            // Assert
            Assert.Equal(expected, actual);
        }

        // --- Upper Case Only variations ---

        [Theory] // Changed from DataTestMethod
        [InlineData("UPPERCASE", "uppercase")] // Changed from DataRow
        [InlineData("UPPER CASE", "upper-case")]
        [InlineData("UPPER_CASE", "upper-case")]
        [InlineData("HTTPRequest", "http-request")] // Acronyms (common case)
        [InlineData("NASA API Key", "nasa-api-key")] // Acronyms mixed
        [InlineData("SOME SQL STATEMENT", "some-sql-statement")]
        public void ToKebabCase_UpperCaseInputs_ReturnsCorrectKebabCase(string input, string expected)
        {
            // Act
            string actual = input.ToKebabCase();

            // Assert
            Assert.Equal(expected, actual);
        }

        // --- Lower Case Only variations ---

        [Theory] // Changed from DataTestMethod
        [InlineData("lowercase", "lowercase")] // Changed from DataRow
        [InlineData("lower case", "lower-case")]
        [InlineData("lower_case", "lower-case")]
        [InlineData("another lower case example", "another-lower-case-example")]
        public void ToKebabCase_LowerCaseInputs_ReturnsCorrectKebabCase(string input, string expected)
        {
            // Act
            string actual = input.ToKebabCase();

            // Assert
            Assert.Equal(expected, actual);
        }

        // --- Non-English Characters ---

        [Theory] // Changed from DataTestMethod
        [InlineData("CrèmeBrûlée", "crème-brûlée")] // Changed from DataRow
        [InlineData("Résumé Example", "résumé-example")]
        [InlineData("straße", "straße")]         // German sharp s preserved
        [InlineData("你好世界", "你好-世界")]         // Other scripts (depends on implementation's unicode handling)
        [InlineData("你好 World", "你好-world")]     // Mixed scripts
        public void ToKebabCase_NonEnglishChars_ReturnsCorrectKebabCase(string input, string expected)
        {
            // Note: Behavior with non-Latin scripts heavily depends on the underlying logic.
            // This test assumes basic preservation and splitting on case change/space.
            // Act
            string actual = input.ToKebabCase();

            // Assert
            Assert.Equal(expected, actual);
        }

        // --- Special Character Handling ---

        [Theory] // Changed from DataTestMethod
        [InlineData("Hello! World?", "hello-world")] // Changed from DataRow
        [InlineData("Hello@World#Again", "hello-world-again")]
        [InlineData("!@#Hello World%$^", "hello-world")]
        [InlineData("Hello!!World", "hello-world")]
        [InlineData("leading space", "leading-space")]
        [InlineData("trailing space ", "trailing-space")]
        [InlineData("  multiple   spaces  ", "multiple-spaces")]
        [InlineData("already-kebab-case", "already-kebab-case")]
        [InlineData("Multiple--Hyphens", "multiple-hyphens")]
        [InlineData("Multiple__Underscores", "multiple-underscores")]
        [InlineData("Mix __-- OfSeparators", "mix-of-separators")]
        [InlineData("-LeadingHyphen", "leading-hyphen")]
        [InlineData("TrailingHyphen-", "trailing-hyphen")]
        [InlineData("-BothEnds-", "both-ends")]
        [InlineData("---LotsOfHyphens---", "lots-of-hyphens")]
        [InlineData("Has (Parentheses) And Stuff", "has-parentheses-and-stuff")]
        public void ToKebabCase_SpecialCharsAndSpacing_ReturnsCorrectKebabCase(string input, string expected)
        {
            // Note: The exact behavior depends on implementation.
            // Act
            string actual = input.ToKebabCase();

            // Assert
            Assert.Equal(expected, actual);
        }

        // --- Combined Complex Cases ---

        [Theory] // Changed from DataTestMethod
        [InlineData("  --My   Awesome_TEST Case V1.0!@# ", "my-awesome-test-case-v1-0")] // Changed from DataRow
        [InlineData(" ProcessHTTPRequest_And_LogResults_v2 ", "process-http-request-and-log-results-v2")]
        [InlineData(" _SOME_Weird__Input_ VALUE123 ", "some-weird-input-value-123")] // Ends with number
        [InlineData("Handle NonEnglish like Straße Name", "handle-non-english-like-straße-name")]
        [InlineData("99_RED_Balloons--Floating Away__ ", "99-red-balloons-floating-away")] // Starts with number
        public void ToKebabCase_ComplexCombinedInputs_ReturnsCorrectKebabCase(string input, string expected)
        {
            // Act
            string actual = input.ToKebabCase();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToSnakeCaseTests()
        {
            Assert.Equal("", StringExtensions.ToSnakeCase(""));
            Assert.Equal("very_long_name", StringExtensions.ToSnakeCase("VeryLongName"));
            Assert.Equal("a_bc_xyz", StringExtensions.ToSnakeCase("aBc_XYZ"));
            Assert.Equal("abc_xyz", StringExtensions.ToSnakeCase("ABC_XYZ"));
            Assert.Equal("abc_xyz", StringExtensions.ToSnakeCase("Abc_XYZ"));
            Assert.Equal("this_is_it", StringExtensions.ToSnakeCase("ThisIs_it"));
            Assert.Equal("this_is_it", StringExtensions.ToSnakeCase("This Is it"));
        }

        [Fact]
        public void ToBooleanTests()
        {
            Assert.True(StringExtensions.ToBoolean("True", false));
            Assert.True(StringExtensions.ToBoolean("true", false));
            Assert.False(StringExtensions.ToBoolean("False", true));
            Assert.False(StringExtensions.ToBoolean("false", true));


            Assert.True(StringExtensions.ToBoolean("T", false));
            Assert.True(StringExtensions.ToBoolean("t", false));
            Assert.False(StringExtensions.ToBoolean("F", true));
            Assert.False(StringExtensions.ToBoolean("f", true));

            Assert.True(StringExtensions.ToBoolean("Yes", false));
            Assert.True(StringExtensions.ToBoolean("yes", false));
            Assert.False(StringExtensions.ToBoolean("No", true));
            Assert.False(StringExtensions.ToBoolean("no", true));

            Assert.True(StringExtensions.ToBoolean("Y", false));
            Assert.True(StringExtensions.ToBoolean("y", false));
            Assert.False(StringExtensions.ToBoolean("N", true));
            Assert.False(StringExtensions.ToBoolean("n", true));

            Assert.True(StringExtensions.ToBoolean("1", false));
            Assert.False(StringExtensions.ToBoolean("0", true));

            Assert.True(StringExtensions.ToBoolean(" True ", false));
            Assert.True(StringExtensions.ToBoolean(" 1 ", false));
            Assert.True(StringExtensions.ToBoolean(" Y ", false));
        }

        [Fact]
        public void FindFirstIndexOfAny_NullOrEmptyInputs()
        {
            Assert.Throws<ArgumentNullException>(() => ((string)null).FindFirstIndexOfAny("test"));
            Assert.Throws<ArgumentNullException>(() => "abc".FindFirstIndexOfAny(null));
            Assert.Throws<ArgumentException>(() => "abc".FindFirstIndexOfAny());
            //TODO: Uncomment when FindFirstIndexOfAny is implemented for empty string
            //Assert.Equal((-1, null), "".FindFirstIndexOfAny("test"));
        }

        [Fact]
        public void EnsureEndsWith_SpecialCharacters()
        {
            Assert.Equal("test.", StringExtensions.EnsureEndsWith("test", "."));
            Assert.Equal("test!", StringExtensions.EnsureEndsWith("test!", "!"));
            Assert.Equal("test@", StringExtensions.EnsureEndsWith("test@", "@"));
            Assert.Equal(".", StringExtensions.EnsureEndsWith("", "."));
            Assert.Null(StringExtensions.EnsureEndsWith(null, "."));
        }

        [Fact]
        public void StartsWithIgnoreCase_SpecialCharacters()
        {
            Assert.True(StringExtensions.StartsWithIgnoreCase("!abc", "!ABC"));
            Assert.False(StringExtensions.StartsWithIgnoreCase("abc", "!ABC"));
            Assert.True(StringExtensions.StartsWithIgnoreCase("123abc", "123ABC"));
            Assert.False(StringExtensions.StartsWithIgnoreCase("abc", "123ABC"));
        }

        [Fact]
        public void EndsWithIgnoreCase_SpecialCharacters()
        {
            Assert.True(StringExtensions.EndsWithIgnoreCase("abc!", "ABC!"));
            Assert.False(StringExtensions.EndsWithIgnoreCase("abc", "ABC!"));
            Assert.True(StringExtensions.EndsWithIgnoreCase("abc123", "ABC123"));
            Assert.False(StringExtensions.EndsWithIgnoreCase("abc", "123ABC"));
        }

        [Fact]
        public void ToPascalCase_NumbersAndSpecialCharacters()
        {
            Assert.Equal("123HelloWorld", "123 hello world".ToPascalCase());
            Assert.Equal("HelloWorld123", "hello world 123".ToPascalCase());
            Assert.Equal("Hello_World_123", "hello_world_123".ToPascalCase(true));
            Assert.Equal("HelloWorld", "hello@world".ToPascalCase());
        }

        [Fact]
        public void ToCamelCase_NumbersAndSpecialCharacters()
        {
            Assert.Equal("123HelloWorld", StringExtensions.ToCamelCase("123_hello_world"));
            Assert.Equal("helloWorld123", StringExtensions.ToCamelCase("hello_world_123"));
            Assert.Equal("helloWorld", StringExtensions.ToCamelCase("hello@world"));
        }

        [Fact]
        public void ToKebabCase_NumbersAndSpecialCharacters()
        {
            Assert.Equal("123-hello-world", StringExtensions.ToKebabCase("123HelloWorld"));
            Assert.Equal("hello-world-123", StringExtensions.ToKebabCase("HelloWorld123"));
            Assert.Equal("hello-world", StringExtensions.ToKebabCase("Hello@World"));
        }

        [Fact]
        public void ToSnakeCase_NumbersAndSpecialCharacters()
        {
            Assert.Equal("123_hello_world", StringExtensions.ToSnakeCase("123HelloWorld"));
            Assert.Equal("hello_world_123", StringExtensions.ToSnakeCase("HelloWorld123"));
            Assert.Equal("hello_world", StringExtensions.ToSnakeCase("Hello@World"));
        }

        [Fact]
        public void ToBoolean_InvalidInputs()
        {
            Assert.False(StringExtensions.ToBoolean("invalid", false));
            Assert.False(StringExtensions.ToBoolean("", false));
            Assert.False(StringExtensions.ToBoolean("123", false));
            Assert.False(StringExtensions.ToBoolean("TrueFalse", false));
            Assert.False(StringExtensions.ToBoolean(null, false));
        }
    }
}
