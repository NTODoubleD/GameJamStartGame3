using Game.Gameplay.Feeding;
using Game.Quests.Base;
using UnityEngine;

namespace Game.Quests
{
    public class TakeMossTask : YakutSubTask
    {
        [SerializeField] private MossInteractionObject _moss;

        public override void Play()
        {
            _moss.MossTaken += OnMossTaken;
        }

        public override void Close()
        {
            _moss.MossTaken -= OnMossTaken;
        }

        private void OnMossTaken()
        {
            Progress = 1;
        }
    }
}