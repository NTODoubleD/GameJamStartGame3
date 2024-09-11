using System;
using UnityEngine;

namespace DoubleDCore.Extensions
{
    /// <summary>
    /// Parse a numeric value into a short scale string representation.
    /// </summary>
    public static class ShortScaleNumbers
    {
        /*
         * Current Version : 1.2 (January 5, 2019)
         */

        /*
         * The ShortScaleString script parses either a double, float, or int value
         * value type into a form that is easier to read. The list supports values up to
         * the largest currently accepted value(one centillion). values given larger than
         * 999 centillion will be represented as "1000 centillion" and so on.
         * It is important to be aware of the limitations of each data type. for intParse, numbers higher than 1 billion
         * will throw an overflow error. similarly a float value will also overflow after too large a number is reached.
         * for applicatins where the needed use is well over 1+E100 use parseDouble as its maximun output value tested is
         * 1000 centillion, thats 306 0's!
         */

        /// <summary>
        /// The list containing all short scale values.
        /// </summary>
        public static readonly string[] ShortScaleReference =
        {
            "thousand", "million", "billion", "trillion", "quadrillion", "quintillion",
            "sextillion", "septillion", "octillion", "nonillion", "decillion",
            "undecillion", "duodecillion", "tredecillion", "quattuordecillion", "quindecillion",
            "sexdecillion", "septendecillion", "octodecillion", "novemdecillion", "vigintillion",
            "unvigintillion", "duovigintillion", "trevigintillion", "quattuorvigintillion",
            "quinvigitillion", "sexvigintillion", "septenvigitillion", "octovigintillion", "novemvigitillion",
            "trigintillion", "untrigintillion", "duotrigintillion", "tretrigintillion", "quattuortrigintillion",
            "quintrigintillion", "sextrigintillion", "septentrigintillion", "octotrigintillion",
            "novemtrigintillion", "quadragintillion", "unquadragintillion", "duoquadragintillion",
            "trequadragintillion", "quattuorquadragintillion", "quinquadragintillion", "sexquadragintillion",
            "septenquadragintillion", "octoquadragintillion", "novemquadragintillion", "quinquagintillion",
            "unquinquagintillion", "duoquinquagintillion", "trequinquagintillion", "quattuorquinquagintillion",
            "quinquinquagintillion", "sexquinquagintillion", "septenquinquagintillion", "octoquinquagintillion",
            "novemquinquagintillion", "sexagintillion", "unsexagintillion", "duosexagintillion", "tresexagintillion",
            "quattuorsexagintillion", "quinsexagintillion", "sexsexagintillion", "septensexagintillion",
            "octosexagintillion", "novemsexagintillion", "septuagintillion", "unseptuagintillion",
            "duoseptuagintillion", "treseptuagintillion", "quattuorseptuagintillion", "quinseptuagintillion",
            "sexseptuagintillion", "septenseptuagintillion", "octoseptuagintillion", "novemseptuagintillion",
            "octogintillion", "unoctogintillion", "duooctogintillion", "treoctogintillion", "quattuoroctogintillion",
            "quinoctogintillion", "sexoctogintillion", "septenoctogintillion", "octooctogintillion",
            "novemoctogintillion", "nonagintillion", "unnonagintillion", "duononagintillion", "trenonagintillion",
            "quattuornonagintillion", "quinnonagintillion", "sexnonagintillion", "septennonagintillion",
            "octononagintillion", "novemnonagintillion", "centillion"
        };

        ///<summary>
        /// List containing short scale values in symbol form. Can be further expanded in future updates.
        /// </summary>
        public static readonly string[] ShortScaleSymbolReference =
            { "K", "M", "B", "T", "q", "Q", "s", "S", "O", "N", "D" };

        /// <summary>
        /// Преобразует значение типа double в сокращенную шкалу.
        /// </summary>
        /// <returns>Строковое представление значения в сокращенной шкале.</returns>
        /// <param name="value">Входное значение, которое будет преобразовано.</param>
        /// <param name="precision">(Опционально) Количество десятичных знаков, которое должно быть представлено (с учетом округления по типу данных). По умолчанию 3.</param>
        /// <param name="startShortScale">(Опционально) Значение, с которого начнется преобразование в сокращенную шкалу. По умолчанию 1 миллион.</param>
        /// <param name="useSymbol">
        /// (Опционально) Использовать единичный символ для более короткого представления. В настоящее время поддерживается только до дециллиона.
        /// </param>
        public static string ParseDouble(double value, int precision = 3, double startShortScale = 1000000,
            bool useSymbol = false)
        {
            int index = -1;
            int isNegative;
            string addPrecision = new string('#', precision);
            double precisionValue = Mathf.Pow(10, precision);

            if (value < 0)
            {
                isNegative = -1;
                value *= isNegative;
            }
            else if (value > 0)
            {
                isNegative = 1;
            }
            else
            {
                return "0";
            }

            if (value < 1000d || value < startShortScale)
            {
                return (Math.Floor(value * isNegative * precisionValue) / precisionValue).ToString(
                    "#,#." + addPrecision);
            }

            while (value >= 1000d)
            {
                if (!useSymbol && index >= ShortScaleReference.Length - 1)
                {
                    break;
                }

                if (useSymbol && index >= ShortScaleSymbolReference.Length - 1)
                {
                    break;
                }

                value /= 1000d;
                index++;
            }

            return (Math.Floor(value * isNegative * precisionValue) / precisionValue).ToString("#,#." + addPrecision)
                   + " "
                   + (useSymbol ? ShortScaleSymbolReference[index] : ShortScaleReference[index]);
        }

        /// <summary>
        /// Преобразует значение типа float в сокращенную шкалу.
        /// </summary>
        /// <returns>Строковое представление значения в сокращенной шкале.</returns>
        /// <param name="value">Входное значение, которое будет преобразовано.</param>
        /// <param name="precision">(Опционально) Количество десятичных знаков, которое должно быть представлено (с учетом округления по типу данных). По умолчанию 3.</param>
        /// <param name="startShortScale">(Опционально) Значение, с которого начнется преобразование в сокращенную шкалу. По умолчанию 1 миллион.</param>
        /// <param name="useSymbol">
        /// (Опционально) Использовать единичный символ для более короткого представления. В настоящее время поддерживается только до дециллиона.
        /// </param>
        public static string ParseFloat(float value, int precision = 3, float startShortScale = 1000000,
            bool useSymbol = false)
        {
            int index = -1;
            int isNegative;
            string addPrecision = new string('#', precision);
            double precisionValue = Mathf.Pow(10, precision);

            if (value < 0)
            {
                isNegative = -1;
                value *= isNegative;
            }
            else if (value > 0)
            {
                isNegative = 1;
            }
            else
            {
                return "0";
            }

            if (value < 1000 || value < startShortScale)
            {
                return (Math.Floor(value * isNegative * precisionValue) / precisionValue).ToString(
                    "#,#." + addPrecision);
            }

            while (value >= 1000.0f)
            {
                if (!useSymbol && index >= ShortScaleReference.Length - 1)
                {
                    break;
                }

                if (useSymbol && index >= ShortScaleSymbolReference.Length - 1)
                {
                    break;
                }

                value /= 1000;
                index++;
            }

            return (Math.Floor(value * isNegative * precisionValue) / precisionValue).ToString("#,#." + addPrecision)
                   + " "
                   + (useSymbol ? ShortScaleSymbolReference[index] : ShortScaleReference[index]);
        }

        /// <summary>
        /// Преобразует значение типа int в сокращенную шкалу.
        /// </summary>
        /// <returns>Строковое представление значения в сокращенной шкале.</returns>
        /// <param name="value">Входное значение, которое будет преобразовано.</param>
        /// <param name="precision">(Опционально) Количество десятичных знаков, которое должно быть представлено (с учетом округления по типу данных). По умолчанию 3.</param>
        /// <param name="startShortScale">(Опционально) Значение, с которого начнется преобразование в сокращенную шкалу. По умолчанию 1 миллион.</param>
        /// <param name="useSymbol">
        /// (Опционально) Использовать единичный символ для более короткого представления. В настоящее время поддерживается только до дециллиона.
        /// </param>
        public static string ParseInt(int value, int precision = 3, int startShortScale = 1000000,
            bool useSymbol = false)
        {
            int index = -1;
            int isNegative;
            string addPrecision = new string('#', precision);

            if (value < 0)
            {
                isNegative = -1;
                value *= isNegative;
            }
            else if (value > 0)
            {
                isNegative = 1;
            }
            else
            {
                return "0";
            }

            if (value < 1000 || value < startShortScale)
            {
                return (value * isNegative).ToString("#,#");
            }

            while (value >= 1000)
            {
                if (!useSymbol && index == ShortScaleReference.Length - 1)
                {
                    break;
                }

                if (useSymbol && index >= ShortScaleSymbolReference.Length - 1)
                {
                    break;
                }

                value /= 1000;
                index++;
            }

            return (value * isNegative).ToString("#,#." + addPrecision)
                   + " "
                   + (useSymbol ? ShortScaleSymbolReference[index] : ShortScaleReference[index]);
        }
    }
}