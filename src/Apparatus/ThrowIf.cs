// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Check.cs" company="Toshal Infotech">
//   http://www.ToshalInfotech.com
//   Copyright (c) 2022-23
//   by Toshal Infotech
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//   documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
//   the rights to use, copy, modify, merge, publish, distribute, sub-license, and/or sell copies of the Software, and 
//   to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//   The above copyright notice and this permission notice shall be included in all copies or substantial portions 
//   of the Software.
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//   TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//   THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//   CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//   DEALINGS IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.CompilerServices;

namespace Apparatus
{
    /// <summary>
    /// Short, one-line checks for method arguments. Each check throws a standard exception when the value is not valid.
    /// Every check exists in two styles: a static call such as <c>ThrowIf.Negative(age)</c>, and an extension call such as <c>age.ThrowIfNegative()</c>.
    /// The compiler fills in the argument name for you, so the exception message names the real variable.
    /// </summary>
    /// <example>
    /// Without this class:
    /// <code>
    /// public void MyMethod(int age, string name)
    /// {
    ///     if (age &lt; 0)
    ///         throw new ArgumentOutOfRangeException(nameof(age), "Age can't be less than zero");
    ///     if (string.IsNullOrEmpty(name))
    ///         throw new ArgumentNullException(nameof(name));
    /// }
    /// </code>
    /// With this class:
    /// <code>
    /// public void MyMethod(int age, string name)
    /// {
    ///     age.ThrowIfNegative();
    ///     name.ThrowIfNullOrEmpty();
    /// }
    /// </code>
    /// </example>
    public static class ThrowIf
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the argument is <c>null</c>.
        /// </summary>
        /// <param name="argument">The value to check.</param>
        /// <param name="argumentName">Filled in automatically by the compiler with the source text of <c>argument</c>. Leave it empty.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is <c>null</c>.</exception>
        /// <example>
        /// <code>
        /// ThrowIf.Null(name);
        /// </code>
        /// </example>
        public static void Null([NotNull] object? argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            if (argument is null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the argument is <c>null</c> or an empty string.
        /// </summary>
        /// <param name="argument">The value to check.</param>
        /// <param name="argumentName">Filled in automatically by the compiler with the source text of <c>argument</c>. Leave it empty.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is <c>null</c> or an empty string.</exception>
        /// <example>
        /// <code>
        /// ThrowIf.NullOrEmpty(name);
        /// </code>
        /// </example>
        public static void NullOrEmpty(string argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            if (string.IsNullOrEmpty(argument))
                throw new ArgumentNullException(argument, argumentName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is zero or less.
        /// </summary>
        /// <param name="argument">The value to check.</param>
        /// <param name="argumentName">Filled in automatically by the compiler with the source text of <c>argument</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is zero or less.</exception>
        /// <example>
        /// <code>
        /// ThrowIf.ZeroOrLess(count);
        /// </code>
        /// </example>
        public static void ZeroOrLess(int argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            if (argument <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + " should greator than zero.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is zero or less.
        /// </summary>
        /// <param name="argument">The value to check.</param>
        /// <param name="argumentName">Filled in automatically by the compiler with the source text of <c>argument</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is zero or less.</exception>
        /// <example>
        /// <code>
        /// ThrowIf.ZeroOrLess(count);
        /// </code>
        /// </example>
        public static void ZeroOrLess(long argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            if (argument <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + " should greator than zero.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is less than zero (zero is allowed).
        /// </summary>
        /// <param name="argument">The value to check.</param>
        /// <param name="argumentName">Filled in automatically by the compiler with the source text of <c>argument</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than zero (zero is allowed).</exception>
        /// <example>
        /// <code>
        /// ThrowIf.Negative(index);
        /// </code>
        /// </example>
        public static void Negative(int argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            if (argument < 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + " should be non negative.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is less than zero (zero is allowed).
        /// </summary>
        /// <param name="argument">The value to check.</param>
        /// <param name="argumentName">Filled in automatically by the compiler with the source text of <c>argument</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than zero (zero is allowed).</exception>
        /// <example>
        /// <code>
        /// ThrowIf.Negative(index);
        /// </code>
        /// </example>
        public static void Negative(long argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            if (argument < 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + " should be non negative.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is <see cref="Guid.Empty"/>.
        /// </summary>
        /// <param name="argument">The value to check.</param>
        /// <param name="argumentName">Filled in automatically by the compiler with the source text of <c>argument</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is <see cref="Guid.Empty"/>.</exception>
        /// <example>
        /// <code>
        /// ThrowIf.EmptyGuid(id);
        /// </code>
        /// </example>
        public static void EmptyGuid(Guid argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            if (Guid.Empty == argument)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + " should be non-empty GUID.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is not equal to <paramref name="actual"/>.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The value to compare with.</param>
        /// <param name="expectedName">Filled in automatically by the compiler with the source text of <c>expected</c>. Leave it empty.</param>
        /// <param name="actualName">Filled in automatically by the compiler with the source text of <c>actual</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is not equal to <paramref name="actual"/>.</exception>
        /// <example>
        /// <code>
        /// ThrowIf.NotEqual(expected, actual);
        /// </code>
        /// </example>
        public static void NotEqual(int expected, int actual, [CallerArgumentExpression("expected")] string? expectedName = null, [CallerArgumentExpression("actual")] string? actualName = null)
        {
            if (expected != actual)
                throw new ArgumentOutOfRangeException($"Expected {expected} {expectedName}, got actual {actual} {actualName}");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is not equal to <paramref name="actual"/>.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The value to compare with.</param>
        /// <param name="expectedName">Filled in automatically by the compiler with the source text of <c>expected</c>. Leave it empty.</param>
        /// <param name="actualName">Filled in automatically by the compiler with the source text of <c>actual</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is not equal to <paramref name="actual"/>.</exception>
        /// <example>
        /// <code>
        /// ThrowIf.NotEqual(expected, actual);
        /// </code>
        /// </example>
        public static void NotEqual(long expected, long actual, [CallerArgumentExpression("expected")] string? expectedName = null, [CallerArgumentExpression("actual")] string? actualName = null)
        {
            if (expected != actual)
                throw new ArgumentOutOfRangeException($"Expected {expected} {expectedName}, got actual {actual} {actualName}");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is less than <paramref name="minimumNeeded"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="minimumNeeded">The smallest allowed value (included).</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="minimumNeededName">Filled in automatically by the compiler with the source text of <c>minimumNeeded</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than <paramref name="minimumNeeded"/>.</exception>
        /// <example>
        /// <code>
        /// ThrowIf.LesserThan(age, 18);
        /// </code>
        /// </example>
        public static void LesserThan(int value, int minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            if (minimumNeeded > value)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} is less than minimum required {minimumNeededName}:{minimumNeeded}.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is less than <paramref name="minimumNeeded"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="minimumNeeded">The smallest allowed value (included).</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="minimumNeededName">Filled in automatically by the compiler with the source text of <c>minimumNeeded</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than <paramref name="minimumNeeded"/>.</exception>
        /// <example>
        /// <code>
        /// ThrowIf.LesserThan(age, 18);
        /// </code>
        /// </example>
        public static void LesserThan(long value, long minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            if (minimumNeeded > value)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} is less than minimum required {minimumNeededName}:{minimumNeeded}.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is greater than <paramref name="maximumAllowed"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="maximumAllowed">The biggest allowed value (included).</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="maximumAllowedName">Filled in automatically by the compiler with the source text of <c>maximumAllowed</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is greater than <paramref name="maximumAllowed"/>.</exception>
        /// <example>
        /// <code>
        /// ThrowIf.GreatorThan(age, 65);
        /// </code>
        /// </example>
        public static void GreatorThan(int value, int maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            if (value > maximumAllowed)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} is greator than maximum allowed {maximumAllowedName}:{maximumAllowed}.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is greater than <paramref name="maximumAllowed"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="maximumAllowed">The biggest allowed value (included).</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="maximumAllowedName">Filled in automatically by the compiler with the source text of <c>maximumAllowed</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is greater than <paramref name="maximumAllowed"/>.</exception>
        /// <example>
        /// <code>
        /// ThrowIf.GreatorThan(age, 65);
        /// </code>
        /// </example>
        public static void GreatorThan(long value, long maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            if (value > maximumAllowed)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} is greator than maximum allowed {maximumAllowedName}:{maximumAllowed}.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is less than or equal to <paramref name="minimumNeeded"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="minimumNeeded">The limit. The value must be greater than this.</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="minimumNeededName">Filled in automatically by the compiler with the source text of <c>minimumNeeded</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than or equal to <paramref name="minimumNeeded"/>.</exception>
        /// <example>
        /// <code>
        /// ThrowIf.LessThanEqualTo(age, 0);
        /// </code>
        /// </example>
        public static void LessThanEqualTo(int value, int minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            if (minimumNeeded >= value)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} should be greator than minimum {minimumNeededName}:{minimumNeeded}.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is less than or equal to <paramref name="minimumNeeded"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="minimumNeeded">The limit. The value must be greater than this.</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="minimumNeededName">Filled in automatically by the compiler with the source text of <c>minimumNeeded</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than or equal to <paramref name="minimumNeeded"/>.</exception>
        /// <example>
        /// <code>
        /// ThrowIf.LessThanEqualTo(age, 0);
        /// </code>
        /// </example>
        public static void LessThanEqualTo(long value, long minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            if (minimumNeeded >= value)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} should be greator than minimum {minimumNeededName}:{minimumNeeded}.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is greater than or equal to <paramref name="maximumAllowed"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="maximumAllowed">The limit. The value must be less than this.</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="maximumAllowedName">Filled in automatically by the compiler with the source text of <c>maximumAllowed</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is greater than or equal to <paramref name="maximumAllowed"/>.</exception>
        /// <example>
        /// <code>
        /// ThrowIf.GreatorThanEqualTo(age, 100);
        /// </code>
        /// </example>
        public static void GreatorThanEqualTo(int value, int maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            if (value >= maximumAllowed)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} is should be less than maximum allowed {maximumAllowedName}:{maximumAllowed}.");
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the argument is greater than or equal to <paramref name="maximumAllowed"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="maximumAllowed">The limit. The value must be less than this.</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="maximumAllowedName">Filled in automatically by the compiler with the source text of <c>maximumAllowed</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is greater than or equal to <paramref name="maximumAllowed"/>.</exception>
        /// <example>
        /// <code>
        /// ThrowIf.GreatorThanEqualTo(age, 100);
        /// </code>
        /// </example>
        public static void GreatorThanEqualTo(long value, long maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            if (value >= maximumAllowed)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} is should be less than maximum allowed {maximumAllowedName}:{maximumAllowed}.");
        }

        #region Extensions

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if this value is <c>null</c>. Same as calling <c>ThrowIf.Null</c>, but written as a method on the value.
        /// </summary>
        /// <param name="argument">The value to check.</param>
        /// <param name="argumentName">Filled in automatically by the compiler with the source text of <c>argument</c>. Leave it empty.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is <c>null</c>.</exception>
        /// <example>
        /// <code>
        /// name.ThrowIfNull();
        /// </code>
        /// </example>
        public static void ThrowIfNull([NotNull] this object? argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            Null(argument, argumentName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if this value is <c>null</c> or an empty string. Same as calling <c>ThrowIf.NullOrEmpty</c>, but written as a method on the value.
        /// </summary>
        /// <param name="argument">The value to check.</param>
        /// <param name="argumentName">Filled in automatically by the compiler with the source text of <c>argument</c>. Leave it empty.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is <c>null</c> or an empty string.</exception>
        /// <example>
        /// <code>
        /// name.ThrowIfNullOrEmpty();
        /// </code>
        /// </example>
        public static void ThrowIfNullOrEmpty(this string argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            NullOrEmpty(argument, argumentName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if this value is zero or less. Same as calling <c>ThrowIf.ZeroOrLess</c>, but written as a method on the value.
        /// </summary>
        /// <param name="argument">The value to check.</param>
        /// <param name="argumentName">Filled in automatically by the compiler with the source text of <c>argument</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is zero or less.</exception>
        /// <example>
        /// <code>
        /// count.ThrowIfZeroOrLess();
        /// </code>
        /// </example>
        public static void ThrowIfZeroOrLess(this int argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            ZeroOrLess(argument, argumentName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if this value is zero or less. Same as calling <c>ThrowIf.ZeroOrLess</c>, but written as a method on the value.
        /// </summary>
        /// <param name="argument">The value to check.</param>
        /// <param name="argumentName">Filled in automatically by the compiler with the source text of <c>argument</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is zero or less.</exception>
        /// <example>
        /// <code>
        /// count.ThrowIfZeroOrLess();
        /// </code>
        /// </example>
        public static void ThrowIfZeroOrLess(this long argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            ZeroOrLess(argument, argumentName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if this value is less than zero (zero is allowed). Same as calling <c>ThrowIf.Negative</c>, but written as a method on the value.
        /// </summary>
        /// <param name="argument">The value to check.</param>
        /// <param name="argumentName">Filled in automatically by the compiler with the source text of <c>argument</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than zero (zero is allowed).</exception>
        /// <example>
        /// <code>
        /// index.ThrowIfNegative();
        /// </code>
        /// </example>
        public static void ThrowIfNegative(this int argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            Negative(argument, argumentName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if this value is less than zero (zero is allowed). Same as calling <c>ThrowIf.Negative</c>, but written as a method on the value.
        /// </summary>
        /// <param name="argument">The value to check.</param>
        /// <param name="argumentName">Filled in automatically by the compiler with the source text of <c>argument</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than zero (zero is allowed).</exception>
        /// <example>
        /// <code>
        /// index.ThrowIfNegative();
        /// </code>
        /// </example>
        public static void ThrowIfNegative(this long argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            Negative(argument, argumentName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if this value is <see cref="Guid.Empty"/>. Same as calling <c>ThrowIf.EmptyGuid</c>, but written as a method on the value.
        /// </summary>
        /// <param name="argument">The value to check.</param>
        /// <param name="argumentName">Filled in automatically by the compiler with the source text of <c>argument</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is <see cref="Guid.Empty"/>.</exception>
        /// <example>
        /// <code>
        /// id.ThrowIfEmptyGuid();
        /// </code>
        /// </example>
        public static void ThrowIfEmptyGuid(this Guid argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            EmptyGuid(argument, argumentName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if this value is not equal to <paramref name="actual"/>. Same as calling <c>ThrowIf.NotEqual</c>, but written as a method on the value.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The value to compare with.</param>
        /// <param name="expectedName">Filled in automatically by the compiler with the source text of <c>expected</c>. Leave it empty.</param>
        /// <param name="actualName">Filled in automatically by the compiler with the source text of <c>actual</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is not equal to <paramref name="actual"/>.</exception>
        /// <example>
        /// <code>
        /// expected.ThrowIfNotEqual(actual);
        /// </code>
        /// </example>
        public static void ThrowIfNotEqual(this int expected, int actual, [CallerArgumentExpression("expected")] string? expectedName = null, [CallerArgumentExpression("actual")] string? actualName = null)
        {
            NotEqual(expected, actual, expectedName, actualName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if this value is not equal to <paramref name="actual"/>. Same as calling <c>ThrowIf.NotEqual</c>, but written as a method on the value.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The value to compare with.</param>
        /// <param name="expectedName">Filled in automatically by the compiler with the source text of <c>expected</c>. Leave it empty.</param>
        /// <param name="actualName">Filled in automatically by the compiler with the source text of <c>actual</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is not equal to <paramref name="actual"/>.</exception>
        /// <example>
        /// <code>
        /// expected.ThrowIfNotEqual(actual);
        /// </code>
        /// </example>
        public static void ThrowIfNotEqual(this long expected, long actual, [CallerArgumentExpression("expected")] string? expectedName = null, [CallerArgumentExpression("actual")] string? actualName = null)
        {
            NotEqual(expected, actual, expectedName, actualName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if this value is less than <paramref name="minimumNeeded"/>. Same as calling <c>ThrowIf.LesserThan</c>, but written as a method on the value.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="minimumNeeded">The smallest allowed value (included).</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="minimumNeededName">Filled in automatically by the compiler with the source text of <c>minimumNeeded</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than <paramref name="minimumNeeded"/>.</exception>
        /// <example>
        /// <code>
        /// age.ThrowIfLesserThan(18);
        /// </code>
        /// </example>
        public static void ThrowIfLesserThan(this int value, int minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            LesserThan(value, minimumNeeded, valueName, minimumNeededName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if this value is less than <paramref name="minimumNeeded"/>. Same as calling <c>ThrowIf.LesserThan</c>, but written as a method on the value.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="minimumNeeded">The smallest allowed value (included).</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="minimumNeededName">Filled in automatically by the compiler with the source text of <c>minimumNeeded</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than <paramref name="minimumNeeded"/>.</exception>
        /// <example>
        /// <code>
        /// age.ThrowIfLesserThan(18);
        /// </code>
        /// </example>
        public static void ThrowIfLesserThan(this long value, long minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            LesserThan(value, minimumNeeded, valueName, minimumNeededName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if this value is greater than <paramref name="maximumAllowed"/>. Same as calling <c>ThrowIf.GreatorThan</c>, but written as a method on the value.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="maximumAllowed">The biggest allowed value (included).</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="maximumAllowedName">Filled in automatically by the compiler with the source text of <c>maximumAllowed</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is greater than <paramref name="maximumAllowed"/>.</exception>
        /// <example>
        /// <code>
        /// age.ThrowIfGreatorThan(65);
        /// </code>
        /// </example>
        public static void ThrowIfGreatorThan(this int value, int maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            GreatorThan(value, maximumAllowed, valueName, maximumAllowedName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if this value is greater than <paramref name="maximumAllowed"/>. Same as calling <c>ThrowIf.GreatorThan</c>, but written as a method on the value.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="maximumAllowed">The biggest allowed value (included).</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="maximumAllowedName">Filled in automatically by the compiler with the source text of <c>maximumAllowed</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is greater than <paramref name="maximumAllowed"/>.</exception>
        /// <example>
        /// <code>
        /// age.ThrowIfGreatorThan(65);
        /// </code>
        /// </example>
        public static void ThrowIfGreatorThan(this long value, long maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            GreatorThan(value, maximumAllowed, valueName, maximumAllowedName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if this value is less than or equal to <paramref name="minimumNeeded"/>. Same as calling <c>ThrowIf.LessThanEqualTo</c>, but written as a method on the value.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="minimumNeeded">The limit. The value must be greater than this.</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="minimumNeededName">Filled in automatically by the compiler with the source text of <c>minimumNeeded</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than or equal to <paramref name="minimumNeeded"/>.</exception>
        /// <example>
        /// <code>
        /// age.ThrowIfLessThanEqualTo(0);
        /// </code>
        /// </example>
        public static void ThrowIfLessThanEqualTo(this int value, int minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            LessThanEqualTo(value, minimumNeeded, valueName, minimumNeededName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if this value is less than or equal to <paramref name="minimumNeeded"/>. Same as calling <c>ThrowIf.LessThanEqualTo</c>, but written as a method on the value.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="minimumNeeded">The limit. The value must be greater than this.</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="minimumNeededName">Filled in automatically by the compiler with the source text of <c>minimumNeeded</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than or equal to <paramref name="minimumNeeded"/>.</exception>
        /// <example>
        /// <code>
        /// age.ThrowIfLessThanEqualTo(0);
        /// </code>
        /// </example>
        public static void ThrowIfLessThanEqualTo(this long value, long minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            LessThanEqualTo(value, minimumNeeded, valueName, minimumNeededName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if this value is greater than or equal to <paramref name="maximumAllowed"/>. Same as calling <c>ThrowIf.GreatorThanEqualTo</c>, but written as a method on the value.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="maximumAllowed">The limit. The value must be less than this.</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="maximumAllowedName">Filled in automatically by the compiler with the source text of <c>maximumAllowed</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is greater than or equal to <paramref name="maximumAllowed"/>.</exception>
        /// <example>
        /// <code>
        /// age.ThrowIfGreatorThanEqualTo(100);
        /// </code>
        /// </example>
        public static void ThrowIfGreatorThanEqualTo(this int value, int maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            GreatorThanEqualTo(value, maximumAllowed, valueName, maximumAllowedName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if this value is greater than or equal to <paramref name="maximumAllowed"/>. Same as calling <c>ThrowIf.GreatorThanEqualTo</c>, but written as a method on the value.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="maximumAllowed">The limit. The value must be less than this.</param>
        /// <param name="valueName">Filled in automatically by the compiler with the source text of <c>value</c>. Leave it empty.</param>
        /// <param name="maximumAllowedName">Filled in automatically by the compiler with the source text of <c>maximumAllowed</c>. Leave it empty.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is greater than or equal to <paramref name="maximumAllowed"/>.</exception>
        /// <example>
        /// <code>
        /// age.ThrowIfGreatorThanEqualTo(100);
        /// </code>
        /// </example>
        public static void ThrowIfGreatorThanEqualTo(this long value, long maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            GreatorThanEqualTo(value, maximumAllowed, valueName, maximumAllowedName);
        }

        #endregion

    }
}
