---
title: EnumExtensions
layout: default
parent: API Reference
nav_order: 7
---

# EnumExtensions

Helpers for working with enumerations.

## Methods

| Method | What it does |
|---|---|
| [`ToCommaSeparatedList<TEnum>`](#tocommaseparatedlist-tenum) | Joins a list of enum values into one comma separated string, either as numbers or as names. |
| [`EnumToDictionary`](#enumtodictionary) | Converts an enumeration type into a dictionary of its names and numeric values. |

## `ToCommaSeparatedList<TEnum>`

```csharp
ToCommaSeparatedList<TEnum>(IEnumerable<TEnum> items, bool useEnumValue)
```

Joins a list of enum values into one comma separated string, either as numbers or as names.

**Type parameters**

| Name | Description |
|---|---|
| `TEnum` | The enum type. |

**Parameters**

| Name | Description |
|---|---|
| `items` | The enum values to join. |
| `useEnumValue` | `true` (default) to write the numeric values; `false` to write the names. |

**Returns**

A string like `"1,3"` or `"Red,Blue"`. Empty string for an empty list.

**Example**

```csharp
var colors = new[] { Color.Red, Color.Blue };
colors.ToCommaSeparatedList();      // "0,2"
colors.ToCommaSeparatedList(false); // "Red,Blue"
```

## `EnumToDictionary`

```csharp
EnumToDictionary(Type t)
```

Converts an enumeration type into a dictionary of its names and numeric values.

**Parameters**

| Name | Description |
|---|---|
| `t` | The enum type, for example `typeof(DayOfWeek)`. |

**Returns**

A dictionary where the key is the enum member name and the value is its `Int32` value.

**Exceptions**

- `NullReferenceException`: `t` is `null`.

**Example**

```csharp
var map = typeof(DayOfWeek).EnumToDictionary(); // { "Sunday": 0, "Monday": 1, ... }
```

