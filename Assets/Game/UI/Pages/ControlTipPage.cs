using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using UnityEngine;
using Zenject;

namespace Game.UI.Pages
{
    public class ControlTipPage : MonoPage, IPayloadPage<ControlTipArgument>
    {
        [SerializeField] private RectTransform _widgetRoot;
        [SerializeField] private Vector2 _offset;
        [SerializeField] private List<ControlTip> _tips;

        private ControlTipArgument _tipArgument;
        private CharacterMover _characterMover;
        private Camera _camera;

        [Inject]
        private void Init(CharacterMover characterMover)
        {
            _characterMover = characterMover;
        }

        private bool _tipIsDisplayed;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public override void Initialize()
        {
            Close();
        }

        public async void Open(ControlTipArgument context)
        {
            if (context.TipIndex >= _tips.Count)
                return;

            SetCanvasState(true);

            if (_tipIsDisplayed)
            {
                _tips[context.TipIndex].Close();
                await UniTask.WaitForSeconds(2f);
            }

            if (context.EndCondition())
                return;

            _tipArgument = context;

            _tips[context.TipIndex].gameObject.SetActive(true);
            _tips[context.TipIndex].Show();

            _tipIsDisplayed = true;
        }

        public override void Close()
        {
            foreach (var tip in _tips)
                tip.gameObject.SetActive(false);

            SetCanvasState(false);
        }

        private void Update()
        {
            if (PageIsDisplayed == false)
                return;

            _widgetRoot.anchoredPosition =
                _camera.WorldToScreenPoint(_characterMover.transform.position) / Canvas.scaleFactor +
                new Vector3(_offset.x, _offset.y, 0);

            if (_tipIsDisplayed == false)
                return;

            if (_tipArgument.EndCondition())
            {
                _tipIsDisplayed = false;
                _tips[_tipArgument.TipIndex].Close();
            }
        }
    }

    public class ControlTipArgument
    {
        public int TipIndex { get; }
        public Func<bool> EndCondition { get; }

        public ControlTipArgument(int tipIndex, Func<bool> endCondition)
        {
            TipIndex = tipIndex;
            EndCondition = endCondition;
        }
    }
}