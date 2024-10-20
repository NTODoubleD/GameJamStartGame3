using UnityEngine;

namespace Game.Gameplay.CharacterCamera
{
    [CreateAssetMenu(menuName = "Configs/Camera Zoom", fileName = "Camera Zoom Config")]
    public class CameraZoomConfig : ScriptableObject
    {
        [SerializeField] private Vector3 _minimalPosition;
        [SerializeField] private Vector3 _maximalPosition;
        [SerializeField] private Vector3 _speed;
        [SerializeField] private float _changeSpeed;

        public Vector3 MinimalPosition => _minimalPosition;
        public Vector3 MaximalPosition => _maximalPosition;
        public Vector3 Speed => _speed;
        public float ChangeSpeed => _changeSpeed;
    }
}