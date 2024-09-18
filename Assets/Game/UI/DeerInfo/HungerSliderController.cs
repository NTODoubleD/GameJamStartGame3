using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public class HungerSliderController : MonoBehaviour
    {
        private const float DEGREE_OFFSET = 0.4f;
        
        [SerializeField] private StatusSliderView _statusSliderView;
        
        private readonly TranslatedText _satietyTranslate = new("Сытость", "Satiety");

        public void UpdateState(DeerInfo deerInfo)
        {
            _statusSliderView.SetTitle(_satietyTranslate.GetText());

            float hungerDegree = 0;

            if (deerInfo.HungerDegree >= DEGREE_OFFSET)
                hungerDegree = Mathf.InverseLerp(DEGREE_OFFSET, 1, deerInfo.HungerDegree);
            
            _statusSliderView.SetSliderState(hungerDegree);
        }
    }
}