# Apparatus – Agent Guide

C# (.NET 8) library of extension methods and helpers. Owner: Toshal Infotech.

## Layout
- `src/Apparatus/` – library (one file per topic, e.g. `StringExtensions.cs`)
- `tests/Apparatus.Tests/` – xUnit tests, one `<Topic>Tests.cs` per source file
- `examples/Apparatus.Examples/` – runnable samples, one `*Examples.cs` per topic (first line `// Title: ...`)
- `tools/ApiDocGenerator/` – builds `docs/api` and `docs/examples` from XML comments and the samples
- `docs/` – GitHub Pages site (Jekyll, just-the-docs). `docs/api` and `docs/examples` are generated: never edit by hand
- `docs/known-issues.md` – bugs found but not fixed (need owner approval)
- `Apparatus.sln` – solution at repo root

## Commands (repo root, Windows)
- `build.cmd` – Release build, errors only
- `test.cmd [filter]` – run tests, optional class/method name filter
- `coverage.cmd` – tests + coverage report in `artifacts\coverage-report`
- `docs.cmd` – regenerate `docs/api` and `docs/examples` (after any XML comment or example change)
- `pack.cmd` – NuGet package into `artifacts\nupkg`

## Rules
1. **Never change behavior of an existing extension method without owner approval.** Found a bug? Report it, do not fix it. Adding XML docs is fine.
2. Every public member needs XML docs: `<summary>`, `<param>`, `<returns>`, `<exception>` where relevant, and a short `<example>` when useful.
3. Target 90% line coverage. Tests: xUnit, `Method_Scenario_Expected` naming, no test depends on another.
4. Add the member to the matching `examples/Apparatus.Examples/*Examples.cs`, then run `docs.cmd` and commit the pages.
   Bug found in existing code: add a skipped test showing the wanted behavior and a line in `docs/known-issues.md`.
5. Commit small and often. Message: imperative, short subject.
6. Keep CLI output short. Docs use plain, everyday English.
