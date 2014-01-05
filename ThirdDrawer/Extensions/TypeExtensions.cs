using System;

namespace ThirdDrawer.Extensions
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
    }
}