using DoubleDTeam.UI;
using DoubleDTeam.UI.Base;
using Game.Monologue;
using UnityEngine;

namespace Game.UI.Pages
{
    public class MessagePage : MonoPage, IPayloadPage<MonologueGroupInfo>
    {
        [SerializeField] private MessageShower _messageShower;

        private void Awake()
        {
            Close();
        }

        public void Open(MonologueGroupInfo context)
        {
            SetCanvasState(true);

            _messageShower.StopMessaging();
            _messageShower.Performed += OnMessageEnd;

            _messageShower.ShowText(context);
        }

        public override void Close()
        {
            _messageShower.Performed -= OnMessageEnd;

            SetCanvasState(false);
        }

        private void OnMessageEnd()
        {
            Close();
        }
    }
}