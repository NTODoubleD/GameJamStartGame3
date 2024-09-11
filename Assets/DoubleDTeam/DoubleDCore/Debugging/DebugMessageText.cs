using DoubleDCore.TimeTools;
using TMPro;
using UnityEngine;

namespace DoubleDCore.Debugging
{
    public class DebugMessageText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private RectTransform _rectTransform;

        private Timer _timer;
        public Timer Timer => _timer;

        public TMP_Text Text => _text;
        public RectTransform RectTransform => _rectTransform;

        public Vector3 WorldPoint;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void Initialize(Timer timer)
        {
            _timer = timer;
        }

        public void FixedUpdate()
        {
            if (_camera == null)
                return;

            Vector3 newPosition = _camera.WorldToScreenPoint(WorldPoint);
            _text.enabled = newPosition.z > 0;
            RectTransform.anchoredPosition = _camera.WorldToScreenPoint(WorldPoint);
        }
    }
}