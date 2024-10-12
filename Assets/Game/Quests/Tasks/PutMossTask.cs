using Game.Gameplay.Feeding;
using Game.Quests.Base;
using UnityEngine;

namespace Game.Quests
{
    public class PutMossTask : YakutSubTask
    {
        [SerializeField] private PlayerMossPickController _moss;

        public override void Play()
        {
            _moss.MossPut += OnMossPut;
        }

        public override void Close()
        {
            _moss.MossPut -= OnMossPut;
        }

        private void OnMossPut()
        {
            Progress = 1;
        }
    }
}