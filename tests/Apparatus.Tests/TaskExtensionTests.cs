using Apparatus;
using Xunit;

namespace ApparatusTests;

public class TaskExtensionTests
{
    private static async Task<int> ResultTestMethod()
    {
        await Task.Yield();
        return 1;
    }

    private static async Task<int> ErrorTestMethod()
    {
        await Task.Yield();
        throw new MyException("my test");
    }

    private static async Task VoidErrorTestMethod()
    {
        await Task.Yield();
        throw new MyException("void test");
    }

    [Fact]
    public void Await_Generic_ReturnsResult() => Assert.Equal(1, ResultTestMethod().Await());

    [Fact]
    public void Await_Generic_ThrowsOriginalException()
    {
        var ex = Assert.Throws<MyException>(() => ErrorTestMethod().Await());
        Assert.Equal("my test", ex.Message);
    }

    [Fact]
    public void Await_Generic_AlreadyCompletedTask() => Assert.Equal(5, Task.FromResult(5).Await());

    [Fact]
    public void Await_NonGeneric_WaitsForCompletion()
    {
        var done = false;
        Task.Run(async () => { await Task.Delay(20); done = true; }).Await();
        Assert.True(done);
    }

    [Fact]
    public void Await_NonGeneric_ThrowsOriginalException()
    {
        var ex = Assert.Throws<MyException>(() => VoidErrorTestMethod().Await());
        Assert.Equal("void test", ex.Message);
    }

    [Fact]
    public void Await_Cancelled_ThrowsOperationCanceled()
    {
        using var cts = new CancellationTokenSource();
        cts.Cancel();
        Assert.ThrowsAny<OperationCanceledException>(() => Task.FromCanceled<int>(cts.Token).Await());
    }

    [Fact]
    public void RunSync_Generic_ReturnsResult() =>
        Assert.Equal(42, Apparatus.TaskExtensions.RunSync(async () => { await Task.Delay(1); return 42; }));

    [Fact]
    public void RunSync_Generic_ThrowsOriginalException() =>
        Assert.Throws<MyException>(() => Apparatus.TaskExtensions.RunSync(() => ErrorTestMethod()));

    [Fact]
    public void RunSync_NonGeneric_RunsFunction()
    {
        var done = false;
        Apparatus.TaskExtensions.RunSync(async () => { await Task.Delay(1); done = true; });
        Assert.True(done);
    }

    [Fact]
    public void RunSync_NonGeneric_ThrowsOriginalException() =>
        Assert.Throws<MyException>(() => Apparatus.TaskExtensions.RunSync(() => VoidErrorTestMethod()));
}

public class MyException : Exception
{
    public MyException() { }
    public MyException(string message) : base(message) { }
    public MyException(string message, Exception inner) : base(message, inner) { }
}
