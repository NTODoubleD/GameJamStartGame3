using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class ExitRadialMenu : MonoBehaviour
    {
        private GameInput _gameInput;

        [Inject]
        private void Init(GameInput gameInput)
        {
            _gameInput = gameInput;
        }

        public void Exit()
        {
            _gameInput.Player.Enable();
            _gameInput.UI.Disable();
        }
    }
}