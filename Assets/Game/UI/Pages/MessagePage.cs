using DoubleDTeam.Containers;
using DoubleDTeam.UI;
using DoubleDTeam.UI.Base;
using Game.Monologue;
using UnityEngine;

namespace Game.UI.Pages
{
    public class MessagePage : MonoPage, IPayloadPage<MonologueGroupInfo>
    {
        [SerializeField] private MessageShower _messageShower;

        private IUIManager _uiManager;

        private void Awake()
        {
            _uiManager = Services.ProjectContext.GetModule<IUIManager>();
            _uiManager.ClosePage<MessagePage>();
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
            _uiManager.ClosePage<MessagePage>();
        }
    }
}