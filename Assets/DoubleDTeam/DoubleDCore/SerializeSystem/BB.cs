using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace DoubleDCore.SerializeSystem
{
    public class ConsoleMethodInfo
    {
        public readonly MethodInfo method;
        public readonly Type[] parameterTypes;
        public readonly object instance;

        public readonly string command;
        public readonly string signature;
        public readonly string[] parameters;

        public ConsoleMethodInfo(MethodInfo method, Type[] parameterTypes, object instance, string command,
            string signature, string[] parameters)
        {
            this.method = method;
            this.parameterTypes = parameterTypes;
            this.instance = instance;
            this.command = command;
            this.signature = signature;
            this.parameters = parameters;
        }

        public bool IsValid()
        {
            if (!method.IsStatic && (instance == null || instance.Equals(null)))
                return false;

            return true;
        }
    }

    public static class DebugLogConsole
    {
        public delegate bool ParseFunction(string input, out object output);

        public delegate void CommandExecutedDelegate(string command, object[] parameters);

        public static event CommandExecutedDelegate OnCommandExecuted;

        // All the commands
        private static readonly List<ConsoleMethodInfo> methods = new List<ConsoleMethodInfo>();
        private static readonly List<ConsoleMethodInfo> matchingMethods = new List<ConsoleMethodInfo>(4);

        // All the parse functions
        private static readonly Dictionary<Type, ParseFunction> parseFunctions = new Dictionary<Type, ParseFunction>()
        {
            { typeof(string), ParseString },
            { typeof(bool), ParseBool },
            { typeof(int), ParseInt },
            { typeof(uint), ParseUInt },
            { typeof(long), ParseLong },
            { typeof(ulong), ParseULong },
            { typeof(byte), ParseByte },
            { typeof(sbyte), ParseSByte },
            { typeof(short), ParseShort },
            { typeof(ushort), ParseUShort },
            { typeof(char), ParseChar },
            { typeof(float), ParseFloat },
            { typeof(double), ParseDouble },
            { typeof(decimal), ParseDecimal },
            { typeof(Vector2), ParseVector2 },
            { typeof(Vector3), ParseVector3 },
            { typeof(Vector4), ParseVector4 },
            { typeof(Quaternion), ParseQuaternion },
            { typeof(Color), ParseColor },
            { typeof(Color32), ParseColor32 },
            { typeof(Rect), ParseRect },
            { typeof(RectOffset), ParseRectOffset },
            { typeof(Bounds), ParseBounds },
            { typeof(GameObject), ParseGameObject },
            { typeof(Vector2Int), ParseVector2Int },
            { typeof(Vector3Int), ParseVector3Int },
            { typeof(RectInt), ParseRectInt },
            { typeof(BoundsInt), ParseBoundsInt },
        };

        // All the readable names of accepted types
        private static readonly Dictionary<Type, string> typeReadableNames = new Dictionary<Type, string>()
        {
            { typeof(string), "String" },
            { typeof(bool), "Boolean" },
            { typeof(int), "Integer" },
            { typeof(uint), "Unsigned Integer" },
            { typeof(long), "Long" },
            { typeof(ulong), "Unsigned Long" },
            { typeof(byte), "Byte" },
            { typeof(sbyte), "Short Byte" },
            { typeof(short), "Short" },
            { typeof(ushort), "Unsigned Short" },
            { typeof(char), "Char" },
            { typeof(float), "Float" },
            { typeof(double), "Double" },
            { typeof(decimal), "Decimal" }
        };

        public static bool ParseString(string input, out object output)
        {
            output = input;
            return true;
        }

        public static bool ParseBool(string input, out object output)
        {
            if (input == "1" || input.ToLowerInvariant() == "true")
            {
                output = true;
                return true;
            }

            if (input == "0" || input.ToLowerInvariant() == "false")
            {
                output = false;
                return true;
            }

            output = false;
            return false;
        }

        public static bool ParseInt(string input, out object output)
        {
            int value;
            bool result = int.TryParse(input, out value);

            output = value;
            return result;
        }

        public static bool ParseUInt(string input, out object output)
        {
            uint value;
            bool result = uint.TryParse(input, out value);

            output = value;
            return result;
        }

        public static bool ParseLong(string input, out object output)
        {
            long value;
            bool result =
                long.TryParse(
                    !input.EndsWith("L", StringComparison.OrdinalIgnoreCase)
                        ? input
                        : input.Substring(0, input.Length - 1), out value);

            output = value;
            return result;
        }

        public static bool ParseULong(string input, out object output)
        {
            ulong value;
            bool result =
                ulong.TryParse(
                    !input.EndsWith("L", StringComparison.OrdinalIgnoreCase)
                        ? input
                        : input.Substring(0, input.Length - 1), out value);

            output = value;
            return result;
        }

        public static bool ParseByte(string input, out object output)
        {
            byte value;
            bool result = byte.TryParse(input, out value);

            output = value;
            return result;
        }

        public static bool ParseSByte(string input, out object output)
        {
            sbyte value;
            bool result = sbyte.TryParse(input, out value);

            output = value;
            return result;
        }

        public static bool ParseShort(string input, out object output)
        {
            short value;
            bool result = short.TryParse(input, out value);

            output = value;
            return result;
        }

        public static bool ParseUShort(string input, out object output)
        {
            ushort value;
            bool result = ushort.TryParse(input, out value);

            output = value;
            return result;
        }

        public static bool ParseChar(string input, out object output)
        {
            char value;
            bool result = char.TryParse(input, out value);

            output = value;
            return result;
        }

        public static bool ParseFloat(string input, out object output)
        {
            float value;
            bool result =
                float.TryParse(
                    !input.EndsWith("f", StringComparison.OrdinalIgnoreCase)
                        ? input
                        : input.Substring(0, input.Length - 1), out value);

            output = value;
            return result;
        }

        public static bool ParseDouble(string input, out object output)
        {
            double value;
            bool result =
                double.TryParse(
                    !input.EndsWith("f", StringComparison.OrdinalIgnoreCase)
                        ? input
                        : input.Substring(0, input.Length - 1), out value);

            output = value;
            return result;
        }

        public static bool ParseDecimal(string input, out object output)
        {
            decimal value;
            bool result =
                decimal.TryParse(
                    !input.EndsWith("f", StringComparison.OrdinalIgnoreCase)
                        ? input
                        : input.Substring(0, input.Length - 1), out value);

            output = value;
            return result;
        }

        public static bool ParseVector2(string input, out object output)
        {
            return ParseVector(input, typeof(Vector2), out output);
        }

        public static bool ParseVector3(string input, out object output)
        {
            return ParseVector(input, typeof(Vector3), out output);
        }

        public static bool ParseVector4(string input, out object output)
        {
            return ParseVector(input, typeof(Vector4), out output);
        }

        public static bool ParseQuaternion(string input, out object output)
        {
            return ParseVector(input, typeof(Quaternion), out output);
        }

        public static bool ParseColor(string input, out object output)
        {
            return ParseVector(input, typeof(Color), out output);
        }

        public static bool ParseColor32(string input, out object output)
        {
            return ParseVector(input, typeof(Color32), out output);
        }

        public static bool ParseRect(string input, out object output)
        {
            return ParseVector(input, typeof(Rect), out output);
        }

        public static bool ParseRectOffset(string input, out object output)
        {
            return ParseVector(input, typeof(RectOffset), out output);
        }

        public static bool ParseBounds(string input, out object output)
        {
            return ParseVector(input, typeof(Bounds), out output);
        }

        public static bool ParseVector2Int(string input, out object output)
        {
            return ParseVector(input, typeof(Vector2Int), out output);
        }

        public static bool ParseVector3Int(string input, out object output)
        {
            return ParseVector(input, typeof(Vector3Int), out output);
        }

        public static bool ParseRectInt(string input, out object output)
        {
            return ParseVector(input, typeof(RectInt), out output);
        }

        public static bool ParseBoundsInt(string input, out object output)
        {
            return ParseVector(input, typeof(BoundsInt), out output);
        }

        public static bool ParseGameObject(string input, out object output)
        {
            output = input == "null" ? null : GameObject.Find(input);
            return true;
        }

        public static bool ParseComponent(string input, Type componentType, out object output)
        {
            GameObject gameObject = input == "null" ? null : GameObject.Find(input);
            output = gameObject ? gameObject.GetComponent(componentType) : null;
            return true;
        }

        public static bool ParseEnum(string input, Type enumType, out object output)
        {
            const int NONE = 0, OR = 1, AND = 2;

            int outputInt = 0;
            int operation = NONE; // 0: nothing, 1: OR with outputInt, 2: AND with outputInt
            for (int i = 0; i < input.Length; i++)
            {
                string enumStr;
                int orIndex = input.IndexOf('|', i);
                int andIndex = input.IndexOf('&', i);
                if (orIndex < 0)
                    enumStr = input.Substring(i, (andIndex < 0 ? input.Length : andIndex) - i).Trim();
                else
                    enumStr = input.Substring(i, (andIndex < 0 ? orIndex : Mathf.Min(andIndex, orIndex)) - i).Trim();

                int value;
                if (!int.TryParse(enumStr, out value))
                {
                    try
                    {
                        // Case-insensitive enum parsing
                        value = Convert.ToInt32(Enum.Parse(enumType, enumStr, true));
                    }
                    catch
                    {
                        output = null;
                        return false;
                    }
                }

                if (operation == NONE)
                    outputInt = value;
                else if (operation == OR)
                    outputInt |= value;
                else
                    outputInt &= value;

                if (orIndex >= 0)
                {
                    if (andIndex > orIndex)
                    {
                        operation = AND;
                        i = andIndex;
                    }
                    else
                    {
                        operation = OR;
                        i = orIndex;
                    }
                }
                else if (andIndex >= 0)
                {
                    operation = AND;
                    i = andIndex;
                }
                else
                    i = input.Length;
            }

            output = Enum.ToObject(enumType, outputInt);
            return true;
        }

        private static bool ParseVector(string input, Type vectorType, out object output)
        {
            List<string> tokens = new List<string>(input.Replace(',', ' ').Trim().Split(' '));
            for (int i = tokens.Count - 1; i >= 0; i--)
            {
                tokens[i] = tokens[i].Trim();
                if (tokens[i].Length == 0)
                    tokens.RemoveAt(i);
            }

            float[] tokenValues = new float[tokens.Count];
            for (int i = 0; i < tokens.Count; i++)
            {
                object val;
                if (!ParseFloat(tokens[i], out val))
                {
                    if (vectorType == typeof(Vector3))
                        output = Vector3.zero;
                    else if (vectorType == typeof(Vector2))
                        output = Vector2.zero;
                    else
                        output = Vector4.zero;

                    return false;
                }

                tokenValues[i] = (float)val;
            }

            if (vectorType == typeof(Vector3))
            {
                Vector3 result = Vector3.zero;

                for (int i = 0; i < tokenValues.Length && i < 3; i++)
                    result[i] = tokenValues[i];

                output = result;
            }
            else if (vectorType == typeof(Vector2))
            {
                Vector2 result = Vector2.zero;

                for (int i = 0; i < tokenValues.Length && i < 2; i++)
                    result[i] = tokenValues[i];

                output = result;
            }
            else if (vectorType == typeof(Vector4))
            {
                Vector4 result = Vector4.zero;

                for (int i = 0; i < tokenValues.Length && i < 4; i++)
                    result[i] = tokenValues[i];

                output = result;
            }
            else if (vectorType == typeof(Quaternion))
            {
                Quaternion result = Quaternion.identity;

                for (int i = 0; i < tokenValues.Length && i < 4; i++)
                    result[i] = tokenValues[i];

                output = result;
            }
            else if (vectorType == typeof(Color))
            {
                Color result = Color.black;

                for (int i = 0; i < tokenValues.Length && i < 4; i++)
                    result[i] = tokenValues[i];

                output = result;
            }
            else if (vectorType == typeof(Color32))
            {
                Color32 result = new Color32(0, 0, 0, 255);

                if (tokenValues.Length > 0)
                    result.r = (byte)Mathf.RoundToInt(tokenValues[0]);
                if (tokenValues.Length > 1)
                    result.g = (byte)Mathf.RoundToInt(tokenValues[1]);
                if (tokenValues.Length > 2)
                    result.b = (byte)Mathf.RoundToInt(tokenValues[2]);
                if (tokenValues.Length > 3)
                    result.a = (byte)Mathf.RoundToInt(tokenValues[3]);

                output = result;
            }
            else if (vectorType == typeof(Rect))
            {
                Rect result = Rect.zero;

                if (tokenValues.Length > 0)
                    result.x = tokenValues[0];
                if (tokenValues.Length > 1)
                    result.y = tokenValues[1];
                if (tokenValues.Length > 2)
                    result.width = tokenValues[2];
                if (tokenValues.Length > 3)
                    result.height = tokenValues[3];

                output = result;
            }
            else if (vectorType == typeof(RectOffset))
            {
                RectOffset result = new RectOffset();

                if (tokenValues.Length > 0)
                    result.left = Mathf.RoundToInt(tokenValues[0]);
                if (tokenValues.Length > 1)
                    result.right = Mathf.RoundToInt(tokenValues[1]);
                if (tokenValues.Length > 2)
                    result.top = Mathf.RoundToInt(tokenValues[2]);
                if (tokenValues.Length > 3)
                    result.bottom = Mathf.RoundToInt(tokenValues[3]);

                output = result;
            }
            else if (vectorType == typeof(Bounds))
            {
                Vector3 center = Vector3.zero;
                for (int i = 0; i < tokenValues.Length && i < 3; i++)
                    center[i] = tokenValues[i];

                Vector3 size = Vector3.zero;
                for (int i = 3; i < tokenValues.Length && i < 6; i++)
                    size[i - 3] = tokenValues[i];

                output = new Bounds(center, size);
            }
            else if (vectorType == typeof(Vector3Int))
            {
                Vector3Int result = Vector3Int.zero;

                for (int i = 0; i < tokenValues.Length && i < 3; i++)
                    result[i] = Mathf.RoundToInt(tokenValues[i]);

                output = result;
            }
            else if (vectorType == typeof(Vector2Int))
            {
                Vector2Int result = Vector2Int.zero;

                for (int i = 0; i < tokenValues.Length && i < 2; i++)
                    result[i] = Mathf.RoundToInt(tokenValues[i]);

                output = result;
            }
            else if (vectorType == typeof(RectInt))
            {
                RectInt result = new RectInt();

                if (tokenValues.Length > 0)
                    result.x = Mathf.RoundToInt(tokenValues[0]);
                if (tokenValues.Length > 1)
                    result.y = Mathf.RoundToInt(tokenValues[1]);
                if (tokenValues.Length > 2)
                    result.width = Mathf.RoundToInt(tokenValues[2]);
                if (tokenValues.Length > 3)
                    result.height = Mathf.RoundToInt(tokenValues[3]);

                output = result;
            }
            else if (vectorType == typeof(BoundsInt))
            {
                Vector3Int center = Vector3Int.zero;
                for (int i = 0; i < tokenValues.Length && i < 3; i++)
                    center[i] = Mathf.RoundToInt(tokenValues[i]);

                Vector3Int size = Vector3Int.zero;
                for (int i = 3; i < tokenValues.Length && i < 6; i++)
                    size[i - 3] = Mathf.RoundToInt(tokenValues[i]);

                output = new BoundsInt(center, size);
            }
            else
            {
                output = null;
                return false;
            }

            return true;
        }
    }
}