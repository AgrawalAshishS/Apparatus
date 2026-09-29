// Title: Numbers, dates, enums and spans
using static Apparatus.Examples.Output;

namespace Apparatus.Examples;

public static class NumberDateExamples
{
    private enum Color { Red, Green, Blue = 5 }

    public static void Run()
    {
        Title("Numbers: In");
        int id = 10;
        Show("10.In(10, 11, 12)", id.In(10, 11, 12));
        Show("10L.In(1L, 2L)", 10L.In(1L, 2L));
        Show("2.5.In(2.5, 3.5)", 2.5.In(2.5, 3.5));
        Show("2.5m.In(2.5m)", 2.5m.In(2.5m));
        Show("2.5f.In(1f, 2f)", 2.5f.In(1f, 2f));
        Show("((short)3).In(3, 4)", ((short)3).In(3, 4));
        Show("((byte)3).In(4, 5)", ((byte)3).In(4, 5));

        Title("Dates: Unix epoch");
        var date = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        long epoch = date.ToEpoch();
        Show("2020-01-01 UTC .ToEpoch()", epoch);
        Show("new DateTimeOffset(...+05:30).ToEpoch()", new DateTimeOffset(2020, 1, 1, 5, 30, 0, TimeSpan.FromHours(5.5)).ToEpoch());
        Show("1577836800L.EpochToUtcDate()", epoch.EpochToUtcDate().ToString("yyyy-MM-dd HH:mm:ss"));
        Show("1000L.EpochDiff(4600L)", 1000L.EpochDiff(4600L));
        Show("0L.EpochDiffInMinutes(150L)", 0L.EpochDiffInMinutes(150L));

        Title("Dates: day helpers");
        Show("new DateTime(2024,5,17,14,45,30).ClearTime()", new DateTime(2024, 5, 17, 14, 45, 30).ClearTime().ToString("yyyy-MM-dd HH:mm:ss"));
        Show("DayOfWeek.Saturday.IsWeekend()", DayOfWeek.Saturday.IsWeekend());
        Show("DayOfWeek.Saturday.IsWeekend(6)", DayOfWeek.Saturday.IsWeekend(6));
        Show("DayOfWeek.Wednesday.IsWeekday()", DayOfWeek.Wednesday.IsWeekday());

        Title("Enums");
        var colors = new[] { Color.Red, Color.Blue };
        Show("colors.ToCommaSeparatedList()", colors.ToCommaSeparatedList());
        Show("colors.ToCommaSeparatedList(false)", colors.ToCommaSeparatedList(false));
        var map = typeof(Color).EnumToDictionary();
        Show("typeof(Color).EnumToDictionary()", map.Select(p => $"{p.Key}={p.Value}"));

        Title("Spans");
        ReadOnlySpan<int> first = new[] { 1, 2 };
        ReadOnlySpan<int> second = new[] { 3 };
        Show("first.Concat(second)", first.Concat(second));
    }
}
