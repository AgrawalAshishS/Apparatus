---
title: SpanExtensions
layout: default
parent: API Reference
nav_order: 16
---

# SpanExtensions

Helpers for `ReadOnlySpan`.

## Methods

| Method | What it does |
|---|---|
| [`Concat<T>`](#concat-t) | Joins two spans into one new array. The first span comes first. |

## `Concat<T>`

```csharp
Concat<T>(ReadOnlySpan<T> s1, ReadOnlySpan<T> s2)
```

Joins two spans into one new array. The first span comes first.

**Type parameters**

| Name | Description |
|---|---|
| `T` | Item type. |

**Parameters**

| Name | Description |
|---|---|
| `s1` | The first span. |
| `s2` | The second span, added after the first. |

**Returns**

A new array holding all items of `s1` followed by all items of `s2`.

**Example**

```csharp
ReadOnlySpan<int> a = stackalloc int[] { 1, 2 };
ReadOnlySpan<int> b = stackalloc int[] { 3 };
int[] all = a.Concat(b); // { 1, 2, 3 }
```

