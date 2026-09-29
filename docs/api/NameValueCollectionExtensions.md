---
title: NameValueCollectionExtensions
layout: default
parent: API Reference
nav_order: 13
---

# NameValueCollectionExtensions

Extension methods for NameValueCollection.

## Methods

| Method | What it does |
|---|---|
| [`ToDictionary`](#todictionary) | Converts a `NameValueCollection` (for example query string values) to a `Dictionary`. |

## `ToDictionary`

```csharp
ToDictionary(NameValueCollection source)
```

Converts a `NameValueCollection` (for example query string values) to a `Dictionary`.

**Parameters**

| Name | Description |
|---|---|
| `source` | The collection to convert. |

**Returns**

A dictionary with one entry per key. If a key has several values, they are joined with a comma (standard `NameValueCollection` behavior).

**Exceptions**

- `NullReferenceException`: `source` is `null`.

**Example**

```csharp
var query = HttpUtility.ParseQueryString("a=1&b=2");
Dictionary<string, string> map = query.ToDictionary(); // { "a": "1", "b": "2" }
```

