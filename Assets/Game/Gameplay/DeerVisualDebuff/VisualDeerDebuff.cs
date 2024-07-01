using System;
using Game.Gameplay;
using UnityEngine;
using UnityEngine.UI;

public class VisualDeerDebuff : MonoBehaviour
{
    [SerializeField] private Deer _deer;

    public Image hungryImage, sickImage;
    
    public Color light, medium, hard;

    private void OnEnable()
    {
        _deer.Initialized += OnDeerInitialized;
        _deer.Died += OnDeerDied;
    }

    private void OnDisable()
    {
        _deer.Initialized -= OnDeerInitialized;
        _deer.Died -= OnDeerDied;
    }

    private void OnDeerInitialized(Deer deer)
    {
        _deer.DeerInfo.HungerChanged += SetHungry;
        _deer.DeerInfo.StatusChanged += SetSick;

        SetHungry(_deer.DeerInfo.HungerDegree);
        SetSick(_deer.DeerInfo.Status);
    }

    private void OnDeerDied(Deer deer)
    {
        _deer.DeerInfo.HungerChanged -= SetHungry;
        _deer.DeerInfo.StatusChanged -= SetSick;

        hungryImage.gameObject.SetActive(false);
        sickImage.gameObject.SetActive(false);
    }

    private void SetHungry(float value) // 0 - notHungry, 3 very hungry 0-1
    {
        switch (value)
        {
            case <= 0.5f :
                hungryImage.gameObject.SetActive(true);
                hungryImage.color = hard;
                break;
            case <= 0.7f :
                hungryImage.gameObject.SetActive(true);
                hungryImage.color = medium;
                break;       
            case <= 0.9f :
                hungryImage.gameObject.SetActive(true);
                hungryImage.color = light;
                break;
            default:
                hungryImage.gameObject.SetActive(false);
                break;
        }
    }
    
    private void SetSick(DeerStatus status) // 0 - notHungry, 3 very hungry 0-1
    {
        switch (status)
        {
            case DeerStatus.None:
                sickImage.gameObject.SetActive(false);
                break;
            case DeerStatus.Standard:
                sickImage.gameObject.SetActive(false);
                break;
            case DeerStatus.Sick:
                sickImage.gameObject.SetActive(true);
                sickImage.color = medium;
                break;
            case DeerStatus.VerySick:
                sickImage.gameObject.SetActive(true);
                sickImage.color = hard;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(status), status, null);
        }
        
    }
}
