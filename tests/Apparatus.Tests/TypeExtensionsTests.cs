using Apparatus;
using Xunit;

namespace ApparatusTests;

public class TypeExtensionsTests
{
    [Fact]
    public void ToType_KnownName_ReturnsType() => Assert.Equal(typeof(string), "System.String".ToType());

    [Fact]
    public void ToType_UnknownName_ReturnsNull() => Assert.Null("Not.A.Real.Type".ToType());

    [Fact]
    public void FullNameWithAssembly_ReturnsNamespaceClassAndAssembly() =>
        Assert.Equal("Apparatus.TypeExtensions, Apparatus", typeof(TypeExtensions).FullNameWithAssembly());

    [Fact]
    public void CanBeCastTo_Generic_SameType() => Assert.True(typeof(string).CanBeCastTo<string>());

    [Fact]
    public void CanBeCastTo_Generic_Interface() => Assert.True(typeof(List<int>).CanBeCastTo<IEnumerable<int>>());

    [Fact]
    public void CanBeCastTo_Generic_Unrelated() => Assert.False(typeof(string).CanBeCastTo<int>());

    [Fact]
    public void CanBeCastTo_Generic_NullType() => Assert.False(((Type)null!).CanBeCastTo<string>());

    [Fact]
    public void CanBeCastTo_SameType() => Assert.True(typeof(int).CanBeCastTo(typeof(int)));

    [Fact]
    public void CanBeCastTo_BaseType() => Assert.True(typeof(string).CanBeCastTo(typeof(object)));

    [Fact]
    public void CanBeCastTo_Unrelated() => Assert.False(typeof(string).CanBeCastTo(typeof(int)));

    [Fact]
    public void CanBeCastTo_NullType() => Assert.False(((Type)null!).CanBeCastTo(typeof(object)));

    [Fact]
    public void In_TypeInList() => Assert.True(typeof(int).In(typeof(long), typeof(int)));

    [Fact]
    public void In_TypeNotInList() => Assert.False(typeof(int).In(typeof(long), typeof(short)));

    [Theory]
    [InlineData(typeof(List<int>), true)]
    [InlineData(typeof(int[]), true)]
    [InlineData(typeof(Dictionary<string, int>), true)]
    [InlineData(typeof(string), false)]
    [InlineData(typeof(int), false)]
    [InlineData(typeof(object), false)]
    public void IsEnumerable_ReturnsExpected(Type type, bool expected) => Assert.Equal(expected, type.IsEnumerable());

    [Fact]
    public void IsAssignableTo_Generic_True() => Assert.True(typeof(string).IsAssignableTo<object>());

    [Fact]
    public void IsAssignableTo_Generic_False() => Assert.False(typeof(object).IsAssignableTo<string>());

    [Fact]
    public void IsAssignableTo_Generic_NullType_Throws() =>
        Assert.Throws<ArgumentNullException>(() => ((Type)null!).IsAssignableTo<object>());

    // NOTE: On .NET 5+ the built-in Type.IsAssignableTo(Type) instance method wins over this extension
    // when written as type.IsAssignableTo(x). The extension is called here as a normal static method.
    [Fact]
    public void IsAssignableTo_True() => Assert.True(TypeExtensions.IsAssignableTo(typeof(int), typeof(object)));

    [Fact]
    public void IsAssignableTo_False() => Assert.False(TypeExtensions.IsAssignableTo(typeof(int), typeof(string)));

    [Fact]
    public void IsAssignableTo_NullType_Throws() =>
        Assert.Throws<ArgumentNullException>(() => TypeExtensions.IsAssignableTo(null!, typeof(object)));

    [Fact]
    public void IsAssignableTo_NullTarget_Throws() =>
        Assert.Throws<ArgumentNullException>(() => TypeExtensions.IsAssignableTo(typeof(int), null!));
}
