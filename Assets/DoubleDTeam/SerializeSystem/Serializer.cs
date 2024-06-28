using DoubleDTeam.SerializeSystem.Base;

namespace DoubleDTeam.SerializeSystem
{
    public static class Serializer
    {
        public static ISerialize<string> String = new StringSerializer();
        public static ISerialize<bool> Boolean = new BoolSerializer();
    }
}