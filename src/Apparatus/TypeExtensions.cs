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
        /// Returns "Type" from given name.
        /// </summary>
        public static Type ToType(this string typeName)
        {
            return Type.GetType(typeName);
        }

        /// <summary>
        /// Returns complete type name in "<Namespace>.<Class>, <Assembly-Name>" format.
        /// </summary>
        public static string FullNameWithAssembly([NotNull] this Type type)
        {
            return $"{type.FullName}, {type.GetTypeInfo().Assembly.GetName().Name}";
        }

        /// <summary>
        /// Check if given type does support cast to T.
        /// This is useful to check if given type implement or inherits certain type.
        /// </summary>
        public static bool CanBeCastTo<T>(this Type type)
        {
            if (type == null) return false;
            var destinationType = typeof(T);

            return CanBeCastTo(type, destinationType);
        }

        /// <summary>
        /// Check if given type does support cast to T.
        /// This is useful to check if given type implement or inherits certain type.
        /// </summary>
        public static bool CanBeCastTo(this Type type, Type destinationType)
        {
            if (type == null) return false;
            if (type == destinationType) return true;

            return destinationType.GetTypeInfo().IsAssignableFrom(type);
        }

        public static bool In(this Type t, params Type[] types)
        {
            return types.Any(x => x == t);
        }

        public static bool IsEnumerable(this Type type)
        {
            return (type.GetInterfaces().Any(x => x == typeof(IEnumerable)) && type.Name != "String");
        }

        public static bool IsAssignableTo<TTarget>([NotNull] this Type type)
        {
            type.ThrowIfNull();

            return type.IsAssignableTo(typeof(TTarget));
        }

        public static bool IsAssignableTo([NotNull] this Type type, [NotNull] Type targetType)
        {
            type.ThrowIfNull();
            targetType.ThrowIfNull();

            return targetType.IsAssignableFrom(type);
        }

    }
}
