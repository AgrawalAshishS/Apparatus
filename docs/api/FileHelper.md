---
title: FileHelper
layout: default
parent: API Reference
nav_order: 9
---

# FileHelper

Helpers for common file tasks: safe delete, file extension and async reading.

## Methods

| Method | What it does |
|---|---|
| [`DeleteIfExists`](#deleteifexists) | Deletes a file if it exists. |
| [`GetExtension`](#getextension) | Gets the extension of a file name, without the dot. |
| [`ReadAllTextAsync`](#readalltextasync) | Reads a whole text file into one string, without blocking the calling thread. |
| [`ReadAllBytesAsync`](#readallbytesasync) | Reads a whole file into a byte array, without blocking the calling thread. |
| [`ReadAllLinesAsync`](#readalllinesasync) | Reads a text file line by line, without blocking the calling thread, and returns the lines as an array. |

## `DeleteIfExists`

```csharp
DeleteIfExists(string filePath)
```

Deletes a file if it exists.

**Parameters**

| Name | Description |
|---|---|
| `filePath` | Path of the file. |

**Returns**

`true` if the file existed and was deleted; `false` if there was nothing to delete.

**Example**

```csharp
bool deleted = FileHelper.DeleteIfExists(@"C:\temp\old.log");
```

## `GetExtension`

```csharp
GetExtension(string fileNameWithExtension)
```

Gets the extension of a file name, without the dot.

**Parameters**

| Name | Description |
|---|---|
| `fileNameWithExtension` | File name or path, for example `"report.pdf"`. Must not be `null` or empty. |

**Returns**

The text after the last dot, for example `"pdf"`. Returns an empty string if the name ends with a dot. Returns `null` if the name has no dot.

**Exceptions**

- `ArgumentNullException`: `fileNameWithExtension` is `null` or empty.

**Example**

```csharp
FileHelper.GetExtension("archive.tar.gz"); // "gz"
FileHelper.GetExtension("README");         // null
```

## `ReadAllTextAsync`

```csharp
ReadAllTextAsync(string path)
```

Reads a whole text file into one string, without blocking the calling thread.

**Parameters**

| Name | Description |
|---|---|
| `path` | Path of the file to read. |

**Returns**

The full text of the file.

**Exceptions**

- `FileNotFoundException`: The file does not exist.

**Example**

```csharp
string text = await FileHelper.ReadAllTextAsync("notes.txt");
```

## `ReadAllBytesAsync`

```csharp
ReadAllBytesAsync(string path)
```

Reads a whole file into a byte array, without blocking the calling thread.

**Parameters**

| Name | Description |
|---|---|
| `path` | Path of the file to read. |

**Returns**

All bytes of the file.

**Exceptions**

- `FileNotFoundException`: The file does not exist.

**Example**

```csharp
byte[] data = await FileHelper.ReadAllBytesAsync("image.png");
```

## `ReadAllLinesAsync`

```csharp
ReadAllLinesAsync(string path, Encoding encoding, FileMode fileMode, FileAccess fileAccess, FileShare fileShare, int bufferSize, FileOptions fileOptions)
```

Reads a text file line by line, without blocking the calling thread, and returns the lines as an array.

**Parameters**

| Name | Description |
|---|---|
| `path` | Path of the file to read. |
| `encoding` | Text encoding of the file. Default is UTF-8. |
| `fileMode` | How the operating system should open the file. Default is `Open`. |
| `fileAccess` | Read or write access. Default is `Read`. |
| `fileShare` | What other readers or writers may do with the file while it is open. Default is `Read`. |
| `bufferSize` | Size of the read buffer in bytes. Default is 4096. |
| `fileOptions` | Extra file options. Default is `Asynchronous` and `SequentialScan`. |

**Returns**

One array item per line. Line break characters are not included.

**Exceptions**

- `FileNotFoundException`: The file does not exist.

**Example**

```csharp
string[] lines = await FileHelper.ReadAllLinesAsync("data.csv");
```

