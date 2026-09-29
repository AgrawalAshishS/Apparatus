using Xunit;
using AppException = Apparatus.Exceptions.Exception;

namespace ApparatusTests;

public class ExceptionTests
{
    [Fact]
    public void Default_HasEmptyErrorCode() => Assert.Equal(string.Empty, new AppException().ErrorCode);

    [Fact]
    public void MessageOnly_SetsMessage()
    {
        var ex = new AppException("boom");
        Assert.Equal("boom", ex.Message);
        Assert.Equal(string.Empty, ex.ErrorCode);
    }

    [Fact]
    public void MessageAndInner_SetsBoth()
    {
        var inner = new InvalidOperationException();
        var ex = new AppException("boom", inner);
        Assert.Equal("boom", ex.Message);
        Assert.Same(inner, ex.InnerException);
    }

    [Fact]
    public void CodeAndMessage_SetsBoth()
    {
        var ex = new AppException("E42", "boom");
        Assert.Equal("E42", ex.ErrorCode);
        Assert.Equal("boom", ex.Message);
    }

    [Fact]
    public void CodeMessageAndInner_SetsAll()
    {
        var inner = new InvalidOperationException();
        var ex = new AppException("E42", "boom", inner);
        Assert.Equal("E42", ex.ErrorCode);
        Assert.Equal("boom", ex.Message);
        Assert.Same(inner, ex.InnerException);
    }

    [Fact]
    public void ErrorCode_CanBeChanged()
    {
        var ex = new AppException { ErrorCode = "X" };
        Assert.Equal("X", ex.ErrorCode);
    }
}
