using UnityEngine;
using UnityEngine.UI;

public class AlphaSpriteHameleon : MonoBehaviour
{
    [SerializeField] private Image _hameleon;
    [SerializeField] private Button _button;

    private void Update()
    {
        if (!_button.interactable && _hameleon.color.a == 1f)
        {
            _hameleon.color = new Color(_hameleon.color.r, _hameleon.color.g, _hameleon.color.b, 0f);
        }
        else if (_button.interactable && _hameleon.color.a == 0f)
        {
            _hameleon.color = new Color(_hameleon.color.r, _hameleon.color.g, _hameleon.color.b, 1f);
        }
    }
}