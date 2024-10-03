using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.UI
{
    public class MonoTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TranslatedText _text;

        private TooltipController _tooltipController;
        
        [Inject]
        private void Init(TooltipController tooltipController)
        {
            _tooltipController = tooltipController;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tooltipController.EnableTooltip(_text.GetText());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tooltipController.DisableTooltip();
        }
    }
}