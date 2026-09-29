---
title: IHydrator
layout: default
parent: API Reference
nav_order: 11
---

# IHydrator

A contract for types that know how to fill themselves from an `IDataReader`. Implement it on your data classes, then use `DataReaderExtension` to fill one object or a whole list in a single call.

**Example**

```csharp
public class Customer : IHydrator
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public void FillObject(IDataReader dr, bool manageDataReader, bool doDrRead)
    {
        if (doDrRead && !dr.Read()) return;
        Id = dr.GetInt32(0);
        Name = dr.GetString(1);
        if (manageDataReader) dr.Close();
    }

    public void FillCollection(IDataReader dr, bool manageDataReader, IList listToFill)
    {
        while (dr.Read())
        {
            var item = new Customer();
            item.FillObject(dr, false, false);
            listToFill.Add(item);
        }
        if (manageDataReader) dr.Close();
    }
}

// usage
List<Customer> customers = cmd.ExecuteReader().FillCollection<Customer>();
```

## Methods

| Method | What it does |
|---|---|
| [`FillCollection`](#fillcollection) | Called when collection filling is expected from data reader. |
| [`FillObject`](#fillobject) | Called when single object filling is expected. |

## `FillCollection`

```csharp
FillCollection(IDataReader dr, bool manageDataReader, IList listToFill)
```

Called when collection filling is expected from data reader.

**Parameters**

| Name | Description |
|---|---|
| `dr` | Object of data reader class, mostly connecting data from database. |
| `manageDataReader` | If true, the implementing method MUST close data reader. Otherwise not. |
| `listToFill` | List that to be filled. |

## `FillObject`

```csharp
FillObject(IDataReader dr, bool manageDataReader, bool doDrRead)
```

Called when single object filling is expected.

**Parameters**

| Name | Description |
|---|---|
| `dr` | Object of data reader class, mostly connecting data from database. |
| `manageDataReader` | If true, the implementing method MUST close data reader. Otherwise not. |
| `doDrRead` | If false, dr.read() should not be called. This is important when multiple different types objects to be created using single DR. |

