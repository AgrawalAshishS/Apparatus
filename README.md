# Apparatus

Extension methods and helpers for C# that remove repeated code and keep your own code short and easy to read.
By Toshal Infotech.

```csharp
using Apparatus;

name.ThrowIfNullOrEmpty();                    // check an argument in one line
"user_first-name".ToPascalCase();             // "UserFirstName"
status.In("open", "pending");                 // instead of status == "open" || status == "pending"
File.ReadAllTextAsync("a.txt").Await();       // call async code from normal code
cmd.ExecuteReader().FillCollection<Customer>(); // fill a list from a database reader
```

## What is inside

Text (`StringExtensions`, `InputFilters`, `EncryptionUtility`), checks (`ThrowIf`, `NumericExtensions`, `ObjectExtensions`),
dates and enums, collections and types, files and streams, async helpers, and database reader helpers.

## Documentation

- Website (GitHub Pages, from the `docs` folder): getting started, full API reference, examples and known issues.
- Every public method has XML comments, so your editor shows a description and example while you type.
- Runnable examples: `dotnet run --project examples/Apparatus.Examples`

## Build and test

Run from the repository root on Windows:

| Command | What it does |
|---|---|
| `build.cmd` | Build in Release mode |
| `test.cmd [filter]` | Run the tests |
| `coverage.cmd` | Run the tests and write a coverage report to `artifacts\coverage-report` |
| `docs.cmd` | Rebuild the API and example pages in `docs` |
| `pack.cmd` | Create the NuGet package |

## Folders

```
src/Apparatus            the library
tests/Apparatus.Tests    unit tests (xUnit)
examples/                runnable examples
tools/ApiDocGenerator    builds docs/api from the XML comments
docs/                    website
```

## License

See [LICENSE](LICENSE).
