using System;
using DoubleDCore.Service;

namespace Game.Gameplay.Interaction
{
    public class SceneInteractionData : MonoService
    {
        public InteractiveObject CurrentObject
        {
            get
            {
                return _currentObject;
            }

            set
            {
                _currentObject = value;
                ObjectChanged?.Invoke(_currentObject);
            }
        }

        private InteractiveObject _currentObject;

        public event Action<InteractiveObject> ObjectChanged;
    }
}