using System.Collections.Generic;
using Game.Gameplay.Items;
using Game.Infrastructure.Items;
using Game.Quests.Base;
using Game.UI.Pages;
using UnityEngine;

namespace Game.Quests
{
    public class CampaignTask : YakutSubTask
    {
        [SerializeField] private SortiePage _sortiePage;

        public override void Play()
        {
            _sortiePage.Sended += OnCamping;
        }

        public override void Close()
        {
            _sortiePage.Sended -= OnCamping;
        }

        private void OnCamping(IReadOnlyDictionary<GameItemInfo, int> arg0, int arg1)
        {
            Progress = 1;
        }
    }
}