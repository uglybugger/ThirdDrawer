using System;
using System.Collections.Generic;
using System.Linq;

namespace ThirdDrawer.Extensions.CollectionExtensionMethods
{
    public static class CollectionExtensions
    {
        public static bool None<T>(this IEnumerable<T> items)
        {
            return !items.Any();
        }

        public static bool None<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            return !items.Any(predicate);
        }

        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> items) where T : class
        {
            return items.Where(item => item != null);
        }

        public static IEnumerable<T> Do<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
                yield return item;
            }
        }

        public static void Done<T>(this IEnumerable<T> items)
        {
            var enumerator = items.GetEnumerator();
            while (enumerator.MoveNext())
            {
                // just force enumeration; that's all.
            }
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items) collection.Add(item);
        }

        public static IEnumerable<T> DepthFirst<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> children)
        {
            foreach (var item in source)
            {
                yield return item;
                foreach (var descendant in children(item).DepthFirst<T>(children)) yield return descendant;
            }
        }
    }
}