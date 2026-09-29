# Apparatus – Agent Guide

C# (.NET 8) library of extension methods and helpers. Owner: Toshal Infotech.

## Layout
- `src/Apparatus/` – library (one file per topic, e.g. `StringExtensions.cs`)
- `tests/Apparatus.Tests/` – xUnit tests, one `<Topic>Tests.cs` per source file
- `examples/` – runnable usage samples (one file per topic)
- `docs/` – GitHub Pages site (Markdown, Jekyll)
- `Apparatus.sln` – solution at repo root

## Commands (repo root, Windows)
- `build.cmd` – Release build, errors only
- `test.cmd [filter]` – run tests, optional class/method name filter
- `coverage.cmd` – tests + coverage report in `artifacts\coverage-report`
- `pack.cmd` – NuGet package into `artifacts\nupkg`

## Rules
1. **Never change behavior of an existing extension method without owner approval.** Found a bug? Report it, do not fix it. Adding XML docs is fine.
2. Every public member needs XML docs: `<summary>`, `<param>`, `<returns>`, `<exception>` where relevant, and a short `<example>` when useful.
3. Target 90% line coverage. Tests: xUnit, `Method_Scenario_Expected` naming, no test depends on another.
4. Add or update the matching page in `docs/` and sample in `examples/` when adding a public member.
5. Commit small and often. Message: imperative, short subject.
6. Keep CLI output short. Docs use plain, everyday English.
