namespace DoubleDCore.SerializeSystem.Base
{
    public class StringSerializer : ISerialize<string>
    {
        public string Serialize(string data)
        {
            return data;
        }

        public bool Deserialize(string data, out string result)
        {
            result = data;
            return true;
        }
    }
}