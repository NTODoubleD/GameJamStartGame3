using DoubleDTeam.Containers;
using DoubleDTeam.InputSystem;
using DoubleDTeam.UI;
using DoubleDTeam.UI.Base;
using Game.InputMaps;

namespace Game.UI.Pages
{
    public class EndGamePage : MonoPage, IUIPage
    {
        private InputController _inputController;

        private void Awake()
        {
            _inputController = Services.ProjectContext.GetModule<InputController>();
            Close();
        }

        public void Open()
        {
            _inputController.EnableMap<UIInputMap>();
            SetCanvasState(true);
        }

        public override void Close()
        {
            SetCanvasState(false);
        }
    }
}