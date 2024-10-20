using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Gameplay.CharacterCamera
{
    public class CameraZoomController : ITickable
    {
        private readonly CinemachineTransposer _transposer;
        private readonly CameraZoomConfig _config;
        private readonly GameInput _gameInput;

        private Vector3 _targetVector;

        public CameraZoomController(CinemachineVirtualCamera characterCamera,
            CameraZoomConfig config, GameInput gameInput)
        {
            _config = config;
            _gameInput = gameInput;
            
            _transposer = characterCamera.GetCinemachineComponent<CinemachineTransposer>();
            _targetVector = _transposer.m_FollowOffset;
            
            _gameInput.Player.Zoom.performed += OnZoomPerformed;
        }

        private void OnZoomPerformed(InputAction.CallbackContext obj)
        {
            float zoomValue = obj.ReadValue<float>();

            if (zoomValue > 0)
                zoomValue = -1;
            else if (zoomValue < 0)
                zoomValue = 1;
            
            float yDelta = _config.Speed.y * zoomValue;
            float zDelta = _config.Speed.z * (-zoomValue);

            Vector3 res = _targetVector;
            res.y = Mathf.Clamp(res.y + yDelta, _config.MinimalPosition.y, _config.MaximalPosition.y);
            res.z = Mathf.Clamp(res.z + zDelta, _config.MaximalPosition.z, _config.MinimalPosition.z);
            
            _targetVector = res;
        }

        public void Tick()
        {
            _transposer.m_FollowOffset =
                Vector3.Lerp(_transposer.m_FollowOffset, _targetVector, _config.ChangeSpeed * Time.deltaTime);
        }
    }
}