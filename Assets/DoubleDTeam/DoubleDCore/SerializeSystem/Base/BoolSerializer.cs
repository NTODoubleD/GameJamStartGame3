using UnityEngine;

namespace DoubleDCore.SerializeSystem.Base
{
    public class BoolSerializer : ISerialize<bool>
    {
        public string Serialize(bool data)
        {
            return data ? "1" : "0";
        }

        public bool Deserialize(string data, out bool result)
        {
            result = false;

            if (data == "1" || data.ToLowerInvariant() == "true")
            {
                result = true;
                return true;
            }

            if (data == "0" || data.ToLowerInvariant() == "false")
            {
                result = false;
                return true;
            }

            Debug.LogWarning($"[{GetType().Name}][Deserialization Warning] input: {data}");
            return false;
        }
    }
}