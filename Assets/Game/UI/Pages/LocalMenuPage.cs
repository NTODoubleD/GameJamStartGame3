using DoubleDTeam.Containers;
using DoubleDTeam.InputSystem;
using DoubleDTeam.TimeTools;
using DoubleDTeam.UI;
using DoubleDTeam.UI.Base;
using Game.InputMaps;

namespace Game.UI.Pages
{
    public class LocalMenuPage : MonoPage, IUIPage
    {
        private const float OpenDelay = 0.1f;

        private InputController _inputManager;

        private UIInputMap _uiInputMap;
        private PlayerInputMap _playerInputMap;

        private Timer _timer;

        private void Awake()
        {
            _inputManager = Services.ProjectContext.GetModule<InputController>();
            _uiInputMap = _inputManager.GetMap<UIInputMap>();
            _playerInputMap = _inputManager.GetMap<PlayerInputMap>();

            _timer = new Timer(this, TimeBindingType.RealTime);

            Close();
        }

        public void Open()
        {
            if (_timer.IsWorked)
                return;

            _playerInputMap.Escape.Started -= Open;

            _inputManager.EnableMap<UIInputMap>();
            SetCanvasState(true);


            _uiInputMap.Close.Started += Close;

            _timer.Start(OpenDelay);
        }

        public override void Close()
        {
            if (_timer.IsWorked)
                return;

            _uiInputMap.Close.Started -= Close;

            SetCanvasState(false);
            _inputManager.EnableMap<PlayerInputMap>();

            _playerInputMap.Escape.Started += Open;

            _timer.Start(OpenDelay);
        }
    }
}