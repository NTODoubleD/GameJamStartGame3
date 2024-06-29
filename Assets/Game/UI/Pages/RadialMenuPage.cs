using System;
using System.Collections.Generic;
using DoubleDTeam.Containers;
using DoubleDTeam.InputSystem;
using DoubleDTeam.UI;
using DoubleDTeam.UI.Base;
using Game.InputMaps;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Xamin;
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
        private InputController _inputController;

        private void Awake()
        {
            _inputController = Services.ProjectContext.GetModule<InputController>();

            CreateButtons();

            var uiManager = Services.ProjectContext.GetModule<IUIManager>();
            uiManager.ClosePage<RadialMenuPage>();
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

            _inputController.EnableMap<UIInputMap>();

            ConfigureMenu(context);

            _circleSelector.Open();
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
    }
}