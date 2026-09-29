---
title: Collections, objects and types
layout: default
parent: Examples
nav_order: 2
---

# Collections, objects and types

This is the file `CollectionTypeExamples.cs` from the `examples/Apparatus.Examples` project. Each line shows a call and, in the console output, what it returns.

```csharp
using System.Collections.Specialized;
using static Apparatus.Examples.Output;

namespace Apparatus.Examples;

public static class CollectionTypeExamples
{
    public static void Run()
    {
        Title("Collections");
        List<int> missing = null;
        Show("missing.IsNullOrEmpty()", missing.IsNullOrEmpty());
        var numbers = new List<int> { 1, 2, 3, 4 };
        numbers.RemoveAll(new[] { 2, 4 });
        Show("{1,2,3,4}.RemoveAll({2,4})", numbers);

        var query = new NameValueCollection { { "page", "2" }, { "sort", "name" } };
        Show("query.ToDictionary()", query.ToDictionary().Select(p => $"{p.Key}={p.Value}"));

        Title("Objects");
        object text = "hello";
        Show("text.As<string>().Length", text.As<string>().Length);
        Show("\"42\".To<int>()", "42".To<int>());
        Show("\"1234.5\".To<decimal>()", "1234.5".To<decimal>());
        Show("guidText.To<Guid>() equals original", Guid.Parse("d3b07384-d9a1-4c3e-8b5a-2f6c5a1e9c00") == "d3b07384-d9a1-4c3e-8b5a-2f6c5a1e9c00".To<Guid>());
        Show("\"text\".CanBeCastTo<IEnumerable<char>>()", "text".CanBeCastTo<IEnumerable<char>>());
        Show("5.Between(1, 10)", 5.Between(1, 10));
        Show("10.Between(1, 10)", 10.Between(1, 10));
        Show("11.Between(1, 10)", 11.Between(1, 10));
        Show("\"b\".In(\"a\", \"b\", \"c\")", "b".In("a", "b", "c"));
        Show("2.In(new List<int> { 1, 2, 3 })", 2.In(new List<int> { 1, 2, 3 }));
        Show("\"abc\".FullNameWithAssembly()", "abc".FullNameWithAssembly());

        bool isFormal = true;
        Show("\"john\".If(isFormal, s => s.ToUpper())", "john".If(isFormal, s => s.ToUpperInvariant()));
        var log = new List<string>();
        log.If(true, l => l.Add("first")).Add("second");
        Show("list.If(true, add).Add(...)", log);

        Title("Types");
        Show("\"System.Guid\".ToType()", "System.Guid".ToType());
        Show("typeof(TypeExtensions).FullNameWithAssembly()", typeof(TypeExtensions).FullNameWithAssembly());
        Show("typeof(List<int>).CanBeCastTo<IEnumerable<int>>()", typeof(List<int>).CanBeCastTo<IEnumerable<int>>());
        Show("typeof(string).CanBeCastTo(typeof(int))", typeof(string).CanBeCastTo(typeof(int)));
        Show("typeof(int).In(typeof(int), typeof(long))", typeof(int).In(typeof(int), typeof(long)));
        Show("typeof(List<int>).IsEnumerable()", typeof(List<int>).IsEnumerable());
        Show("typeof(string).IsEnumerable()", typeof(string).IsEnumerable());
        Show("typeof(string).IsAssignableTo<object>()", typeof(string).IsAssignableTo<object>());
        // The non-generic version has to be called like this, because .NET already has Type.IsAssignableTo:
        Show("TypeExtensions.IsAssignableTo(typeof(int), typeof(object))", TypeExtensions.IsAssignableTo(typeof(int), typeof(object)));
    }
}
```
