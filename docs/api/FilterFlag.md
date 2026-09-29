---
title: FilterFlag
layout: default
parent: API Reference
nav_order: 10
---

# FilterFlag

Options for `InputFilter`. Combine several with `|`, for example `FilterFlag.NoMarkup | FilterFlag.NoScripting`.

## Properties and fields

| Name | What it does |
|---|---|
| `MultiLine` | Replaces line breaks with `<br />` so multi-line text keeps its shape in HTML. Ignored when `NoSQL` is also set. |
| `NoMarkup` | If the text contains HTML tags, HTML-encodes the whole text so the tags are shown as plain text. Ignored when `NoSQL` is also set. |
| `NoScripting` | Replaces risky parts such as `<script>`, `<iframe>`, `javascript:` and `onerror` with a space. Ignored when `NoSQL` is also set. |
| `NoSQL` | Replaces common SQL words and symbols (such as `select`, `drop`, `--`, `;`) with a space and doubles single quotes. When set, the `MultiLine`, `NoMarkup` and `NoScripting` filters are skipped. |
| `NoAngleBrackets` | Removes all `<` and `>` characters. |

