---
title: API Reference
layout: default
nav_order: 2
has_children: true
permalink: /api/
---

# API Reference

Every public class of the Apparatus library. This page is generated from the XML comments in the source code.

| Class | What it is for |
|---|---|
| [CollectionExtensions](CollectionExtensions.html) | Extension methods for generic collections. |
| [DataReaderExtension](DataReaderExtension.html) | Short helpers for `IDataReader`: loop over rows, move between result sets, close the reader without try/catch, and fill objects that implement `IHydrator`. |
| [DateTimeExtensions](DateTimeExtensions.html) | Helpers for `DateTime`, `DateTimeOffset`, `DayOfWeek` and Unix epoch seconds. |
| [DirectoryHelper](DirectoryHelper.html) | Safe helpers for creating and deleting folders. |
| [EncryptionUtility](EncryptionUtility.html) | Simple string encryption and decryption using a passphrase. |
| [EnumExtensions](EnumExtensions.html) | Helpers for working with enumerations. |
| [Exception](Exception.html) | Provides common exception that has Error code facility required for batter UI handling and processing. |
| [FileHelper](FileHelper.html) | Helpers for common file tasks: safe delete, file extension and async reading. |
| [FilterFlag](FilterFlag.html) | Options for `InputFilter`. |
| [IHydrator](IHydrator.html) | A contract for types that know how to fill themselves from an `IDataReader`. |
| [InputFilters](InputFilters.html) | Basic clean-up of text typed by users before it is shown or stored. |
| [NameValueCollectionExtensions](NameValueCollectionExtensions.html) | Extension methods for NameValueCollection. |
| [NumericExtensions](NumericExtensions.html) | Helpers for numeric types such as `Int32`, `Int64` and `Decimal`. |
| [ObjectExtensions](ObjectExtensions.html) | Extension methods of Object class; so for everything. |
| [SpanExtensions](SpanExtensions.html) | Helpers for `ReadOnlySpan`. |
| [StreamExtensions](StreamExtensions.html) | Helpers for reading a whole `Stream` into memory or copying it. |
| [StringExtensions](StringExtensions.html) | Everyday helpers for `String`: checking, comparing, changing case style (Pascal, camel, snake, kebab), splitting, compressing and converting. |
| [TaskExtensions](TaskExtensions.html) | Helpers to run async code from normal (blocking) code. |
| [ThrowIf](ThrowIf.html) | Short, one-line checks for method arguments. |
| [TypeExtensions](TypeExtensions.html) | Extensions for Type class (object.GetType()). |
