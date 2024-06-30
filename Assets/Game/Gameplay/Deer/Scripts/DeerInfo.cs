using UnityEngine;

namespace Game.Gameplay
{
    public class DeerInfo
    {
        public string Name;
        public GenderType Gender;
        public DeerAge Age;
        public float HungerDegree;
        public DeerStatus Status;
    }

    public enum DeerAge
    {
        None,
        Young,
        Adult,
        Old
    }
}