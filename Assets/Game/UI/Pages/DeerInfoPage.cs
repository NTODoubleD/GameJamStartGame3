using System;
using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Gameplay;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI.Pages
{
    public class DeerInfoPage : MonoPage, IPayloadPage<DeerInfoPageArgument>
    {
        [SerializeField] private TextMeshProUGUI _text;

        private GameInput _inputController;

        private DeerInfoPageArgument _context;

        [Inject]
        private void Init(GameInput inputController)
        {
            _inputController = inputController;
        }

        public override void Initialize()
        {
            SetCanvasState(false);
        }

        private readonly TranslatedText _nameTranslate = new("Имя", "Name");
        private readonly TranslatedText _genderTranslate = new("Пол", "Gender");
        private readonly TranslatedText _ageTranslate = new("Возраст", "Age");
        private readonly TranslatedText _satietyTranslate = new("Сытость", "Satiety");
        private readonly TranslatedText _statusTranslate = new("Статус", "Status");

        public void Open(DeerInfoPageArgument context)
        {
            _context = context;

            _inputController.Player.Disable();
            _inputController.UI.Enable();

            SetCanvasState(true);

            _text.text = $"{_nameTranslate.GetText()} - {context.Info.Name}\n" +
                         $"{_genderTranslate.GetText()} - {context.Info.Gender.ToText()}\n" +
                         $"{_ageTranslate.GetText()} - {context.Info.Age.ToText()} ({context.Info.AgeDays})\n" +
                         $"{_satietyTranslate.GetText()} - {Mathf.RoundToInt(context.Info.HungerDegree * 100)}%\n" +
                         $"{_statusTranslate.GetText()} - {context.Info.Status.ToText()}";
        }

        public override void Close()
        {
            SetCanvasState(false);

            _inputController.UI.Disable();
            _inputController.Player.Enable();

            _context?.OnClose?.Invoke();

            _context = null;
        }
    }

    public class DeerInfoPageArgument
    {
        public DeerInfo Info;
        public Action OnClose;
    }
}