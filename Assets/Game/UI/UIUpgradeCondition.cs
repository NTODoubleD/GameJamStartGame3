using DoubleDCore.Extensions;
using Game.Gameplay.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIUpgradeCondition : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _resourcesText;
        [SerializeField] private TMP_Text _textDescription;

        public void Initialize(GameItemInfo resourceInfo, int currentCount, int neccessaryCount)
        {
            _icon.enabled = true;
            
            _icon.sprite = resourceInfo.Icon;
            _resourcesText.text = $" - {currentCount}/{neccessaryCount}"
                .Color(currentCount >= neccessaryCount ? Color.green : Color.red);
        }

        public void Initialize(GameItemInfo resourceInfo, string count)
        {
            _icon.sprite = resourceInfo.Icon;
            _resourcesText.text = count;
        }

        public void Initialize(string textDescription)
        {
            _icon.enabled = false;
            _textDescription.text = textDescription;
        }
    }
}