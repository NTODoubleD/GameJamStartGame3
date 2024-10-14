using System;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Gameplay.SurvivalMechanics.Frost;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Pages
{
    public class StrongFrostPage : MonoPage, IPayloadPage<StrongFrostPageArgument>
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _sliderSpeed = 0.2f;

        private StrongFrostPageArgument _argument;

        public void Open(StrongFrostPageArgument context)
        {
            _argument = context;
            SetCanvasState(true);       
        }

        private void Update()
        {
            if (PageIsDisplayed == false)
                return;

            _slider.value = _argument.GetTimeLeftPart();
        }

        public override void Close()
        {
            SetCanvasState(false);
        }
    }

    public class StrongFrostPageArgument
    {
        private readonly FrostStarter _frostStarter;

        public StrongFrostPageArgument(FrostStarter frostStarter)
        {
            _frostStarter = frostStarter;
        }
        
        public float GetTimeLeftPart()
        {
            return _frostStarter.CurrentFrostTimeLeft / _frostStarter.CurrentFrostDuration;
        }
    }
}