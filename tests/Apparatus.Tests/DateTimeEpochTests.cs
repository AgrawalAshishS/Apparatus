using Apparatus;
using Xunit;

namespace ApparatusTests;

public class DateTimeEpochTests
{
    private static readonly DateTime Y2020 = new(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    [Fact]
    public void ToEpoch_DateTime_ReturnsUnixSeconds() => Assert.Equal(1577836800L, Y2020.ToEpoch());

    [Fact]
    public void ToEpoch_DateTime_BeforeEpoch_IsNegative() =>
        Assert.Equal(-86400L, new DateTime(1969, 12, 31, 0, 0, 0, DateTimeKind.Utc).ToEpoch());

    [Fact]
    public void ToEpoch_DateTime_LocalKind_IsConvertedToUtc()
    {
        var local = Y2020.ToLocalTime();
        Assert.Equal(1577836800L, local.ToEpoch());
    }

    [Fact]
    public void ToEpoch_DateTimeOffset_UsesOffset()
    {
        var value = new DateTimeOffset(2020, 1, 1, 5, 30, 0, TimeSpan.FromHours(5.5));
        Assert.Equal(1577836800L, value.ToEpoch());
    }

    [Fact]
    public void EpochToUtcDate_ReturnsUtcDate()
    {
        var result = 1577836800L.EpochToUtcDate();
        Assert.Equal(Y2020, result);
        Assert.Equal(DateTimeKind.Utc, result.Kind);
    }

    [Fact]
    public void EpochToUtcDate_RoundTripsWithToEpoch() =>
        Assert.Equal(1234567890L, 1234567890L.EpochToUtcDate().ToEpoch());

    [Fact]
    public void EpochDiff_ReturnsTimeSpan() => Assert.Equal(TimeSpan.FromHours(1), 1000L.EpochDiff(4600L));

    [Fact]
    public void EpochDiff_EndBeforeStart_IsNegative() => Assert.Equal(TimeSpan.FromSeconds(-60), 100L.EpochDiff(40L));

    [Theory]
    [InlineData(0L, 150L, 2L)]
    [InlineData(0L, 59L, 0L)]
    [InlineData(0L, 60L, 1L)]
    [InlineData(120L, 0L, -2L)]
    public void EpochDiffInMinutes_ReturnsWholeMinutes(long start, long end, long expected) =>
        Assert.Equal(expected, start.EpochDiffInMinutes(end));

    [Fact]
    public void ClearTime_RemovesTimePart()
    {
        var result = new DateTime(2024, 5, 17, 14, 45, 30, 123).ClearTime();
        Assert.Equal(new DateTime(2024, 5, 17), result);
    }

    [Fact]
    public void ClearTime_KeepsKind() =>
        Assert.Equal(DateTimeKind.Utc, new DateTime(2024, 5, 17, 1, 2, 3, DateTimeKind.Utc).ClearTime().Kind);
}
