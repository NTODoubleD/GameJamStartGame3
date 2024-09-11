using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DoubleDCore.Extensions
{
    public static class ListExtensions
    {
        private static readonly int DefaultSeed = (int)DateTime.Now.Ticks & 0x0000FFFF;
        private static readonly Random DefaultRandom = new(DefaultSeed);

        public static void Shuffle<T>(this IList<T> list)
        {
            list.Shuffle(DefaultRandom.Next());
        }

        public static void Shuffle<T>(this IList<T> list, int seed)
        {
            Random random = new Random(seed);

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static T Choose<T>(this IList<T> list)
        {
            return list.Choose(DefaultRandom.Next());
        }

        public static T Choose<T>(this IList<T> list, int seed)
        {
            Random random = new Random(seed);

            if (list.Count <= 0)
                throw new InvalidDataException("List is empty");

            int randomInt = random.Next(0, list.Count);
            return list[randomInt];
        }

        public static IList<T> ChooseRange<T>(this IList<T> list, int countMembers)
        {
            return list.ChooseRange(countMembers, DefaultRandom.Next());
        }

        public static IList<T> ChooseRange<T>(this IList<T> list, int countMembers, int seed)
        {
            if (list.Count <= 0)
                return new List<T>();

            if (countMembers > list.Count)
                countMembers = list.Count;

            var result = new List<T>();
            var listCopy = new List<T>(list);

            for (int i = 0; i < countMembers; i++)
            {
                var choose = listCopy.Choose(seed);
                listCopy.Remove(choose);
                result.Add(choose);
            }

            return result;
        }

        public static IList<T> ChooseRange<T>(this IList<T> list, float percentage)
        {
            return list.ChooseRange(percentage, DefaultRandom.Next());
        }

        public static IList<T> ChooseRange<T>(this IList<T> list, float percentage, int seed)
        {
            percentage = Math.Clamp(percentage, 0f, 1f);
            return list.ChooseRange((int)Math.Floor(percentage * list.Count), seed);
        }

        public static bool Contains<T>(this IEnumerable<T> list, Predicate<T> condition) where T : class
        {
            return list.Any(element => condition(element));
        }

        public static void Remove<T>(this IList<T> list, Predicate<T> condition) where T : class
        {
            foreach (var element in list.ToArray())
            {
                if (condition(element))
                    list.Remove(element);
            }
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || list.Sum(_ => 1) <= 0;
        }

        public static T FirstFromEnd<T>(this IList<T> list, Predicate<T> condition)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (condition(list[i]))
                    return list[i];
            }

            return default;
        }
    }
}