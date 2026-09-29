using System.Collections;
using System.Data;
using Apparatus;
using Xunit;

namespace ApparatusTests;

public class DataReaderExtensionTests
{
    /// <summary>Simple type that fills itself from the first two columns (Id, Name).</summary>
    public class Person : IHydrator
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool ClosedByHydrator { get; private set; }

        public void FillObject(IDataReader dr, bool manageDataReader, bool doDrRead)
        {
            if (doDrRead && !dr.Read()) return;
            Id = dr.GetInt32(0);
            Name = dr.GetString(1);
            if (manageDataReader) { dr.Close(); ClosedByHydrator = true; }
        }

        public void FillCollection(IDataReader dr, bool manageDataReader, IList listToFill)
        {
            while (dr.Read())
            {
                var item = new Person();
                item.FillObject(dr, false, false);
                listToFill.Add(item);
            }
            if (manageDataReader) dr.Close();
        }
    }

    private static DataTable PeopleTable(params (int id, string name)[] rows)
    {
        var table = new DataTable();
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("Name", typeof(string));
        foreach (var (id, name) in rows) table.Rows.Add(id, name);
        return table;
    }

    private static IDataReader TwoPeople() => PeopleTable((1, "Ann"), (2, "Bob")).CreateDataReader();

    // ---- FillCollection ----
    [Fact]
    public void FillCollection_NewList_ReadsAllRowsAndClosesReader()
    {
        var reader = TwoPeople();
        var list = reader.FillCollection<Person>();
        Assert.Equal(2, list.Count);
        Assert.Equal("Bob", list[1].Name);
        Assert.True(reader.IsClosed);
    }

    [Fact]
    public void FillCollection_CloseFalse_KeepsReaderOpen()
    {
        var reader = TwoPeople();
        var list = reader.FillCollection<Person>(false);
        Assert.Equal(2, list.Count);
        Assert.False(reader.IsClosed);
    }

    [Fact]
    public void FillCollection_CloseTrue_ClosesReader()
    {
        var reader = TwoPeople();
        reader.FillCollection<Person>(true);
        Assert.True(reader.IsClosed);
    }

    [Fact]
    public void FillCollection_ExistingList_AppendsAndKeepsReaderOpen()
    {
        var reader = TwoPeople();
        var list = new ArrayList { "existing" };
        var returned = reader.FillCollection<Person>(list);
        Assert.Same(reader, returned);
        Assert.Equal(3, list.Count);
        Assert.False(reader.IsClosed);
    }

    [Fact]
    public void FillCollection_WithCloseFlagAndList_Fills()
    {
        var reader = TwoPeople();
        var list = new List<Person>();
        reader.FillCollection<Person>(true, list);
        Assert.Equal(2, list.Count);
        Assert.True(reader.IsClosed);
    }

    [Fact]
    public void FillCollection_EmptyReader_ReturnsEmptyList() =>
        Assert.Empty(PeopleTable().CreateDataReader().FillCollection<Person>());

    // ---- FillObject ----
    [Fact]
    public void FillObject_ExistingObject_FillsFromNextRowAndReturnsReader()
    {
        var reader = TwoPeople();
        var person = new Person();
        var returned = reader.FillObject(person);
        Assert.Same(reader, returned);
        Assert.Equal(1, person.Id);
        Assert.Equal("Ann", person.Name);
        Assert.False(reader.IsClosed);
    }

    [Fact]
    public void FillObject_ExistingObject_NoRows_LeavesObjectUntouched()
    {
        var person = new Person { Name = "keep" };
        PeopleTable().CreateDataReader().FillObject(person);
        Assert.Equal("keep", person.Name);
    }

    [Fact]
    public void FillObject_NullObject_DoesNotThrow()
    {
        var reader = TwoPeople();
        Person? person = null;
        var returned = reader.FillObject(person!);
        Assert.Same(reader, returned);
    }

    [Fact]
    public void FillObject_New_ReturnsFilledObjectAndClosesReader()
    {
        var reader = TwoPeople();
        var person = reader.FillObject<Person>();
        Assert.NotNull(person);
        Assert.Equal("Ann", person!.Name);
        Assert.True(person.ClosedByHydrator);
        Assert.True(reader.IsClosed);
    }

    [Fact]
    public void FillObject_New_NoRows_ReturnsNull() =>
        Assert.Null(PeopleTable().CreateDataReader().FillObject<Person>());

    [Fact]
    public void FillObject_New_CloseFalse_KeepsReaderOpen()
    {
        var reader = TwoPeople();
        var person = reader.FillObject<Person>(false);
        Assert.Equal(1, person!.Id);
        Assert.False(reader.IsClosed);
    }

    [Fact]
    public void FillObject_New_CloseFalse_NoRows_ReturnsNull() =>
        Assert.Null(PeopleTable().CreateDataReader().FillObject<Person>(false));

    [Fact]
    public void FillObject_DoDrReadTrue_ReadsRow()
    {
        var person = TwoPeople().FillObject<Person>(false, true);
        Assert.Equal("Ann", person!.Name);
    }

    [Fact]
    public void FillObject_DoDrReadTrue_NoRows_ReturnsNull() =>
        Assert.Null(PeopleTable().CreateDataReader().FillObject<Person>(false, true));

    [Fact]
    public void FillObject_DoDrReadFalse_UsesCurrentRow()
    {
        var reader = TwoPeople();
        reader.Read();
        reader.Read(); // now on Bob
        var person = reader.FillObject<Person>(false, false);
        Assert.Equal("Bob", person!.Name);
    }

    // ---- ForEachRecord ----
    [Fact]
    public void ForEachRecord_RunsActionForEachRowAndClosesReader()
    {
        var reader = TwoPeople();
        var names = new List<string>();
        reader.ForEachRecord(dr => names.Add(dr.GetString(1)));
        Assert.Equal(new[] { "Ann", "Bob" }, names);
        Assert.True(reader.IsClosed);
    }

    [Fact]
    public void ForEachRecord_CloseFalse_KeepsReaderOpenAndReturnsIt()
    {
        var reader = TwoPeople();
        var count = 0;
        var returned = reader.ForEachRecord(false, _ => count++);
        Assert.Equal(2, count);
        Assert.Same(reader, returned);
        Assert.False(reader.IsClosed);
    }

    [Fact]
    public void ForEachRecord_CloseTrue_ClosesReader()
    {
        var reader = TwoPeople();
        reader.ForEachRecord(true, _ => { });
        Assert.True(reader.IsClosed);
    }

    [Fact]
    public void ForEachRecord_ActionThrows_ClosesReaderAndRethrows()
    {
        var reader = TwoPeople();
        Assert.Throws<InvalidOperationException>(() => reader.ForEachRecord(false, _ => throw new InvalidOperationException("boom")));
        Assert.True(reader.IsClosed);
    }

    [Fact]
    public void ForEachRecord_CloseTrue_ActionThrows_ClosesReaderAndRethrows()
    {
        var reader = TwoPeople();
        Assert.Throws<InvalidOperationException>(() => reader.ForEachRecord(true, _ => throw new InvalidOperationException("boom")));
        Assert.True(reader.IsClosed);
    }

    // ---- CloseSafely / NextResultSafely ----
    [Fact]
    public void CloseSafely_ClosesReader()
    {
        var reader = TwoPeople();
        reader.CloseSafely();
        Assert.True(reader.IsClosed);
    }

    [Fact]
    public void CloseSafely_AlreadyClosed_DoesNotThrow()
    {
        var reader = TwoPeople();
        reader.Close();
        reader.CloseSafely();
        Assert.True(reader.IsClosed);
    }

    [Fact]
    public void NextResultSafely_MovesToNextResultSet()
    {
        var set = new DataSet();
        set.Tables.Add(PeopleTable((1, "Ann")));
        set.Tables.Add(PeopleTable((2, "Bob")));
        var reader = set.CreateDataReader();
        reader.Read();
        var returned = reader.NextResultSafely();
        Assert.Same(reader, returned);
        reader.Read();
        Assert.Equal("Bob", reader.GetString(1));
    }

    [Fact]
    public void NextResultSafely_ClosedReader_DoesNotThrow()
    {
        var reader = TwoPeople();
        reader.Close();
        Assert.Same(reader, reader.NextResultSafely());
    }

    // ---- ForEachResult ----
    [Fact]
    public void ForEachResult_RunsOneActionPerResultSet()
    {
        var set = new DataSet();
        set.Tables.Add(PeopleTable((1, "Ann"), (2, "Bob")));
        set.Tables.Add(PeopleTable((3, "Cy")));
        var first = new List<int>();
        var second = new List<int>();

        set.CreateDataReader().ForEachResult(dr => first.Add(dr.GetInt32(0)), dr => second.Add(dr.GetInt32(0)));

        Assert.Equal(new[] { 1, 2 }, first);
        Assert.Equal(new[] { 3 }, second);
    }

    [Fact]
    public void ForEachResult_SingleAction_ReadsFirstResultOnly()
    {
        var count = 0;
        TwoPeople().ForEachResult(_ => count++);
        Assert.Equal(2, count);
    }

    // Known issue: with no actions the code closes the reader and then still reads action[0].
    [Fact]
    public void ForEachResult_NoActions_CurrentlyThrowsIndexOutOfRange() =>
        Assert.Throws<IndexOutOfRangeException>(() => TwoPeople().ForEachResult());

    [Fact(Skip = "Known issue: ForEachResult with no actions should just close the reader. Waiting for owner approval to fix.")]
    public void ForEachResult_NoActions_ShouldJustCloseReader()
    {
        var reader = TwoPeople();
        reader.ForEachResult();
        Assert.True(reader.IsClosed);
    }

    // ---- NoRecord ----
    [Fact]
    public void NoRecord_NoColumns_RunsAction()
    {
        var called = false;
        var reader = new DataTable().CreateDataReader();
        var returned = reader.NoRecord(() => called = true);
        Assert.True(called);
        Assert.Same(reader, returned);
    }

    [Fact]
    public void NoRecord_HasColumns_DoesNotRunAction()
    {
        var called = false;
        TwoPeople().NoRecord(() => called = true);
        Assert.False(called);
    }
}
