using System.Collections.Generic;

namespace DoubleDTeam.Extensions
{
    public static class HashSetExtension
    {
        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> array)
        {
            foreach (var element in array)
                hashSet.Add(element);
        }
    }
}