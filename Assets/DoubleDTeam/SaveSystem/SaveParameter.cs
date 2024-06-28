using System;

namespace DoubleDTeam.SaveSystem
{
    [Serializable]
    public class SaveParameter
    {
        public string Key;
        public string Data;

        public SaveParameter(string key, string data)
        {
            Key = key;
            Data = data;
        }
    }
}