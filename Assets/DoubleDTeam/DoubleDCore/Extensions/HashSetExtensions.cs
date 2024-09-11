using System.Collections.Generic;

namespace DoubleDCore.Extensions
{
    public static class HashSetExtensions
    {
        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> array)
        {
            foreach (var element in array)
                hashSet.Add(element);
        }
    }
}