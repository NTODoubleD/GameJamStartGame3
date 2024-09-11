using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DoubleDCore.Cryptography;
using UnityEngine;

namespace DoubleDCore.Extensions
{
    public static class StringExtensions
    {
        private static readonly SHA256 SHA256Hasher = SHA256.Create();
        private static readonly CRC32 CRC32Hasher = new();

        public static string GetSHA265(this string input)
        {
            var bytes = SHA256Hasher.ComputeHash(Encoding.ASCII.GetBytes(input));
            return string.Concat(bytes.Select(x => ((int)x).ToString("x2")));
        }

        public static string GetCRC32(this string input)
        {
            var bytes = CRC32Hasher.ComputeHash(Encoding.ASCII.GetBytes(input));
            return string.Concat(bytes.Select(x => ((int)x).ToString("x2")));
        }

        public static string Bold(this string text) => "<b>" + text + "</b>";

        public static string Color(this string text, Color color) =>
            $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{text}</color>";

        public static string Italic(this string text) => "<i>" + text + "</i>";

        public static string Size(this string text, int size) => $"<size={size}>{text}</size>";

        public static string Size(this string text, float percent) => $"<size={percent}%>{text}</size>";

        //TODO: ПРОТЕСТИТЬ!!!
        public static string ReplaceTimes(this string text, string oldString, string newString,
            int count, bool isReverse = false)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (string.IsNullOrEmpty(oldString))
                throw new ArgumentException("Old string cannot be null or empty", nameof(oldString));

            StringBuilder stringBuilder = new StringBuilder(text);
            int position = isReverse ? text.Length : 0;

            for (int i = 0; i < count; i++)
            {
                position = isReverse
                    ? stringBuilder.ToString().LastIndexOf(oldString, position, StringComparison.Ordinal)
                    : stringBuilder.ToString().IndexOf(oldString, position, StringComparison.Ordinal);

                if (position < 0)
                    break;

                stringBuilder.Remove(position, oldString.Length);
                stringBuilder.Insert(position, newString);

                if (!isReverse)
                    position += newString.Length;
            }

            return stringBuilder.ToString();
        }
    }
}