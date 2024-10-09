using System;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Pages
{
    public class ConfirmPage : MonoPage, IPayloadPage<ConfirmPageArgument>
    {
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _message;

        private ConfirmPageArgument _argument;
        
        public void Open(ConfirmPageArgument context)
        {
            _argument = context;
            
            _title.text = context.Title;
            _message.text = context.Message;
            
            _confirmButton.onClick.AddListener(OnConfirmClicked);
            _cancelButton.onClick.AddListener(OnCancelClicked);
            
            SetCanvasState(true);
        }

        private void OnConfirmClicked()
        {
            Close();
            _argument.SuccessAction?.Invoke();
        }

        private void OnCancelClicked()
        {
            Close();
            _argument.CancelAction?.Invoke();
        }

        public override void Close()
        {
            _confirmButton.onClick.RemoveListener(OnConfirmClicked);
            _cancelButton.onClick.RemoveListener(OnCancelClicked);
            SetCanvasState(false);
        }
    }
    
    public class ConfirmPageArgument
    {
        public string Title { get; }
        public string Message { get; }
        public Action SuccessAction { get; }
        public Action CancelAction { get; }
        
        public ConfirmPageArgument(string title, string message, Action successAction, Action cancelAction)
        {
            Title = title;
            Message = message;
            SuccessAction = successAction;
            CancelAction = cancelAction;
        }
    }
}