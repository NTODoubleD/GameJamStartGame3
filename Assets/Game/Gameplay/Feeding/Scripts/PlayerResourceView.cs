using UnityEngine;

namespace Game.Gameplay.Feeding
{
    public class PlayerResourceView : MonoBehaviour
    {
        [SerializeField] private GameObject _resourceModel;

        public void Enable()
        {
            _resourceModel.SetActive(true);
        }

        public void Disable()
        {
            _resourceModel.SetActive(false);
        }
    }
}