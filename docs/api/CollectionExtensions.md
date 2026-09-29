---
title: CollectionExtensions
layout: default
parent: API Reference
nav_order: 2
---

# CollectionExtensions

Extension methods for generic collections.

## Methods

| Method | What it does |
|---|---|
| [`IsNullOrEmpty<T>`](#isnullorempty-t) | Checks whether a collection is `null` or has no items. |
| [`RemoveAll<T>`](#removeall-t) | Removes from a collection every item that also appears in another list. Each matching item is removed once per occurrence in `items`. |

## `IsNullOrEmpty<T>`

```csharp
IsNullOrEmpty<T>(ICollection<T> source)
```

Checks whether a collection is `null` or has no items.

**Type parameters**

| Name | Description |
|---|---|
| `T` | Type of the items. |

**Parameters**

| Name | Description |
|---|---|
| `source` | The collection to check. May be `null`. |

**Returns**

`true` if `source` is `null` or empty; otherwise `false`.

**Example**

```csharp
List<int>? list = null;
list.IsNullOrEmpty(); // true
```

## `RemoveAll<T>`

```csharp
RemoveAll<T>(ICollection<T> source, IEnumerable<T> items)
```

Removes from a collection every item that also appears in another list. Each matching item is removed once per occurrence in `items`.

**Type parameters**

| Name | Description |
|---|---|
| `T` | Type of the items. |

**Parameters**

| Name | Description |
|---|---|
| `source` | The collection to remove items from. Must not be `null`. |
| `items` | The items to remove. Must not be `null`. |

**Exceptions**

- `ArgumentNullException`: `source` or `items` is `null`.

**Example**

```csharp
var numbers = new List<int> { 1, 2, 3, 4 };
numbers.RemoveAll(new[] { 2, 4 }); // numbers is now { 1, 3 }
```

