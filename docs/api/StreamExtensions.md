---
title: StreamExtensions
layout: default
parent: API Reference
nav_order: 17
---

# StreamExtensions

Helpers for reading a whole `Stream` into memory or copying it.

## Methods

| Method | What it does |
|---|---|
| [`GetAllBytes`](#getallbytes) | Reads the whole stream from the beginning and returns all bytes. The stream position is reset to the start before reading, so the stream must support seeking. |
| [`GetAllBytesAsync`](#getallbytesasync) | Reads the whole stream from the beginning and returns all bytes, without blocking the calling thread. The stream position is reset to the start before reading, so the stream must support seeking. |
| [`CopyToAsync`](#copytoasync) | Copies the whole stream, from the beginning, to another stream. The source position is reset to the start before copying. Uses the default 81920 byte buffer. |

## `GetAllBytes`

```csharp
GetAllBytes(Stream stream)
```

Reads the whole stream from the beginning and returns all bytes. The stream position is reset to the start before reading, so the stream must support seeking.

**Parameters**

| Name | Description |
|---|---|
| `stream` | A seekable, readable stream. |

**Returns**

All bytes in the stream. Empty array for an empty stream.

**Exceptions**

- `NotSupportedException`: The stream does not support seeking.

**Example**

```csharp
using var stream = File.OpenRead("data.bin");
byte[] bytes = stream.GetAllBytes();
```

## `GetAllBytesAsync`

```csharp
GetAllBytesAsync(Stream stream, CancellationToken cancellationToken)
```

Reads the whole stream from the beginning and returns all bytes, without blocking the calling thread. The stream position is reset to the start before reading, so the stream must support seeking.

**Parameters**

| Name | Description |
|---|---|
| `stream` | A seekable, readable stream. |
| `cancellationToken` | Token to cancel the copy. |

**Returns**

A task that returns all bytes in the stream.

**Exceptions**

- `NotSupportedException`: The stream does not support seeking.
- `OperationCanceledException`: The token was cancelled.

**Example**

```csharp
byte[] bytes = await stream.GetAllBytesAsync();
```

## `CopyToAsync`

```csharp
CopyToAsync(Stream stream, Stream destination, CancellationToken cancellationToken)
```

Copies the whole stream, from the beginning, to another stream. The source position is reset to the start before copying. Uses the default 81920 byte buffer.

**Parameters**

| Name | Description |
|---|---|
| `stream` | The source stream. Must support seeking. |
| `destination` | The stream to write to. |
| `cancellationToken` | Token to cancel the copy. |

**Returns**

A task that completes when the copy is done.

**Exceptions**

- `NotSupportedException`: The source stream does not support seeking.

**Remarks**

The framework already has `Stream.CopyToAsync(Stream, CancellationToken)`, and the compiler picks it over this extension when you write `source.CopyToAsync(destination, token)`. To use this version (which rewinds the source first), call it as `StreamExtensions.CopyToAsync(source, destination, token)`.

**Example**

```csharp
await StreamExtensions.CopyToAsync(source, destination, CancellationToken.None);
```

