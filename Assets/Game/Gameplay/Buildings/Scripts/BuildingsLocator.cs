using DoubleDCore.Service;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public class BuildingsLocator : MonoService
    {
        [SerializeField] private TownHallBuilding _townHall;
        [SerializeField] private PastureBuilding _pastureBuilding;

        public TownHallBuilding TownHall => _townHall;
        public PastureBuilding PastureBuilding => _pastureBuilding;
    }
}