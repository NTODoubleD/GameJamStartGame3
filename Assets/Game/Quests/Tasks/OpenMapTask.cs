using Game.Quests.Base;
using Game.WorldMap;
using Zenject;

namespace Game.Quests.Tasks
{
    public class OpenMapTask : YakutSubTask
    {
        private WorldMapController _worldMapController;
        
        [Inject]
        private void Init(WorldMapController worldMapController)
        {
            _worldMapController = worldMapController;
        }
        
        public override void Play()
        {
            _worldMapController.Opened += OnMapOpened;
        }

        private void OnMapOpened()
        {
            Progress = 1;
        }

        public override void Close()
        {
            _worldMapController.Opened -= OnMapOpened;
        }
    }
}