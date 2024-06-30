using System;
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

        public Action OnEnd;
    }

    public enum DeerAge
    {
        Young,
        Adult,
        Old
    }
}