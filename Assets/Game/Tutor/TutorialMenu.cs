using System;
using System.Collections;
using System.Collections.Generic;
using DoubleDCore.TranslationTools.Data;
using Game;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMenu : MonoBehaviour
{
    public List<Tutorial> Tutorials;

    public Image Image;

    public GameObject TutorPanel;
    
    private int _idCurrentTutorial;
    private int _idCurrentList;

    private void OnEnable()
    {
        Image.sprite = null;
        Image.gameObject.SetActive(false);
        TutorPanel.SetActive(false);
    }

    public void StartTutorial(int id)
    {
        if(id < 0 || id >= Tutorials.Count)
            return;
        _idCurrentTutorial = id;
        _idCurrentList = 0;
        UpdatePage();
    }


    public void Next()
    {
        if (_idCurrentList == Tutorials[_idCurrentTutorial].Sprites.Count - 1)
        {
            _idCurrentList = 0;
            UpdatePage();
            return;
        }

        _idCurrentList++;
        UpdatePage();
    }

    public void Previous()
    {
        if (_idCurrentList == 0)
        {
            _idCurrentList = Tutorials[_idCurrentTutorial].Sprites.Count - 1;
            UpdatePage();
            return;
        }

        _idCurrentList--;
        UpdatePage();
    }
    
    public void UpdatePage()
    {
        TutorPanel.SetActive(true);
        Image.gameObject.SetActive(true);
        Image.sprite = Tutorials[_idCurrentTutorial].Sprites[_idCurrentList].GetSprite();
    }
}

[Serializable]
public class Tutorial
{
    public List<TranslatableTutorialPart> Sprites;
}
[Serializable]
public class TranslatableTutorialPart
{
    public Sprite en;
    public Sprite ru;

    public Sprite GetSprite() => 
        StaticLanguageProvider.GetLanguage() == LanguageType.Ru ? ru : en;
}