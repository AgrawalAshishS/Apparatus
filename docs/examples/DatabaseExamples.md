---
title: Reading database rows (DataReader extensions and IHydrator)
layout: default
parent: Examples
nav_order: 3
---

# Reading database rows (DataReader extensions and IHydrator)

This is the file `DatabaseExamples.cs` from the `examples/Apparatus.Examples` project. Each line shows a call and, in the console output, what it returns.

```csharp
using System.Collections;
using System.Data;
using static Apparatus.Examples.Output;

namespace Apparatus.Examples;

/// <summary>A data class that knows how to fill itself from a reader. Needed for FillObject / FillCollection.</summary>
public class Customer : IHydrator
{
    public int Id { get; set; }
    public string Name { get; set; }

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

    public override string ToString() => $"{Id}:{Name}";
}

public static class DatabaseExamples
{
    // A real program would use cmd.ExecuteReader(). A DataTable gives us the same IDataReader without a database.
    private static IDataReader NewReader()
    {
        var table = new DataTable();
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Rows.Add(1, "Ann");
        table.Rows.Add(2, "Bob");
        return table.CreateDataReader();
    }

    public static void Run()
    {
        Title("IHydrator: fill objects with one call");
        Show("NewReader().FillCollection<Customer>()", NewReader().FillCollection<Customer>());
        Show("NewReader().FillObject<Customer>()", NewReader().FillObject<Customer>());

        var existing = new List<Customer>();
        var reader = NewReader().FillCollection<Customer>(existing);
        Show("FillCollection<Customer>(existingList) -> list", existing);
        Show("...reader still open", !reader.IsClosed);

        Title("Without IHydrator: ForEachRecord");
        var names = new List<string>();
        NewReader().ForEachRecord(dr => names.Add(dr.GetString(1)));
        Show("ForEachRecord(dr => names.Add(...))", names);

        Title("Several result sets in one chain");
        var set = new DataSet();
        set.Tables.Add(new DataTable());
        set.Tables[0].Columns.Add("Id", typeof(int));
        set.Tables[0].Rows.Add(1);
        set.Tables[0].Rows.Add(2);
        set.Tables.Add(new DataTable());
        set.Tables[1].Columns.Add("Id", typeof(int));
        set.Tables[1].Rows.Add(10);

        var first = new List<int>();
        var second = new List<int>();
        set.CreateDataReader()
            .ForEachRecord(false, dr => first.Add(dr.GetInt32(0)))
            .NextResultSafely()
            .ForEachRecord(true, dr => second.Add(dr.GetInt32(0)));
        Show("first result set", first);
        Show("second result set", second);

        first.Clear();
        second.Clear();
        set.CreateDataReader().ForEachResult(dr => first.Add(dr.GetInt32(0)), dr => second.Add(dr.GetInt32(0)));
        Show("ForEachResult -> first", first);
        Show("ForEachResult -> second", second);

        Title("Small helpers");
        var open = NewReader();
        open.CloseSafely();
        Show("CloseSafely() -> IsClosed", open.IsClosed);
        NewReader().NoRecord(() => Console.WriteLine("(not printed: reader has columns)"));
        new DataTable().CreateDataReader().NoRecord(() => Console.WriteLine("NoRecord() => empty reader detected"));
    }
}
```
