using System;
using DG.Tweening;
using ModestTree.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Notifications
{
    public class UINotification : MonoBehaviour
    {
        [SerializeField] private Image _backFrame;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private CanvasGroup _canvasGroup;

        private Tweener _currentTweener;

        public event Action<UINotification> CloseRequested;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }

        public void Initialize(Color color, Sprite icon, string title, string description)
        {
            _backFrame.color = color;

            _icon.sprite = icon;

            _title.text = title;
            _description.text = description;
        }
        
        public void Open()
        {
            if (_currentTweener != null && _currentTweener.IsActive())
                _currentTweener.Kill();
            
            gameObject.SetActive(true);
            _currentTweener = _canvasGroup.DOFade(1, 0.4f);
        }
        
        public void Close()
        {
            if (_currentTweener != null && _currentTweener.IsActive())
                _currentTweener.Kill();
            
            _currentTweener = _canvasGroup.DOFade(0, 0.4f).OnComplete(Destroy);
        }

        private void Destroy()
        {
            GameObject.Destroy(gameObject);
        }

        private void OnExitButtonClicked()
        {
            CloseRequested?.Invoke(this);
        }
    }
}