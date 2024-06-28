using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace DoubleDTeam.SaveSystem
{
    [Serializable]
    public class SaveFile
    {
        public string Date;
        public SaveType SaveType;
        public SaveParameter[] Data;

        public SaveFile(SaveType saveType, IEnumerable<SaveParameter> data)
        {
            Date = GetDate();
            SaveType = saveType;
            Data = data.ToArray();
        }

        public void SetParameter(SaveParameter parameter)
        {
            int index = IndexOfParameter(parameter.Key);

            if (index >= 0)
            {
                Data[index] = parameter;
                return;
            }

            var newParameters = new List<SaveParameter>(Data) { parameter };

            Data = newParameters.ToArray();
        }

        public bool TryGetParameter(string key, out SaveParameter saveParameter)
        {
            saveParameter = Data.FirstOrDefault(s => s.Key == key);

            return saveParameter != null;
        }

        public SaveParameter GetParameter(string key)
        {
            bool isSuccess = TryGetParameter(key, out SaveParameter saveParameter);

            if (isSuccess)
                return saveParameter;

            Debug.LogError($"Key {key} does not exist in Data");
            return saveParameter;
        }

        public string Serialize()
        {
            Date = GetDate();

            return JsonUtility.ToJson(this);
        }

        private string GetDate()
            => DateTime.Now.ToString("g", DateTimeFormatInfo.InvariantInfo);

        private int IndexOfParameter(string key)
        {
            if (TryGetParameter(key, out _) == false)
                return -1;

            for (int i = 0; i < Date.Length; i++)
            {
                if (Data[i].Key == key)
                    return i;
            }

            return -1;
        }
    }
}