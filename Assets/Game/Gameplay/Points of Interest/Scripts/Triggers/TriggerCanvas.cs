using DoubleDCore.Service;
using UnityEngine;

namespace Game.Gameplay.Interaction
{
    [RequireComponent(typeof(Canvas))]
    public class TriggerCanvas : MonoService
    {
        private Canvas _canvas;

        public void SetActive(bool isActive)
        {
            if (_canvas == null)
                _canvas = GetComponent<Canvas>();

            _canvas.enabled = isActive;
        }
    }
}