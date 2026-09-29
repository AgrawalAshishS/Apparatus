---
title: Contributing
layout: default
nav_order: 6
---

# Contributing

## Folder layout

| Folder | What is in it |
|---|---|
| `src/Apparatus` | The library. One file per topic. |
| `tests/Apparatus.Tests` | xUnit tests, one test file per topic. |
| `examples/Apparatus.Examples` | Runnable samples. Also shown on the [Examples](examples/) pages. |
| `tools/ApiDocGenerator` | Builds the API pages in `docs/api` from the XML comments. |
| `docs` | This website (GitHub Pages). |

## Everyday commands

Run these from the repository root on Windows.

| Command | What it does |
|---|---|
| `build.cmd` | Builds everything in Release mode and shows only errors. |
| `test.cmd` | Runs all tests. `test.cmd StringExtensions` runs only tests whose name contains that text. |
| `coverage.cmd` | Runs tests with coverage and writes a report to `artifacts\coverage-report`. |
| `docs.cmd` | Rebuilds `docs/api` and `docs/examples`. Run it after you change any XML comment. |
| `pack.cmd` | Creates the NuGet package in `artifacts\nupkg`. |

## Rules for new or changed methods

1. Write XML comments: a summary, every parameter, the return value, the exceptions, and a short `<example>`.
2. Add tests. The goal is at least 90% line coverage.
3. Add the method to the matching file in `examples/Apparatus.Examples`.
4. Run `docs.cmd` and commit the changed pages.
5. Changing what an existing method does needs the owner's approval.

## Publish the website

In the GitHub repository open **Settings > Pages**, choose **Deploy from a branch**, then select the `main` branch and the `/docs` folder.
