---
title: ThrowIf
layout: default
parent: API Reference
nav_order: 20
---

# ThrowIf

Short, one-line checks for method arguments. Each check throws a standard exception when the value is not valid. Every check exists in two styles: a static call such as `ThrowIf.Negative(age)`, and an extension call such as `age.ThrowIfNegative()`. The compiler fills in the argument name for you, so the exception message names the real variable.

**Example**

Without this class:

```csharp
public void MyMethod(int age, string name)
{
    if (age < 0)
        throw new ArgumentOutOfRangeException(nameof(age), "Age can't be less than zero");
    if (string.IsNullOrEmpty(name))
        throw new ArgumentNullException(nameof(name));
}
```

 With this class:

```csharp
public void MyMethod(int age, string name)
{
    age.ThrowIfNegative();
    name.ThrowIfNullOrEmpty();
}
```

## Methods

| Method | What it does |
|---|---|
| [`Null`](#null) | Throws an `ArgumentNullException` if the argument is `null`. |
| [`NullOrEmpty`](#nullorempty) | Throws an `ArgumentNullException` if the argument is `null` or an empty string. |
| [`ZeroOrLess`](#zeroorless) | Throws an `ArgumentOutOfRangeException` if the argument is zero or less. |
| [`ZeroOrLess`](#zeroorless-1) | Throws an `ArgumentOutOfRangeException` if the argument is zero or less. |
| [`Negative`](#negative) | Throws an `ArgumentOutOfRangeException` if the argument is less than zero (zero is allowed). |
| [`Negative`](#negative-1) | Throws an `ArgumentOutOfRangeException` if the argument is less than zero (zero is allowed). |
| [`EmptyGuid`](#emptyguid) | Throws an `ArgumentOutOfRangeException` if the argument is `Empty`. |
| [`NotEqual`](#notequal) | Throws an `ArgumentOutOfRangeException` if the argument is not equal to `actual`. |
| [`NotEqual`](#notequal-1) | Throws an `ArgumentOutOfRangeException` if the argument is not equal to `actual`. |
| [`LesserThan`](#lesserthan) | Throws an `ArgumentOutOfRangeException` if the argument is less than `minimumNeeded`. |
| [`LesserThan`](#lesserthan-1) | Throws an `ArgumentOutOfRangeException` if the argument is less than `minimumNeeded`. |
| [`GreatorThan`](#greatorthan) | Throws an `ArgumentOutOfRangeException` if the argument is greater than `maximumAllowed`. |
| [`GreatorThan`](#greatorthan-1) | Throws an `ArgumentOutOfRangeException` if the argument is greater than `maximumAllowed`. |
| [`LessThanEqualTo`](#lessthanequalto) | Throws an `ArgumentOutOfRangeException` if the argument is less than or equal to `minimumNeeded`. |
| [`LessThanEqualTo`](#lessthanequalto-1) | Throws an `ArgumentOutOfRangeException` if the argument is less than or equal to `minimumNeeded`. |
| [`GreatorThanEqualTo`](#greatorthanequalto) | Throws an `ArgumentOutOfRangeException` if the argument is greater than or equal to `maximumAllowed`. |
| [`GreatorThanEqualTo`](#greatorthanequalto-1) | Throws an `ArgumentOutOfRangeException` if the argument is greater than or equal to `maximumAllowed`. |
| [`ThrowIfNull`](#throwifnull) | Throws an `ArgumentNullException` if this value is `null`. Same as calling `ThrowIf.Null`, but written as a method on the value. |
| [`ThrowIfNullOrEmpty`](#throwifnullorempty) | Throws an `ArgumentNullException` if this value is `null` or an empty string. Same as calling `ThrowIf.NullOrEmpty`, but written as a method on the value. |
| [`ThrowIfZeroOrLess`](#throwifzeroorless) | Throws an `ArgumentOutOfRangeException` if this value is zero or less. Same as calling `ThrowIf.ZeroOrLess`, but written as a method on the value. |
| [`ThrowIfZeroOrLess`](#throwifzeroorless-1) | Throws an `ArgumentOutOfRangeException` if this value is zero or less. Same as calling `ThrowIf.ZeroOrLess`, but written as a method on the value. |
| [`ThrowIfNegative`](#throwifnegative) | Throws an `ArgumentOutOfRangeException` if this value is less than zero (zero is allowed). Same as calling `ThrowIf.Negative`, but written as a method on the value. |
| [`ThrowIfNegative`](#throwifnegative-1) | Throws an `ArgumentOutOfRangeException` if this value is less than zero (zero is allowed). Same as calling `ThrowIf.Negative`, but written as a method on the value. |
| [`ThrowIfEmptyGuid`](#throwifemptyguid) | Throws an `ArgumentOutOfRangeException` if this value is `Empty`. Same as calling `ThrowIf.EmptyGuid`, but written as a method on the value. |
| [`ThrowIfNotEqual`](#throwifnotequal) | Throws an `ArgumentOutOfRangeException` if this value is not equal to `actual`. Same as calling `ThrowIf.NotEqual`, but written as a method on the value. |
| [`ThrowIfNotEqual`](#throwifnotequal-1) | Throws an `ArgumentOutOfRangeException` if this value is not equal to `actual`. Same as calling `ThrowIf.NotEqual`, but written as a method on the value. |
| [`ThrowIfLesserThan`](#throwiflesserthan) | Throws an `ArgumentOutOfRangeException` if this value is less than `minimumNeeded`. Same as calling `ThrowIf.LesserThan`, but written as a method on the value. |
| [`ThrowIfLesserThan`](#throwiflesserthan-1) | Throws an `ArgumentOutOfRangeException` if this value is less than `minimumNeeded`. Same as calling `ThrowIf.LesserThan`, but written as a method on the value. |
| [`ThrowIfGreatorThan`](#throwifgreatorthan) | Throws an `ArgumentOutOfRangeException` if this value is greater than `maximumAllowed`. Same as calling `ThrowIf.GreatorThan`, but written as a method on the value. |
| [`ThrowIfGreatorThan`](#throwifgreatorthan-1) | Throws an `ArgumentOutOfRangeException` if this value is greater than `maximumAllowed`. Same as calling `ThrowIf.GreatorThan`, but written as a method on the value. |
| [`ThrowIfLessThanEqualTo`](#throwiflessthanequalto) | Throws an `ArgumentOutOfRangeException` if this value is less than or equal to `minimumNeeded`. Same as calling `ThrowIf.LessThanEqualTo`, but written as a method on the value. |
| [`ThrowIfLessThanEqualTo`](#throwiflessthanequalto-1) | Throws an `ArgumentOutOfRangeException` if this value is less than or equal to `minimumNeeded`. Same as calling `ThrowIf.LessThanEqualTo`, but written as a method on the value. |
| [`ThrowIfGreatorThanEqualTo`](#throwifgreatorthanequalto) | Throws an `ArgumentOutOfRangeException` if this value is greater than or equal to `maximumAllowed`. Same as calling `ThrowIf.GreatorThanEqualTo`, but written as a method on the value. |
| [`ThrowIfGreatorThanEqualTo`](#throwifgreatorthanequalto-1) | Throws an `ArgumentOutOfRangeException` if this value is greater than or equal to `maximumAllowed`. Same as calling `ThrowIf.GreatorThanEqualTo`, but written as a method on the value. |

## `Null`

```csharp
Null(object argument, string argumentName)
```

Throws an `ArgumentNullException` if the argument is `null`.

**Parameters**

| Name | Description |
|---|---|
| `argument` | The value to check. |
| `argumentName` | Filled in automatically by the compiler with the source text of `argument`. Leave it empty. |

**Exceptions**

- `ArgumentNullException`: Thrown when the value is `null`.

**Example**

```csharp
ThrowIf.Null(name);
```

## `NullOrEmpty`

```csharp
NullOrEmpty(string argument, string argumentName)
```

Throws an `ArgumentNullException` if the argument is `null` or an empty string.

**Parameters**

| Name | Description |
|---|---|
| `argument` | The value to check. |
| `argumentName` | Filled in automatically by the compiler with the source text of `argument`. Leave it empty. |

**Exceptions**

- `ArgumentNullException`: Thrown when the value is `null` or an empty string.

**Example**

```csharp
ThrowIf.NullOrEmpty(name);
```

## `ZeroOrLess`

```csharp
ZeroOrLess(int argument, string argumentName)
```

Throws an `ArgumentOutOfRangeException` if the argument is zero or less.

**Parameters**

| Name | Description |
|---|---|
| `argument` | The value to check. |
| `argumentName` | Filled in automatically by the compiler with the source text of `argument`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is zero or less.

**Example**

```csharp
ThrowIf.ZeroOrLess(count);
```

## `ZeroOrLess`

```csharp
ZeroOrLess(long argument, string argumentName)
```

Throws an `ArgumentOutOfRangeException` if the argument is zero or less.

**Parameters**

| Name | Description |
|---|---|
| `argument` | The value to check. |
| `argumentName` | Filled in automatically by the compiler with the source text of `argument`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is zero or less.

**Example**

```csharp
ThrowIf.ZeroOrLess(count);
```

## `Negative`

```csharp
Negative(int argument, string argumentName)
```

Throws an `ArgumentOutOfRangeException` if the argument is less than zero (zero is allowed).

**Parameters**

| Name | Description |
|---|---|
| `argument` | The value to check. |
| `argumentName` | Filled in automatically by the compiler with the source text of `argument`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is less than zero (zero is allowed).

**Example**

```csharp
ThrowIf.Negative(index);
```

## `Negative`

```csharp
Negative(long argument, string argumentName)
```

Throws an `ArgumentOutOfRangeException` if the argument is less than zero (zero is allowed).

**Parameters**

| Name | Description |
|---|---|
| `argument` | The value to check. |
| `argumentName` | Filled in automatically by the compiler with the source text of `argument`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is less than zero (zero is allowed).

**Example**

```csharp
ThrowIf.Negative(index);
```

## `EmptyGuid`

```csharp
EmptyGuid(Guid argument, string argumentName)
```

Throws an `ArgumentOutOfRangeException` if the argument is `Empty`.

**Parameters**

| Name | Description |
|---|---|
| `argument` | The value to check. |
| `argumentName` | Filled in automatically by the compiler with the source text of `argument`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is `Empty`.

**Example**

```csharp
ThrowIf.EmptyGuid(id);
```

## `NotEqual`

```csharp
NotEqual(int expected, int actual, string expectedName, string actualName)
```

Throws an `ArgumentOutOfRangeException` if the argument is not equal to `actual`.

**Parameters**

| Name | Description |
|---|---|
| `expected` | The expected value. |
| `actual` | The value to compare with. |
| `expectedName` | Filled in automatically by the compiler with the source text of `expected`. Leave it empty. |
| `actualName` | Filled in automatically by the compiler with the source text of `actual`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is not equal to `actual`.

**Example**

```csharp
ThrowIf.NotEqual(expected, actual);
```

## `NotEqual`

```csharp
NotEqual(long expected, long actual, string expectedName, string actualName)
```

Throws an `ArgumentOutOfRangeException` if the argument is not equal to `actual`.

**Parameters**

| Name | Description |
|---|---|
| `expected` | The expected value. |
| `actual` | The value to compare with. |
| `expectedName` | Filled in automatically by the compiler with the source text of `expected`. Leave it empty. |
| `actualName` | Filled in automatically by the compiler with the source text of `actual`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is not equal to `actual`.

**Example**

```csharp
ThrowIf.NotEqual(expected, actual);
```

## `LesserThan`

```csharp
LesserThan(int value, int minimumNeeded, string valueName, string minimumNeededName)
```

Throws an `ArgumentOutOfRangeException` if the argument is less than `minimumNeeded`.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `minimumNeeded` | The smallest allowed value (included). |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `minimumNeededName` | Filled in automatically by the compiler with the source text of `minimumNeeded`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is less than `minimumNeeded`.

**Example**

```csharp
ThrowIf.LesserThan(age, 18);
```

## `LesserThan`

```csharp
LesserThan(long value, long minimumNeeded, string valueName, string minimumNeededName)
```

Throws an `ArgumentOutOfRangeException` if the argument is less than `minimumNeeded`.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `minimumNeeded` | The smallest allowed value (included). |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `minimumNeededName` | Filled in automatically by the compiler with the source text of `minimumNeeded`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is less than `minimumNeeded`.

**Example**

```csharp
ThrowIf.LesserThan(age, 18);
```

## `GreatorThan`

```csharp
GreatorThan(int value, int maximumAllowed, string valueName, string maximumAllowedName)
```

Throws an `ArgumentOutOfRangeException` if the argument is greater than `maximumAllowed`.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `maximumAllowed` | The biggest allowed value (included). |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `maximumAllowedName` | Filled in automatically by the compiler with the source text of `maximumAllowed`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is greater than `maximumAllowed`.

**Example**

```csharp
ThrowIf.GreatorThan(age, 65);
```

## `GreatorThan`

```csharp
GreatorThan(long value, long maximumAllowed, string valueName, string maximumAllowedName)
```

Throws an `ArgumentOutOfRangeException` if the argument is greater than `maximumAllowed`.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `maximumAllowed` | The biggest allowed value (included). |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `maximumAllowedName` | Filled in automatically by the compiler with the source text of `maximumAllowed`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is greater than `maximumAllowed`.

**Example**

```csharp
ThrowIf.GreatorThan(age, 65);
```

## `LessThanEqualTo`

```csharp
LessThanEqualTo(int value, int minimumNeeded, string valueName, string minimumNeededName)
```

Throws an `ArgumentOutOfRangeException` if the argument is less than or equal to `minimumNeeded`.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `minimumNeeded` | The limit. The value must be greater than this. |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `minimumNeededName` | Filled in automatically by the compiler with the source text of `minimumNeeded`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is less than or equal to `minimumNeeded`.

**Example**

```csharp
ThrowIf.LessThanEqualTo(age, 0);
```

## `LessThanEqualTo`

```csharp
LessThanEqualTo(long value, long minimumNeeded, string valueName, string minimumNeededName)
```

Throws an `ArgumentOutOfRangeException` if the argument is less than or equal to `minimumNeeded`.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `minimumNeeded` | The limit. The value must be greater than this. |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `minimumNeededName` | Filled in automatically by the compiler with the source text of `minimumNeeded`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is less than or equal to `minimumNeeded`.

**Example**

```csharp
ThrowIf.LessThanEqualTo(age, 0);
```

## `GreatorThanEqualTo`

```csharp
GreatorThanEqualTo(int value, int maximumAllowed, string valueName, string maximumAllowedName)
```

Throws an `ArgumentOutOfRangeException` if the argument is greater than or equal to `maximumAllowed`.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `maximumAllowed` | The limit. The value must be less than this. |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `maximumAllowedName` | Filled in automatically by the compiler with the source text of `maximumAllowed`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is greater than or equal to `maximumAllowed`.

**Example**

```csharp
ThrowIf.GreatorThanEqualTo(age, 100);
```

## `GreatorThanEqualTo`

```csharp
GreatorThanEqualTo(long value, long maximumAllowed, string valueName, string maximumAllowedName)
```

Throws an `ArgumentOutOfRangeException` if the argument is greater than or equal to `maximumAllowed`.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `maximumAllowed` | The limit. The value must be less than this. |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `maximumAllowedName` | Filled in automatically by the compiler with the source text of `maximumAllowed`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is greater than or equal to `maximumAllowed`.

**Example**

```csharp
ThrowIf.GreatorThanEqualTo(age, 100);
```

## `ThrowIfNull`

```csharp
ThrowIfNull(object argument, string argumentName)
```

Throws an `ArgumentNullException` if this value is `null`. Same as calling `ThrowIf.Null`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `argument` | The value to check. |
| `argumentName` | Filled in automatically by the compiler with the source text of `argument`. Leave it empty. |

**Exceptions**

- `ArgumentNullException`: Thrown when the value is `null`.

**Example**

```csharp
name.ThrowIfNull();
```

## `ThrowIfNullOrEmpty`

```csharp
ThrowIfNullOrEmpty(string argument, string argumentName)
```

Throws an `ArgumentNullException` if this value is `null` or an empty string. Same as calling `ThrowIf.NullOrEmpty`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `argument` | The value to check. |
| `argumentName` | Filled in automatically by the compiler with the source text of `argument`. Leave it empty. |

**Exceptions**

- `ArgumentNullException`: Thrown when the value is `null` or an empty string.

**Example**

```csharp
name.ThrowIfNullOrEmpty();
```

## `ThrowIfZeroOrLess`

```csharp
ThrowIfZeroOrLess(int argument, string argumentName)
```

Throws an `ArgumentOutOfRangeException` if this value is zero or less. Same as calling `ThrowIf.ZeroOrLess`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `argument` | The value to check. |
| `argumentName` | Filled in automatically by the compiler with the source text of `argument`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is zero or less.

**Example**

```csharp
count.ThrowIfZeroOrLess();
```

## `ThrowIfZeroOrLess`

```csharp
ThrowIfZeroOrLess(long argument, string argumentName)
```

Throws an `ArgumentOutOfRangeException` if this value is zero or less. Same as calling `ThrowIf.ZeroOrLess`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `argument` | The value to check. |
| `argumentName` | Filled in automatically by the compiler with the source text of `argument`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is zero or less.

**Example**

```csharp
count.ThrowIfZeroOrLess();
```

## `ThrowIfNegative`

```csharp
ThrowIfNegative(int argument, string argumentName)
```

Throws an `ArgumentOutOfRangeException` if this value is less than zero (zero is allowed). Same as calling `ThrowIf.Negative`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `argument` | The value to check. |
| `argumentName` | Filled in automatically by the compiler with the source text of `argument`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is less than zero (zero is allowed).

**Example**

```csharp
index.ThrowIfNegative();
```

## `ThrowIfNegative`

```csharp
ThrowIfNegative(long argument, string argumentName)
```

Throws an `ArgumentOutOfRangeException` if this value is less than zero (zero is allowed). Same as calling `ThrowIf.Negative`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `argument` | The value to check. |
| `argumentName` | Filled in automatically by the compiler with the source text of `argument`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is less than zero (zero is allowed).

**Example**

```csharp
index.ThrowIfNegative();
```

## `ThrowIfEmptyGuid`

```csharp
ThrowIfEmptyGuid(Guid argument, string argumentName)
```

Throws an `ArgumentOutOfRangeException` if this value is `Empty`. Same as calling `ThrowIf.EmptyGuid`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `argument` | The value to check. |
| `argumentName` | Filled in automatically by the compiler with the source text of `argument`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is `Empty`.

**Example**

```csharp
id.ThrowIfEmptyGuid();
```

## `ThrowIfNotEqual`

```csharp
ThrowIfNotEqual(int expected, int actual, string expectedName, string actualName)
```

Throws an `ArgumentOutOfRangeException` if this value is not equal to `actual`. Same as calling `ThrowIf.NotEqual`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `expected` | The expected value. |
| `actual` | The value to compare with. |
| `expectedName` | Filled in automatically by the compiler with the source text of `expected`. Leave it empty. |
| `actualName` | Filled in automatically by the compiler with the source text of `actual`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is not equal to `actual`.

**Example**

```csharp
expected.ThrowIfNotEqual(actual);
```

## `ThrowIfNotEqual`

```csharp
ThrowIfNotEqual(long expected, long actual, string expectedName, string actualName)
```

Throws an `ArgumentOutOfRangeException` if this value is not equal to `actual`. Same as calling `ThrowIf.NotEqual`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `expected` | The expected value. |
| `actual` | The value to compare with. |
| `expectedName` | Filled in automatically by the compiler with the source text of `expected`. Leave it empty. |
| `actualName` | Filled in automatically by the compiler with the source text of `actual`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is not equal to `actual`.

**Example**

```csharp
expected.ThrowIfNotEqual(actual);
```

## `ThrowIfLesserThan`

```csharp
ThrowIfLesserThan(int value, int minimumNeeded, string valueName, string minimumNeededName)
```

Throws an `ArgumentOutOfRangeException` if this value is less than `minimumNeeded`. Same as calling `ThrowIf.LesserThan`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `minimumNeeded` | The smallest allowed value (included). |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `minimumNeededName` | Filled in automatically by the compiler with the source text of `minimumNeeded`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is less than `minimumNeeded`.

**Example**

```csharp
age.ThrowIfLesserThan(18);
```

## `ThrowIfLesserThan`

```csharp
ThrowIfLesserThan(long value, long minimumNeeded, string valueName, string minimumNeededName)
```

Throws an `ArgumentOutOfRangeException` if this value is less than `minimumNeeded`. Same as calling `ThrowIf.LesserThan`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `minimumNeeded` | The smallest allowed value (included). |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `minimumNeededName` | Filled in automatically by the compiler with the source text of `minimumNeeded`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is less than `minimumNeeded`.

**Example**

```csharp
age.ThrowIfLesserThan(18);
```

## `ThrowIfGreatorThan`

```csharp
ThrowIfGreatorThan(int value, int maximumAllowed, string valueName, string maximumAllowedName)
```

Throws an `ArgumentOutOfRangeException` if this value is greater than `maximumAllowed`. Same as calling `ThrowIf.GreatorThan`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `maximumAllowed` | The biggest allowed value (included). |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `maximumAllowedName` | Filled in automatically by the compiler with the source text of `maximumAllowed`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is greater than `maximumAllowed`.

**Example**

```csharp
age.ThrowIfGreatorThan(65);
```

## `ThrowIfGreatorThan`

```csharp
ThrowIfGreatorThan(long value, long maximumAllowed, string valueName, string maximumAllowedName)
```

Throws an `ArgumentOutOfRangeException` if this value is greater than `maximumAllowed`. Same as calling `ThrowIf.GreatorThan`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `maximumAllowed` | The biggest allowed value (included). |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `maximumAllowedName` | Filled in automatically by the compiler with the source text of `maximumAllowed`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is greater than `maximumAllowed`.

**Example**

```csharp
age.ThrowIfGreatorThan(65);
```

## `ThrowIfLessThanEqualTo`

```csharp
ThrowIfLessThanEqualTo(int value, int minimumNeeded, string valueName, string minimumNeededName)
```

Throws an `ArgumentOutOfRangeException` if this value is less than or equal to `minimumNeeded`. Same as calling `ThrowIf.LessThanEqualTo`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `minimumNeeded` | The limit. The value must be greater than this. |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `minimumNeededName` | Filled in automatically by the compiler with the source text of `minimumNeeded`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is less than or equal to `minimumNeeded`.

**Example**

```csharp
age.ThrowIfLessThanEqualTo(0);
```

## `ThrowIfLessThanEqualTo`

```csharp
ThrowIfLessThanEqualTo(long value, long minimumNeeded, string valueName, string minimumNeededName)
```

Throws an `ArgumentOutOfRangeException` if this value is less than or equal to `minimumNeeded`. Same as calling `ThrowIf.LessThanEqualTo`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `minimumNeeded` | The limit. The value must be greater than this. |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `minimumNeededName` | Filled in automatically by the compiler with the source text of `minimumNeeded`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is less than or equal to `minimumNeeded`.

**Example**

```csharp
age.ThrowIfLessThanEqualTo(0);
```

## `ThrowIfGreatorThanEqualTo`

```csharp
ThrowIfGreatorThanEqualTo(int value, int maximumAllowed, string valueName, string maximumAllowedName)
```

Throws an `ArgumentOutOfRangeException` if this value is greater than or equal to `maximumAllowed`. Same as calling `ThrowIf.GreatorThanEqualTo`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `maximumAllowed` | The limit. The value must be less than this. |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `maximumAllowedName` | Filled in automatically by the compiler with the source text of `maximumAllowed`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is greater than or equal to `maximumAllowed`.

**Example**

```csharp
age.ThrowIfGreatorThanEqualTo(100);
```

## `ThrowIfGreatorThanEqualTo`

```csharp
ThrowIfGreatorThanEqualTo(long value, long maximumAllowed, string valueName, string maximumAllowedName)
```

Throws an `ArgumentOutOfRangeException` if this value is greater than or equal to `maximumAllowed`. Same as calling `ThrowIf.GreatorThanEqualTo`, but written as a method on the value.

**Parameters**

| Name | Description |
|---|---|
| `value` | The value to check. |
| `maximumAllowed` | The limit. The value must be less than this. |
| `valueName` | Filled in automatically by the compiler with the source text of `value`. Leave it empty. |
| `maximumAllowedName` | Filled in automatically by the compiler with the source text of `maximumAllowed`. Leave it empty. |

**Exceptions**

- `ArgumentOutOfRangeException`: Thrown when the value is greater than or equal to `maximumAllowed`.

**Example**

```csharp
age.ThrowIfGreatorThanEqualTo(100);
```

