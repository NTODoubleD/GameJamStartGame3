using System;
using DoubleDTeam.Containers;
using DoubleDTeam.InputSystem;
using DoubleDTeam.UI;
using DoubleDTeam.UI.Base;
using Game.Gameplay;
using Game.InputMaps;
using TMPro;
using UnityEngine;

namespace Game.UI.Pages
{
    public class DeerInfoPage : MonoPage, IPayloadPage<DeerInfoPageArgument>
    {
        [SerializeField] private TextMeshProUGUI _text;

        private InputController _inputController;

        private DeerInfoPageArgument _context;

        private void Awake()
        {
            _inputController = Services.ProjectContext.GetModule<InputController>();

            Close();
        }

        public void Open(DeerInfoPageArgument context)
        {
            _context = context;

            _inputController.EnableMap<UIInputMap>();

            SetCanvasState(true);

            _text.text = $"Имя - {context.Info.Name}\n" +
                         $"Пол - {context.Info.Gender.ToText()}\n" +
                         $"Возраст - {context.Info.Age.ToText()}\n" +
                         $"Сытость - {context.Info.HungerDegree * 100}%\n" +
                         $"Статус - {context.Info.Status.ToText()}";
        }

        public override void Close()
        {
            SetCanvasState(false);

            _inputController.EnableMap<PlayerInputMap>();

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