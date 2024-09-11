using UnityEngine;
using UnityEngine.UI;

namespace DoubleDCore.UI.Base
{
    public interface IPage
    {
        public bool IsDisplayed { get; }

        public Canvas Canvas { get; }

        public GraphicRaycaster GraphicRaycaster { get; }

        public void Initialize();

        public void Close();

        public void Reset();
    }
}