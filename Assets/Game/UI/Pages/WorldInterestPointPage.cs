using System;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.WorldMap;
using TMPro;
using UnityEngine;

namespace Game.UI.Pages
{
    public class WorldInterestPointPage : MonoPage, IPayloadPage<InterestPointArgument>
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private RectTransform _widgetRoot;
        [SerializeField] private ClickButton _startSortieButton;

        [Space, SerializeField] private UIResource _wood;
        [SerializeField] private UIResource _moss;
        [SerializeField] private UIResource _healGrass;
        [Range(0.01f, 500f), SerializeField] private float _scaleFactor;

        private Camera _camera;

        public override void Initialize()
        {
            _camera = Camera.main;

            SetCanvasState(false);
        }

        private InterestPointArgument _currentContext;

        public void Open(InterestPointArgument context)
        {
            _nameText.text = context.Name;

            SetResourcePriorities(context);

            _startSortieButton.Clicked += StartSortie;
            _currentContext = context;
            SetCanvasState(true);
        }

        public override void Close()
        {
            _startSortieButton.Clicked -= StartSortie;
            SetCanvasState(false);
        }

        private void LateUpdate()
        {
            if (!PageIsDisplayed)
                return;

            UpdateWidgetPosition(_currentContext.Position);
        }

        private void SetResourcePriorities(InterestPointArgument context)
        {
            _wood.Refresh(context.SortieResource.Wood.Priority);
            _moss.Refresh(context.SortieResource.Moss.Priority);
            _healGrass.Refresh(context.SortieResource.HealGrass.Priority);
        }

        private void UpdateWidgetPosition(Vector3 interestPointPosition)
        {
            _widgetRoot.anchoredPosition = _camera.WorldToScreenPoint(interestPointPosition) / Canvas.scaleFactor;

            var cameraPosition = _camera.transform.position;

            _widgetRoot.localScale = Vector3.one *
                                     (_scaleFactor /
                                      Vector2.Distance(
                                          new Vector2(cameraPosition.y, cameraPosition.z),
                                          new Vector2(interestPointPosition.y, interestPointPosition.z)));
        }

        private void StartSortie()
        {
            _currentContext.StartSortieCallback?.Invoke(_currentContext.SortieResource);
        }
    }

    public class InterestPointArgument
    {
        public string Name;

        public SortieResourceArgument SortieResource;

        public Vector3 Position;
        public Action<SortieResourceArgument> StartSortieCallback;
    }
}