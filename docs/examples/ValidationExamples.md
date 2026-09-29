---
title: Checking input (ThrowIf, InputFilters, Exception)
layout: default
parent: Examples
nav_order: 8
---

# Checking input (ThrowIf, InputFilters, Exception)

This is the file `ValidationExamples.cs` from the `examples/Apparatus.Examples` project. Each line shows a call and, in the console output, what it returns.

```csharp
using static Apparatus.Examples.Output;
using static Apparatus.InputFilters;
using AppException = Apparatus.Exceptions.Exception;

namespace Apparatus.Examples;

public static class ValidationExamples
{
    public static void Run()
    {
        Title("ThrowIf: static style");
        Try("ThrowIf.Null(null)", () => ThrowIf.Null(null));
        Try("ThrowIf.NullOrEmpty(\"\")", () => ThrowIf.NullOrEmpty(""));
        Try("ThrowIf.ZeroOrLess(0)", () => ThrowIf.ZeroOrLess(0));
        Try("ThrowIf.Negative(-1)", () => ThrowIf.Negative(-1));
        Try("ThrowIf.EmptyGuid(Guid.Empty)", () => ThrowIf.EmptyGuid(Guid.Empty));
        Try("ThrowIf.NotEqual(1, 2)", () => ThrowIf.NotEqual(1, 2));
        Try("ThrowIf.LesserThan(5, 10)", () => ThrowIf.LesserThan(5, 10));
        Try("ThrowIf.GreatorThan(15, 10)", () => ThrowIf.GreatorThan(15, 10));
        Try("ThrowIf.LessThanEqualTo(10, 10)", () => ThrowIf.LessThanEqualTo(10, 10));
        Try("ThrowIf.GreatorThanEqualTo(10, 10)", () => ThrowIf.GreatorThanEqualTo(10, 10));

        Title("ThrowIf: extension style (the compiler puts the variable name in the message)");
        int age = -3;
        Try("age.ThrowIfNegative()", () => age.ThrowIfNegative());
        int quantity = 0;
        Try("quantity.ThrowIfZeroOrLess()", () => quantity.ThrowIfZeroOrLess());
        long size = 5000;
        Try("size.ThrowIfGreatorThan(1000)", () => size.ThrowIfGreatorThan(1000));
        string name = "";
        Try("name.ThrowIfNullOrEmpty()", () => name.ThrowIfNullOrEmpty());
        object customer = null;
        Try("customer.ThrowIfNull()", () => customer.ThrowIfNull());
        Guid userId = Guid.Empty;
        Try("userId.ThrowIfEmptyGuid()", () => userId.ThrowIfEmptyGuid());
        int expected = 1;
        Try("expected.ThrowIfNotEqual(2)", () => expected.ThrowIfNotEqual(2));
        int low = 1;
        Try("low.ThrowIfLesserThan(5)", () => low.ThrowIfLesserThan(5));
        int equal = 5;
        Try("equal.ThrowIfLessThanEqualTo(5)", () => equal.ThrowIfLessThanEqualTo(5));
        Try("equal.ThrowIfGreatorThanEqualTo(5)", () => equal.ThrowIfGreatorThanEqualTo(5));

        Title("InputFilters");
        Show("InputFilter(\"<b>hi</b>\", NoMarkup)", InputFilter("<b>hi</b>", FilterFlag.NoMarkup));
        Show("InputFilter(\"<script>bad()</script>ok\", NoScripting)", InputFilter("<script>bad()</script>ok", FilterFlag.NoScripting));
        Show("InputFilter(\"a<b>c\", NoAngleBrackets)", InputFilter("a<b>c", FilterFlag.NoAngleBrackets));
        Show("InputFilter(\"it's\", NoSQL)", InputFilter("it's", FilterFlag.NoSQL));
        Show("InputFilter(two lines, MultiLine)", InputFilter("a" + Environment.NewLine + "b", FilterFlag.MultiLine));
        Show("ValidateInput(\"hello\", NoMarkup)", ValidateInput("hello", FilterFlag.NoMarkup));
        Show("ValidateInput(\"<b>hi</b>\", NoMarkup)", ValidateInput("<b>hi</b>", FilterFlag.NoMarkup));

        Title("Exception with an error code");
        try
        {
            throw new AppException("USER_NOT_FOUND", "No user with that id.");
        }
        catch (AppException ex)
        {
            Show("ex.ErrorCode", ex.ErrorCode);
            Show("ex.Message", ex.Message);
        }
    }

    private static void Try(string call, Action action)
    {
        try
        {
            action();
            Show(call, "no exception");
        }
        catch (Exception ex)
        {
            Show(call, $"{ex.GetType().Name}: {ex.Message.Split('\n')[0]}");
        }
    }
}
```
