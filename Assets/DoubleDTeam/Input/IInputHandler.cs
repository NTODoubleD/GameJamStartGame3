using DoubleDTeam.Containers.Base;
using UnityEngine;

namespace DoubleDTeam.Input
{
    public interface IInputHandler : IModule
    {
        public Vector2 MoveVector { get; }
    }
}