using System;
using DoubleDCore.Service;
using UnityEngine;
using UnityEngine.UI;

public class BloodOption : MonoService
{
    private const string PREFS_KEY = "BloodActive";
    
    [SerializeField] private Toggle _bloodToggle;

    public bool IsActive
    {
        get
        {
            return Convert.ToBoolean(PlayerPrefs.GetInt(PREFS_KEY, 1));
        }

        private set
        {
            PlayerPrefs.SetInt(PREFS_KEY, Convert.ToInt32(value));
        }
    }

    public event Action<bool> ValueChanged;

    private void Awake()
    {
        _bloodToggle.isOn = IsActive;
    }

    private void OnEnable()
    {
        _bloodToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnDisable()
    {
        _bloodToggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool newValue)
    {
        IsActive = newValue;
        ValueChanged?.Invoke(newValue);
    }
}