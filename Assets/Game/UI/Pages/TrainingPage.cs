using System;
using System.Collections.Generic;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.UI.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace Game.UI.Pages
{
    public class TrainingPage : MonoPage, IPayloadPage<TrainingPageArgument>
    {
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private ClickButton _rightButton;
        [SerializeField] private ClickButton _leftButton;
        [SerializeField] private ClickButton _closeButton;
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
                _dotsPool[i].gameObject.SetActive(_context.TrainingTips.Count > 1 && _context.TrainingTips.Count > i);

            SetTip(0);
        }

        private void SetTip(int index)
        {
            if (index < 0 || index >= _context.TrainingTips.Count)
                return;

            if (index == _currentTipIndex)
                return;

            _videoPlayer.Stop();
            _videoPlayer.frame = 0;

            var tip = _context.TrainingTips[index];

            _videoPlayer.clip = tip.Video;
            _videoPlayer.Play();
            _text.text = tip.Text;

            foreach (var uiDot in _dotsPool)
                uiDot.SetHighlight(false);

            _currentTipIndex = index;

            _leftButton.gameObject.SetActive(_currentTipIndex > 0);
            _rightButton.gameObject.SetActive(_currentTipIndex < _context.TrainingTips.Count - 1);
            _closeButton.gameObject.SetActive(_currentTipIndex == _context.TrainingTips.Count - 1);

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
            Close();
        }
    }
}