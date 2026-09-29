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

Add `using Apparatus;` and every method shows up in code completion with its description and an example.

Areas: text, argument checks, numbers and dates, enums, collections and types, files and streams, async helpers and database reader helpers.
