using System.Text;
using Apparatus;
using Xunit;

namespace ApparatusTests;

public sealed class IOHelperTests : IDisposable
{
    private readonly string _root = Path.Combine(Path.GetTempPath(), "ApparatusTests_" + Guid.NewGuid().ToString("N"));

    public IOHelperTests() => Directory.CreateDirectory(_root);

    public void Dispose()
    {
        if (Directory.Exists(_root)) Directory.Delete(_root, true);
    }

    // ---- DirectoryHelper ----
    [Fact]
    public void CreateIfNotExists_String_CreatesFolderAndParents()
    {
        var path = Path.Combine(_root, "a", "b");
        DirectoryHelper.CreateIfNotExists(path);
        Assert.True(Directory.Exists(path));
    }

    [Fact]
    public void CreateIfNotExists_String_ExistingFolder_DoesNotThrow()
    {
        DirectoryHelper.CreateIfNotExists(_root);
        Assert.True(Directory.Exists(_root));
    }

    [Fact]
    public void CreateIfNotExists_DirectoryInfo_CreatesFolder()
    {
        var info = new DirectoryInfo(Path.Combine(_root, "info"));
        DirectoryHelper.CreateIfNotExists(info);
        Assert.True(Directory.Exists(info.FullName));
    }

    [Fact]
    public void CreateIfNotExists_DirectoryInfo_ExistingFolder_DoesNotThrow() =>
        DirectoryHelper.CreateIfNotExists(new DirectoryInfo(_root));

    [Fact]
    public void DeleteIfExists_EmptyFolder_Deletes()
    {
        var path = Path.Combine(_root, "empty");
        Directory.CreateDirectory(path);
        DirectoryHelper.DeleteIfExists(path);
        Assert.False(Directory.Exists(path));
    }

    [Fact]
    public void DeleteIfExists_MissingFolder_DoesNotThrow() =>
        DirectoryHelper.DeleteIfExists(Path.Combine(_root, "missing"));

    [Fact]
    public void DeleteIfExists_NonEmptyFolder_NotRecursive_Throws()
    {
        var path = Path.Combine(_root, "full");
        Directory.CreateDirectory(path);
        File.WriteAllText(Path.Combine(path, "f.txt"), "x");
        Assert.Throws<IOException>(() => DirectoryHelper.DeleteIfExists(path));
    }

    [Fact]
    public void DeleteIfExists_Recursive_DeletesContent()
    {
        var path = Path.Combine(_root, "full");
        Directory.CreateDirectory(path);
        File.WriteAllText(Path.Combine(path, "f.txt"), "x");
        DirectoryHelper.DeleteIfExists(path, true);
        Assert.False(Directory.Exists(path));
    }

    [Fact]
    public void DeleteIfExists_Recursive_MissingFolder_DoesNotThrow() =>
        DirectoryHelper.DeleteIfExists(Path.Combine(_root, "missing"), true);

    // ---- FileHelper ----
    [Fact]
    public void FileDeleteIfExists_ExistingFile_ReturnsTrueAndDeletes()
    {
        var file = Path.Combine(_root, "a.txt");
        File.WriteAllText(file, "x");
        Assert.True(FileHelper.DeleteIfExists(file));
        Assert.False(File.Exists(file));
    }

    [Fact]
    public void FileDeleteIfExists_MissingFile_ReturnsFalse() =>
        Assert.False(FileHelper.DeleteIfExists(Path.Combine(_root, "missing.txt")));

    [Theory]
    [InlineData("report.pdf", "pdf")]
    [InlineData("archive.tar.gz", "gz")]
    [InlineData(@"C:\dir\file.txt", "txt")]
    [InlineData("ends.with.dot.", "")]
    public void GetExtension_ReturnsTextAfterLastDot(string name, string expected) =>
        Assert.Equal(expected, FileHelper.GetExtension(name));

    [Fact]
    public void GetExtension_NoDot_ReturnsNull() => Assert.Null(FileHelper.GetExtension("README"));

    [Fact]
    public void GetExtension_Empty_Throws() =>
        Assert.Throws<ArgumentNullException>(() => FileHelper.GetExtension(""));

    [Fact]
    public void GetExtension_Null_Throws() =>
        Assert.Throws<ArgumentNullException>(() => FileHelper.GetExtension(null!));

    [Fact]
    public async Task ReadAllTextAsync_ReturnsFileText()
    {
        var file = Path.Combine(_root, "t.txt");
        await File.WriteAllTextAsync(file, "hello\nworld");
        Assert.Equal("hello\nworld", await FileHelper.ReadAllTextAsync(file));
    }

    [Fact]
    public async Task ReadAllTextAsync_MissingFile_Throws() =>
        await Assert.ThrowsAsync<FileNotFoundException>(() => FileHelper.ReadAllTextAsync(Path.Combine(_root, "none.txt")));

    [Fact]
    public async Task ReadAllBytesAsync_ReturnsFileBytes()
    {
        var file = Path.Combine(_root, "b.bin");
        var bytes = new byte[] { 1, 2, 3, 250 };
        await File.WriteAllBytesAsync(file, bytes);
        Assert.Equal(bytes, await FileHelper.ReadAllBytesAsync(file));
    }

    [Fact]
    public async Task ReadAllBytesAsync_EmptyFile_ReturnsEmpty()
    {
        var file = Path.Combine(_root, "empty.bin");
        await File.WriteAllBytesAsync(file, Array.Empty<byte>());
        Assert.Empty(await FileHelper.ReadAllBytesAsync(file));
    }

    [Fact]
    public async Task ReadAllLinesAsync_ReturnsLines()
    {
        var file = Path.Combine(_root, "l.txt");
        await File.WriteAllTextAsync(file, "one\r\ntwo\nthree");
        Assert.Equal(new[] { "one", "two", "three" }, await FileHelper.ReadAllLinesAsync(file));
    }

    [Fact]
    public async Task ReadAllLinesAsync_UsesGivenEncoding()
    {
        var file = Path.Combine(_root, "u.txt");
        await File.WriteAllTextAsync(file, "caf\u00e9", Encoding.Latin1);
        var lines = await FileHelper.ReadAllLinesAsync(file, Encoding.Latin1);
        Assert.Equal(new[] { "caf\u00e9" }, lines);
    }

    [Fact]
    public async Task ReadAllLinesAsync_EmptyFile_ReturnsEmpty()
    {
        var file = Path.Combine(_root, "e.txt");
        await File.WriteAllTextAsync(file, "");
        Assert.Empty(await FileHelper.ReadAllLinesAsync(file));
    }
}
