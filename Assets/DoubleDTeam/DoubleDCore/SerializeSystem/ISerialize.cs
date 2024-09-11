namespace DoubleDCore.SerializeSystem
{
    public interface ISerialize<TType>
    {
        public string Serialize(TType data);

        public bool Deserialize(string data, out TType result);
    }
}