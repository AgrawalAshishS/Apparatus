using Apparatus;
using Xunit;

namespace ApparatusTests;

public class NumericExtensionsTests
{
    [Fact] public void In_Long() { Assert.True(10L.In(9L, 10L)); Assert.False(10L.In(1L, 2L)); }
    [Fact] public void In_Int() { Assert.True(10.In(9, 10)); Assert.False(10.In(1, 2)); }
    [Fact] public void In_Double() { Assert.True(2.5.In(1.5, 2.5)); Assert.False(2.5.In(1.5)); }
    [Fact] public void In_Decimal() { Assert.True(2.5m.In(1.5m, 2.5m)); Assert.False(2.5m.In(1.5m)); }
    [Fact] public void In_Float() { Assert.True(2.5f.In(1.5f, 2.5f)); Assert.False(2.5f.In(1.5f)); }
    [Fact] public void In_Short() { Assert.True(((short)10).In(9, 10)); Assert.False(((short)10).In(1, 2)); }
    [Fact] public void In_Byte() { Assert.True(((byte)10).In(9, 10)); Assert.False(((byte)10).In(1, 2)); }
    [Fact] public void In_NoValues_ReturnsFalse() => Assert.False(10.In());
}
