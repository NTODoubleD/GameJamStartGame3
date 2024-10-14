using UnityEngine;

namespace Game.Сompass
{
    public abstract class CommpassView : MonoBehaviour
    {
        public abstract void UpdateCompass(Vector3 targetPosition);
        public abstract void SetActive(bool active);
    }
}