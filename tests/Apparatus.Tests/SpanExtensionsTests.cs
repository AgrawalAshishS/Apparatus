using Apparatus;
using Xunit;

namespace ApparatusTests;

public class SpanExtensionsTests
{
    [Fact]
    public void Concat_JoinsInOrder()
    {
        ReadOnlySpan<int> a = new[] { 1, 2 };
        ReadOnlySpan<int> b = new[] { 3 };
        Assert.Equal(new[] { 1, 2, 3 }, a.Concat(b));
    }

    [Fact]
    public void Concat_EmptySpans_ReturnsEmptyArray() =>
        Assert.Empty(ReadOnlySpan<int>.Empty.Concat(ReadOnlySpan<int>.Empty));

    [Fact]
    public void Concat_OneEmpty_ReturnsOther()
    {
        ReadOnlySpan<string> a = new[] { "x" };
        Assert.Equal(new[] { "x" }, a.Concat(ReadOnlySpan<string>.Empty));
        Assert.Equal(new[] { "x" }, ReadOnlySpan<string>.Empty.Concat(a));
    }
}
