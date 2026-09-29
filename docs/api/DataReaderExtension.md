---
title: DataReaderExtension
layout: default
parent: API Reference
nav_order: 3
---

# DataReaderExtension

Short helpers for `IDataReader`: loop over rows, move between result sets, close the reader without try/catch, and fill objects that implement `IHydrator`.

## Methods

| Method | What it does |
|---|---|
| [`FillCollection<T>`](#fillcollection-t) | Fills an existing list from the reader and returns the reader, so you can chain more calls (for example `NextResultSafely`) to read several result sets. The reader stays open. |
| [`FillCollection<T>`](#fillcollection-t-1) | Reads all rows of the reader into a new list of `T`, and asks the type to close the reader. |
| [`FillCollection<T>`](#fillcollection-t-2) | Reads all rows of the reader into a new list of `T`, and lets you choose whether the reader is closed. |
| [`FillCollection<T>`](#fillcollection-t-3) | Reads all rows of the reader and adds the new objects to a list you already have. |
| [`FillObject<T>`](#fillobject-t) | Moves the reader to the next row (calls `Read()`) and, if there is a row, fills `src` from it. Returns the reader so you can chain calls. The reader stays open. |
| [`FillObject<T>`](#fillobject-t-1) | Reads the next row (calls `Read()`) into a new object of type `T` and asks the type to close the reader. |
| [`FillObject<T>`](#fillobject-t-2) | Reads the next row (calls `Read()`) into a new object of type `T`, and lets you choose whether the reader is closed. |
| [`FillObject<T>`](#fillobject-t-3) | Fills a new object from the current row, and lets you decide whether `Read()` is called first. |
| [`ForEachRecord`](#foreachrecord) | Runs an action for every row of the reader, then closes the reader. Use it when the row type does not implement `IHydrator`. |
| [`ForEachRecord`](#foreachrecord-1) | Runs an action for every row of the reader, and lets you choose whether the reader is closed. Keep it open to read the next result set in the same chain. |
| [`CloseSafely`](#closesafely) | Closes the reader and ignores any error, so it is safe to call inside `finally`. |
| [`NextResultSafely`](#nextresultsafely) | Moves to the next result set of the reader and ignores any error, so you do not need a try/catch. |
| [`ForEachResult`](#foreachresult) | Reads several result sets. The first action runs for every row of the first result set, the second action for the second result set, and so on. The reader is disposed at the end. |
| [`NoRecord`](#norecord) | Runs an action when the reader has no columns, which is how an empty result is detected without calling `Read()` (so other reading code is not disturbed). |

## `FillCollection<T>`

```csharp
FillCollection<T>(IDataReader dr, IList listToFill)
```

Fills an existing list from the reader and returns the reader, so you can chain more calls (for example `NextResultSafely`) to read several result sets. The reader stays open.

**Type parameters**

| Name | Description |
|---|---|
| `T` | A type that implements `IHydrator` and has a public parameterless constructor. |

**Parameters**

| Name | Description |
|---|---|
| `dr` | An open data reader. |
| `listToFill` | The list that receives the new objects. |

**Returns**

The same `dr`, for chaining.

**Example**

```csharp
var tasks = new List<TaskItem>();
var comments = new List<Comment>();
cmd.ExecuteReader()
    .FillCollection<TaskItem>(tasks)
    .NextResultSafely()
    .FillCollection<Comment>(comments);
```

## `FillCollection<T>`

```csharp
FillCollection<T>(IDataReader dr)
```

Reads all rows of the reader into a new list of `T`, and asks the type to close the reader.

**Type parameters**

| Name | Description |
|---|---|
| `T` | A type that implements `IHydrator` and has a public parameterless constructor. |

**Parameters**

| Name | Description |
|---|---|
| `dr` | An open data reader. |

**Returns**

A new list with one object per row.

**Remarks**

The reader is closed afterwards (the `IHydrator` implementation must do it).

**Example**

```csharp
List<Customer> customers = cmd.ExecuteReader().FillCollection<Customer>();
```

## `FillCollection<T>`

```csharp
FillCollection<T>(IDataReader dr, bool closeConnection)
```

Reads all rows of the reader into a new list of `T`, and lets you choose whether the reader is closed.

**Type parameters**

| Name | Description |
|---|---|
| `T` | A type that implements `IHydrator` and has a public parameterless constructor. |

**Parameters**

| Name | Description |
|---|---|
| `dr` | An open data reader. |
| `closeConnection` | `true` to close the reader after filling; `false` to keep it open. |

**Returns**

A new list with one object per row.

**Example**

```csharp
List<Customer> customers = cmd.ExecuteReader().FillCollection<Customer>(false);
```

## `FillCollection<T>`

```csharp
FillCollection<T>(IDataReader dr, bool closeConnection, IList listToFill)
```

Reads all rows of the reader and adds the new objects to a list you already have.

**Type parameters**

| Name | Description |
|---|---|
| `T` | A type that implements `IHydrator` and has a public parameterless constructor. |

**Parameters**

| Name | Description |
|---|---|
| `dr` | An open data reader. |
| `closeConnection` | `true` to close the reader after filling; `false` to keep it open. |
| `listToFill` | The list that receives the new objects. |

**Example**

```csharp
var customers = new List<Customer>();
cmd.ExecuteReader().FillCollection<Customer>(true, customers);
```

## `FillObject<T>`

```csharp
FillObject<T>(IDataReader dr, T src)
```

Moves the reader to the next row (calls `Read()`) and, if there is a row, fills `src` from it. Returns the reader so you can chain calls. The reader stays open.

**Type parameters**

| Name | Description |
|---|---|
| `T` | A type that implements `IHydrator` and has a public parameterless constructor. |

**Parameters**

| Name | Description |
|---|---|
| `dr` | An open data reader. |
| `src` | The object to fill. Pass an existing object. |

**Returns**

The same `dr`, for chaining.

**Remarks**

Known issue: when `src` is `null`, a new object is created inside the method but the caller never receives it. Always pass an existing object.

**Example**

```csharp
var task = new TaskItem();
var comments = new List<Comment>();
cmd.ExecuteReader()
    .FillObject(task)
    .NextResultSafely()
    .FillCollection<Comment>(comments);
```

## `FillObject<T>`

```csharp
FillObject<T>(IDataReader dr)
```

Reads the next row (calls `Read()`) into a new object of type `T` and asks the type to close the reader.

**Type parameters**

| Name | Description |
|---|---|
| `T` | A type that implements `IHydrator` and has a public parameterless constructor. |

**Parameters**

| Name | Description |
|---|---|
| `dr` | An open data reader. |

**Returns**

A new object filled from the row, or `null` when there is no row.

**Example**

```csharp
Customer? customer = cmd.ExecuteReader().FillObject<Customer>();
```

## `FillObject<T>`

```csharp
FillObject<T>(IDataReader dr, bool closeConnection)
```

Reads the next row (calls `Read()`) into a new object of type `T`, and lets you choose whether the reader is closed.

**Type parameters**

| Name | Description |
|---|---|
| `T` | A type that implements `IHydrator` and has a public parameterless constructor. |

**Parameters**

| Name | Description |
|---|---|
| `dr` | An open data reader. |
| `closeConnection` | `true` to ask the type to close the reader; `false` to keep it open. |

**Returns**

A new object filled from the row, or `null` when there is no row.

**Example**

```csharp
Customer? customer = cmd.ExecuteReader().FillObject<Customer>(false);
```

## `FillObject<T>`

```csharp
FillObject<T>(IDataReader dr, bool closeConnection, bool doDrRead)
```

Fills a new object from the current row, and lets you decide whether `Read()` is called first.

**Type parameters**

| Name | Description |
|---|---|
| `T` | A type that implements `IHydrator` and has a public parameterless constructor. |

**Parameters**

| Name | Description |
|---|---|
| `dr` | An open data reader. |
| `closeConnection` | `true` to ask the type to close the reader; `false` to keep it open. |
| `doDrRead` | `true` to call `Read()` first. `false` to use the row the reader is already on, which is needed when several different types are built from the same row. |

**Returns**

A new object filled from the row, or `null` when `doDrRead` is `true` and there is no row.

**Example**

```csharp
while (dr.Read())
{
    var customer = dr.FillObject<Customer>(false, false);
    var address = dr.FillObject<Address>(false, false);
}
```

## `ForEachRecord`

```csharp
ForEachRecord(IDataReader dr, Action<IDataReader> action)
```

Runs an action for every row of the reader, then closes the reader. Use it when the row type does not implement `IHydrator`.

**Parameters**

| Name | Description |
|---|---|
| `dr` | An open data reader. |
| `action` | Code to run for each row. It receives the reader positioned on that row. |

**Exceptions**

- `Exception`: Any exception thrown by `action` is thrown again after the reader is closed.

**Remarks**

The reader is closed afterwards, even if the action throws.

**Example**

```csharp
var list = new List<MyType>();
cmd.ExecuteReader().ForEachRecord(dr =>
{
    list.Add(new MyType { Id = dr.GetInt32(0), Name = dr.GetString(1) });
});
```

## `ForEachRecord`

```csharp
ForEachRecord(IDataReader dr, bool closeConnection, Action<IDataReader> action)
```

Runs an action for every row of the reader, and lets you choose whether the reader is closed. Keep it open to read the next result set in the same chain.

**Parameters**

| Name | Description |
|---|---|
| `dr` | An open data reader. |
| `closeConnection` | `true` to close the reader after the last row; `false` to keep it open. |
| `action` | Code to run for each row. It receives the reader positioned on that row. |

**Returns**

The same `dr`, for chaining.

**Remarks**

If the action throws, the reader is closed and the exception is thrown again.

**Example**

```csharp
var tasks = new List<TaskItem>();
var comments = new List<Comment>();
cmd.ExecuteReader()
    .ForEachRecord(false, dr => tasks.Add(new TaskItem { Id = dr.GetInt32(0) }))
    .NextResultSafely()
    .ForEachRecord(true, dr => comments.Add(new Comment { Id = dr.GetInt32(0) }));
```

## `CloseSafely`

```csharp
CloseSafely(IDataReader dr)
```

Closes the reader and ignores any error, so it is safe to call inside `finally`.

**Parameters**

| Name | Description |
|---|---|
| `dr` | The reader to close. Already closed or disposed readers are fine. |

**Example**

```csharp
try
{
    while (dr.Read()) { /* ... */ }
}
finally
{
    dr.CloseSafely();
}
```

## `NextResultSafely`

```csharp
NextResultSafely(IDataReader dr)
```

Moves to the next result set of the reader and ignores any error, so you do not need a try/catch.

**Parameters**

| Name | Description |
|---|---|
| `dr` | An open data reader. |

**Returns**

The same `dr`, for chaining.

**Example**

```csharp
while (dr.Read()) { /* first result */ }
dr.NextResultSafely();
while (dr.Read()) { /* second result */ }
```

## `ForEachResult`

```csharp
ForEachResult(IDataReader dr, Action<IDataReader>[] action)
```

Reads several result sets. The first action runs for every row of the first result set, the second action for the second result set, and so on. The reader is disposed at the end.

**Parameters**

| Name | Description |
|---|---|
| `dr` | An open data reader. |
| `action` | One action per result set, in order. |

**Remarks**

Known issue: calling this without any action fails with an `IndexOutOfRangeException` (or a `NullReferenceException` for `null`) instead of just closing the reader.

**Example**

```csharp
cmd.ExecuteReader().ForEachResult(
    dr => tasks.Add(new TaskItem { Id = dr.GetInt32(0) }),
    dr => comments.Add(new Comment { Id = dr.GetInt32(0) }));
```

## `NoRecord`

```csharp
NoRecord(IDataReader dr, Action noRecordAction)
```

Runs an action when the reader has no columns, which is how an empty result is detected without calling `Read()` (so other reading code is not disturbed).

**Parameters**

| Name | Description |
|---|---|
| `dr` | An open data reader. |
| `noRecordAction` | Code to run when `FieldCount` is 0. |

**Returns**

The same `dr`, for chaining.

**Example**

```csharp
cmd.ExecuteReader().NoRecord(() => Console.WriteLine("Nothing found"));
```

