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
    /// Provides shorthand for argument value checking.
    /// For example before 
    /// @code
    /// public void MyMethod(int age, string name)
    /// {
    ///     if( age < 0)
    ///         throw new ArgumentOutOfRangeException("age", "Age can't be less than zero");
    ///     if(string.IsNullOrEmpty(name))
    ///         throw new ArgumentNullException("name");
    /// }
    /// @endcode
    /// 
    /// With extensions
    /// @code
    /// public void MyMethod(int age, string name)
    /// {
    ///     //Check.IsPositive(age, "Age"); OR
    ///     age.CheckPositive("Age");
    ///
    ///     //Check.NotNull(name, "Name"); OR
    ///     name.CheckNotNullOrEmpty("Name");
    /// }
    /// @endcode
    /// </summary>
    public static class ThrowIf
    {
        public static void Null([NotNull] object? argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            if (argument is null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void NullOrEmpty(string argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            if (string.IsNullOrEmpty(argument))
                throw new ArgumentNullException(argument, argumentName);
        }

        public static void ZeroOrLess(int argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            if (argument <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + " should greator than zero.");
        }

        public static void ZeroOrLess(long argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            if (argument <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + " should greator than zero.");
        }

        public static void Negative(int argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            if (argument < 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + " should be non negative.");
        }

        public static void Negative(long argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            if (argument < 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + " should be non negative.");
        }

        public static void EmptyGuid(Guid argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            if (Guid.Empty == argument)
                throw new ArgumentOutOfRangeException(argumentName, argumentName + " should be non-empty GUID.");
        }

        public static void NotEqual(int expected, int actual, [CallerArgumentExpression("expected")] string? expectedName = null, [CallerArgumentExpression("actual")] string? actualName = null)
        {
            if (expected != actual)
                throw new ArgumentOutOfRangeException($"Expected {expected} {expectedName}, got actual {actual} {actualName}");
        }

        public static void NotEqual(long expected, long actual, [CallerArgumentExpression("expected")] string? expectedName = null, [CallerArgumentExpression("actual")] string? actualName = null)
        {
            if (expected != actual)
                throw new ArgumentOutOfRangeException($"Expected {expected} {expectedName}, got actual {actual} {actualName}");
        }

        public static void LesserThan(int value, int minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            if (minimumNeeded > value)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} is less than minimum required {minimumNeededName}:{minimumNeeded}.");
        }

        public static void LesserThan(long value, long minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            if (minimumNeeded > value)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} is less than minimum required {minimumNeededName}:{minimumNeeded}.");
        }

        public static void GreatorThan(int value, int maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            if (value > maximumAllowed)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} is greator than maximum allowed {maximumAllowedName}:{maximumAllowed}.");
        }

        public static void GreatorThan(long value, long maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            if (value > maximumAllowed)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} is greator than maximum allowed {maximumAllowedName}:{maximumAllowed}.");
        }

        public static void LessThanEqualTo(int value, int minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            if (minimumNeeded >= value)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} should be greator than minimum {minimumNeededName}:{minimumNeeded}.");
        }

        public static void LessThanEqualTo(long value, long minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            if (minimumNeeded >= value)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} should be greator than minimum {minimumNeededName}:{minimumNeeded}.");
        }

        public static void GreatorThanEqualTo(int value, int maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            if (value >= maximumAllowed)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} is should be less than maximum allowed {maximumAllowedName}:{maximumAllowed}.");
        }

        public static void GreatorThanEqualTo(long value, long maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            if (value >= maximumAllowed)
                throw new ArgumentOutOfRangeException(valueName, $"{valueName}:{value} is should be less than maximum allowed {maximumAllowedName}:{maximumAllowed}.");
        }

        #region Extensions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ThrowIfNull([NotNull] this object? argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            Null(argument, argumentName);
        }

        public static void ThrowIfNullOrEmpty(this string argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            NullOrEmpty(argument, argumentName);
        }

        public static void ThrowIfZeroOrLess(this int argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            ZeroOrLess(argument, argumentName);
        }

        public static void ThrowIfZeroOrLess(this long argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            ZeroOrLess(argument, argumentName);
        }

        public static void ThrowIfNegative(this int argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            Negative(argument, argumentName);
        }

        public static void ThrowIfNegative(this long argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            Negative(argument, argumentName);
        }

        public static void ThrowIfEmptyGuid(this Guid argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        {
            EmptyGuid(argument, argumentName);
        }

        public static void ThrowIfNotEqual(this int expected, int actual, [CallerArgumentExpression("expected")] string? expectedName = null, [CallerArgumentExpression("actual")] string? actualName = null)
        {
            NotEqual(expected, actual, expectedName, actualName);
        }

        public static void ThrowIfNotEqual(this long expected, long actual, [CallerArgumentExpression("expected")] string? expectedName = null, [CallerArgumentExpression("actual")] string? actualName = null)
        {
            NotEqual(expected, actual, expectedName, actualName);
        }

        public static void ThrowIfLesserThan(this int value, int minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            LesserThan(value, minimumNeeded, valueName, minimumNeededName);
        }

        public static void ThrowIfLesserThan(this long value, long minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            LesserThan(value, minimumNeeded, valueName, minimumNeededName);
        }

        public static void ThrowIfGreatorThan(this int value, int maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            GreatorThan(value, maximumAllowed, valueName, maximumAllowedName);
        }

        public static void ThrowIfGreatorThan(this long value, long maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            GreatorThan(value, maximumAllowed, valueName, maximumAllowedName);
        }

        public static void ThrowIfLessThanEqualTo(this int value, int minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            LessThanEqualTo(value, minimumNeeded, valueName, minimumNeededName);
        }

        public static void ThrowIfLessThanEqualTo(this long value, long minimumNeeded, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("minimumNeeded")] string? minimumNeededName = null)
        {
            LessThanEqualTo(value, minimumNeeded, valueName, minimumNeededName);
        }

        public static void ThrowIfGreatorThanEqualTo(this int value, int maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            GreatorThanEqualTo(value, maximumAllowed, valueName, maximumAllowedName);
        }

        public static void ThrowIfGreatorThanEqualTo(this long value, long maximumAllowed, [CallerArgumentExpression("value")] string? valueName = null, [CallerArgumentExpression("maximumAllowed")] string? maximumAllowedName = null)
        {
            GreatorThanEqualTo(value, maximumAllowed, valueName, maximumAllowedName);
        }

        #endregion

    }
}
