using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public class HealthSliderController : MonoBehaviour
    {
        [SerializeField] private StatusSliderView _statusSliderView;
        
        private readonly TranslatedText _healthTranslate = new("Здоровье", "Health");

        public void UpdateState(DeerInfo deerInfo)
        {
            _statusSliderView.SetTitle(_healthTranslate.GetText());

            float healthDegree =
                Mathf.InverseLerp((int)DeerStatus.VerySick, (int)DeerStatus.Standard, (int)deerInfo.Status);
            
            _statusSliderView.SetSliderState(healthDegree);
        }
    }
}