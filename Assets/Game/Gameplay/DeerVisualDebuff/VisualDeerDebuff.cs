using System;
using System.Collections;
using System.Collections.Generic;
using Game.Gameplay;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class VisualDeerDebuff : MonoBehaviour
{
    public Image hungryImage, sickImage;
    
    public Color light, medium, hard;
    
    [Button]
    public void SetHungry(float value) // 0 - notHungry, 3 very hungry 0-1
    {
        switch (value)
        {
            case <= 0.6f :
                hungryImage.gameObject.SetActive(true);
                hungryImage.color = hard;
                break;
            case <= 0.8f :
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
    
    [Button]
    public void SetSick(DeerStatus status) // 0 - notHungry, 3 very hungry 0-1
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
