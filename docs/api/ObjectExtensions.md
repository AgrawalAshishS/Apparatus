---
title: ObjectExtensions
layout: default
parent: API Reference
nav_order: 15
---

# ObjectExtensions

Extension methods of Object class; so for everything.

## Methods

| Method | What it does |
|---|---|
| [`FullNameWithAssembly`](#fullnamewithassembly) | Gets the type name of this object with the assembly name, in `Namespace.Class, AssemblyName` format. |
| [`As<T>`](#as-t) | Casts the object to `T`. Useful when writing fluent code without extra brackets. |
| [`To<T>`](#to-t) | Converts the object to a value type such as `Int32`, `Decimal`, `DateTime` or `Guid`. Uses the invariant culture, so results do not depend on the computer's language settings. |
| [`CanBeCastTo<T>`](#canbecastto-t) | Checks whether this object can be cast to `T`. |
| [`Between<T>`](#between-t) | Checks whether a value is between two limits. Both limits are included. |
| [`In<T>`](#in-t) | Checks whether a value is one of the given values. Shorter than many `\|\|` checks. |
| [`In<T>`](#in-t-1) | Checks whether a value is in a collection. |
| [`If<T>`](#if-t) | Applies a change to a value only when a condition is true, so a fluent chain does not need to break. |
| [`If<T>`](#if-t-1) | Runs an action on a value only when a condition is true, and always returns the same value so a fluent chain can continue. |

## `FullNameWithAssembly`

```csharp
FullNameWithAssembly(object src)
```

Gets the type name of this object with the assembly name, in `Namespace.Class, AssemblyName` format.

**Parameters**

| Name | Description |
|---|---|
| `src` | The object. Must not be `null`. |

**Returns**

For example `"System.String, System.Private.CoreLib"`.

**Exceptions**

- `NullReferenceException`: `src` is `null`.

**Example**

```csharp
string name = "text".FullNameWithAssembly();
```

## `As<T>`

```csharp
As<T>(object src)
```

Casts the object to `T`. Useful when writing fluent code without extra brackets.

**Type parameters**

| Name | Description |
|---|---|
| `T` | The reference type to cast to. |

**Parameters**

| Name | Description |
|---|---|
| `src` | The object to cast. |

**Returns**

The same object as `T`.

**Exceptions**

- `InvalidCastException`: The object is not a `T`.

**Example**

```csharp
object o = "text";
int length = o.As<string>().Length;
```

## `To<T>`

```csharp
To<T>(object src)
```

Converts the object to a value type such as `Int32`, `Decimal`, `DateTime` or `Guid`. Uses the invariant culture, so results do not depend on the computer's language settings.

**Type parameters**

| Name | Description |
|---|---|
| `T` | The value type to convert to. |

**Parameters**

| Name | Description |
|---|---|
| `src` | The value to convert, for example a string like `"42"`. |

**Returns**

The converted value.

**Exceptions**

- `FormatException`: The value is not in a valid format.
- `InvalidCastException`: The conversion is not supported.
- `OverflowException`: The value is too big or too small for `T`.

**Example**

```csharp
int number = "42".To<int>();
Guid id = "d3b07384-d9a1-4c3e-8b5a-2f6c5a1e9c00".To<Guid>();
```

## `CanBeCastTo<T>`

```csharp
CanBeCastTo<T>(object src)
```

Checks whether this object can be cast to `T`.

**Type parameters**

| Name | Description |
|---|---|
| `T` | The target type. |

**Parameters**

| Name | Description |
|---|---|
| `src` | The object to check. A `null` value returns `false`. |

**Returns**

`true` if the object's type is, inherits from or implements `T`; otherwise `false`.

**Example**

```csharp
"text".CanBeCastTo<IEnumerable<char>>(); // true
```

## `Between<T>`

```csharp
Between<T>(T src, T from, T to)
```

Checks whether a value is between two limits. Both limits are included.

**Type parameters**

| Name | Description |
|---|---|
| `T` | Any comparable type, for example numbers, dates or strings. |

**Parameters**

| Name | Description |
|---|---|
| `src` | The value to check. |
| `from` | The lower limit (included). |
| `to` | The upper limit (included). |

**Returns**

`true` if `from` <= `src` <= `to`; otherwise `false`.

**Example**

```csharp
5.Between(1, 10);  // true
10.Between(1, 10); // true
11.Between(1, 10); // false
```

## `In<T>`

```csharp
In<T>(T src, T[] list)
```

Checks whether a value is one of the given values. Shorter than many `||` checks.

**Type parameters**

| Name | Description |
|---|---|
| `T` | The value type. |

**Parameters**

| Name | Description |
|---|---|
| `src` | The value to look for. |
| `list` | The allowed values. |

**Returns**

`true` if the value is in the list; otherwise `false`.

**Example**

```csharp
"b".In("a", "b", "c"); // true
```

## `In<T>`

```csharp
In<T>(T src, IEnumerable<T> list)
```

Checks whether a value is in a collection.

**Type parameters**

| Name | Description |
|---|---|
| `T` | The value type. |

**Parameters**

| Name | Description |
|---|---|
| `src` | The value to look for. |
| `list` | The collection to search. |

**Returns**

`true` if the value is in the collection; otherwise `false`.

**Example**

```csharp
var allowed = new List<int> { 1, 2, 3 };
2.In(allowed); // true
```

## `If<T>`

```csharp
If<T>(T src, bool condition, Func<T, T> func)
```

Applies a change to a value only when a condition is true, so a fluent chain does not need to break.

**Type parameters**

| Name | Description |
|---|---|
| `T` | The value type. |

**Parameters**

| Name | Description |
|---|---|
| `src` | The value to work on. |
| `condition` | When `false`, nothing happens and `src` is returned as it is. |
| `func` | Function that takes the value and returns the new value. |

**Returns**

The result of `func` if `condition` is `true`; otherwise `src`.

**Example**

```csharp
string name = "john".If(isFormal, s => s.ToUpper());
```

## `If<T>`

```csharp
If<T>(T src, bool condition, Action<T> action)
```

Runs an action on a value only when a condition is true, and always returns the same value so a fluent chain can continue.

**Type parameters**

| Name | Description |
|---|---|
| `T` | The value type. |

**Parameters**

| Name | Description |
|---|---|
| `src` | The value to work on. |
| `condition` | When `false`, the action is not run. |
| `action` | Action that receives the value. |

**Returns**

Always `src`.

**Example**

```csharp
list.If(log, l => Console.WriteLine(l.Count)).Add(5);
```

