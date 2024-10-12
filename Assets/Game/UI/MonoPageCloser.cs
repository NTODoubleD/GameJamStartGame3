using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class MonoPageCloser : MonoBehaviour
    {
        [SerializeField] private MonoPage _page;

        private IUIManager _uiManager;
        
        [Inject]
        private void Init(IUIManager uiManager)
        {
            _uiManager = uiManager;
        }
        
        public void Close()
        {
            _uiManager.ClosePage(_page);
        }
    }
}