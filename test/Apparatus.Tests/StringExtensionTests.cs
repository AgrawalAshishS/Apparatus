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
        public void ToCamelCaseRemoves_()
        {
            Assert.Equal("abcXYZ", StringExtensions.ToCamelCase("abc_XYZ"));
            Assert.Equal("aBcXYZ", StringExtensions.ToCamelCase("aBc_XYZ"));
            Assert.Equal("abcXyz", StringExtensions.ToCamelCase("ABC_XYZ"));
            Assert.Equal("thisIsIt", StringExtensions.ToCamelCase("ThisIs_it"));
        }


        [Fact]
        public void ToKebabCaseTests()
        {
            Assert.Equal("very-long-name", StringExtensions.ToKebabCase("VeryLongName"));
            Assert.Equal("a-bc-xYZ", StringExtensions.ToKebabCase("aBc_XYZ"));
            Assert.Equal("abc-xyz", StringExtensions.ToKebabCase("ABC_XYZ"));
            Assert.Equal("abc-xYZ", StringExtensions.ToKebabCase("Abc_XYZ"));
            Assert.Equal("this-is-it", StringExtensions.ToKebabCase("ThisIs_it"));
            Assert.Equal("this-is-it", StringExtensions.ToKebabCase("This Is it"));
        }

        [Fact]
        public void ToSnakeCaseTests()
        {
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
    }
}
