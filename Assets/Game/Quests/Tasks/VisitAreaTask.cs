using DoubleDCore.PhysicsTools.CollisionImpacts;
using Game.Quests.Base;
using UnityEngine;

namespace Game.Quests
{
    public class VisitAreaTask : YakutSubTask
    {
        [SerializeField] private TriggerListener<CharacterMover> _playerListener;

        public override void Play()
        {
            if (_playerListener.IsTargetInTrigger())
                Progress = 1;
            else
                _playerListener.TriggerEnter += ListenerOnTriggerEnter;
        }

        public override void Close()
        {
            _playerListener.TriggerEnter -= ListenerOnTriggerEnter;
        }

        private void ListenerOnTriggerEnter(CharacterMover obj)
        {
            Progress = 1;
        }
    }
}