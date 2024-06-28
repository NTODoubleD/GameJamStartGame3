using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace DoubleDTeam.Extensions
{
    public static class StringExtension
    {
        private static readonly SHA256 Hasher = SHA256.Create();

        public static string GetSHA265(this string input)
        {
            var bytes = Hasher.ComputeHash(Encoding.ASCII.GetBytes(input));
            return string.Concat(bytes.Select(x => ((int)x).ToString("x2")));
        }

        public static string Bold(this string line) => "<b>" + line + "</b>";

        public static string Color(this string line, Color color) =>
            $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{line}</color>";

        public static string Italic(this string line) => "<i>" + line + "</i>";

        public static string Size(this string line, int size) => $"<size={size}>{line}</size>";

        
        //TODO: ПРОТЕСТИТЬ!!!
        public static string ReplaceTimes(this string text, string oldString, string newString,
            int count, bool isReverse = false)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count));

            string result = text;

            StringBuilder stringBuilder = new StringBuilder();

            int position = isReverse ? text.Length : 0;

            for (int i = 0; i < count; i++)
            {
                position = isReverse
                    ? text.LastIndexOf(oldString, position, StringComparison.Ordinal)
                    : text.IndexOf(oldString, position, StringComparison.Ordinal);

                if (position < 0)
                    return result;

                stringBuilder.Append(text[..position]);
                stringBuilder.Append(newString);
                stringBuilder.Append(text[(position + oldString.Length)..]);

                result = stringBuilder.ToString();

                stringBuilder.Clear();
            }

            return result;
        }
    }
}