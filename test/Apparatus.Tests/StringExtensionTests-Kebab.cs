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
    public partial class StringExtensionTests
    {
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

            Assert.Empty(actual);
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
            Assert.Equal("hello-world", StringExtensions.ToKebabCase("hello world"));
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
        [InlineData("MyV1Api", "my-v-1-api")]               // Number within mixed case
        [InlineData("ValueEndsWith9", "value-ends-with-9")]// Ends with single digit
        [InlineData("ValueEndsWith123", "value-ends-with-123")]// Ends with multiple digits
        [InlineData("Value Ends With 456", "value-ends-with-456")]// Ends with digits after space
        [InlineData("Value_Ends_With_789", "value-ends-with-789")]// Ends with digits after underscore
        [InlineData("123StartsWith", "123-starts-with")]  // Starts with multiple digits (Pascal)
        [InlineData("456 Starts With", "456-starts-with")] // Starts with multiple digits (space)
        [InlineData("789_Starts_With", "789-starts-with")] // Starts with multiple digits (underscore)
        [InlineData("V1StartsWithDigit", "v-1-starts-with-digit")] // Mix starting with letter then digit
        [InlineData("EndsWithDigitV1", "ends-with-digit-v-1")] // Mix ending with letter then digit
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
        [InlineData("你好世界", "你好世界")]         // Other scripts (depends on implementation's unicode handling)
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
        [InlineData("  --My   Awesome_TEST Case V1.0!@# ", "my-awesome-test-case-v-1-0")] // Changed from DataRow
        [InlineData(" ProcessHTTPRequest_And_LogResults_v2 ", "process-http-request-and-log-results-v-2")]
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
    }
}
