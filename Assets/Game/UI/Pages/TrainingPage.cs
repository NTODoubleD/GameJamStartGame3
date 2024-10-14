using System;
using System.Collections.Generic;
using DG.Tweening;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.UI.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Game.UI.Pages
{
    public class TrainingPage : MonoPage, IPayloadPage<TrainingPageArgument>
    {
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private RawImage _videoPlayerImage;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private ClickButton _rightButton;
        [SerializeField] private ClickButton _leftButton;
        [SerializeField] private ClickButton _closeButton;
        [SerializeField] private TrainingPageAnimator _animator;
        [SerializeField] private List<UIDot> _dotsPool;

        private TrainingPageArgument _context;
        private int _currentTipIndex = -1;

        public override void Initialize()
        {
            SetCanvasState(false);
        }

        private readonly List<Action> _dotsSubscribes = new();

        public void Open(TrainingPageArgument context)
        {
            _rightButton.Clicked += OnRight;
            _leftButton.Clicked += OnLeft;
            _closeButton.Clicked += OnClose;

            for (int i = 0; i < _dotsPool.Count; i++)
            {
                var index = i;
                var subscribe = new Action(() => SetTip(index));

                _dotsSubscribes.Add(subscribe);
                _dotsPool[index].Clicked += subscribe;
            }

            _context = context;

            _animator.StartOpenAnimation();

            StartTraining();

            SetCanvasState(true);
        }

        public override void Close()
        {
            SetCanvasState(false);

            _rightButton.Clicked -= OnRight;
            _leftButton.Clicked -= OnLeft;
            _closeButton.Clicked -= OnClose;


            for (int i = 0; i < _dotsSubscribes.Count; i++)
            {
                _dotsPool[i].Clicked -= _dotsSubscribes[i];
            }

            _dotsSubscribes.Clear();

            _context = null;
            _currentTipIndex = -1;
            _videoPlayer.Stop();
        }

        private void StartTraining()
        {
            for (int i = 0; i < _dotsPool.Count; i++)
            {
                _dotsPool[i].gameObject.SetActive(_context.TrainingInfo.TrainingTips.Count > 1 &&
                                                  _context.TrainingInfo.TrainingTips.Count > i);
            }

            SetTip(0);
        }

        private void SetTip(int index)
        {
            if (index < 0 || index >= _context.TrainingInfo.TrainingTips.Count)
                return;

            if (index == _currentTipIndex)
                return;

            StartContentAnimation();

            _videoPlayer.Stop();
            _videoPlayer.frame = 0;
            _videoPlayer.clip = null;

            _image.sprite = null;

            var tip = _context.TrainingInfo.TrainingTips[index];

            _videoPlayer.gameObject.SetActive(_context.TrainingInfo.TrainingTips[index].Video);
            _image.gameObject.SetActive(_context.TrainingInfo.TrainingTips[index].Image);

            _videoPlayer.clip = tip.Video;
            _videoPlayer.Play();

            _image.sprite = tip.Image;

            _text.text = tip.Text;

            foreach (var uiDot in _dotsPool)
                uiDot.SetHighlight(false);

            _currentTipIndex = index;

            _leftButton.gameObject.SetActive(_currentTipIndex > 0);
            _rightButton.gameObject.SetActive(_currentTipIndex < _context.TrainingInfo.TrainingTips.Count - 1);
            _closeButton.gameObject.SetActive(_currentTipIndex == _context.TrainingInfo.TrainingTips.Count - 1);

            _dotsPool[_currentTipIndex].SetHighlight(true);
        }

        private void OnLeft()
        {
            SetTip(_currentTipIndex - 1);
        }

        private void OnRight()
        {
            SetTip(_currentTipIndex + 1);
        }

        private void OnClose()
        {
            _context.OnClose?.Invoke();

            _animator.StartCloseAnimation(Close);
        }

        private void StartContentAnimation()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_image.DOFade(0, 0))
                .Append(_text.DOFade(0, 0))
                .Append(_videoPlayerImage.DOFade(0, 0));

            sequence.Play().SetUpdate(true).OnComplete(() =>
            {
                _image.DOFade(1, 0.5f).SetUpdate(true);
                _videoPlayerImage.DOFade(1, 0.5f).SetUpdate(true);
                _text.DOFade(1, 1f).SetUpdate(true);
            });
        }
    }

    public class TrainingPageArgument
    {
        public TrainingInfo TrainingInfo;
        public Action OnClose;
    }
}