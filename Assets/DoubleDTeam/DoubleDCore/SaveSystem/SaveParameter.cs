using System;
using DoubleDCore.Extensions;
using UnityEngine;

namespace DoubleDCore.SaveSystem
{
    [Serializable]
    public class SaveParameter : ISerializationCallbackReceiver
    {
        [SerializeField] private string _key;
        [SerializeField] private string _data;
        [SerializeField] private string _hash;

        public bool IsValidParameter { get; private set; }

        public string Key => _key;
        public string Data => _data;
        public string Hash => _hash;

        public void UpdateParameter(string data)
        {
            _data = data;
        }

        public SaveParameter(string key, string data)
        {
            IsValidParameter = true;

            _key = key;

            UpdateParameter(data);
        }

        public void OnBeforeSerialize()
        {
            RecalculateHash();
        }

        public void OnAfterDeserialize()
        {
            IsValidParameter = _hash == GetHash(Data);
        }

        private void RecalculateHash()
        {
            _hash = GetHash(Data);
        }

        public static string GetHash(string data)
        {
            return data.GetCRC32();
        }
    }
}