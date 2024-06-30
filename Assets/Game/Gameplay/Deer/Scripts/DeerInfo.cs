using UnityEngine;

namespace Game.Gameplay
{
    public class DeerInfo
    {
        public Vector3 WorldPosition;
        public string Name;
        public GenderType Gender;
        public DeerAge Age;
        public float HungerDegree;
        public DeerStatus Status;
    }

    public enum DeerAge
    {
        Young,
        Adult,
        Old
    }
}