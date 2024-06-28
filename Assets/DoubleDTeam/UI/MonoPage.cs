using DoubleDTeam.Attributes;
using DoubleDTeam.UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace DoubleDTeam.UI
{
    [RequireComponent(typeof(Canvas)), RequireComponent(typeof(GraphicRaycaster))]
    public abstract class MonoPage : MonoBehaviour, IPage
    {
        [ReadOnlyProperty, SerializeField] private Canvas _canvas;
        [ReadOnlyProperty, SerializeField] private GraphicRaycaster _graphicRaycaster;

        protected bool PageIsDisplayed;
        public bool IsDisplayed => PageIsDisplayed;

        public Canvas Canvas => _canvas;
        public GraphicRaycaster GraphicRaycaster => _graphicRaycaster;

        protected virtual void OnValidate()
        {
            _canvas = GetComponent<Canvas>();
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
        }

        public virtual void Initialize()
        {
        }

        public virtual void Close()
        {
        }

        public virtual void Reset()
        {
        }

        protected void SetCanvasState(bool isActive)
        {
            PageIsDisplayed = isActive;

            GraphicRaycaster.enabled = isActive;
            Canvas.enabled = isActive;
        }
    }
}