// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="Toshal Infotech">
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

namespace Apparatus
{
    using System.Collections;
    using System.Reflection;

    /// <summary>
    /// Extensions for Type class (object.GetType()).
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Finds a <see cref="Type"/> from its name. See <see cref="Type.GetType(string)"/> for the name rules.
        /// </summary>
        /// <param name="typeName">Type name, for example <c>"System.String"</c> or an assembly qualified name.</param>
        /// <returns>The type, or <c>null</c> if it cannot be found.</returns>
        /// <example>
        /// <code>
        /// Type t = "System.Guid".ToType(); // typeof(Guid)
        /// </code>
        /// </example>
        public static Type ToType(this string typeName)
        {
            return Type.GetType(typeName);
        }

        /// <summary>
        /// Gets the type name with the assembly name, in <c>Namespace.Class, AssemblyName</c> format.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>For example <c>"System.String, System.Private.CoreLib"</c>.</returns>
        /// <example>
        /// <code>
        /// string name = typeof(TypeExtensions).FullNameWithAssembly(); // "Apparatus.TypeExtensions, Apparatus"
        /// </code>
        /// </example>
        public static string FullNameWithAssembly([NotNull] this Type type)
        {
            return $"{type.FullName}, {type.GetTypeInfo().Assembly.GetName().Name}";
        }

        /// <summary>
        /// Checks whether this type can be cast to <typeparamref name="T"/>, which means it is the same type,
        /// inherits from it, or implements it.
        /// </summary>
        /// <typeparam name="T">The target type.</typeparam>
        /// <param name="type">The type to check. A <c>null</c> value returns <c>false</c>.</param>
        /// <returns><c>true</c> if the cast is possible; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// typeof(List&lt;int&gt;).CanBeCastTo&lt;IEnumerable&lt;int&gt;&gt;(); // true
        /// </code>
        /// </example>
        public static bool CanBeCastTo<T>(this Type type)
        {
            if (type == null) return false;
            var destinationType = typeof(T);

            return CanBeCastTo(type, destinationType);
        }

        /// <summary>
        /// Checks whether this type can be cast to <paramref name="destinationType"/>, which means it is the same type,
        /// inherits from it, or implements it.
        /// </summary>
        /// <param name="type">The type to check. A <c>null</c> value returns <c>false</c>.</param>
        /// <param name="destinationType">The target type.</param>
        /// <returns><c>true</c> if the cast is possible; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// typeof(string).CanBeCastTo(typeof(object)); // true
        /// typeof(string).CanBeCastTo(typeof(int));    // false
        /// </code>
        /// </example>
        public static bool CanBeCastTo(this Type type, Type destinationType)
        {
            if (type == null) return false;
            if (type == destinationType) return true;

            return destinationType.GetTypeInfo().IsAssignableFrom(type);
        }

        /// <summary>
        /// Checks whether this type is exactly one of the given types.
        /// </summary>
        /// <param name="t">The type to check.</param>
        /// <param name="types">The types to compare against.</param>
        /// <returns><c>true</c> if <paramref name="t"/> equals any type in <paramref name="types"/>; otherwise <c>false</c>.</returns>
        /// <example>
        /// <code>
        /// typeof(int).In(typeof(int), typeof(long)); // true
        /// </code>
        /// </example>
        public static bool In(this Type t, params Type[] types)
        {
            return types.Any(x => x == t);
        }

        /// <summary>
        /// Checks whether a type is a collection that can be looped with <c>foreach</c> (implements <see cref="System.Collections.IEnumerable"/>).
        /// <see cref="string"/> is not treated as a collection.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns><c>true</c> for lists, arrays, dictionaries and similar; <c>false</c> for <see cref="string"/> and normal types.</returns>
        /// <example>
        /// <code>
        /// typeof(List&lt;int&gt;).IsEnumerable(); // true
        /// typeof(string).IsEnumerable();      // false
        /// </code>
        /// </example>
        public static bool IsEnumerable(this Type type)
        {
            return (type.GetInterfaces().Any(x => x == typeof(IEnumerable)) && type.Name != "String");
        }

        /// <summary>
        /// Checks whether a value of this type can be assigned to a variable of type <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <param name="type">The source type. Must not be <c>null</c>.</param>
        /// <returns><c>true</c> if assignable; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <c>null</c>.</exception>
        /// <example>
        /// <code>
        /// typeof(string).IsAssignableTo&lt;object&gt;(); // true
        /// </code>
        /// </example>
        public static bool IsAssignableTo<TTarget>([NotNull] this Type type)
        {
            type.ThrowIfNull();

            return type.IsAssignableTo(typeof(TTarget));
        }

        /// <summary>
        /// Checks whether a value of this type can be assigned to a variable of type <paramref name="targetType"/>.
        /// </summary>
        /// <param name="type">The source type. Must not be <c>null</c>.</param>
        /// <param name="targetType">The target type. Must not be <c>null</c>.</param>
        /// <returns><c>true</c> if assignable; otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> or <paramref name="targetType"/> is <c>null</c>.</exception>
        /// <remarks>
        /// .NET 5 and later already have <c>Type.IsAssignableTo(Type)</c>, and the compiler picks it over this extension
        /// when you write <c>type.IsAssignableTo(target)</c>. To use this version, call it as <c>TypeExtensions.IsAssignableTo(type, target)</c>.
        /// </remarks>
        /// <example>
        /// <code>
        /// TypeExtensions.IsAssignableTo(typeof(int), typeof(object)); // true
        /// </code>
        /// </example>
        public static bool IsAssignableTo([NotNull] this Type type, [NotNull] Type targetType)
        {
            type.ThrowIfNull();
            targetType.ThrowIfNull();

            return targetType.IsAssignableFrom(type);
        }

    }
}
