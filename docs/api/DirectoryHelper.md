---
title: DirectoryHelper
layout: default
parent: API Reference
nav_order: 5
---

# DirectoryHelper

Safe helpers for creating and deleting folders. They check first, so they never fail just because the folder is (or is not) already there.

## Methods

| Method | What it does |
|---|---|
| [`CreateIfNotExists`](#createifnotexists) | Creates a folder (and any missing parent folders) only if it does not exist yet. |
| [`DeleteIfExists`](#deleteifexists) | Deletes a folder if it exists. The folder must be empty; use the overload with `recursive` to delete its content too. |
| [`DeleteIfExists`](#deleteifexists-1) | Deletes a folder if it exists, and optionally everything inside it. |
| [`CreateIfNotExists`](#createifnotexists-1) | Creates the folder described by a `DirectoryInfo` only if it does not exist yet. |

## `CreateIfNotExists`

```csharp
CreateIfNotExists(string directory)
```

Creates a folder (and any missing parent folders) only if it does not exist yet.

**Parameters**

| Name | Description |
|---|---|
| `directory` | Full or relative path of the folder. |

**Example**

```csharp
DirectoryHelper.CreateIfNotExists(@"C:\temp\reports");
```

## `DeleteIfExists`

```csharp
DeleteIfExists(string directory)
```

Deletes a folder if it exists. The folder must be empty; use the overload with `recursive` to delete its content too.

**Parameters**

| Name | Description |
|---|---|
| `directory` | Full or relative path of the folder. |

**Exceptions**

- `IOException`: The folder exists but is not empty.

**Example**

```csharp
DirectoryHelper.DeleteIfExists(@"C:\temp\empty-folder");
```

## `DeleteIfExists`

```csharp
DeleteIfExists(string directory, bool recursive)
```

Deletes a folder if it exists, and optionally everything inside it.

**Parameters**

| Name | Description |
|---|---|
| `directory` | Full or relative path of the folder. |
| `recursive` | `true` to also delete all files and sub-folders; `false` to delete only an empty folder. |

**Exceptions**

- `IOException`: `recursive` is `false` and the folder is not empty.

**Example**

```csharp
DirectoryHelper.DeleteIfExists(@"C:\temp\reports", true);
```

## `CreateIfNotExists`

```csharp
CreateIfNotExists(DirectoryInfo directory)
```

Creates the folder described by a `DirectoryInfo` only if it does not exist yet.

**Parameters**

| Name | Description |
|---|---|
| `directory` | The folder to create. |

**Example**

```csharp
DirectoryHelper.CreateIfNotExists(new DirectoryInfo(@"C:\temp\reports"));
```

