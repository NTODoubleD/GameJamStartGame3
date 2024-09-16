using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class PointerHoverDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent PointEntered;
        public UnityEvent PointExited;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            PointEntered?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointExited?.Invoke();
        }
    }
}