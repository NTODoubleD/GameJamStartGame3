using UnityEngine;

namespace Game.GameEngine.Utils
{
    public class WorldCanvasCameraLook : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            transform.forward = _camera.transform.forward;
        }
    }
}