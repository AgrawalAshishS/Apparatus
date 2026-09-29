using Apparatus;
using Xunit;

namespace ApparatusTests;

public class ThrowIfMethodsTests
{
    // ---- Null ----
    [Fact] public void Null_NullValue_Throws() => Assert.Throws<ArgumentNullException>(() => ThrowIf.Null(null));
    [Fact] public void Null_Value_DoesNotThrow() => ThrowIf.Null("x");

    [Fact]
    public void Null_UsesVariableNameAsParamName()
    {
        object? customer = null;
        var ex = Assert.Throws<ArgumentNullException>(() => ThrowIf.Null(customer));
        Assert.Equal("customer", ex.ParamName);
    }

    // ---- NullOrEmpty ----
    [Fact] public void NullOrEmpty_Null_Throws() => Assert.Throws<ArgumentNullException>(() => ThrowIf.NullOrEmpty(null!));
    [Fact] public void NullOrEmpty_Empty_Throws() => Assert.Throws<ArgumentNullException>(() => ThrowIf.NullOrEmpty(""));
    [Fact] public void NullOrEmpty_Text_DoesNotThrow() => ThrowIf.NullOrEmpty("x");
    [Fact] public void NullOrEmpty_Whitespace_DoesNotThrow() => ThrowIf.NullOrEmpty(" ");

    // ---- ZeroOrLess ----
    [Theory] [InlineData(0)] [InlineData(-1)]
    public void ZeroOrLess_Int_Throws(int value) => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.ZeroOrLess(value));
    [Fact] public void ZeroOrLess_Int_Positive_DoesNotThrow() => ThrowIf.ZeroOrLess(1);
    [Theory] [InlineData(0L)] [InlineData(-1L)]
    public void ZeroOrLess_Long_Throws(long value) => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.ZeroOrLess(value));
    [Fact] public void ZeroOrLess_Long_Positive_DoesNotThrow() => ThrowIf.ZeroOrLess(1L);

    // ---- Negative ----
    [Fact] public void Negative_Int_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.Negative(-1));
    [Fact] public void Negative_Int_Zero_DoesNotThrow() => ThrowIf.Negative(0);
    [Fact] public void Negative_Long_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.Negative(-1L));
    [Fact] public void Negative_Long_Zero_DoesNotThrow() => ThrowIf.Negative(0L);

    // ---- EmptyGuid ----
    [Fact] public void EmptyGuid_Empty_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.EmptyGuid(Guid.Empty));
    [Fact] public void EmptyGuid_Value_DoesNotThrow() => ThrowIf.EmptyGuid(Guid.NewGuid());

    // ---- NotEqual ----
    [Fact] public void NotEqual_Int_Different_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.NotEqual(1, 2));
    [Fact] public void NotEqual_Int_Same_DoesNotThrow() => ThrowIf.NotEqual(2, 2);
    [Fact] public void NotEqual_Long_Different_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.NotEqual(1L, 2L));
    [Fact] public void NotEqual_Long_Same_DoesNotThrow() => ThrowIf.NotEqual(2L, 2L);

    // ---- LesserThan ----
    [Fact] public void LesserThan_Int_Below_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.LesserThan(1, 2));
    [Fact] public void LesserThan_Int_Equal_DoesNotThrow() => ThrowIf.LesserThan(2, 2);
    [Fact] public void LesserThan_Long_Below_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.LesserThan(1L, 2L));
    [Fact] public void LesserThan_Long_Equal_DoesNotThrow() => ThrowIf.LesserThan(2L, 2L);

    // ---- GreatorThan ----
    [Fact] public void GreatorThan_Int_Above_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.GreatorThan(3, 2));
    [Fact] public void GreatorThan_Int_Equal_DoesNotThrow() => ThrowIf.GreatorThan(2, 2);
    [Fact] public void GreatorThan_Long_Above_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.GreatorThan(3L, 2L));
    [Fact] public void GreatorThan_Long_Equal_DoesNotThrow() => ThrowIf.GreatorThan(2L, 2L);

    // ---- LessThanEqualTo ----
    [Fact] public void LessThanEqualTo_Int_Equal_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.LessThanEqualTo(2, 2));
    [Fact] public void LessThanEqualTo_Int_Below_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.LessThanEqualTo(1, 2));
    [Fact] public void LessThanEqualTo_Int_Above_DoesNotThrow() => ThrowIf.LessThanEqualTo(3, 2);
    [Fact] public void LessThanEqualTo_Long_Equal_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.LessThanEqualTo(2L, 2L));
    [Fact] public void LessThanEqualTo_Long_Above_DoesNotThrow() => ThrowIf.LessThanEqualTo(3L, 2L);

    // ---- GreatorThanEqualTo ----
    [Fact] public void GreatorThanEqualTo_Int_Equal_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.GreatorThanEqualTo(2, 2));
    [Fact] public void GreatorThanEqualTo_Int_Above_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.GreatorThanEqualTo(3, 2));
    [Fact] public void GreatorThanEqualTo_Int_Below_DoesNotThrow() => ThrowIf.GreatorThanEqualTo(1, 2);
    [Fact] public void GreatorThanEqualTo_Long_Equal_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.GreatorThanEqualTo(2L, 2L));
    [Fact] public void GreatorThanEqualTo_Long_Below_DoesNotThrow() => ThrowIf.GreatorThanEqualTo(1L, 2L);

    // ---- Extension forms ----
    [Fact] public void Ext_NullOrEmpty_Throws() => Assert.Throws<ArgumentNullException>(() => "".ThrowIfNullOrEmpty());
    [Fact] public void Ext_NullOrEmpty_Ok() => "x".ThrowIfNullOrEmpty();

    [Fact] public void Ext_ZeroOrLess_Int_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => 0.ThrowIfZeroOrLess());
    [Fact] public void Ext_ZeroOrLess_Int_Ok() => 1.ThrowIfZeroOrLess();
    [Fact] public void Ext_ZeroOrLess_Long_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => 0L.ThrowIfZeroOrLess());
    [Fact] public void Ext_ZeroOrLess_Long_Ok() => 1L.ThrowIfZeroOrLess();

    [Fact] public void Ext_Negative_Int_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => (-1).ThrowIfNegative());
    [Fact] public void Ext_Negative_Int_Ok() => 0.ThrowIfNegative();
    [Fact] public void Ext_Negative_Long_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => (-1L).ThrowIfNegative());
    [Fact] public void Ext_Negative_Long_Ok() => 0L.ThrowIfNegative();

    [Fact] public void Ext_EmptyGuid_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => Guid.Empty.ThrowIfEmptyGuid());
    [Fact] public void Ext_EmptyGuid_Ok() => Guid.NewGuid().ThrowIfEmptyGuid();

    [Fact] public void Ext_NotEqual_Int_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => 1.ThrowIfNotEqual(2));
    [Fact] public void Ext_NotEqual_Int_Ok() => 2.ThrowIfNotEqual(2);
    [Fact] public void Ext_NotEqual_Long_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => 1L.ThrowIfNotEqual(2L));
    [Fact] public void Ext_NotEqual_Long_Ok() => 2L.ThrowIfNotEqual(2L);

    [Fact] public void Ext_LesserThan_Int_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => 1.ThrowIfLesserThan(2));
    [Fact] public void Ext_LesserThan_Int_Ok() => 2.ThrowIfLesserThan(2);
    [Fact] public void Ext_LesserThan_Long_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => 1L.ThrowIfLesserThan(2L));
    [Fact] public void Ext_LesserThan_Long_Ok() => 2L.ThrowIfLesserThan(2L);

    [Fact] public void Ext_GreatorThan_Int_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => 3.ThrowIfGreatorThan(2));
    [Fact] public void Ext_GreatorThan_Int_Ok() => 2.ThrowIfGreatorThan(2);
    [Fact] public void Ext_GreatorThan_Long_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => 3L.ThrowIfGreatorThan(2L));
    [Fact] public void Ext_GreatorThan_Long_Ok() => 2L.ThrowIfGreatorThan(2L);

    [Fact] public void Ext_LessThanEqualTo_Int_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => 2.ThrowIfLessThanEqualTo(2));
    [Fact] public void Ext_LessThanEqualTo_Int_Ok() => 3.ThrowIfLessThanEqualTo(2);
    [Fact] public void Ext_LessThanEqualTo_Long_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => 2L.ThrowIfLessThanEqualTo(2L));
    [Fact] public void Ext_LessThanEqualTo_Long_Ok() => 3L.ThrowIfLessThanEqualTo(2L);

    [Fact] public void Ext_GreatorThanEqualTo_Int_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => 2.ThrowIfGreatorThanEqualTo(2));
    [Fact] public void Ext_GreatorThanEqualTo_Int_Ok() => 1.ThrowIfGreatorThanEqualTo(2);
    [Fact] public void Ext_GreatorThanEqualTo_Long_Throws() => Assert.Throws<ArgumentOutOfRangeException>(() => 2L.ThrowIfGreatorThanEqualTo(2L));
    [Fact] public void Ext_GreatorThanEqualTo_Long_Ok() => 1L.ThrowIfGreatorThanEqualTo(2L);
}
