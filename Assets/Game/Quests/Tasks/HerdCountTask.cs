using System.Linq;
using Game.Gameplay.Scripts;
using Game.Quests.Base;
using Zenject;

namespace Game.Quests
{
    public class HerdCountTask : YakutSubTask
    {
        private Herd _herd;

        [Inject]
        private void Init(Herd herd)
        {
            _herd = herd;
        }

        public override void Play()
        {
            Progress = _herd.CurrentHerd.Count();
            _herd.HerdCountChanged += OnHerdCountChanged;
        }

        private void OnHerdCountChanged(int count)
        {
            Progress = count;
        }

        public override void Close()
        {
            _herd.HerdCountChanged -= OnHerdCountChanged;
        }
    }
}