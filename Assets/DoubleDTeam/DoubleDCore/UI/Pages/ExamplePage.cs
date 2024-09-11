using DoubleDCore.UI.Base;

namespace DoubleDCore.UI.Pages
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

        public override void Close()
        {
            SetCanvasState(false);
        }

        public override void Reset()
        {
            SetCanvasState(false);
        }
    }
}