using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public class TownHallLocator : MonoBehaviour
    {
        public static TownHallLocator Instance { get; private set; }

        [SerializeField] private TownHallBuilding _townHall;

        public TownHallBuilding TownHall => _townHall;

        private void Awake()
        {
            Instance = this;
        }
    }
}