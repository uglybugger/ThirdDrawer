using System;
using System.Linq;
using System.Reflection;
using ThirdDrawer.Extensions.CollectionExtensionMethods;

namespace ThirdDrawer.Extensions.TypeExtensionMethods
{
    public static class TypeExtensions
    {
        public static bool IsInstantiable(this Type type)
        {
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsInterface) return false;
            if (typeInfo.IsAbstract) return false;
            if (typeInfo.ContainsGenericParameters) return false;
            return true;
        }

        public static bool IsAssignableTo<TTarget>(this Type type)
        {
            return typeof (TTarget).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo());
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
            if (!openGenericType.GetTypeInfo().IsGenericType) throw new ArgumentException("It's a bit difficult to have a closed type of a non-open-generic type", nameof(openGenericType));

            var interfaces = type.GetTypeInfo().ImplementedInterfaces;
            var baseTypes = new[] {type}.DepthFirst(t => t.GetTypeInfo().BaseType == null ? new Type[0] : new[] {t.GetTypeInfo().BaseType});
            var typeAndAllThatThatEntails = new[] {type}.Union(interfaces).Union(baseTypes).ToArray();
            var genericTypes = typeAndAllThatThatEntails.Where(i => i.GetTypeInfo().IsGenericType);
            var closedGenericTypes = genericTypes.Where(i => !i.GetTypeInfo().IsGenericTypeDefinition);
            var assignableGenericTypes = closedGenericTypes.Where(i => openGenericType.GetTypeInfo().IsAssignableFrom(i.GetGenericTypeDefinition().GetTypeInfo()));

            return assignableGenericTypes.Any();
        }

        public static bool IsClosedTypeOf(this Type type, params Type[] openGenericTypes)
        {
            return openGenericTypes.Any(type.IsClosedTypeOf);
        }

        public static Type[] GetGenericInterfacesClosing(this Type type, Type genericInterface)
        {
            var genericInterfaces = type.GetTypeInfo().ImplementedInterfaces
                .Where(i => i.IsClosedTypeOf(genericInterface))
                .ToArray();
            return genericInterfaces;
        }
    }
}