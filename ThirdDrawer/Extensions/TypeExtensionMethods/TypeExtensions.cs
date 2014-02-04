using System;

namespace ThirdDrawer.Extensions.TypeExtensionMethods
{
    public static class TypeExtensions
    {
        public static bool IsInstantiable(this Type type)
        {
            if (type.IsInterface) return false;
            if (type.IsAbstract) return false;
            if (type.ContainsGenericParameters) return false;
            return true;
        }

        public static bool IsAssignableTo<TTarget>(this Type type)
        {
            return typeof (TTarget).IsAssignableFrom(type);
        }

        /// <summary>
        ///     Alias for IsAssignableTo.
        /// </summary>
        /// <remarks>
        ///     This alias is to avoid collisions with Autofac's extension method of the same name.
        /// </remarks>
        public static bool CanBeAssignedTo<TTarget>(this Type type)
        {
            return IsAssignableTo<TTarget>(type);
        }
    }
}