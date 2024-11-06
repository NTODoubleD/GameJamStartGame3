using System.Collections.Generic;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.WorldMap;
using UnityEngine;

namespace Game.UI.Pages
{
    public class WorldInterestPointPage : MonoPage, IPayloadPage<InterestPointArgument>
    {
        [SerializeField] private RectTransform _widgetRoot;
        [SerializeField] private InterestPointWidget _prefab;
        [Range(0.01f, 500f), SerializeField] private float _scaleFactor;
        [SerializeField] private float _worldWidgetOffsetDistance = 10f;

        private Camera _camera;

        private readonly List<InterestPointWidget> _pool = new();

        public override void Initialize()
        {
            _camera = Camera.main;

            for (int i = 0; i < 10; i++)
            {
                var widget = Instantiate(_prefab, _widgetRoot);
                widget.SetActive(false);

                _pool.Add(widget);
            }

            SetCanvasState(false);
        }

        private InterestPointArgument _currentContext;

        public void Open(InterestPointArgument context)
        {
            _currentContext = context;

            for (int i = 0; i < context.Points.Count; i++)
            {
                var point = context.Points[i];

                var widget = _pool[i];
                widget.SetInfo(point);
                widget.SetActive(true);
            }

            SetCanvasState(true);
        }

        public override void Close()
        {
            foreach (var interestPointWidget in _pool)
                interestPointWidget.SetActive(false);

            SetCanvasState(false);
        }

        private void LateUpdate()
        {
            if (!PageIsDisplayed)
                return;

            for (int i = 0; i < _currentContext.Points.Count; i++)
            {
                var point = _currentContext.Points[i];
                var widget = _pool[i];

                UpdateWidgetPosition(point.Position, widget);
            }
        }

        private void UpdateWidgetPosition(Vector3 interestPointPosition, InterestPointWidget target)
        {
            Vector3 widgetPosition = interestPointPosition + new Vector3(0, 0, -_worldWidgetOffsetDistance);

            target.RectTransform.anchoredPosition =
                _camera.WorldToScreenPoint(widgetPosition) / Canvas.scaleFactor;

            var cameraPosition = _camera.transform.position;

            float scale = _scaleFactor / Vector2.Distance(
                new Vector2(cameraPosition.y, cameraPosition.z),
                new Vector2(widgetPosition.y, widgetPosition.z));

            target.RectTransform.localScale = Vector3.one * scale;
        }
    }

    public class InterestPointArgument
    {
        public IReadOnlyList<PointInfo> Points;
    }

    public class PointInfo
    {
        public string Name;

        public SortieResourceArgument SortieResource;
        public int SleighLevel;

        public Vector3 Position;
    }
}