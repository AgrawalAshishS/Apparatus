---
title: DateTimeExtensions
layout: default
parent: API Reference
nav_order: 4
---

# DateTimeExtensions

Helpers for `DateTime`, `DateTimeOffset`, `DayOfWeek` and Unix epoch seconds.

## Methods

| Method | What it does |
|---|---|
| [`ToEpoch`](#toepoch) | Converts a `DateTime` to Unix epoch time (whole seconds since 1970-01-01 00:00:00 UTC). |
| [`ToEpoch`](#toepoch-1) | Converts a `DateTimeOffset` to Unix epoch time (whole seconds since 1970-01-01 00:00:00 UTC). |
| [`EpochToUtcDate`](#epochtoutcdate) | Converts Unix epoch seconds back to a UTC `DateTime`. |
| [`EpochDiff`](#epochdiff) | Gets the time between two Unix epoch values. |
| [`EpochDiffInMinutes`](#epochdiffinminutes) | Gets the number of whole minutes between two Unix epoch values. |
| [`ClearTime`](#cleartime) | Removes the time part of a date (hours, minutes, seconds and milliseconds), keeping only the day. |
| [`IsWeekend`](#isweekend) | Checks whether a `DayOfWeek` is a weekend day. Sunday is always a weekend. Any day with a number greater than `numberOfDaysInWeek` is also a weekend (Monday = 1 ... Saturday = 6). |
| [`IsWeekday`](#isweekday) | Checks whether a `DayOfWeek` is a working day. This is the opposite of `IsWeekend`. |

## `ToEpoch`

```csharp
ToEpoch(DateTime dateTime)
```

Converts a `DateTime` to Unix epoch time (whole seconds since 1970-01-01 00:00:00 UTC).

**Parameters**

| Name | Description |
|---|---|
| `dateTime` | The date to convert. It is converted to UTC first, so `Local` values are handled correctly. |

**Returns**

Seconds since the Unix epoch. Negative for dates before 1970. Fractions of a second are dropped.

**Example**

```csharp
long epoch = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc).ToEpoch(); // 1577836800
```

## `ToEpoch`

```csharp
ToEpoch(DateTimeOffset dateTime)
```

Converts a `DateTimeOffset` to Unix epoch time (whole seconds since 1970-01-01 00:00:00 UTC).

**Parameters**

| Name | Description |
|---|---|
| `dateTime` | The date to convert. The offset is taken into account. |

**Returns**

Seconds since the Unix epoch. Negative for dates before 1970. Fractions of a second are dropped.

**Example**

```csharp
long epoch = new DateTimeOffset(2020, 1, 1, 5, 30, 0, TimeSpan.FromHours(5.5)).ToEpoch(); // 1577836800
```

## `EpochToUtcDate`

```csharp
EpochToUtcDate(long epoch)
```

Converts Unix epoch seconds back to a UTC `DateTime`.

**Parameters**

| Name | Description |
|---|---|
| `epoch` | Seconds since 1970-01-01 00:00:00 UTC. |

**Returns**

The matching date and time, with `Utc`.

**Example**

```csharp
DateTime date = 1577836800L.EpochToUtcDate(); // 2020-01-01 00:00:00 UTC
```

## `EpochDiff`

```csharp
EpochDiff(long startEpoch, long endEpoch)
```

Gets the time between two Unix epoch values.

**Parameters**

| Name | Description |
|---|---|
| `startEpoch` | Start time in epoch seconds. |
| `endEpoch` | End time in epoch seconds. |

**Returns**

`endEpoch` minus `startEpoch`. Negative when the end is before the start.

**Example**

```csharp
TimeSpan diff = 1000L.EpochDiff(4600L); // 1 hour
```

## `EpochDiffInMinutes`

```csharp
EpochDiffInMinutes(long startEpoch, long endEpoch)
```

Gets the number of whole minutes between two Unix epoch values.

**Parameters**

| Name | Description |
|---|---|
| `startEpoch` | Start time in epoch seconds. |
| `endEpoch` | End time in epoch seconds. |

**Returns**

Whole minutes between the two values. Partial minutes are dropped (rounded toward zero).

**Example**

```csharp
long minutes = 0L.EpochDiffInMinutes(150L); // 2
```

## `ClearTime`

```csharp
ClearTime(DateTime dateTime)
```

Removes the time part of a date (hours, minutes, seconds and milliseconds), keeping only the day.

**Parameters**

| Name | Description |
|---|---|
| `dateTime` | The date to clear. |

**Returns**

The same day at 00:00:00.000. The `Kind` is kept.

**Example**

```csharp
DateTime day = new DateTime(2024, 5, 17, 14, 45, 30).ClearTime(); // 2024-05-17 00:00:00
```

## `IsWeekend`

```csharp
IsWeekend(DayOfWeek dayOfWeek, short numberOfDaysInWeek)
```

Checks whether a `DayOfWeek` is a weekend day. Sunday is always a weekend. Any day with a number greater than `numberOfDaysInWeek` is also a weekend (Monday = 1 ... Saturday = 6).

**Parameters**

| Name | Description |
|---|---|
| `dayOfWeek` | The day to check. |
| `numberOfDaysInWeek` | Number of working days counted from Monday. Default is 5 (Monday to Friday work, Saturday and Sunday off). Use 6 for a Monday to Saturday work week. |

**Returns**

`true` if the day is a weekend; otherwise `false`.

**Example**

```csharp
DayOfWeek.Saturday.IsWeekend();  // true
DayOfWeek.Saturday.IsWeekend(6); // false, Saturday is a working day
DayOfWeek.Monday.IsWeekend();    // false
```

## `IsWeekday`

```csharp
IsWeekday(DayOfWeek dayOfWeek, short numberOfDaysInWeek)
```

Checks whether a `DayOfWeek` is a working day. This is the opposite of `IsWeekend`.

**Parameters**

| Name | Description |
|---|---|
| `dayOfWeek` | The day to check. |
| `numberOfDaysInWeek` | Number of working days counted from Monday. Default is 5. |

**Returns**

`true` if the day is a working day; otherwise `false`.

**Example**

```csharp
DayOfWeek.Wednesday.IsWeekday(); // true
```

