namespace Apparatus.Examples;

/// <summary>Tiny helper that prints "what we called => what we got".</summary>
internal static class Output
{
    public static void Title(string title)
    {
        Console.WriteLine();
        Console.WriteLine($"=== {title} ===");
    }

    public static void Show(string call, object result)
    {
        var text = result switch
        {
            null => "null",
            string s => $"\"{s}\"",
            System.Collections.IEnumerable items and not string => "[" + string.Join(", ", items.Cast<object>()) + "]",
            _ => result.ToString(),
        };
        Console.WriteLine($"{call} => {text}");
    }
}
