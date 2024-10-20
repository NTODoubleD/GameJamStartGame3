using DoubleDCore.Tween.Effects;
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
        [SerializeField] private NumberFeedback _numberFeedback;

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

        public void PlayFeedback(int delta)
        {
            _numberFeedback.StartFeedback(delta, OnAnimationEnd);
        }

        private void OnAnimationEnd()
        {
        }
    }
}