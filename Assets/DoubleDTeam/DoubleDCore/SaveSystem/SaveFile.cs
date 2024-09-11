using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DoubleDCore.Extensions;
using UnityEngine;

namespace DoubleDCore.SaveSystem
{
    [Serializable]
    public class SaveFile : ISerializationCallbackReceiver
    {
        [SerializeField] private string _createDate;
        [SerializeField] private SaveParameter[] _data;
        [SerializeField] private string _hash;

        private readonly Dictionary<string, SaveParameter> _dataDictionary = new();

        public bool IsValidFile { get; private set; }

        public string CreateDate => _createDate;

        public IEnumerable<SaveParameter> Date => _dataDictionary.Values;

        public SaveFile()
        {
            IsValidFile = true;
            _createDate = GetDate();
        }

        public void AddParameter(SaveParameter parameter)
        {
            _dataDictionary.TryAdd(parameter.Key, parameter);
        }

        public void UpdateParameter(string key, string data)
        {
            if (TryGetParameter(key, out var saveParameter) == false)
                return;

            saveParameter.UpdateParameter(data);
        }

        public void RemoveParameter(string key)
        {
            _dataDictionary.Remove(key);
        }

        public bool ContainsParameter(string key)
        {
            return _dataDictionary.ContainsKey(key);
        }

        public bool TryGetParameter(string key, out SaveParameter saveParameter)
        {
            saveParameter = null;

            if (ContainsParameter(key) == false)
                return false;

            saveParameter = _dataDictionary[key];

            return true;
        }

        public string Serialize()
        {
            return JsonUtility.ToJson(this);
        }

        public static SaveFile Deserialize(string saveFileData)
            => JsonUtility.FromJson<SaveFile>(saveFileData);

        private string GetDate()
            => DateTime.Now.ToString("g", DateTimeFormatInfo.InvariantInfo);

        public void OnBeforeSerialize()
        {
            _data = _dataDictionary.Values.ToArray();

            RecalculateHash(_data);
        }

        public void OnAfterDeserialize()
        {
            foreach (var saveParameter in _data)
                AddParameter(saveParameter);

            IsValidFile = _hash == GetHash(_data);
        }

        private string GetHash(IEnumerable<SaveParameter> data)
        {
            var concatData = string.Concat(data.Select(d => d.Data));

            return concatData.GetCRC32();
        }

        public void RecalculateHash(IEnumerable<SaveParameter> data)
        {
            _hash = GetHash(data);
        }
    }
}