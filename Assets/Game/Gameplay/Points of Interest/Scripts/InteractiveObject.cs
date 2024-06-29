using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private GameObject _name;
    [SerializeField] private GameObject _buttonToInteract;
    [SerializeField] private GameObject _outlineCopy;

    public void EnableHighlight()
    {
        _outlineCopy.SetActive(true);
        _name.SetActive(true);
    }

    public void DisableHighlight()
    {
        _outlineCopy.SetActive(false);
        _name.SetActive(false);
    }

    public void ShowInteractButton()
    {
        _buttonToInteract.SetActive(true);
    }

    public void HideInteractButton()
    {
        _buttonToInteract.SetActive(false);
    }
}