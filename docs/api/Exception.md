---
title: Exception
layout: default
parent: API Reference
nav_order: 8
---

# Exception

Provides common exception that has Error code facility required for batter UI handling and processing.

## Methods

| Method | What it does |
|---|---|
| [`Exception`](#exception) | Creates an exception with no message and an empty `ErrorCode`. |
| [`Exception`](#exception-1) | Creates an exception with a message. |
| [`Exception`](#exception-2) | Creates an exception with a message and the exception that caused it. |
| [`Exception`](#exception-3) | Creates an exception with an error code and a message. |
| [`Exception`](#exception-4) | Creates an exception with an error code, a message and the exception that caused it. |

## `Exception`

```csharp
new Exception()
```

Creates an exception with no message and an empty `ErrorCode`.

## `Exception`

```csharp
new Exception(string message)
```

Creates an exception with a message.

**Parameters**

| Name | Description |
|---|---|
| `message` | The error message. |

## `Exception`

```csharp
new Exception(string message, Exception inner)
```

Creates an exception with a message and the exception that caused it.

**Parameters**

| Name | Description |
|---|---|
| `message` | The error message. |
| `inner` | The exception that caused this one. |

## `Exception`

```csharp
new Exception(string errorCode, string message)
```

Creates an exception with an error code and a message.

**Parameters**

| Name | Description |
|---|---|
| `errorCode` | A short code the UI or caller can use to decide what to show, for example `"USER_NOT_FOUND"`. |
| `message` | The error message. |

## `Exception`

```csharp
new Exception(string errorCode, string message, Exception inner)
```

Creates an exception with an error code, a message and the exception that caused it.

**Parameters**

| Name | Description |
|---|---|
| `errorCode` | A short code the UI or caller can use to decide what to show. |
| `message` | The error message. |
| `inner` | The exception that caused this one. |

## Properties and fields

| Name | What it does |
|---|---|
| `ErrorCode` | Gets or sets a short machine readable code for this error. Empty when no code was given. |

