using System;
using System.Linq;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

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

        public static bool IsClosedTypeOf(this Type type, Type openGenericType)
        {
            if (!openGenericType.IsGenericType) throw new ArgumentException("It's a bit difficult to have a closed type of a non-open-generic type", "openGenericType");

            var interfaces = type.GetInterfaces();
            var baseTypes = new[] {type}.DepthFirst(t => t.BaseType == null ? new Type[0] : new[] {t.BaseType});
            var typeAndAllThatThatEntails = new[] {type}.Union(interfaces).Union(baseTypes).ToArray();
            var genericTypes = typeAndAllThatThatEntails.Where(i => i.IsGenericType);
            var closedGenericTypes = genericTypes.Where(i => !i.IsGenericTypeDefinition);
            var assignableGenericTypes = closedGenericTypes.Where(i => openGenericType.IsAssignableFrom(i.GetGenericTypeDefinition()));

            return assignableGenericTypes.Any();
        }

        public static bool IsClosedTypeOf(this Type type, params Type[] openGenericTypes)
        {
            return openGenericTypes.Any(type.IsClosedTypeOf);
        }

        public static Type[] GetGenericInterfacesClosing(this Type type, Type genericInterface)
        {
            var genericInterfaces = type.GetInterfaces()
                .Where(i => i.IsClosedTypeOf(genericInterface))
                .ToArray();
            return genericInterfaces;
        }
    }
}