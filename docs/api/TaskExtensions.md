---
title: TaskExtensions
layout: default
parent: API Reference
nav_order: 19
---

# TaskExtensions

Helpers to run async code from normal (blocking) code.

Prefer `await` whenever you can. Use these only in places that cannot be async, such as a constructor or a `Main` method written without async.

## Methods

| Method | What it does |
|---|---|
| [`Await<T>`](#await-t) | Waits for a task to finish and returns its result. Use it to call async code from non-async code. The task runs on the thread pool, so it does not deadlock on UI or ASP.NET classic synchronization contexts. If the task fails, the real exception is thrown, not an `AggregateException`. |
| [`Await`](#await) | Waits for a task to finish. Use it to call async code from non-async code. The task runs on the thread pool, so it does not deadlock on UI or ASP.NET classic synchronization contexts. If the task fails, the real exception is thrown, not an `AggregateException`. |
| [`RunSync<TResult>`](#runsync-tresult) | Runs an async function on the thread pool, waits for it, and returns its result. |
| [`RunSync`](#runsync) | Runs an async function on the thread pool and waits for it to finish. |

## `Await<T>`

```csharp
Await<T>(Task<T> task)
```

Waits for a task to finish and returns its result. Use it to call async code from non-async code. The task runs on the thread pool, so it does not deadlock on UI or ASP.NET classic synchronization contexts. If the task fails, the real exception is thrown, not an `AggregateException`.

**Type parameters**

| Name | Description |
|---|---|
| `T` | Type of the task result. |

**Parameters**

| Name | Description |
|---|---|
| `task` | The task to wait for. |

**Returns**

The result of the task.

**Exceptions**

- `OperationCanceledException`: The task was cancelled.

**Example**

```csharp
string text = File.ReadAllTextAsync("a.txt").Await();
```

## `Await`

```csharp
Await(Task task)
```

Waits for a task to finish. Use it to call async code from non-async code. The task runs on the thread pool, so it does not deadlock on UI or ASP.NET classic synchronization contexts. If the task fails, the real exception is thrown, not an `AggregateException`.

**Parameters**

| Name | Description |
|---|---|
| `task` | The task to wait for. |

**Exceptions**

- `OperationCanceledException`: The task was cancelled.

**Example**

```csharp
Task.Delay(100).Await();
```

## `RunSync<TResult>`

```csharp
RunSync<TResult>(Func<Task<TResult>> func)
```

Runs an async function on the thread pool, waits for it, and returns its result.

**Type parameters**

| Name | Description |
|---|---|
| `TResult` | Type of the result. |

**Parameters**

| Name | Description |
|---|---|
| `func` | The async function to run. |

**Returns**

The result of the function.

**Example**

```csharp
int value = TaskExtensions.RunSync(async () => { await Task.Delay(10); return 42; });
```

## `RunSync`

```csharp
RunSync(Func<Task> func)
```

Runs an async function on the thread pool and waits for it to finish.

**Parameters**

| Name | Description |
|---|---|
| `func` | The async function to run. |

**Example**

```csharp
TaskExtensions.RunSync(async () => await Task.Delay(10));
```

