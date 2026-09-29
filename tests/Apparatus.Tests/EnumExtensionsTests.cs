using Apparatus;
using Xunit;

namespace ApparatusTests;

public class EnumExtensionsTests
{
    private enum Color { Red = 0, Green = 1, Blue = 5 }

    [Fact]
    public void ToCommaSeparatedList_DefaultUsesNumbers() =>
        Assert.Equal("0,5", new[] { Color.Red, Color.Blue }.ToCommaSeparatedList());

    [Fact]
    public void ToCommaSeparatedList_UseNames() =>
        Assert.Equal("Red,Blue", new[] { Color.Red, Color.Blue }.ToCommaSeparatedList(false));

    [Fact]
    public void ToCommaSeparatedList_Empty_ReturnsEmptyString() =>
        Assert.Equal(string.Empty, Array.Empty<Color>().ToCommaSeparatedList());

    [Fact]
    public void EnumToDictionary_ReturnsNamesAndValues()
    {
        var map = typeof(Color).EnumToDictionary();
        Assert.Equal(3, map.Count);
        Assert.Equal(0, map["Red"]);
        Assert.Equal(1, map["Green"]);
        Assert.Equal(5, map["Blue"]);
    }

    [Fact]
    public void EnumToDictionary_Null_Throws() =>
        Assert.Throws<NullReferenceException>(() => ((Type)null!).EnumToDictionary());
}
