using System;
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

        public void Open(DeerInfoPageArgument context)
        {
            _context = context;

            _inputController.Player.Disable();
            _inputController.UI.Enable();

            SetCanvasState(true);

            _text.text = $"Имя - {context.Info.Name}\n" +
                         $"Пол - {context.Info.Gender.ToText()}\n" +
                         $"Возраст - {context.Info.Age.ToText()}\n" +
                         $"Сытость - {Mathf.RoundToInt(context.Info.HungerDegree * 100)}%\n" +
                         $"Статус - {context.Info.Status.ToText()}";
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