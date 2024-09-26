using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(LayoutElement)), RequireComponent(typeof(TMP_Text))]
    public class PreferredSizeSyncer : MonoBehaviour
    {
        private TMP_Text _text;
        private LayoutElement _layoutElement;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _layoutElement = GetComponent<LayoutElement>();
        }

        private void Update()
        {
            _layoutElement.preferredWidth = _text.preferredWidth;
            _layoutElement.preferredHeight = _text.preferredHeight;
        }
    }
}