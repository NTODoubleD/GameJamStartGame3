using System;
using Game.Gameplay.Interaction;
using UnityEngine;

namespace Game.Gameplay.SnowPicking
{
    public class SnowInteractionObject : SimpleInteractiveObject
    {
        [SerializeField] private PlayerSnowPickController _pickController;
        
        public event Action SnowTaken;

        public override void Interact()
        {
            _pickController.InteractWithSnowPack();

            SnowTaken?.Invoke();
        }
    }
}