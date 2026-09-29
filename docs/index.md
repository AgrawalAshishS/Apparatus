---
title: Home
layout: default
nav_order: 1
permalink: /
---

# Apparatus

Apparatus is a small C# library of extension methods and helpers. It removes the boring, repeated code
from everyday work so your own code stays short and easy to read.

Made by Toshal Infotech.

## A quick look

```csharp
using Apparatus;

// Check arguments in one line each
public void Save(string name, int age)
{
    name.ThrowIfNullOrEmpty();
    age.ThrowIfNegative();
}

// Change text style
"user_first-name".ToPascalCase();   // "UserFirstName"
"HTTPRequest".ToKebabCase();        // "http-request"

// Short checks
if (status.In("open", "pending")) { /* ... */ }
if (DayOfWeek.Saturday.IsWeekend()) { /* ... */ }

// Call async code from normal code
var text = File.ReadAllTextAsync("a.txt").Await();

// Fill objects from a database reader in one call
List<Customer> customers = cmd.ExecuteReader().FillCollection<Customer>();
```

## Where to go next

| I want to... | Go to |
|---|---|
| Install the library and try it | [Getting started](getting-started.html) |
| Look up one method | [API Reference](api/) |
| See working code | [Examples](examples/) |
| Know about bugs I should avoid today | [Known issues](known-issues.html) |
| Build, test or contribute | [Contributing](contributing.html) |

## What is inside

| Area | Classes |
|---|---|
| Text | `StringExtensions`, `InputFilters`, `EncryptionUtility` |
| Checking values | `ThrowIf`, `NumericExtensions`, `ObjectExtensions` |
| Dates and enums | `DateTimeExtensions`, `EnumExtensions` |
| Collections and types | `CollectionExtensions`, `NameValueCollectionExtensions`, `TypeExtensions`, `SpanExtensions` |
| Files and streams | `FileHelper`, `DirectoryHelper`, `StreamExtensions` |
| Async | `TaskExtensions` |
| Databases | `DataReaderExtension`, `IHydrator` |
| Errors | `Apparatus.Exceptions.Exception` (an exception with an error code) |
