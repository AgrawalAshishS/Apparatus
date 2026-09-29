---
title: TypeExtensions
layout: default
parent: API Reference
nav_order: 21
---

# TypeExtensions

Extensions for Type class (object.GetType()).

## Methods

| Method | What it does |
|---|---|
| [`ToType`](#totype) | Finds a `Type` from its name. See `GetType` for the name rules. |
| [`FullNameWithAssembly`](#fullnamewithassembly) | Gets the type name with the assembly name, in `Namespace.Class, AssemblyName` format. |
| [`CanBeCastTo<T>`](#canbecastto-t) | Checks whether this type can be cast to `T`, which means it is the same type, inherits from it, or implements it. |
| [`CanBeCastTo`](#canbecastto) | Checks whether this type can be cast to `destinationType`, which means it is the same type, inherits from it, or implements it. |
| [`In`](#in) | Checks whether this type is exactly one of the given types. |
| [`IsEnumerable`](#isenumerable) | Checks whether a type is a collection that can be looped with `foreach` (implements `IEnumerable`). `String` is not treated as a collection. |
| [`IsAssignableTo<TTarget>`](#isassignableto-ttarget) | Checks whether a value of this type can be assigned to a variable of type `TTarget`. |
| [`IsAssignableTo`](#isassignableto) | Checks whether a value of this type can be assigned to a variable of type `targetType`. |

## `ToType`

```csharp
ToType(string typeName)
```

Finds a `Type` from its name. See `GetType` for the name rules.

**Parameters**

| Name | Description |
|---|---|
| `typeName` | Type name, for example `"System.String"` or an assembly qualified name. |

**Returns**

The type, or `null` if it cannot be found.

**Example**

```csharp
Type t = "System.Guid".ToType(); // typeof(Guid)
```

## `FullNameWithAssembly`

```csharp
FullNameWithAssembly(Type type)
```

Gets the type name with the assembly name, in `Namespace.Class, AssemblyName` format.

**Parameters**

| Name | Description |
|---|---|
| `type` | The type. |

**Returns**

For example `"System.String, System.Private.CoreLib"`.

**Example**

```csharp
string name = typeof(TypeExtensions).FullNameWithAssembly(); // "Apparatus.TypeExtensions, Apparatus"
```

## `CanBeCastTo<T>`

```csharp
CanBeCastTo<T>(Type type)
```

Checks whether this type can be cast to `T`, which means it is the same type, inherits from it, or implements it.

**Type parameters**

| Name | Description |
|---|---|
| `T` | The target type. |

**Parameters**

| Name | Description |
|---|---|
| `type` | The type to check. A `null` value returns `false`. |

**Returns**

`true` if the cast is possible; otherwise `false`.

**Example**

```csharp
typeof(List<int>).CanBeCastTo<IEnumerable<int>>(); // true
```

## `CanBeCastTo`

```csharp
CanBeCastTo(Type type, Type destinationType)
```

Checks whether this type can be cast to `destinationType`, which means it is the same type, inherits from it, or implements it.

**Parameters**

| Name | Description |
|---|---|
| `type` | The type to check. A `null` value returns `false`. |
| `destinationType` | The target type. |

**Returns**

`true` if the cast is possible; otherwise `false`.

**Example**

```csharp
typeof(string).CanBeCastTo(typeof(object)); // true
typeof(string).CanBeCastTo(typeof(int));    // false
```

## `In`

```csharp
In(Type t, Type[] types)
```

Checks whether this type is exactly one of the given types.

**Parameters**

| Name | Description |
|---|---|
| `t` | The type to check. |
| `types` | The types to compare against. |

**Returns**

`true` if `t` equals any type in `types`; otherwise `false`.

**Example**

```csharp
typeof(int).In(typeof(int), typeof(long)); // true
```

## `IsEnumerable`

```csharp
IsEnumerable(Type type)
```

Checks whether a type is a collection that can be looped with `foreach` (implements `IEnumerable`). `String` is not treated as a collection.

**Parameters**

| Name | Description |
|---|---|
| `type` | The type to check. |

**Returns**

`true` for lists, arrays, dictionaries and similar; `false` for `String` and normal types.

**Example**

```csharp
typeof(List<int>).IsEnumerable(); // true
typeof(string).IsEnumerable();      // false
```

## `IsAssignableTo<TTarget>`

```csharp
IsAssignableTo<TTarget>(Type type)
```

Checks whether a value of this type can be assigned to a variable of type `TTarget`.

**Type parameters**

| Name | Description |
|---|---|
| `TTarget` | The target type. |

**Parameters**

| Name | Description |
|---|---|
| `type` | The source type. Must not be `null`. |

**Returns**

`true` if assignable; otherwise `false`.

**Exceptions**

- `ArgumentNullException`: `type` is `null`.

**Example**

```csharp
typeof(string).IsAssignableTo<object>(); // true
```

## `IsAssignableTo`

```csharp
IsAssignableTo(Type type, Type targetType)
```

Checks whether a value of this type can be assigned to a variable of type `targetType`.

**Parameters**

| Name | Description |
|---|---|
| `type` | The source type. Must not be `null`. |
| `targetType` | The target type. Must not be `null`. |

**Returns**

`true` if assignable; otherwise `false`.

**Exceptions**

- `ArgumentNullException`: `type` or `targetType` is `null`.

**Remarks**

.NET 5 and later already have `Type.IsAssignableTo(Type)`, and the compiler picks it over this extension when you write `type.IsAssignableTo(target)`. To use this version, call it as `TypeExtensions.IsAssignableTo(type, target)`.

**Example**

```csharp
TypeExtensions.IsAssignableTo(typeof(int), typeof(object)); // true
```

