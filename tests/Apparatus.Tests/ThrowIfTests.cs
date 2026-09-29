using Apparatus;
using System;
using System.Collections.Generic;
using Xunit;


namespace ApparatusTests;


public class ThrowIfTests
{
    [Fact]
    public void CheckingLesserThanMinimumWithoutArgNames()
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.LesserThan(10, 11));
        Assert.Equal($"10:10 is less than minimum required 11:11. (Parameter '10')", ex.Message);
    }

    [Fact]
    public void CheckingLesserThanMinimumWithArgNames()
    {
        int one = 10;
        int two = 11;

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => ThrowIf.LesserThan(one, two));
        Assert.Equal($"one:10 is less than minimum required two:11. (Parameter 'one')", ex.Message);
    }

    [Fact]
    public void CheckThrowIfNullExtensionMethodThrows()
    {
        object? a = null;
        Assert.Throws<ArgumentNullException>(() => a.ThrowIfNull());
    }

    [Fact]
    public void CheckThrowIfNullExtensionMethodDoesNotThrow()
    {
        List<string> args = new List<string>();
        args.ThrowIfNull();
        Assert.True(true);
    }
}

