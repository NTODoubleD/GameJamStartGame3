using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Zenject;

namespace Game.UI.Pages
{
    public class EndGamePage : MonoPage, IUIPage
    {
        private GameInput _inputController;

        [Inject]
        private void Init(GameInput inputController)
        {
            _inputController = inputController;
        }

        public override void Initialize()
        {
            SetCanvasState(false);
        }

        public void Open()
        {
            _inputController.UI.Enable();

            SetCanvasState(true);
        }

        public override void Close()
        {
            SetCanvasState(false);
        }
    }
}