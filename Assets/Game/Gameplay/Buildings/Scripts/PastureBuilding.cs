using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public class PastureBuilding : UpgradableBuilding
    {
        [SerializeField] private int _testCount;

        public int DeerCount => _testCount;
    }
}