using DoubleDTeam.Containers;
using DoubleDTeam.InputSystem;
using UnityEngine;

namespace Game.InputMaps
{
    public class InputMapSwitcher : MonoBehaviour
    {
        private InputController _inputController;

        private void Awake()
        {
            _inputController = Services.ProjectContext.GetModule<InputController>();
        }

        public void EnablePlayerMap()
        {
            _inputController.EnableMap<PlayerInputMap>();
        }
    }
}