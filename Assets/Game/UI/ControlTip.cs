using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class ControlTip : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private const float CloseDelay = 1f;

        public void Show()
        {
            _canvasGroup.DOFade(1, 1);
        }

        public async void Close()
        {
            await UniTask.WaitForSeconds(CloseDelay);

            _canvasGroup.DOFade(0, 1);
        }
    }
}