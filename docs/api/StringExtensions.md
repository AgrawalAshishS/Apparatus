---
title: StringExtensions
layout: default
parent: API Reference
nav_order: 18
---

# StringExtensions

Everyday helpers for `String`: checking, comparing, changing case style (Pascal, camel, snake, kebab), splitting, compressing and converting.

## Methods

| Method | What it does |
|---|---|
| [`IndexOfAny`](#indexofany) | Finds the position of the first match of any of the given values in this string. |
| [`FindFirstIndexOfAny`](#findfirstindexofany) | Finds the first match of any of the given values and tells you both where it is and which value matched. |
| [`In`](#in) | Checks whether this string is equal to any of the given values. The comparison is case sensitive. |
| [`In`](#in-1) | Checks whether this string is equal to any item in a collection. The comparison is case sensitive. |
| [`In`](#in-2) | Checks whether this string is equal to any of the given values, using the comparison rule you choose. |
| [`In`](#in-3) | Checks whether this string is equal to any item in a collection, using the comparison rule you choose. |
| [`IsNullOrEmpty`](#isnullorempty) | Checks whether the string is `null` or has no characters. Same as `IsNullOrEmpty`, written as a method on the string. |
| [`IsNullOrWhiteSpace`](#isnullorwhitespace) | Checks whether the string is `null`, empty, or has only white space. Same as `IsNullOrWhiteSpace`, written as a method on the string. |
| [`IsEmpty`](#isempty) | Checks whether the string is `null`, empty, or has only white space. |
| [`IsNotEmpty`](#isnotempty) | Checks whether the string has visible text. This is the opposite of `IsEmpty`. |
| [`NotIn`](#notin) | Checks that this string is different from every one of the given values. The comparison is case sensitive. |
| [`NotIn`](#notin-1) | Checks a string against a collection of values that it should not match. |
| [`EqualsIgnoreCase`](#equalsignorecase) | Compares two strings and ignores upper and lower case, using the current culture. |
| [`StartsWithIgnoreCase`](#startswithignorecase) | Checks whether the string starts with some text, ignoring upper and lower case (current culture). |
| [`EndsWithIgnoreCase`](#endswithignorecase) | Checks whether the string ends with some text, ignoring upper and lower case (current culture). |
| [`ToFormat`](#toformat) | Shorthand for `Format`, written as a method on the format text. |
| [`Capitalize`](#capitalize) | Makes the first letter of every word upper case and the rest lower case, using the current culture. Words that are fully in upper case (like `"USA"`) are left as they are. Same as `ToTitleCase`. |
| [`ToTitleCase`](#totitlecase) | Makes the first letter of every word upper case and the rest lower case, using the current culture. Words that are fully in upper case (like `"USA"`) are left as they are. Same as `Capitalize`. |
| [`ToPascalCase`](#topascalcase) | Converts text to PascalCase: no spaces or separators, and each word starts with a capital letter. Anything that is not a letter or digit (space, dash, dot, @ and so on) starts a new word and is removed. A text written fully in capitals is first changed to lower case, so `"HTTP_CODE"` becomes `"HttpCode"`. |
| [`ToCamelCase`](#tocamelcase) | Converts text to camelCase: like PascalCase, but the first letter is lower case. |
| [`ToKebabCase`](#tokebabcase) | Converts text to kebab-case: lower case words joined by single dashes. It understands PascalCase, camelCase, spaces, underscores, existing dashes, acronyms (`"HTTPRequest"` becomes `"http-request"`) and numbers. Other special characters are treated as word separators and removed. |
| [`ToSnakeCase`](#tosnakecase) | Converts text to snake_case: lower case words joined by underscores. |
| [`ToHash`](#tohash) | Gets the MD5 hash of the text (UTF-8 bytes) as 32 lower case hex characters. |
| [`ToMd5`](#tomd5) | Gets the MD5 hash of the text. Same as `ToHash`. |
| [`ToBoolean`](#toboolean) | Reads a yes/no style text as a `Boolean`. Understands `True`, `False`, `Y`, `N`, `Yes`, `No`, `T`, `F`, `1` and `0`, in any case. |
| [`Left`](#left) | Gets the first characters of the string. |
| [`Right`](#right) | Gets the last characters of the string. |
| [`Split`](#split) | Splits the string by a text separator (not just a single character). |
| [`Split`](#split-1) | Splits the string by a text separator (not just a single character), with options. |
| [`SplitToLines`](#splittolines) | Splits the string into lines, using `NewLine` as the separator. On Windows that is a CR+LF pair, so text with only LF line breaks is not split there. |
| [`SplitToLines`](#splittolines-1) | Splits the string into lines, using `NewLine` as the separator, with options. |
| [`SplitCamelCase`](#splitcamelcase) | Puts spaces between the words of a camelCase or PascalCase text. |
| [`SplitPascalCase`](#splitpascalcase) | Splits a PascalCase text into separate words. Groups of capitals (like `"XML"`) stay together. |
| [`EnsureEndsWith`](#ensureendswith) | Makes sure the string ends with some text. If it does not, the text is added at the end. |
| [`EnsureStartsWith`](#ensurestartswith) | Makes sure the string starts with some text. If it does not, the text is added at the start. |
| [`Reverse`](#reverse) | Returns the characters of the string in reverse order. |
| [`FindFirst`](#findfirst) | Finds which of the given words appears in the string. Upper and lower case are ignored. |
| [`FindFirst`](#findfirst-1) | Finds which of the given words appears in the string, using the comparison rule you choose. |
| [`Compress`](#compress) | Compresses text with GZip and returns it as a Base64 string. Useful for storing or sending long text in less space. |
| [`Decompress`](#decompress) | Turns text made by `Compress` back into the original text. |
| [`ToEnum<T>`](#toenum-t) | Reads text as an enum value, ignoring upper and lower case. Gives a default value when the text is empty or not a valid name. |
| [`Join`](#join) | Joins a list of strings into one string with a separator between them. Same as `Join`, written as a method on the list. |

## `IndexOfAny`

```csharp
IndexOfAny(string src, string[] values)
```

Finds the position of the first match of any of the given values in this string.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to search. |
| `values` | The words to look for. Each value is used as a regular expression, so characters such as `.`, `(` or `+` have a special meaning. The search is case sensitive. |

**Returns**

The zero-based index of the earliest match, or `-1` if nothing matches.

**Example**

```csharp
int index = "this is fine".IndexOfAny("is", "are", "f"); // 2 (the "is" inside "this")
```

## `FindFirstIndexOfAny`

```csharp
FindFirstIndexOfAny(string src, string[] values)
```

Finds the first match of any of the given values and tells you both where it is and which value matched.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to search. Must not be `null` or empty. |
| `values` | The words to look for. Each value is used as a regular expression, so characters such as `.` or `(` have a special meaning. The search is case sensitive. |

**Returns**

A tuple of (index, matched text). When nothing matches, it is `(-1, "")`.

**Exceptions**

- `ArgumentNullException`: `values` or `src` is `null`.
- `ArgumentException`: `values` is empty, or `src` is an empty string.

**Example**

```csharp
var found = "This is subject".FindFirstIndexOfAny("is", "subject");
Console.WriteLine(found.Item1); // 2 (the "is" inside "This")
Console.WriteLine(found.Item2); // "is"
```

## `In`

```csharp
In(string src, string[] values)
```

Checks whether this string is equal to any of the given values. The comparison is case sensitive.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to look for. |
| `values` | The allowed values. |

**Returns**

`true` if `src` equals at least one value; otherwise `false`.

**Example**

```csharp
if ("to".In("abc", "pqr", "to")) { /* runs */ }
```

## `In`

```csharp
In(string src, IEnumerable<string> values)
```

Checks whether this string is equal to any item in a collection. The comparison is case sensitive.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to look for. |
| `values` | The allowed values. |

**Returns**

`true` if `src` equals at least one item; otherwise `false`.

**Example**

```csharp
var allowed = new List<string> { "red", "green" };
"red".In(allowed); // true
```

## `In`

```csharp
In(string src, StringComparison comparison, string[] values)
```

Checks whether this string is equal to any of the given values, using the comparison rule you choose.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to look for. |
| `comparison` | How to compare, for example `OrdinalIgnoreCase` to ignore upper and lower case. |
| `values` | The allowed values. |

**Returns**

`true` if `src` equals at least one value; otherwise `false`.

**Example**

```csharp
"RED".In(StringComparison.OrdinalIgnoreCase, "red", "green"); // true
```

## `In`

```csharp
In(string src, StringComparison comparison, IEnumerable<string> values)
```

Checks whether this string is equal to any item in a collection, using the comparison rule you choose.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to look for. |
| `comparison` | How to compare, for example `OrdinalIgnoreCase`. |
| `values` | The allowed values. |

**Returns**

`true` if `src` equals at least one item; otherwise `false`.

**Example**

```csharp
var allowed = new[] { "red", "green" };
"GREEN".In(StringComparison.OrdinalIgnoreCase, allowed); // true
```

## `IsNullOrEmpty`

```csharp
IsNullOrEmpty(string str)
```

Checks whether the string is `null` or has no characters. Same as `IsNullOrEmpty`, written as a method on the string.

**Parameters**

| Name | Description |
|---|---|
| `str` | The string to check. May be `null`. |

**Returns**

`true` if `str` is `null` or `""`; otherwise `false`.

**Example**

```csharp
"".IsNullOrEmpty();  // true
" ".IsNullOrEmpty(); // false
```

## `IsNullOrWhiteSpace`

```csharp
IsNullOrWhiteSpace(string str)
```

Checks whether the string is `null`, empty, or has only white space. Same as `IsNullOrWhiteSpace`, written as a method on the string.

**Parameters**

| Name | Description |
|---|---|
| `str` | The string to check. May be `null`. |

**Returns**

`true` if `str` is `null`, empty or only white space; otherwise `false`.

**Example**

```csharp
"  ".IsNullOrWhiteSpace(); // true
```

## `IsEmpty`

```csharp
IsEmpty(string src)
```

Checks whether the string is `null`, empty, or has only white space.

**Parameters**

| Name | Description |
|---|---|
| `src` | The string to check. May be `null`. |

**Returns**

`true` if there is no visible text; otherwise `false`.

**Example**

```csharp
"  ".IsEmpty();   // true
"abc".IsEmpty();  // false
```

## `IsNotEmpty`

```csharp
IsNotEmpty(string src)
```

Checks whether the string has visible text. This is the opposite of `IsEmpty`.

**Parameters**

| Name | Description |
|---|---|
| `src` | The string to check. May be `null`. |

**Returns**

`true` if the string has at least one character that is not white space; otherwise `false`.

**Example**

```csharp
"abc".IsNotEmpty(); // true
```

## `NotIn`

```csharp
NotIn(string src, string[] values)
```

Checks that this string is different from every one of the given values. The comparison is case sensitive.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to check. |
| `values` | The values it must not match. |

**Returns**

`true` if `src` matches none of the values; otherwise `false`.

**Example**

```csharp
"blue".NotIn("red", "green"); // true
```

## `NotIn`

```csharp
NotIn(string src, IEnumerable<string> values)
```

Checks a string against a collection of values that it should not match.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to check. |
| `values` | The values it should not match. |

**Returns**

`true` if at least one item in `values` is different from `src`; otherwise `false`.

**Remarks**

Known issue: unlike `NotIn`, this overload returns `true` as soon as *one* item differs, so `"red".NotIn(new[] { "red", "green" })` returns `true`. Use `!src.In(values)` until this is fixed.

## `EqualsIgnoreCase`

```csharp
EqualsIgnoreCase(string src, string otherString)
```

Compares two strings and ignores upper and lower case, using the current culture.

**Parameters**

| Name | Description |
|---|---|
| `src` | The first string. |
| `otherString` | The string to compare with. |

**Returns**

`true` if both are equal apart from case; otherwise `false`.

**Exceptions**

- `NullReferenceException`: `src` is `null`.

**Example**

```csharp
"abc".EqualsIgnoreCase("ABC"); // true
```

## `StartsWithIgnoreCase`

```csharp
StartsWithIgnoreCase(string src, string find)
```

Checks whether the string starts with some text, ignoring upper and lower case (current culture).

**Parameters**

| Name | Description |
|---|---|
| `src` | The string to check. |
| `find` | The text it should start with. |

**Returns**

`true` if `src` starts with `find`; otherwise `false`.

**Exceptions**

- `NullReferenceException`: `src` is `null`.

**Example**

```csharp
"Hello World".StartsWithIgnoreCase("hello"); // true
```

## `EndsWithIgnoreCase`

```csharp
EndsWithIgnoreCase(string src, string find)
```

Checks whether the string ends with some text, ignoring upper and lower case (current culture).

**Parameters**

| Name | Description |
|---|---|
| `src` | The string to check. |
| `find` | The text it should end with. |

**Returns**

`true` if `src` ends with `find`; otherwise `false`.

**Exceptions**

- `NullReferenceException`: `src` is `null`.

**Example**

```csharp
"Hello World".EndsWithIgnoreCase("WORLD"); // true
```

## `ToFormat`

```csharp
ToFormat(string src, object[] args)
```

Shorthand for `Format`, written as a method on the format text.

**Parameters**

| Name | Description |
|---|---|
| `src` | The format text, for example `"Hi {0}"`. |
| `args` | The values to put in place of `{0}`, `{1}` and so on. |

**Returns**

The formatted text.

**Exceptions**

- `FormatException`: The format text is not valid or needs more arguments than given.

**Example**

```csharp
string message = "Hi {0}, you have {1} messages".ToFormat("Sam", 3); // "Hi Sam, you have 3 messages"
```

## `Capitalize`

```csharp
Capitalize(string src)
```

Makes the first letter of every word upper case and the rest lower case, using the current culture. Words that are fully in upper case (like `"USA"`) are left as they are. Same as `ToTitleCase`.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to change. The original string is not changed. |

**Returns**

The text in title case.

**Example**

```csharp
"this is final".Capitalize(); // "This Is Final"
```

## `ToTitleCase`

```csharp
ToTitleCase(string src)
```

Makes the first letter of every word upper case and the rest lower case, using the current culture. Words that are fully in upper case (like `"USA"`) are left as they are. Same as `Capitalize`.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to change. The original string is not changed. |

**Returns**

The text in title case.

**Example**

```csharp
"this is final".ToTitleCase(); // "This Is Final"
```

## `ToPascalCase`

```csharp
ToPascalCase(string src, bool preserveUnderscores, bool useRawNames, bool handleDigitStart)
```

Converts text to PascalCase: no spaces or separators, and each word starts with a capital letter. Anything that is not a letter or digit (space, dash, dot, @ and so on) starts a new word and is removed. A text written fully in capitals is first changed to lower case, so `"HTTP_CODE"` becomes `"HttpCode"`.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to change. The original string is not changed. `null`, empty or white space is returned as it is. |
| `preserveUnderscores` | `true` to keep underscores in the result (the next letter is still capitalized). Default is `false`. |
| `useRawNames` | `true` to keep the letters exactly as they are (no capitals are added), while still removing separators. Underscores are kept. Default is `false`. |
| `handleDigitStart` | `true` to put an underscore in front when the result starts with a digit, so it can be used as a C# name. Default is `false`. See the remarks: this option currently drops the last character. |

**Returns**

The PascalCase text.

**Remarks**

Known issue: with `handleDigitStart` set to `true` and a result that starts with a digit, the underscore is added but the last character of the result is cut off (`"1st place"` gives `"_1stPlac"`). Leave the option off until this is fixed.

**Example**

```csharp
"hello world".ToPascalCase();               // "HelloWorld"
"user_first-name".ToPascalCase();           // "UserFirstName"
"hello_world".ToPascalCase(true);           // "Hello_World"
```

## `ToCamelCase`

```csharp
ToCamelCase(string src, bool useCurrentCulture)
```

Converts text to camelCase: like PascalCase, but the first letter is lower case.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to change. The original string is not changed. `null`, empty or white space is returned as it is. |
| `useCurrentCulture` | `true` to lower-case the first letter with the current culture rules. Default is `false`, which uses the invariant culture. |

**Returns**

The camelCase text.

**Example**

```csharp
"ThisIs_it".ToCamelCase();      // "thisIsIt"
"hello world".ToCamelCase();    // "helloWorld"
```

## `ToKebabCase`

```csharp
ToKebabCase(string input)
```

Converts text to kebab-case: lower case words joined by single dashes. It understands PascalCase, camelCase, spaces, underscores, existing dashes, acronyms (`"HTTPRequest"` becomes `"http-request"`) and numbers. Other special characters are treated as word separators and removed.

**Parameters**

| Name | Description |
|---|---|
| `input` | The text to convert. |

**Returns**

The kebab-case text. An empty string is returned when the input is `null`, empty or only white space.

**Example**

```csharp
"HelloWorld".ToKebabCase();      // "hello-world"
"some_value here".ToKebabCase(); // "some-value-here"
"HTTPRequest".ToKebabCase();     // "http-request"
```

## `ToSnakeCase`

```csharp
ToSnakeCase(string src)
```

Converts text to snake_case: lower case words joined by underscores.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to convert. `null`, empty or white space is returned as it is. |

**Returns**

The snake_case text.

**Example**

```csharp
"VeryLongName".ToSnakeCase(); // "very_long_name"
```

## `ToHash`

```csharp
ToHash(string src)
```

Gets the MD5 hash of the text (UTF-8 bytes) as 32 lower case hex characters.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to hash. Must not be `null`. |

**Returns**

The hash, for example `"5d41402abc4b2a76b9719d911017c592"` for `"hello"`.

**Exceptions**

- `ArgumentNullException`: `src` is `null`.

**Remarks**

MD5 is fine for cache keys or quick checks, but it is **not** safe for passwords or security decisions.

**Example**

```csharp
string hash = "hello".ToHash(); // "5d41402abc4b2a76b9719d911017c592"
```

## `ToMd5`

```csharp
ToMd5(string src)
```

Gets the MD5 hash of the text. Same as `ToHash`.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to hash. Must not be `null`. |

**Returns**

The hash as 32 lower case hex characters.

**Exceptions**

- `ArgumentNullException`: `src` is `null`.

**Example**

```csharp
string hash = "hello".ToMd5(); // "5d41402abc4b2a76b9719d911017c592"
```

## `ToBoolean`

```csharp
ToBoolean(string src, bool defaultValue)
```

Reads a yes/no style text as a `Boolean`. Understands `True`, `False`, `Y`, `N`, `Yes`, `No`, `T`, `F`, `1` and `0`, in any case.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to read. May be `null`. |
| `defaultValue` | Returned when the text is `null`, empty, or not understood. |

**Returns**

The value that the text means. A whole number is `true` unless it is 0. Any other text is decided by its first letter: Y or T means `true`, N or F means `false`.

**Example**

```csharp
"yes".ToBoolean(false);     // true
"0".ToBoolean(true);        // false
"maybe".ToBoolean(true);    // true  (not understood, so the default is used)
```

## `Left`

```csharp
Left(string src, int len)
```

Gets the first characters of the string.

**Parameters**

| Name | Description |
|---|---|
| `src` | The string to cut. Must not be `null`. |
| `len` | The number of characters wanted. |

**Returns**

The first `len` characters.

**Exceptions**

- `ArgumentNullException`: `src` is `null`.
- `ArgumentOutOfRangeException`: See the remarks.

**Remarks**

Known issue: the length check is the wrong way round, so this method only works when `len` is exactly the length of the string. For any other length it throws `ArgumentOutOfRangeException`. Use `src.Substring(0, len)` until this is fixed.

## `Right`

```csharp
Right(string src, int len)
```

Gets the last characters of the string.

**Parameters**

| Name | Description |
|---|---|
| `src` | The string to cut. Must not be `null`. |
| `len` | The number of characters wanted. |

**Returns**

The last `len` characters.

**Exceptions**

- `ArgumentNullException`: `src` is `null`.
- `ArgumentOutOfRangeException`: See the remarks.

**Remarks**

Known issue: the length check is the wrong way round, so this method only works when `len` is exactly the length of the string. For any other length it throws `ArgumentOutOfRangeException`. Use `src.Substring(src.Length - len)` until this is fixed.

## `Split`

```csharp
Split(string src, string separator)
```

Splits the string by a text separator (not just a single character).

**Parameters**

| Name | Description |
|---|---|
| `src` | The string to split. |
| `separator` | The text that separates the parts, for example `", "`. |

**Returns**

The parts. Empty parts are kept.

**Example**

```csharp
string[] parts = "a, b, c".Split(", "); // { "a", "b", "c" }
```

## `Split`

```csharp
Split(string src, string separator, StringSplitOptions options)
```

Splits the string by a text separator (not just a single character), with options.

**Parameters**

| Name | Description |
|---|---|
| `src` | The string to split. |
| `separator` | The text that separates the parts. |
| `options` | For example `RemoveEmptyEntries` to drop empty parts. |

**Returns**

The parts.

**Example**

```csharp
"a,,b".Split(",", StringSplitOptions.RemoveEmptyEntries); // { "a", "b" }
```

## `SplitToLines`

```csharp
SplitToLines(string src)
```

Splits the string into lines, using `NewLine` as the separator. On Windows that is a CR+LF pair, so text with only LF line breaks is not split there.

**Parameters**

| Name | Description |
|---|---|
| `src` | The string to split. |

**Returns**

The lines. Empty lines are kept.

**Example**

```csharp
string[] lines = ("a" + Environment.NewLine + "b").SplitToLines(); // { "a", "b" }
```

## `SplitToLines`

```csharp
SplitToLines(string src, StringSplitOptions options)
```

Splits the string into lines, using `NewLine` as the separator, with options.

**Parameters**

| Name | Description |
|---|---|
| `src` | The string to split. |
| `options` | For example `RemoveEmptyEntries` to drop empty lines. |

**Returns**

The lines.

**Example**

```csharp
var text = "a" + Environment.NewLine + Environment.NewLine + "b";
text.SplitToLines(StringSplitOptions.RemoveEmptyEntries); // { "a", "b" }
```

## `SplitCamelCase`

```csharp
SplitCamelCase(string src)
```

Puts spaces between the words of a camelCase or PascalCase text.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to split. Must not be `null`. |

**Returns**

The text with spaces between words.

**Exceptions**

- `ArgumentNullException`: `src` is `null`.

**Example**

```csharp
"thisIsFinal".SplitCamelCase(); // "this Is Final"
```

## `SplitPascalCase`

```csharp
SplitPascalCase(string value)
```

Splits a PascalCase text into separate words. Groups of capitals (like `"XML"`) stay together.

**Parameters**

| Name | Description |
|---|---|
| `value` | The PascalCase text to split. Must not be `null`. |

**Returns**

The words separated by single spaces, with no space at the start or end.

**Exceptions**

- `ArgumentNullException`: `value` is `null`.

**Example**

```csharp
"ParseXMLFile".SplitPascalCase(); // "Parse XML File"
```

## `EnsureEndsWith`

```csharp
EnsureEndsWith(string src, string endsWith, StringComparison comparisonType)
```

Makes sure the string ends with some text. If it does not, the text is added at the end.

**Parameters**

| Name | Description |
|---|---|
| `src` | The string to check. A `null` value is returned as `null`. |
| `endsWith` | The ending that is required. |
| `comparisonType` | How to compare the ending. Default is `Ordinal` (case sensitive). |

**Returns**

The same string if it already ends with `endsWith`; otherwise the string with `endsWith` added.

**Example**

```csharp
"this is ".EnsureEndsWith("final");     // "this is final"
"this is final".EnsureEndsWith("final"); // "this is final"
"folder".EnsureEndsWith("/");            // "folder/"
```

## `EnsureStartsWith`

```csharp
EnsureStartsWith(string src, string starstWith, StringComparison comparisonType)
```

Makes sure the string starts with some text. If it does not, the text is added at the start.

**Parameters**

| Name | Description |
|---|---|
| `src` | The string to check. A `null` value is returned as `null`. |
| `starstWith` | The beginning that is required. |
| `comparisonType` | How to compare the beginning. Default is `Ordinal` (case sensitive). |

**Returns**

The same string if it already starts with `starstWith`; otherwise the string with `starstWith` added in front.

**Example**

```csharp
"path".EnsureStartsWith("/"); // "/path"
```

## `Reverse`

```csharp
Reverse(string src)
```

Returns the characters of the string in reverse order.

**Parameters**

| Name | Description |
|---|---|
| `src` | The string to reverse. Must not be `null`. |

**Returns**

The reversed string.

**Exceptions**

- `NullReferenceException`: `src` is `null`.

**Remarks**

The reversal works on single UTF-16 characters. Emoji and other characters made of two parts (surrogate pairs) or combining marks will not stay correct.

**Example**

```csharp
"abc".Reverse(); // "cba"
```

## `FindFirst`

```csharp
FindFirst(string src, string[] values)
```

Finds which of the given words appears in the string. Upper and lower case are ignored.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to search. |
| `values` | The words to look for, in order of priority. |

**Returns**

The first word from `values` (in the order you gave them) that is found in `src`, or `null` if none is found.

**Example**

```csharp
"Order shipped today".FindFirst("SHIPPED", "delivered"); // "SHIPPED"
```

## `FindFirst`

```csharp
FindFirst(string src, StringComparison comparer, string[] values)
```

Finds which of the given words appears in the string, using the comparison rule you choose.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to search. |
| `comparer` | How to compare, for example `Ordinal` for case sensitive. |
| `values` | The words to look for, in order of priority. |

**Returns**

The first word from `values` (in the order you gave them) that is found in `src`, or `null` if none is found.

**Example**

```csharp
"Order shipped".FindFirst(StringComparison.Ordinal, "SHIPPED", "shipped"); // "shipped"
```

## `Compress`

```csharp
Compress(string src)
```

Compresses text with GZip and returns it as a Base64 string. Useful for storing or sending long text in less space.

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to compress. `null` returns `null`. |

**Returns**

A Base64 string that `Decompress` can turn back into the original text.

**Example**

```csharp
string packed = longText.Compress();
string original = packed.Decompress();
```

## `Decompress`

```csharp
Decompress(string src)
```

Turns text made by `Compress` back into the original text.

**Parameters**

| Name | Description |
|---|---|
| `src` | The Base64 compressed text. `null` returns `null`. |

**Returns**

The original text.

**Exceptions**

- `FormatException`: `src` is not valid Base64.

**Example**

```csharp
string original = packed.Decompress();
```

## `ToEnum<T>`

```csharp
ToEnum<T>(string src, T defaultValue)
```

Reads text as an enum value, ignoring upper and lower case. Gives a default value when the text is empty or not a valid name.

**Type parameters**

| Name | Description |
|---|---|
| `T` | The enum type. |

**Parameters**

| Name | Description |
|---|---|
| `src` | The text to read, for example `"monday"`. Spaces around it are ignored. |
| `defaultValue` | Returned when `src` is `null`, empty, or cannot be read. |

**Returns**

The enum value, or `defaultValue`.

**Example**

```csharp
DayOfWeek day = "friday".ToEnum(DayOfWeek.Monday);  // DayOfWeek.Friday
DayOfWeek other = "nope".ToEnum(DayOfWeek.Monday);  // DayOfWeek.Monday
```

## `Join`

```csharp
Join(IEnumerable<string> source, string separator)
```

Joins a list of strings into one string with a separator between them. Same as `Join`, written as a method on the list.

**Parameters**

| Name | Description |
|---|---|
| `source` | The strings to join. |
| `separator` | The text to put between the items. |

**Returns**

The joined text. An empty list gives an empty string.

**Example**

```csharp
new[] { "a", "b", "c" }.Join(", "); // "a, b, c"
```

