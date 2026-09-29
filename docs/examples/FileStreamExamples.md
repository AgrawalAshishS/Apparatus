---
title: Files, folders, streams and encryption
layout: default
parent: Examples
nav_order: 4
---

# Files, folders, streams and encryption

This is the file `FileStreamExamples.cs` from the `examples/Apparatus.Examples` project. Each line shows a call and, in the console output, what it returns.

```csharp
using static Apparatus.Examples.Output;

namespace Apparatus.Examples;

public static class FileStreamExamples
{
    public static void Run()
    {
        var root = Path.Combine(Path.GetTempPath(), "ApparatusExamples_" + Guid.NewGuid().ToString("N"));

        try
        {
            Title("Folders (DirectoryHelper)");
            var reports = Path.Combine(root, "reports");
            DirectoryHelper.CreateIfNotExists(reports);
            Show("CreateIfNotExists(reports) -> exists", Directory.Exists(reports));
            DirectoryHelper.CreateIfNotExists(new DirectoryInfo(Path.Combine(root, "logs")));
            Show("CreateIfNotExists(DirectoryInfo) -> exists", Directory.Exists(Path.Combine(root, "logs")));
            DirectoryHelper.DeleteIfExists(Path.Combine(root, "logs"));
            Show("DeleteIfExists(empty folder) -> exists", Directory.Exists(Path.Combine(root, "logs")));

            Title("Files (FileHelper)");
            var file = Path.Combine(reports, "data.csv");
            File.WriteAllText(file, "id,name\n1,Ann\n2,Bob");
            Show("FileHelper.GetExtension(\"archive.tar.gz\")", FileHelper.GetExtension("archive.tar.gz"));
            Show("FileHelper.GetExtension(\"README\")", FileHelper.GetExtension("README"));
            Show("ReadAllTextAsync(...).Await() length", FileHelper.ReadAllTextAsync(file).Await().Length);
            Show("ReadAllLinesAsync(...).Await()", FileHelper.ReadAllLinesAsync(file).Await());
            Show("ReadAllBytesAsync(...).Await() length", FileHelper.ReadAllBytesAsync(file).Await().Length);
            Show("FileHelper.DeleteIfExists(file)", FileHelper.DeleteIfExists(file));
            Show("FileHelper.DeleteIfExists(file) again", FileHelper.DeleteIfExists(file));
            DirectoryHelper.DeleteIfExists(reports, true);
            Show("DeleteIfExists(reports, recursive) -> exists", Directory.Exists(reports));

            Title("Streams");
            using var stream = new MemoryStream(new byte[] { 1, 2, 3, 4, 5 });
            stream.Position = 3; // GetAllBytes always reads from the start
            Show("stream.GetAllBytes()", stream.GetAllBytes());
            Show("stream.GetAllBytesAsync().Await()", stream.GetAllBytesAsync().Await());
            using var copy = new MemoryStream();
            // Call as a normal method: the framework has its own Stream.CopyToAsync(Stream, CancellationToken).
            StreamExtensions.CopyToAsync(stream, copy, CancellationToken.None).Await();
            Show("StreamExtensions.CopyToAsync -> copy", copy.ToArray());

            Title("Encryption (light protection only, see the warning in the docs)");
            var secret = "meet me at noon".EncryptString("my pass phrase");
            Show("\"meet me at noon\".EncryptString(...)", secret);
            Show("secret.DecryptString(...)", secret.DecryptString("my pass phrase"));
        }
        finally
        {
            DirectoryHelper.DeleteIfExists(root, true);
        }
    }
}
```
