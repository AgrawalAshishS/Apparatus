---
title: Known issues
layout: default
nav_order: 5
---

# Known issues

These problems were found while writing tests and documentation. Nothing here has been changed in the
library code yet, because changing what a method does needs the owner's approval. Tests that show the wanted
behavior are already in the test project, marked as skipped, so they can be switched on when the fix is made.

## Methods that do not work as their name says

| Method | What happens | Workaround |
|---|---|---|
| `StringExtensions.Left(len)` | The length check is the wrong way round. It only works when `len` equals the string length; every other value throws `ArgumentOutOfRangeException`. | `text.Substring(0, len)` |
| `StringExtensions.Right(len)` | Same problem as `Left`. | `text.Substring(text.Length - len)` |
| `StringExtensions.NotIn(IEnumerable<string>)` | Returns `true` as soon as one item differs. `"red".NotIn(new[] { "red", "green" })` gives `true`. The `params string[]` version is correct. | `!text.In(values)` |
| `StringExtensions.ToPascalCase(handleDigitStart: true)` | When the result starts with a digit, the underscore is added but the last character is cut off. `"1st place"` gives `"_1stPlac"`. | Leave the option off. |
| `DataReaderExtension.ForEachResult()` with no actions | Throws `IndexOutOfRangeException` instead of just closing the reader. | Always pass at least one action. |
| `DataReaderExtension.FillObject<T>(dr, T src)` with `src` = `null` | A new object is created inside the method, but the caller never receives it. | Pass an existing object. |
| `EnumExtensions.EnumToDictionary` | Throws `InvalidCastException` for enums whose base type is not `int` (for example `enum X : byte`). | Use enums with the default `int` base type. |

## ThrowIf messages

| Method | What happens |
|---|---|
| `ThrowIf.NullOrEmpty` / `ThrowIfNullOrEmpty` | The exception is built with the value and the argument name swapped, so the message is the empty value and `ParamName` is not the argument name. |
| `ThrowIf.NotEqual` / `ThrowIfNotEqual` | The whole error text is passed as the parameter name, so `ParamName` is a long sentence. |
| Names and messages | `GreatorThan`, `GreatorThanEqualTo` and the words "greator" and "is should be" in messages are spelling mistakes. Renaming public methods would break existing callers, so this needs a decision. |

## Names that clash with .NET

| What | What happens | What to do |
|---|---|---|
| `Apparatus.TaskExtensions` | Same name as `System.Threading.Tasks.TaskExtensions`. With implicit usings, `TaskExtensions.RunSync(...)` is ambiguous. | Write `Apparatus.TaskExtensions.RunSync(...)`. |
| `TypeExtensions.IsAssignableTo(Type, Type)` | .NET has its own `Type.IsAssignableTo(Type)`, and the compiler picks it. The Apparatus version is never used with `type.IsAssignableTo(x)`. | `TypeExtensions.IsAssignableTo(type, x)`. |
| `StreamExtensions.CopyToAsync(Stream, CancellationToken)` | .NET has its own `Stream.CopyToAsync(Stream, CancellationToken)`, and the compiler picks it. It does not rewind the stream first. | `StreamExtensions.CopyToAsync(source, destination, token)`. |

## Security notes

- `EncryptionUtility` uses Triple DES in ECB mode with an MD5-based key. It only hides text lightly. Do not use it for passwords or personal data.
- `StringExtensions.ToHash` / `ToMd5` use MD5. Fine for cache keys, not for security.
- `InputFilters` uses simple text replacement. It does not replace HTML encoding or parameterized SQL.

## Tests that fail today

Two tests written earlier fail because the test and the code disagree. Someone has to decide which one is right.

| Test | Test expects | Code returns |
|---|---|---|
| `ToSnakeCase_NumbersAndSpecialCharacters` | `"123HelloWorld"` gives `123_hello_world`, `"HelloWorld123"` gives `hello_world_123` | `123hello_world` and `hello_world123` |
| `ToBoolean_InvalidInputs` | `"TrueFalse"` gives `false` | `true` (any text starting with T, Y is true; N, F is false) |
