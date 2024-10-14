using Game.Gameplay.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIResource : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _countText;

        public void Initialize(GameItemInfo item)
        {
            _image.sprite = item.Icon;
        }

        public void Initialize(GameItemInfo item, int count)
        {
            Initialize(item);
            Refresh(count);
        }

        public void Refresh(int count)
        {
            _countText.text = count.ToString();
        }
    }
}