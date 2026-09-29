using Apparatus;
using Xunit;

namespace ApparatusTests;

public class ObjectExtensionsTests
{
    [Fact]
    public void FullNameWithAssembly_ReturnsTypeAndAssembly() =>
        Assert.Equal("Apparatus.Exceptions.Exception, Apparatus", new Apparatus.Exceptions.Exception().FullNameWithAssembly());

    [Fact]
    public void As_CorrectType_ReturnsSameInstance()
    {
        object o = "text";
        Assert.Same(o, o.As<string>());
    }

    [Fact]
    public void As_WrongType_Throws()
    {
        object o = "text";
        Assert.Throws<InvalidCastException>(() => o.As<List<int>>());
    }

    [Fact]
    public void To_StringToInt() => Assert.Equal(42, "42".To<int>());

    [Fact]
    public void To_StringToDecimal_UsesInvariantCulture() => Assert.Equal(1234.5m, "1234.5".To<decimal>());

    [Fact]
    public void To_StringToGuid()
    {
        var id = Guid.NewGuid();
        Assert.Equal(id, id.ToString().To<Guid>());
    }

    [Fact]
    public void To_IntToDouble() => Assert.Equal(3.0, 3.To<double>());

    [Fact]
    public void To_InvalidFormat_Throws() => Assert.Throws<FormatException>(() => "abc".To<int>());

    [Fact]
    public void CanBeCastTo_True() => Assert.True("text".CanBeCastTo<IEnumerable<char>>());

    [Fact]
    public void CanBeCastTo_False() => Assert.False("text".CanBeCastTo<int>());

    [Fact]
    public void CanBeCastTo_Null_ReturnsFalse() => Assert.False(((object)null!).CanBeCastTo<string>());

    [Theory]
    [InlineData(5, 1, 10, true)]
    [InlineData(1, 1, 10, true)]
    [InlineData(10, 1, 10, true)]
    [InlineData(0, 1, 10, false)]
    [InlineData(11, 1, 10, false)]
    public void Between_IncludesBothLimits(int value, int from, int to, bool expected) =>
        Assert.Equal(expected, value.Between(from, to));

    [Fact]
    public void Between_Strings() => Assert.True("b".Between("a", "c"));

    [Fact]
    public void In_Params_Found() => Assert.True("b".In("a", "b", "c"));

    [Fact]
    public void In_Params_NotFound() => Assert.False("z".In("a", "b", "c"));

    [Fact]
    public void In_Enumerable_Found() => Assert.True(2.In(new List<int> { 1, 2, 3 }));

    [Fact]
    public void In_Enumerable_NotFound() => Assert.False(9.In(new List<int> { 1, 2, 3 }));

    [Fact]
    public void If_Func_ConditionTrue_Applies() => Assert.Equal("ABC", "abc".If(true, s => s.ToUpperInvariant()));

    [Fact]
    public void If_Func_ConditionFalse_ReturnsOriginal() => Assert.Equal("abc", "abc".If(false, s => s.ToUpperInvariant()));

    [Fact]
    public void If_Action_ConditionTrue_RunsAndReturnsSame()
    {
        var list = new List<int>();
        var result = list.If(true, l => l.Add(1));
        Assert.Same(list, result);
        Assert.Single(list);
    }

    [Fact]
    public void If_Action_ConditionFalse_DoesNotRun()
    {
        var list = new List<int>();
        list.If(false, l => l.Add(1));
        Assert.Empty(list);
    }
}
