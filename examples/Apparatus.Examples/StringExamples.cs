// Title: Working with text (StringExtensions)
using static Apparatus.Examples.Output;

namespace Apparatus.Examples;

public static class StringExamples
{
    private enum Level { Low, High }

    public static void Run()
    {
        Title("Text: checking");
        Show("\"  \".IsEmpty()", "  ".IsEmpty());                          // true: only white space
        Show("\"abc\".IsNotEmpty()", "abc".IsNotEmpty());                  // true
        Show("\"\".IsNullOrEmpty()", "".IsNullOrEmpty());                  // true
        Show("\" \".IsNullOrWhiteSpace()", " ".IsNullOrWhiteSpace());      // true
        Show("\"to\".In(\"abc\", \"pqr\", \"to\")", "to".In("abc", "pqr", "to"));
        Show("\"RED\".In(OrdinalIgnoreCase, ...)", "RED".In(StringComparison.OrdinalIgnoreCase, "red", "green"));
        Show("\"red\".In(list)", "red".In(new List<string> { "red", "green" }));
        Show("\"blue\".NotIn(\"red\", \"green\")", "blue".NotIn("red", "green"));
        Show("\"abc\".EqualsIgnoreCase(\"ABC\")", "abc".EqualsIgnoreCase("ABC"));
        Show("\"Hello World\".StartsWithIgnoreCase(\"hello\")", "Hello World".StartsWithIgnoreCase("hello"));
        Show("\"Hello World\".EndsWithIgnoreCase(\"WORLD\")", "Hello World".EndsWithIgnoreCase("WORLD"));

        Title("Text: searching");
        Show("\"this is fine\".IndexOfAny(\"is\", \"are\", \"f\")", "this is fine".IndexOfAny("is", "are", "f"));
        var found = "This is subject".FindFirstIndexOfAny("is", "subject");
        Show("\"This is subject\".FindFirstIndexOfAny(...)", $"{found.Item1}, {found.Item2}");
        Show("\"Order shipped\".FindFirst(\"SHIPPED\", \"delivered\")", "Order shipped".FindFirst("SHIPPED", "delivered"));

        Title("Text: changing case style");
        Show("\"hello world\".ToPascalCase()", "hello world".ToPascalCase());
        Show("\"user_first-name\".ToPascalCase()", "user_first-name".ToPascalCase());
        Show("\"hello_world\".ToPascalCase(true)", "hello_world".ToPascalCase(true));
        Show("\"ThisIs_it\".ToCamelCase()", "ThisIs_it".ToCamelCase());
        Show("\"VeryLongName\".ToSnakeCase()", "VeryLongName".ToSnakeCase());
        Show("\"HTTPRequest\".ToKebabCase()", "HTTPRequest".ToKebabCase());
        Show("\"this is final\".Capitalize()", "this is final".Capitalize());
        Show("\"this is final\".ToTitleCase()", "this is final".ToTitleCase());
        Show("\"thisIsFinal\".SplitCamelCase()", "thisIsFinal".SplitCamelCase());
        Show("\"ParseXMLFile\".SplitPascalCase()", "ParseXMLFile".SplitPascalCase());

        Title("Text: cutting, joining, building");
        Show("\"a, b, c\".Split(\", \")", "a, b, c".Split(", "));
        Show("\"a,,b\".Split(\",\", RemoveEmptyEntries)", "a,,b".Split(",", StringSplitOptions.RemoveEmptyEntries));
        var lines = ("one" + Environment.NewLine + "two").SplitToLines();
        Show("\"one<NewLine>two\".SplitToLines()", lines);
        var noBlank = ("one" + Environment.NewLine + Environment.NewLine + "two").SplitToLines(StringSplitOptions.RemoveEmptyEntries);
        Show("... SplitToLines(RemoveEmptyEntries)", noBlank);
        Show("new[] { \"a\", \"b\" }.Join(\"-\")", new[] { "a", "b" }.Join("-"));
        Show("\"Hi {0}, you have {1}\".ToFormat(\"Sam\", 3)", "Hi {0}, you have {1}".ToFormat("Sam", 3));
        Show("\"folder\".EnsureEndsWith(\"/\")", "folder".EnsureEndsWith("/"));
        Show("\"path\".EnsureStartsWith(\"/\")", "path".EnsureStartsWith("/"));
        Show("\"abc\".Reverse()", "abc".Reverse());

        Title("Text: converting");
        Show("\"yes\".ToBoolean(false)", "yes".ToBoolean(false));
        Show("\"0\".ToBoolean(true)", "0".ToBoolean(true));
        Show("\"maybe\".ToBoolean(true)", "maybe".ToBoolean(true));
        Show("\"HIGH\".ToEnum(Level.Low)", "HIGH".ToEnum(Level.Low));
        Show("\"unknown\".ToEnum(Level.Low)", "unknown".ToEnum(Level.Low));
        Show("\"hello\".ToHash()", "hello".ToHash());
        Show("\"hello\".ToMd5()", "hello".ToMd5());

        var packed = new string('a', 500).Compress();
        Show("500 x 'a' -> Compress() length", packed.Length);
        Show("packed.Decompress() length", packed.Decompress().Length);
    }
}
