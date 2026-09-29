---
title: EncryptionUtility
layout: default
parent: API Reference
nav_order: 6
---

# EncryptionUtility

Simple string encryption and decryption using a passphrase.

**Not for sensitive data.** This class uses Triple DES in ECB mode with a key made from an MD5 hash of the passphrase. These are old algorithms and modes. The same text always gives the same result, and there is no protection against tampering. Use it only to lightly hide values (for example query string values). For passwords, personal data or anything that needs real security, use AES-GCM from `System.Security.Cryptography`.



 Only the first 48 characters of the passphrase are used. A shorter passphrase is padded with the letter X.

## Methods

| Method | What it does |
|---|---|
| [`DecryptString`](#decryptstring) | Decrypts text that was encrypted by `EncryptString` with the same passphrase. |
| [`EncryptString`](#encryptstring) | Encrypts text with a passphrase and returns it as a Base64 string. |

## `DecryptString`

```csharp
DecryptString(string source, string passphrase)
```

Decrypts text that was encrypted by `EncryptString` with the same passphrase.

**Parameters**

| Name | Description |
|---|---|
| `source` | The Base64 encrypted text. |
| `passphrase` | The same passphrase that was used to encrypt. |

**Returns**

The original text.

**Exceptions**

- `FormatException`: `source` is not valid Base64.
- `CryptographicException`: The passphrase is wrong or the data is damaged.

**Example**

```csharp
string plain = encrypted.DecryptString("my secret phrase");
```

## `EncryptString`

```csharp
EncryptString(string source, string passphrase)
```

Encrypts text with a passphrase and returns it as a Base64 string.

**Parameters**

| Name | Description |
|---|---|
| `source` | The text to encrypt. |
| `passphrase` | The secret phrase. Keep it private; you need the same one to decrypt. |

**Returns**

The encrypted text in Base64 format.

**Example**

```csharp
string encrypted = "hello".EncryptString("my secret phrase");
string plain = encrypted.DecryptString("my secret phrase"); // "hello"
```

