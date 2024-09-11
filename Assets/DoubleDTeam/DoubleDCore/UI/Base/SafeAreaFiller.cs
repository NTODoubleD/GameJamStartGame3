using UnityEngine;

namespace DoubleDCore.UI.Base
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaFiller : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private ScreenOrientation _currentOrientation;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            UpdateSafeArea();
        }

        private void LateUpdate()
        {
            if (_currentOrientation == Screen.orientation)
                return;

            _currentOrientation = Screen.orientation;
            UpdateSafeArea();
        }

        private void UpdateSafeArea()
        {
            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = anchorMin + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
        }
    }
}