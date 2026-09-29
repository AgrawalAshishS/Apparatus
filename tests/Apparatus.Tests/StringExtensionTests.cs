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
            Assert.True(StringExtensions.ToBoolean("123", false));
            Assert.False(StringExtensions.ToBoolean("TrueFalse", false));
            Assert.False(StringExtensions.ToBoolean(null, false));
        }
    }
}
