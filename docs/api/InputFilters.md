---
title: InputFilters
layout: default
parent: API Reference
nav_order: 12
---

# InputFilters

Basic clean-up of text typed by users before it is shown or stored. Choose what to clean with `FilterFlag`, then call `InputFilter` or `ValidateInput`.

These filters are simple text replacements. They help, but they are **not** a full security solution. Always also use proper HTML encoding when writing user text into a page, and parameterized queries for SQL. The code is based on the input filters of the old DotNetNuke project.

## Methods

| Method | What it does |
|---|---|
| [`FilterStrings`](#filterstrings) | This function uses Regex search strings to remove HTML tags which are targeted in Cross-site scripting (XSS) attacks.  This function will evolve to provide more robust checking as additional holes are found. |
| [`FormatDisableScripting`](#formatdisablescripting) | This function uses Regex search strings to remove HTML tags which are targeted in Cross-site scripting (XSS) attacks.  This function will evolve to provide more robust checking as additional holes are found. |
| [`FormatAngleBrackets`](#formatanglebrackets) | This filter removes angle brackets i.e. |
| [`FormatMultiLine`](#formatmultiline) | This filter removes CrLf characters and inserts br |
| [`FormatRemoveSQL`](#formatremovesql) | This function verifies raw SQL statements to prevent SQL injection attacks and replaces a similar function (PreventSQLInjection) from the Common.Globals.vb module |
| [`IncludesMarkup`](#includesmarkup) | This function determines if the Input string contains any markup. |
| [`InputFilter`](#inputfilter) | Cleans user text by applying the filters you choose. |
| [`ValidateInput`](#validateinput) | Checks whether text is already clean, which means the chosen filters would not change it. |

## `FilterStrings`

```csharp
FilterStrings(string strInput)
```

This function uses Regex search strings to remove HTML tags which are targeted in Cross-site scripting (XSS) attacks.  This function will evolve to provide more robust checking as additional holes are found.

**Parameters**

| Name | Description |
|---|---|
| `strInput` | This is the string to be filtered |

**Returns**

Filtered UserInput

**Remarks**

This is a private function that is used internally by the FormatDisableScripting function

## `FormatDisableScripting`

```csharp
FormatDisableScripting(string strInput)
```

This function uses Regex search strings to remove HTML tags which are targeted in Cross-site scripting (XSS) attacks.  This function will evolve to provide more robust checking as additional holes are found.

**Parameters**

| Name | Description |
|---|---|
| `strInput` | This is the string to be filtered |

**Returns**

Filtered UserInput

**Remarks**

This is a private function that is used internally by the InputFilter function

## `FormatAngleBrackets`

```csharp
FormatAngleBrackets(string strInput)
```

This filter removes angle brackets i.e.

**Parameters**

| Name | Description |
|---|---|
| `strInput` | This is the string to be filtered |

**Returns**

Filtered UserInput

**Remarks**

This is a private function that is used internally by the InputFilter function

## `FormatMultiLine`

```csharp
FormatMultiLine(string strInput)
```

This filter removes CrLf characters and inserts br

**Parameters**

| Name | Description |
|---|---|
| `strInput` | This is the string to be filtered |

**Returns**

Filtered UserInput

**Remarks**

This is a private function that is used internally by the InputFilter function

## `FormatRemoveSQL`

```csharp
FormatRemoveSQL(string strSQL)
```

This function verifies raw SQL statements to prevent SQL injection attacks and replaces a similar function (PreventSQLInjection) from the Common.Globals.vb module

**Parameters**

| Name | Description |
|---|---|
| `strSQL` | This is the string to be filtered |

**Returns**

Filtered UserInput

**Remarks**

This is a private function that is used internally by the InputFilter function

## `IncludesMarkup`

```csharp
IncludesMarkup(string strInput)
```

This function determines if the Input string contains any markup.

**Parameters**

| Name | Description |
|---|---|
| `strInput` | This is the string to be checked |

**Returns**

True if string contains Markup tag(s)

**Remarks**

This is a private function that is used internally by the InputFilter function

## `InputFilter`

```csharp
InputFilter(string userInput, FilterFlag filterType)
```

Cleans user text by applying the filters you choose.

**Parameters**

| Name | Description |
|---|---|
| `userInput` | The text to clean. A `null` value gives an empty string. |
| `filterType` | The filters to apply. Combine several with `\|`. |

**Returns**

The cleaned text, or `Empty` when `userInput` is `null`.

**Example**

```csharp
string safe = InputFilters.InputFilter("<b>hi</b>", InputFilters.FilterFlag.NoMarkup);
// "&lt;b&gt;hi&lt;/b&gt;"
```

## `ValidateInput`

```csharp
ValidateInput(string userInput, FilterFlag filterType)
```

Checks whether text is already clean, which means the chosen filters would not change it.

**Parameters**

| Name | Description |
|---|---|
| `userInput` | The text to check. |
| `filterType` | The filters to apply. Combine several with `\|`. |

**Returns**

`true` if the filtered text is the same as the input; otherwise `false`. A `null` input returns `false` because it is filtered to an empty string.

**Example**

```csharp
InputFilters.ValidateInput("hello", InputFilters.FilterFlag.NoMarkup);     // true
InputFilters.ValidateInput("<b>hi</b>", InputFilters.FilterFlag.NoMarkup); // false
```

