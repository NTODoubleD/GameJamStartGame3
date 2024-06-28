using System;
using System.Collections.Generic;
using System.IO;
using DoubleDTeam.Extensions;
using DoubleDTeam.SaveSystem.Base;
using UnityEngine;

namespace DoubleDTeam.SaveSystem.Savers
{
    public class PlayerPrefsSaver : ISaveController
    {
        private const string SavePropertyName = "Data";

        private readonly SaveFile _saveFile;

        private readonly Dictionary<string, ISaveObject> _saveObjects = new();

        public PlayerPrefsSaver()
        {
            string data = PlayerPrefs.GetString(SavePropertyName);

            _saveFile = string.IsNullOrEmpty(data)
                ? new SaveFile(SaveType.File, Array.Empty<SaveParameter>())
                : JsonUtility.FromJson<SaveFile>(data);
        }

        public void Register(string key, ISaveObject saveObject)
        {
            if (_saveObjects.TryAdd(key, saveObject) == false)
            {
                Debug.LogError($"Key {key} has already been registered");
                return;
            }

            Load(key);
        }

        public void Remove(string key)
        {
            _saveObjects.Remove(key);
        }

        public void Save(string key)
        {
            _Save(key, true);
        }

        public void SaveAll()
        {
            foreach (var (key, _) in _saveObjects)
                _Save(key, false);

            PushSave();
        }

        public void Load(string key)
        {
            if (ContainSave(key) == false)
            {
                Debug.LogError($"Key {key} does not register");
                return;
            }

            ISaveObject saveObject = _saveObjects[key];
            bool hasParameter = _saveFile.TryGetParameter(key, out SaveParameter saveParameter);

            if (hasParameter == false)
            {
                Save(key);
                saveParameter = _saveFile.GetParameter(key);
            }

            saveObject.OnLoad(saveParameter.Data);
        }

        public void LoadAll()
        {
            foreach (var (key, _) in _saveObjects)
                Load(key);
        }

        public bool ContainSave(string key)
        {
            return _saveObjects.ContainsKey(key);
        }

        private void _Save(string key, bool isPushed)
        {
            if (ContainSave(key) == false)
            {
                Debug.LogError($"Key {key} does not register");
                return;
            }

            string objectData = _saveObjects[key].GetData();

            bool hasParameter = _saveFile.TryGetParameter(key, out SaveParameter saveParameter);

            if (hasParameter == false)
            {
                _saveFile.SetParameter(new SaveParameter(key, objectData));

                if (isPushed)
                    PushSave();

                return;
            }

            if (saveParameter.Data == objectData)
                return;

            saveParameter.Data = objectData;

            if (isPushed)
                PushSave();
        }

        private void PushSave()
        {
            string data = _saveFile.Serialize();

            using (var streamWriter = new StreamWriter(Application.persistentDataPath + "/save.json", false))
                streamWriter.Write(data);

            PlayerPrefs.SetString(SavePropertyName, data);
            Debug.Log("Saved".Color(Color.green));
        }
    }
}