using System;
using System.Collections.Generic;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Xamin;
using Zenject;
using Button = Xamin.Button;

namespace Game.UI.Pages
{
    public class RadialMenuPage : MonoPage, IPayloadPage<RadialMenuArgument>
    {
        [SerializeField] private CircleSelector _circleSelector;
        [SerializeField] private Button _prefab;
        [SerializeField] private Transform _buttonContainer;
        [SerializeField] private int _maxButtons = 8;
        [SerializeField] private TextMeshProUGUI _label;

        private readonly List<Button> _buttons = new();
        private GameInput _inputController;

        public event UnityAction Opened;

        [Inject]
        private void Init(GameInput inputController)
        {
            _inputController = inputController;
        }

        public override void Initialize()
        {
            SetCanvasState(false);

            CreateButtons();
        }

        private void CreateButtons()
        {
            for (int i = 0; i < _maxButtons; i++)
            {
                var inst = Instantiate(_prefab, Vector3.zero, Quaternion.identity, _buttonContainer);

                _buttons.Add(inst);

                inst.gameObject.SetActive(false);
            }
        }

        public void Open(RadialMenuArgument context)
        {
            SetCanvasState(true);

            _inputController.Player.Disable();
            _inputController.UI.Enable();

            ConfigureMenu(context);

            _circleSelector.Open();

            Opened?.Invoke();
        }

        public override void Close()
        {
            SetCanvasState(false);

            CleanMenu();
        }

        private void ConfigureMenu(RadialMenuArgument context)
        {
            _label.text = context.Name;

            for (int i = 0; i < context.Buttons.Count; i++)
            {
                var button = _buttons[i];
                var buttonInfo = context.Buttons[i];

                if (buttonInfo.IsUnlock())
                    continue;

                button.gameObject.SetActive(true);
                _circleSelector.Buttons.Add(button.gameObject);


                button.action.AddListener(() => buttonInfo.Action?.Invoke());
                button.action.AddListener(Close);

                button.image = buttonInfo.Image;
                button.optionsName.text = buttonInfo.Name;
            }
        }

        private void CleanMenu()
        {
            _circleSelector.Buttons.Clear();

            foreach (var button in _buttons)
            {
                button.action.RemoveAllListeners();
                button.gameObject.SetActive(false);
            }
        }
    }

    public class RadialMenuArgument
    {
        public string Name;
        public List<RadialButtonInfo> Buttons;
    }

    [Serializable]
    public class RadialButtonInfo
    {
        public string Name;
        public Sprite Image;
        public UnityEvent Action;
        public bool HasCondition;

        [ShowIf("HasCondition")] public ConditionObject ShowCondition;

        public bool IsUnlock()
            => HasCondition && ShowCondition.ConditionIsDone() == false;
    }
}