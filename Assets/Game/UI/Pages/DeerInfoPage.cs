using System;
using DoubleDCore.Configuration;
using DoubleDCore.Configuration.Base;
using DoubleDCore.GameResources.Base;
using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Gameplay;
using Game.Gameplay.Scripts.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Pages
{
    public class DeerInfoPage : MonoPage, IPayloadPage<DeerInfoPageArgument>
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _deerIcon;
        [SerializeField] private Image _deerFrame;
        [SerializeField] private TMP_InputField _name;

        private GameInput _inputController;
        private DeerInfoPageArgument _context;
        private DeerImagesConfig _deerImagesConfig;

        [Inject]
        private void Init(GameInput inputController, IResourcesContainer resourcesContainer)
        {
            _deerImagesConfig = resourcesContainer.GetResource<ConfigsResource>().GetConfig<DeerImagesConfig>();
            _inputController = inputController;
        }

        private void OnEnable()
        {
            _name.onValueChanged.AddListener(OnNameChanged);
        }

        private void OnDisable()
        {
            _name.onValueChanged.RemoveListener(OnNameChanged);
        }

        private void OnNameChanged(string newName)
        {
            if (_context == null)
                return;
            
            if (newName != string.Empty)
                _context.Info.Name = newName;

            _name.text = _context.Info.Name;
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
        private readonly TranslatedText _dayTranslate = new("день", "day");

        public void Open(DeerInfoPageArgument context)
        {
            _context = context;

            _inputController.Player.Disable();
            _inputController.UI.Enable();

            SetCanvasState(true);

            _deerIcon.sprite = _deerImagesConfig.GetDeerImage(context.Info.Age);
            _deerFrame.color = context.Info.Gender == GenderType.Female
                ? _deerImagesConfig.FemaleFrameColor
                : _deerImagesConfig.MaleFrameColor;

            _name.text = $"{context.Info.Name}";
            _text.text = $"{_ageTranslate.GetText()} - {context.Info.Age.ToText()} ({context.Info.AgeDays} {_dayTranslate.GetText()})\n" +
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