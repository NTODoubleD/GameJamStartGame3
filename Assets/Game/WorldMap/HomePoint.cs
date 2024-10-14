using Zenject;

namespace Game.WorldMap
{
    public class HomePoint : WorldInterestPoint
    {
        private WorldMapController _worldMapController;

        [Inject]
        private void Init(WorldMapController worldMapController)
        {
            _worldMapController = worldMapController;
        }

        public override void Interact()
        {
            _worldMapController.ToPlayerState();
        }
    }
}