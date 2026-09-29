using Apparatus;
using Xunit;
using static Apparatus.InputFilters;

namespace ApparatusTests;

public class InputFiltersTests
{
    [Fact]
    public void InputFilter_Null_ReturnsEmpty() => Assert.Equal(string.Empty, InputFilter(null!, FilterFlag.NoMarkup));

    [Fact]
    public void InputFilter_NoFlags_ReturnsSameText() =>
        Assert.Equal("<b>x</b>", InputFilter("<b>x</b>", 0));

    // ---- NoAngleBrackets ----
    [Fact]
    public void NoAngleBrackets_RemovesBrackets() =>
        Assert.Equal("ab/a", InputFilter("<a>b</a>", FilterFlag.NoAngleBrackets));

    // ---- NoMarkup ----
    [Fact]
    public void NoMarkup_TextWithTags_IsHtmlEncoded() =>
        Assert.Equal("&lt;b&gt;hi&lt;/b&gt;", InputFilter("<b>hi</b>", FilterFlag.NoMarkup));

    [Fact]
    public void NoMarkup_PlainText_Unchanged() =>
        Assert.Equal("hello & bye", InputFilter("hello & bye", FilterFlag.NoMarkup));

    [Fact]
    public void NoMarkup_LoneLessThan_Unchanged() =>
        Assert.Equal("a < b", InputFilter("a < b", FilterFlag.NoMarkup));

    // ---- NoScripting ----
    [Theory]
    [InlineData("<script>alert(1)</script>hi", "hi")]
    [InlineData("x javascript:run() y", "x  run() y")]
    [InlineData("vbscript:run", "run")]
    [InlineData("<a onerror=x>", "<a  =x>")]
    [InlineData("<a onmouseover=x>", "<a  =x>")]
    [InlineData("<iframe src=x></iframe>after", "after")]
    [InlineData("<object data=x></object>after", "after")]
    [InlineData("<embed src=x></embed>after", "after")]
    [InlineData("<form action=x>a</form>b", "b")]
    public void NoScripting_RemovesDangerousParts(string input, string expectedTrimmed) =>
        Assert.Equal(expectedTrimmed, InputFilter(input, FilterFlag.NoScripting).Trim());

    [Fact]
    public void NoScripting_HarmlessText_Unchanged() =>
        Assert.Equal("just some text", InputFilter("just some text", FilterFlag.NoScripting));

    [Fact]
    public void NoScripting_IsCaseInsensitive() =>
        Assert.DoesNotContain("script", InputFilter("<SCRIPT>bad()</SCRIPT>ok", FilterFlag.NoScripting), StringComparison.OrdinalIgnoreCase);

    [Fact]
    public void NoScripting_EncodedInput_IsDecodedCleanedAndEncodedAgain()
    {
        var result = InputFilter("&lt;script&gt;bad()&lt;/script&gt;ok", FilterFlag.NoScripting);
        Assert.DoesNotContain("script", result, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("ok", result);
    }

    // ---- NoSQL ----
    [Fact]
    public void NoSql_RemovesKeywordsAndSymbols()
    {
        var result = InputFilter("x; DROP TABLE users -- select", FilterFlag.NoSQL);
        Assert.DoesNotContain("DROP", result, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("select", result, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(";", result);
        Assert.DoesNotContain("--", result);
    }

    [Fact]
    public void NoSql_DoublesSingleQuotes() => Assert.Equal("it''s", InputFilter("it's", FilterFlag.NoSQL));

    [Fact]
    public void NoSql_SkipsOtherFilters() =>
        Assert.Equal("<b>hi</b>", InputFilter("<b>hi</b>", FilterFlag.NoSQL | FilterFlag.NoMarkup));

    [Fact]
    public void NoSql_CombinesWithAngleBrackets() =>
        Assert.Equal("hi", InputFilter("<hi>", FilterFlag.NoSQL | FilterFlag.NoAngleBrackets));

    // ---- MultiLine ----
    [Fact]
    public void MultiLine_ReplacesNewLineWithBr() =>
        Assert.Equal("a<br />b", InputFilter("a" + Environment.NewLine + "b", FilterFlag.MultiLine));

    [Fact]
    public void MultiLine_ReplacesCarriageReturn() =>
        Assert.Equal("a<br />b", InputFilter("a\rb", FilterFlag.MultiLine));

    [Fact]
    public void Combined_Flags_ApplyTogether()
    {
        var result = InputFilter("<b>x</b>" + Environment.NewLine + "y", FilterFlag.NoMarkup | FilterFlag.MultiLine);
        Assert.Equal("&lt;b&gt;x&lt;/b&gt;<br />y", result);
    }

    // ---- ValidateInput ----
    [Fact]
    public void ValidateInput_CleanText_ReturnsTrue() => Assert.True(ValidateInput("hello", FilterFlag.NoMarkup | FilterFlag.NoScripting));

    [Fact]
    public void ValidateInput_TextWithMarkup_ReturnsFalse() => Assert.False(ValidateInput("<b>hi</b>", FilterFlag.NoMarkup));

    [Fact]
    public void ValidateInput_Null_ReturnsFalse() => Assert.False(ValidateInput(null!, FilterFlag.NoMarkup));
}
