using DoubleDTeam.UI.Base;

namespace DoubleDTeam.UI.Pages
{
    public sealed class ExamplePage : MonoPage, IPayloadPage<string>
    {
        public override void Initialize()
        {
            SetCanvasState(false);
        }

        public void Open(string context)
        {
            SetCanvasState(true);
        }

        public void Close(string context)
        {
            SetCanvasState(false);
        }

        public override void Reset()
        {
            SetCanvasState(false);
        }
    }
}