using System.Collections.Specialized;
using Apparatus;
using Xunit;

namespace ApparatusTests;

public class NameValueCollectionExtensionsTests
{
    [Fact]
    public void ToDictionary_CopiesKeysAndValues()
    {
        var source = new NameValueCollection { { "a", "1" }, { "b", "2" } };
        var result = source.ToDictionary();
        Assert.Equal(2, result.Count);
        Assert.Equal("1", result["a"]);
        Assert.Equal("2", result["b"]);
    }

    [Fact]
    public void ToDictionary_RepeatedKey_JoinsValuesWithComma()
    {
        var source = new NameValueCollection { { "a", "1" }, { "a", "2" } };
        Assert.Equal("1,2", source.ToDictionary()["a"]);
    }

    [Fact]
    public void ToDictionary_Empty_ReturnsEmpty() => Assert.Empty(new NameValueCollection().ToDictionary());

    [Fact]
    public void ToDictionary_Null_Throws() =>
        Assert.ThrowsAny<Exception>(() => ((NameValueCollection)null!).ToDictionary());
}
