---
title: Getting started
layout: default
nav_order: 4
---

# Getting started

## Install

The package id is `Apparatus`. It targets .NET 8.

```
dotnet add package Apparatus
```

To use the source directly instead, add a project reference to `src/Apparatus/Apparatus.csproj`.

## Use it

Add one `using` line. Every extension method then shows up in code completion, with its description.

```csharp
using Apparatus;

var name = "  ";
if (name.IsEmpty())
{
    Console.WriteLine("Please type a name.");
}
```

## Two things to know

**Name clashes.** A few names exist in .NET too. When you use implicit usings (the default in new projects):

- `TaskExtensions` clashes with `System.Threading.Tasks.TaskExtensions`. Write `Apparatus.TaskExtensions.RunSync(...)` in full. The extension methods `Await()` work normally.
- `Type.IsAssignableTo(Type)` and `Stream.CopyToAsync(Stream, CancellationToken)` already exist in .NET, and the compiler picks the .NET version over the Apparatus one when you write `type.IsAssignableTo(x)`. Call the Apparatus version as a normal method, for example `TypeExtensions.IsAssignableTo(type, x)`.

**Null handling.** Most methods do not accept `null` unless their description says so. Use `ThrowIf` or `IsNullOrEmpty` to check first.

## Find what you need

- Every method has a short description, parameter notes and a small example that shows in your editor while you type.
- The [API Reference](api/) lists the same information for all classes.
- The [Examples](examples/) run as a console program: `dotnet run --project examples/Apparatus.Examples`.
