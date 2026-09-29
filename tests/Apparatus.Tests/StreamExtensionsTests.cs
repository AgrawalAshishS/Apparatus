using Apparatus;
using Xunit;

namespace ApparatusTests;

public class StreamExtensionsTests
{
    private static readonly byte[] Data = { 1, 2, 3, 4, 5 };

    [Fact]
    public void GetAllBytes_ReturnsAllBytes() => Assert.Equal(Data, new MemoryStream(Data).GetAllBytes());

    [Fact]
    public void GetAllBytes_ReadsFromStart_EvenIfPositionMoved()
    {
        var stream = new MemoryStream(Data) { Position = 3 };
        Assert.Equal(Data, stream.GetAllBytes());
    }

    [Fact]
    public void GetAllBytes_EmptyStream_ReturnsEmpty() => Assert.Empty(new MemoryStream().GetAllBytes());

    [Fact]
    public async Task GetAllBytesAsync_ReturnsAllBytes()
    {
        var stream = new MemoryStream(Data) { Position = 2 };
        Assert.Equal(Data, await stream.GetAllBytesAsync());
    }

    [Fact]
    public async Task GetAllBytesAsync_CancelledToken_Throws()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => new MemoryStream(Data).GetAllBytesAsync(cts.Token));
    }

    // NOTE: Stream.CopyToAsync(Stream, CancellationToken) exists in the framework and wins over this extension
    // when written as source.CopyToAsync(...). The extension is called here as a normal static method.
    [Fact]
    public async Task CopyToAsync_CopiesFromStart()
    {
        var source = new MemoryStream(Data) { Position = 4 };
        var destination = new MemoryStream();
        await StreamExtensions.CopyToAsync(source, destination, CancellationToken.None);
        Assert.Equal(Data, destination.ToArray());
    }
}
