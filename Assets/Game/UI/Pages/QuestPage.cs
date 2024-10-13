using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DoubleDCore.Extensions;
using DoubleDCore.QuestsSystem.Base;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Quests.Base;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI.Pages
{
    public class QuestPage : MonoPage, IUIPage
    {
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _subText;
        [SerializeField] private CanvasGroup _canvasGroup;

        private IQuestController _questController;
        private int _currentQuestCount;

        [Inject]
        private void Init(IQuestController questController)
        {
            _questController = questController;
        }

        public override void Initialize()
        {
            _questController.QuestCompleted += OnQuestCompleted;
            _questController.QuestIssued += OnQuestIssued;
            
            Open();
        }

        public void Open()
        {
            if (PageIsDisplayed)
                return;

            _canvasGroup.alpha = 0;

            if (_currentQuestCount > 0)
                _canvasGroup.DOFade(1, 0.4f);

            SetCanvasState(true);
        }

        public override void Close()
        {
            if (PageIsDisplayed == false)
                return;
            
            _canvasGroup.DOFade(0, 0.4f).OnComplete(() => SetCanvasState(false));
        }

        private async void OnQuestIssued(IQuest quest)
        {
            if (quest is not YakutQuest yakutQuest)
                return;

            _currentQuestCount++;
            yakutQuest.TaskProgressChanged += OnTaskStateChanged;

            await UniTask.WaitForSeconds(1.2f);

            UpdateInformation(yakutQuest);

            _canvasGroup.DOFade(1, 0.4f);
        }

        private void OnQuestCompleted(IQuest quest)
        {
            if (quest is not YakutQuest yakutQuest)
                return;

            _currentQuestCount--;
            yakutQuest.TaskProgressChanged -= OnTaskStateChanged;

            _canvasGroup.DOFade(0, 0.4f).SetDelay(0.8f);
        }

        private void OnTaskStateChanged(YakutQuest yakutQuest)
        {
            UpdateInformation(yakutQuest);
        }

        private void UpdateInformation(YakutQuest quest)
        {
            _header.text = quest.Header;

            var subTaskTexts = new List<string>();

            foreach (var subTask in quest.SubTasks)
            {
                string text = $"• {subTask.TaskName}";

                if (subTask.MaxProgress != 1)
                    text += $" — {subTask.Progress}/{subTask.MaxProgress}";

                if (subTask.Progress >= subTask.MaxProgress)
                    text = text.Color("00B1FF");

                subTaskTexts.Add(text);
            }

            _subText.text = string.Join("\n", subTaskTexts);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _questController.QuestCompleted -= OnQuestCompleted;
            _questController.QuestIssued -= OnQuestIssued;
        }
    }
}