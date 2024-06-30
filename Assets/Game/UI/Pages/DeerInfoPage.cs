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
    public class DeerInfoPage : MonoPage, IPayloadPage<DeerInfo>
    {
        [SerializeField] private TextMeshProUGUI _text;

        private InputController _inputController;

        private DeerInfo _context;

        private void Awake()
        {
            _inputController = Services.ProjectContext.GetModule<InputController>();

            Close();
        }

        public void Open(DeerInfo context)
        {
            _context = context;

            _inputController.EnableMap<UIInputMap>();

            SetCanvasState(true);

            _text.text = $"Name - {context.Name}\n" +
                         $"Gender - {context.Gender}\n" +
                         $"Age - {context.Age}\n" +
                         $"Hunger - {context.HungerDegree * 100}%\n" +
                         $"Status - {context.Status}";
        }

        public override void Close()
        {
            SetCanvasState(false);

            _inputController.EnableMap<PlayerInputMap>();

            _context?.OnEnd?.Invoke();

            _context = null;
        }
    }
}