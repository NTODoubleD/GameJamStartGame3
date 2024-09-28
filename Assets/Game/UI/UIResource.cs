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

        public void Refresh(GameItemInfo item, int count)
        {
            _image.sprite = item.Icon;
            _countText.text = count.ToString();
        }
    }
}