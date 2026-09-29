---
title: NumericExtensions
layout: default
parent: API Reference
nav_order: 14
---

# NumericExtensions

Helpers for numeric types such as `Int32`, `Int64` and `Decimal`.

## Methods

| Method | What it does |
|---|---|
| [`In`](#in) | Checks whether this `Int64` value equals any of the given values. Shorter and easier to read than a chain of `\|\|` comparisons. |
| [`In`](#in-1) | Checks whether this `Int32` value equals any of the given values. Shorter and easier to read than a chain of `\|\|` comparisons. |
| [`In`](#in-2) | Checks whether this `Double` value equals any of the given values. Shorter and easier to read than a chain of `\|\|` comparisons. |
| [`In`](#in-3) | Checks whether this `Decimal` value equals any of the given values. Shorter and easier to read than a chain of `\|\|` comparisons. |
| [`In`](#in-4) | Checks whether this `Single` value equals any of the given values. Shorter and easier to read than a chain of `\|\|` comparisons. |
| [`In`](#in-5) | Checks whether this `Int16` value equals any of the given values. Shorter and easier to read than a chain of `\|\|` comparisons. |
| [`In`](#in-6) | Checks whether this `Byte` value equals any of the given values. Shorter and easier to read than a chain of `\|\|` comparisons. |

## `In`

```csharp
In(long toCheck, long[] values)
```

Checks whether this `Int64` value equals any of the given values. Shorter and easier to read than a chain of `||` comparisons.

**Parameters**

| Name | Description |
|---|---|
| `toCheck` | The value to look for. |
| `values` | The values to compare against. |

**Returns**

`true` if `toCheck` equals at least one of `values`; otherwise `false`. Returns `false` when no values are given.

**Example**

```csharp
long id = 10;
if (id.In(10, 11, 12))
{
    // runs, because the value is in the list
}
```

## `In`

```csharp
In(int toCheck, int[] values)
```

Checks whether this `Int32` value equals any of the given values. Shorter and easier to read than a chain of `||` comparisons.

**Parameters**

| Name | Description |
|---|---|
| `toCheck` | The value to look for. |
| `values` | The values to compare against. |

**Returns**

`true` if `toCheck` equals at least one of `values`; otherwise `false`. Returns `false` when no values are given.

**Example**

```csharp
int id = 10;
if (id.In(10, 11, 12))
{
    // runs, because the value is in the list
}
```

## `In`

```csharp
In(double toCheck, double[] values)
```

Checks whether this `Double` value equals any of the given values. Shorter and easier to read than a chain of `||` comparisons.

**Parameters**

| Name | Description |
|---|---|
| `toCheck` | The value to look for. |
| `values` | The values to compare against. |

**Returns**

`true` if `toCheck` equals at least one of `values`; otherwise `false`. Returns `false` when no values are given.

**Example**

```csharp
double d = 2.5;
if (d.In(2.5, 3.5))
{
    // runs, because the value is in the list
}
```

## `In`

```csharp
In(decimal toCheck, decimal[] values)
```

Checks whether this `Decimal` value equals any of the given values. Shorter and easier to read than a chain of `||` comparisons.

**Parameters**

| Name | Description |
|---|---|
| `toCheck` | The value to look for. |
| `values` | The values to compare against. |

**Returns**

`true` if `toCheck` equals at least one of `values`; otherwise `false`. Returns `false` when no values are given.

**Example**

```csharp
decimal d = 2.5m;
if (d.In(2.5m, 3.5m))
{
    // runs, because the value is in the list
}
```

## `In`

```csharp
In(float toCheck, float[] values)
```

Checks whether this `Single` value equals any of the given values. Shorter and easier to read than a chain of `||` comparisons.

**Parameters**

| Name | Description |
|---|---|
| `toCheck` | The value to look for. |
| `values` | The values to compare against. |

**Returns**

`true` if `toCheck` equals at least one of `values`; otherwise `false`. Returns `false` when no values are given.

**Example**

```csharp
float f = 2.5f;
if (f.In(2.5f, 3.5f))
{
    // runs, because the value is in the list
}
```

## `In`

```csharp
In(short toCheck, short[] values)
```

Checks whether this `Int16` value equals any of the given values. Shorter and easier to read than a chain of `||` comparisons.

**Parameters**

| Name | Description |
|---|---|
| `toCheck` | The value to look for. |
| `values` | The values to compare against. |

**Returns**

`true` if `toCheck` equals at least one of `values`; otherwise `false`. Returns `false` when no values are given.

**Example**

```csharp
short s = 10;
if (s.In(10, 11, 12))
{
    // runs, because the value is in the list
}
```

## `In`

```csharp
In(byte toCheck, byte[] values)
```

Checks whether this `Byte` value equals any of the given values. Shorter and easier to read than a chain of `||` comparisons.

**Parameters**

| Name | Description |
|---|---|
| `toCheck` | The value to look for. |
| `values` | The values to compare against. |

**Returns**

`true` if `toCheck` equals at least one of `values`; otherwise `false`. Returns `false` when no values are given.

**Example**

```csharp
byte b = 10;
if (b.In(10, 11, 12))
{
    // runs, because the value is in the list
}
```

