using System;
using Game.Gameplay.Interaction;
using UnityEngine;

namespace Game.Gameplay.Feeding
{
    public class MossInteractionObject : SimpleInteractiveObject
    {
        [SerializeField] private PlayerMossPickController _pickController;

        public event Action MossTaken;

        public override void Interact()
        {
            _pickController.InteractWithMossPack();

            MossTaken?.Invoke();
        }
    }
}