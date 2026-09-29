using System.Security.Cryptography;
using Apparatus;
using Xunit;

namespace ApparatusTests;

public class EncryptionUtilityTests
{
    [Theory]
    [InlineData("hello")]
    [InlineData("")]
    [InlineData("unicode é中文 text")]
    [InlineData("a much longer text that is bigger than one cipher block of eight bytes, for sure.")]
    public void EncryptThenDecrypt_ReturnsOriginal(string text)
    {
        var encrypted = text.EncryptString("pass");
        Assert.Equal(text, encrypted.DecryptString("pass"));
    }

    [Fact]
    public void EncryptString_ReturnsBase64_DifferentFromInput()
    {
        var encrypted = "hello".EncryptString("pass");
        Assert.NotEqual("hello", encrypted);
        Assert.NotNull(Convert.FromBase64String(encrypted));
    }

    [Fact]
    public void EncryptString_SameInput_GivesSameOutput() =>
        Assert.Equal("hello".EncryptString("pass"), "hello".EncryptString("pass"));

    [Fact]
    public void EncryptString_DifferentPassphrase_GivesDifferentOutput() =>
        Assert.NotEqual("hello".EncryptString("pass1"), "hello".EncryptString("pass2"));

    [Fact]
    public void EncryptThenDecrypt_PassphraseLongerThan48_UsesFirst48Characters()
    {
        var longPass = new string('k', 48);
        var encrypted = "hello".EncryptString(longPass + "extra");
        Assert.Equal("hello", encrypted.DecryptString(longPass));
    }

    [Fact]
    public void EncryptThenDecrypt_PassphraseExactly48()
    {
        var pass = new string('k', 48);
        Assert.Equal("hello", "hello".EncryptString(pass).DecryptString(pass));
    }

    [Fact]
    public void DecryptString_InvalidBase64_Throws() =>
        Assert.Throws<FormatException>(() => "not base64 !!".DecryptString("pass"));

    [Fact]
    public void DecryptString_WrongPassphrase_ThrowsOrReturnsDifferentText()
    {
        var encrypted = "hello world".EncryptString("right");
        try
        {
            Assert.NotEqual("hello world", encrypted.DecryptString("wrong"));
        }
        catch (CryptographicException)
        {
            // Expected most of the time: padding check fails with a wrong key.
        }
    }
}
