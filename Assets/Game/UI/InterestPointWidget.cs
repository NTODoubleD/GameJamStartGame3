using Game.UI.Pages;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class InterestPointWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;

        [Space, SerializeField] private UIResource _wood;
        [SerializeField] private UIResource _moss;
        [SerializeField] private UIResource _healGrass;

        public RectTransform RectTransform => transform as RectTransform;

        public void SetInfo(PointInfo pointInfo)
        {
            _nameText.text = pointInfo.Name;

            _wood.Refresh(pointInfo.SortieResource.Wood.GetCount(pointInfo.SleighLevel));
            _moss.Refresh(pointInfo.SortieResource.Moss.GetCount(pointInfo.SleighLevel));
            _healGrass.Refresh(pointInfo.SortieResource.HealGrass.GetCount(pointInfo.SleighLevel));
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}