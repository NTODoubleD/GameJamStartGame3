using System.Collections.Generic;
using DoubleDCore.Attributes;
using DoubleDCore.Identification;
using DoubleDCore.QuestsSystem.Base;
using DoubleDCore.QuestsSystem.Data;
using DoubleDCore.SaveSystem;
using UnityEngine;
using Zenject;

namespace DoubleDCore.QuestsSystem
{
    [RequireComponent(typeof(QuestBehaviour))]
    public class QuestSaver : MonoSaver, IIdentifying
    {
        [ReadOnlyProperty, SerializeField] private string _id;
        public string ID => _id;
        public override string Key => ID;

        private readonly Dictionary<string, QuestBehaviour> _quests = new();

        private IQuestController _questController;

        [Inject]
        private void Init(IQuestController questController)
        {
            _questController = questController;
        }

        private void Awake()
        {
            var quests = GetComponents<QuestBehaviour>();

            foreach (var questBehaviour in quests)
                _quests.TryAdd(questBehaviour.ID, questBehaviour);
        }

        public override string GetData()
        {
            var allQuests = new List<QuestBehaviour>(_quests.Values);
            var encryptQuests = new EncryptQuest[allQuests.Count];

            for (int i = 0; i < allQuests.Count; i++)
            {
                var quest = allQuests[i];

                encryptQuests[i] = new EncryptQuest
                {
                    ID = quest.ID,
                    Progress = quest.Progress,
                    Status = quest.Status
                };
            }

            var result = new EncryptQuests { Quests = encryptQuests };

            return JsonUtility.ToJson(result);
        }

        public override string GetDefaultData()
        {
            return JsonUtility.ToJson(new EncryptQuests());
        }

        public override void OnLoad(string data)
        {
            var encryptQuests = JsonUtility.FromJson<EncryptQuests>(data);

            foreach (var savedQuest in encryptQuests.Quests)
            {
                if (_quests.TryGetValue(savedQuest.ID, out var questBehaviour) == false)
                    continue;

                questBehaviour.RestoreProgress(savedQuest.Progress, savedQuest.Status);

                if (questBehaviour.Status.HasFlag(QuestStatus.InProgress))
                    _questController.IssueQuest(questBehaviour);
            }
        }

        public string GenerateIdentifier()
        {
            string result = "questsaver/";
            result += gameObject.name.ToLower() + "/";
            result += IDHelperServices.GetOrderID(result, this);
            return result;
        }

        public void SetIdentifier(string id)
        {
            _id = id;
        }
    }
}