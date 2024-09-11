using DoubleDCore.SerializeSystem.Base;

namespace DoubleDCore.SerializeSystem
{
    public static class Serializer
    {
        public static ISerialize<string> String = new StringSerializer();
        public static ISerialize<bool> Boolean = new BoolSerializer();
    }
}