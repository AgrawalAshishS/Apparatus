// Builds the API reference pages (Markdown) in docs/api from the XML documentation file of the Apparatus library.
// It can also turn the sample files of the examples project into pages in docs/examples.
// Usage: ApiDocGenerator <path-to-Apparatus.xml> <api-output-folder> [<examples-folder> <examples-output-folder>]
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

if (args.Length != 2 && args.Length != 4)
{
    Console.Error.WriteLine("Usage: ApiDocGenerator <path-to-Apparatus.xml> <api-output-folder> [<examples-folder> <examples-output-folder>]");
    return 1;
}

var xmlPath = args[0];
var outDir = args[1];
Directory.CreateDirectory(outDir);
foreach (var old in Directory.GetFiles(outDir, "*.md")) File.Delete(old);

var members = XDocument.Load(xmlPath).Root!.Element("members")!.Elements("member")
    .Select(m => new Member(m.Attribute("name")!.Value, m))
    .ToList();

var types = members.Where(m => m.Kind == 'T').OrderBy(m => m.ShortName, StringComparer.Ordinal).ToList();
var indexRows = new List<string>();
var order = 2;

foreach (var type in types)
{
    var typeName = type.ShortName;
    var typeMembers = members
        .Where(m => m.Kind is 'M' or 'P' or 'F' && m.TypeName == type.FullName)
        .ToList();

    var sb = new StringBuilder();
    sb.AppendLine("---");
    sb.AppendLine($"title: {typeName}");
    sb.AppendLine("layout: default");
    sb.AppendLine("parent: API Reference");
    sb.AppendLine($"nav_order: {order++}");
    sb.AppendLine("---");
    sb.AppendLine();
    sb.AppendLine($"# {typeName}");
    sb.AppendLine();
    sb.AppendLine(Text(type.Xml.Element("summary")));
    sb.AppendLine();

    var typeRemarks = type.Xml.Element("remarks");
    if (typeRemarks != null)
    {
        sb.AppendLine(Text(typeRemarks));
        sb.AppendLine();
    }

    WriteExample(sb, type.Xml);

    var methods = typeMembers.Where(m => m.Kind == 'M').ToList();
    if (methods.Count > 0)
    {
        sb.AppendLine("## Methods");
        sb.AppendLine();
        sb.AppendLine("| Method | What it does |");
        sb.AppendLine("|---|---|");
        // The site builder gives repeated headings the ids name, name-1, name-2 ... so the links are built the same way.
        var seen = new Dictionary<string, int>();
        var anchors = new Dictionary<Member, string>();
        foreach (var m in methods)
        {
            seen.TryGetValue(m.Anchor, out var n);
            seen[m.Anchor] = n + 1;
            anchors[m] = n == 0 ? m.Anchor : m.Anchor + "-" + n;
        }
        foreach (var m in methods)
            sb.AppendLine($"| [`{m.MethodName}`](#{anchors[m]}) | {Cell(Text(m.Xml.Element("summary")))} |");
        sb.AppendLine();

        foreach (var m in methods)
        {
            sb.AppendLine($"## `{m.MethodName}`");
            sb.AppendLine();
            sb.AppendLine("```csharp");
            sb.AppendLine(m.Signature(m.Xml));
            sb.AppendLine("```");
            sb.AppendLine();
            sb.AppendLine(Text(m.Xml.Element("summary")));
            sb.AppendLine();
            WriteParams(sb, "Type parameters", m.Xml.Elements("typeparam"));
            WriteParams(sb, "Parameters", m.Xml.Elements("param"));

            var returns = m.Xml.Element("returns");
            if (returns != null)
            {
                sb.AppendLine("**Returns**");
                sb.AppendLine();
                sb.AppendLine(Text(returns));
                sb.AppendLine();
            }

            var exceptions = m.Xml.Elements("exception").ToList();
            if (exceptions.Count > 0)
            {
                sb.AppendLine("**Exceptions**");
                sb.AppendLine();
                foreach (var e in exceptions)
                    sb.AppendLine($"- `{Short(e.Attribute("cref")?.Value ?? "")}`: {Text(e)}");
                sb.AppendLine();
            }

            var remarks = m.Xml.Element("remarks");
            if (remarks != null)
            {
                sb.AppendLine("**Remarks**");
                sb.AppendLine();
                sb.AppendLine(Text(remarks));
                sb.AppendLine();
            }

            WriteExample(sb, m.Xml);
        }
    }

    var others = typeMembers.Where(m => m.Kind is 'P' or 'F').ToList();
    if (others.Count > 0)
    {
        sb.AppendLine("## Properties and fields");
        sb.AppendLine();
        sb.AppendLine("| Name | What it does |");
        sb.AppendLine("|---|---|");
        foreach (var m in others)
            sb.AppendLine($"| `{m.ShortName}` | {Cell(Text(m.Xml.Element("summary")))} |");
        sb.AppendLine();
    }

    File.WriteAllText(Path.Combine(outDir, typeName + ".md"), Protect(sb.ToString()), new UTF8Encoding(false));
    indexRows.Add($"| [{typeName}]({typeName}.html) | {Cell(FirstSentence(Text(type.Xml.Element("summary"))))} |");
}

var index = new StringBuilder();
index.AppendLine("---");
index.AppendLine("title: API Reference");
index.AppendLine("layout: default");
index.AppendLine("nav_order: 2");
index.AppendLine("has_children: true");
index.AppendLine("permalink: /api/");
index.AppendLine("---");
index.AppendLine();
index.AppendLine("# API Reference");
index.AppendLine();
index.AppendLine("Every public class of the Apparatus library. This page is generated from the XML comments in the source code.");
index.AppendLine();
index.AppendLine("| Class | What it is for |");
index.AppendLine("|---|---|");
foreach (var row in indexRows) index.AppendLine(row);
File.WriteAllText(Path.Combine(outDir, "index.md"), index.ToString(), new UTF8Encoding(false));

Console.WriteLine($"Wrote {types.Count} class pages to {outDir}");

if (args.Length == 4)
{
    WriteExamplePages(args[2], args[3]);
}

return 0;

static void WriteExamplePages(string sourceDir, string outputDir)
{
    Directory.CreateDirectory(outputDir);
    foreach (var old in Directory.GetFiles(outputDir, "*.md")) File.Delete(old);

    var files = Directory.GetFiles(sourceDir, "*Examples.cs").OrderBy(f => f, StringComparer.Ordinal).ToList();
    var rows = new List<string>();
    var order = 2;
    foreach (var file in files)
    {
        var lines = File.ReadAllText(file).Replace("\r", "").Split('\n');
        var first = lines[0].Trim();
        var title = first.StartsWith("// Title:", StringComparison.Ordinal) ? first["// Title:".Length..].Trim() : Path.GetFileNameWithoutExtension(file);
        var name = Path.GetFileNameWithoutExtension(file);
        var body = string.Join("\n", lines.Skip(1));

        var sb = new StringBuilder();
        sb.AppendLine("---");
        sb.AppendLine($"title: {title}");
        sb.AppendLine("layout: default");
        sb.AppendLine("parent: Examples");
        sb.AppendLine($"nav_order: {order++}");
        sb.AppendLine("---");
        sb.AppendLine();
        sb.AppendLine($"# {title}");
        sb.AppendLine();
        sb.AppendLine($"This is the file `{Path.GetFileName(file)}` from the `examples/Apparatus.Examples` project. Each line shows a call and, in the console output, what it returns.");
        sb.AppendLine();
        sb.AppendLine("```csharp");
        sb.AppendLine(body.Trim());
        sb.AppendLine("```");
        File.WriteAllText(Path.Combine(outputDir, name + ".md"), Protect(sb.ToString()), new UTF8Encoding(false));
        rows.Add($"| [{title}]({name}.html) | `{Path.GetFileName(file)}` |");
    }

    var index = new StringBuilder();
    index.AppendLine("---");
    index.AppendLine("title: Examples");
    index.AppendLine("layout: default");
    index.AppendLine("nav_order: 3");
    index.AppendLine("has_children: true");
    index.AppendLine("permalink: /examples/");
    index.AppendLine("---");
    index.AppendLine();
    index.AppendLine("# Examples");
    index.AppendLine();
    index.AppendLine("Runnable samples that use every public method. To run them all:");
    index.AppendLine();
    index.AppendLine("```");
    index.AppendLine("dotnet run --project examples/Apparatus.Examples");
    index.AppendLine("```");
    index.AppendLine();
    index.AppendLine("| Topic | Source file |");
    index.AppendLine("|---|---|");
    foreach (var row in rows) index.AppendLine(row);
    File.WriteAllText(Path.Combine(outputDir, "index.md"), index.ToString(), new UTF8Encoding(false));
    Console.WriteLine($"Wrote {files.Count} example pages to {outputDir}");
}

// ---------------------------------------------------------------------------------------------
static void WriteParams(StringBuilder sb, string title, IEnumerable<XElement> items)
{
    var list = items.ToList();
    if (list.Count == 0) return;
    sb.AppendLine($"**{title}**");
    sb.AppendLine();
    sb.AppendLine("| Name | Description |");
    sb.AppendLine("|---|---|");
    foreach (var p in list)
        sb.AppendLine($"| `{p.Attribute("name")?.Value}` | {Cell(Text(p))} |");
    sb.AppendLine();
}

static void WriteExample(StringBuilder sb, XElement xml)
{
    foreach (var ex in xml.Elements("example"))
    {
        sb.AppendLine("**Example**");
        sb.AppendLine();
        sb.AppendLine(Text(ex));
        sb.AppendLine();
    }
}

// Jekyll uses Liquid; keep any {{ or {% in the text from being treated as a template tag.
static string Protect(string s) => s.Contains("{{") || s.Contains("{%") ? "{% raw %}\n" + s + "\n{% endraw %}" : s;

static string Cell(string s) => s.Replace("\r", "").Replace("\n", " ").Replace("|", "\\|").Trim();

static string FirstSentence(string s)
{
    var flat = s.Replace("\r", "").Replace("\n", " ").Trim();
    var dot = flat.IndexOf(". ", StringComparison.Ordinal);
    return dot > 0 ? flat[..(dot + 1)] : flat;
}

static string Short(string cref)
{
    var id = cref.Contains(':') ? cref[(cref.IndexOf(':') + 1)..] : cref;
    var paren = id.IndexOf('(');
    if (paren >= 0) id = id[..paren];
    id = Regex.Replace(id, "`+\\d+", "");
    var dot = id.LastIndexOf('.');
    return dot >= 0 ? id[(dot + 1)..] : id;
}

// Turns the inner XML of a doc comment into Markdown.
static string Text(XElement? e)
{
    if (e == null) return "";
    var sb = new StringBuilder();
    foreach (var node in e.Nodes()) Render(node, sb);
    return Regex.Replace(sb.ToString(), "[ \\t]+\\n", "\n").Trim();
}

static void Render(XNode node, StringBuilder sb)
{
    if (node is XText t)
    {
        var s = Regex.Replace(t.Value, "\\s*\\n\\s*", " ");
        sb.Append(s);
        return;
    }
    if (node is not XElement el) return;

    switch (el.Name.LocalName)
    {
        case "see":
            sb.Append('`').Append(el.Attribute("cref") != null ? Short(el.Attribute("cref")!.Value) : el.Attribute("langword")?.Value ?? el.Value).Append('`');
            break;
        case "seealso":
            break;
        case "paramref":
        case "typeparamref":
            sb.Append('`').Append(el.Attribute("name")?.Value).Append('`');
            break;
        case "c":
            sb.Append('`').Append(el.Value).Append('`');
            break;
        case "b":
            sb.Append("**").Append(el.Value).Append("**");
            break;
        case "em":
        case "i":
            sb.Append('*').Append(el.Value).Append('*');
            break;
        case "code":
            var lines = el.Value.Replace("\r", "").Split('\n').ToList();
            while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[0])) lines.RemoveAt(0);
            while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[^1])) lines.RemoveAt(lines.Count - 1);
            var indent = lines.Where(l => l.Trim().Length > 0).Select(l => l.Length - l.TrimStart().Length).DefaultIfEmpty(0).Min();
            sb.Append("\n\n```csharp\n");
            foreach (var l in lines) sb.Append(l.Length >= indent ? l[indent..] : l.TrimStart()).Append('\n');
            sb.Append("```\n\n");
            break;
        case "para":
            sb.Append("\n\n");
            foreach (var child in el.Nodes()) Render(child, sb);
            sb.Append("\n\n");
            break;
        default:
            foreach (var child in el.Nodes()) Render(child, sb);
            break;
    }
}

// One entry of the XML documentation file.
sealed class Member
{
    private static readonly Dictionary<string, string> Aliases = new()
    {
        ["System.String"] = "string", ["System.Int32"] = "int", ["System.Int64"] = "long", ["System.Int16"] = "short",
        ["System.Byte"] = "byte", ["System.Boolean"] = "bool", ["System.Double"] = "double", ["System.Single"] = "float",
        ["System.Decimal"] = "decimal", ["System.Object"] = "object", ["System.Void"] = "void", ["System.Char"] = "char",
    };

    public Member(string id, XElement xml)
    {
        Id = id;
        Xml = xml;
        Kind = id[0];
    }

    public string Id { get; }
    public XElement Xml { get; }
    public char Kind { get; }

    private string Body => Id[2..];
    private string NameWithoutParams => Body.Contains('(') ? Body[..Body.IndexOf('(')] : Body;

    /// <summary>Full name of the class this member belongs to (or of the class itself for type entries).</summary>
    public string FullName => Kind == 'T' ? Body : "";
    public string TypeName => Kind == 'T' ? Body : NameWithoutParams[..NameWithoutParams.LastIndexOf('.')];
    public string ShortName => Kind == 'T' ? Body[(Body.LastIndexOf('.') + 1)..] : NameWithoutParams[(NameWithoutParams.LastIndexOf('.') + 1)..];

    public string MethodName
    {
        get
        {
            var name = Regex.Replace(ShortName, "``\\d+", "");
            if (name == "#ctor") return TypeName[(TypeName.LastIndexOf('.') + 1)..];
            var generics = Regex.Match(ShortName, "``(\\d+)");
            return generics.Success ? name + "<" + string.Join(", ", Enumerable.Range(0, int.Parse(generics.Groups[1].Value)).Select(i => TypeParameterName(i))) + ">" : name;
        }
    }

    public string Anchor => Regex.Replace(MethodName.ToLowerInvariant(), "[^a-z0-9]+", "-").Trim('-');

    private string TypeParameterName(int index)
    {
        var names = Xml.Elements("typeparam").Select(x => x.Attribute("name")?.Value).ToList();
        return index < names.Count && names[index] != null ? names[index]! : "T" + index;
    }

    public string Signature(XElement xml)
    {
        var names = xml.Elements("param").Select(p => p.Attribute("name")!.Value).ToList();
        var open = Body.IndexOf('(');
        var types = open < 0 ? new List<string>() : SplitTop(Body[(open + 1)..^1]);
        var parts = new List<string>();
        for (var i = 0; i < types.Count; i++)
            parts.Add(NiceType(types[i]) + " " + (i < names.Count ? names[i] : "arg" + i));
        return (ShortName == "#ctor" ? "new " : "") + MethodName + "(" + string.Join(", ", parts) + ")";
    }

    private string NiceType(string t)
    {
        t = t.Replace("@", "");
        var array = "";
        while (t.EndsWith("[]", StringComparison.Ordinal)) { array += "[]"; t = t[..^2]; }
        var brace = t.IndexOf('{');
        if (brace >= 0)
        {
            var outer = t[..brace];
            var inner = SplitTop(t[(brace + 1)..^1]).Select(NiceType);
            var outerShort = Regex.Replace(outer[(outer.LastIndexOf('.') + 1)..], "`\\d+", "");
            return outerShort + "<" + string.Join(", ", inner) + ">" + array;
        }
        if (t.StartsWith("``", StringComparison.Ordinal))
            return TypeParameterName(int.Parse(t[2..])) + array;
        if (Aliases.TryGetValue(t, out var alias)) return alias + array;
        return t[(t.LastIndexOf('.') + 1)..] + array;
    }

    private static List<string> SplitTop(string s)
    {
        var result = new List<string>();
        var depth = 0;
        var start = 0;
        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == '{') depth++;
            else if (s[i] == '}') depth--;
            else if (s[i] == ',' && depth == 0) { result.Add(s[start..i]); start = i + 1; }
        }
        if (s.Length > 0) result.Add(s[start..]);
        return result;
    }
}
