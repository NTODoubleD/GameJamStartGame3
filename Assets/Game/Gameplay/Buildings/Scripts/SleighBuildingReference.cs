using DoubleDCore.Service;
using UnityEngine;

namespace Game.Gameplay.Buildings
{
    public class SleighBuildingReference : MonoService
    {
        [SerializeField] private SleighBuilding _sleighBuilding;

        public SleighBuilding SleighBuilding => _sleighBuilding;
    }
}