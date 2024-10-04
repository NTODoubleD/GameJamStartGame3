using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIFuelView : MonoBehaviour
    {
        [SerializeField] private Button _addFuelButton;
        [SerializeField] private TMP_Text _timeLeft;

        public event Action AddFuelRequested;

        public void SetButtonInteractable(bool isInteractable)
        {
            _addFuelButton.interactable = isInteractable;
        }

        public void SetTimeLeft(float timeLeft)
        {
            _timeLeft.text = Mathf.RoundToInt(timeLeft).ToString();
        }
    }
}