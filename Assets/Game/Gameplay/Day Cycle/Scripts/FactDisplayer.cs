using System.Collections.Generic;
using System.Linq;
using DoubleDCore.TranslationTools.Data;
using TMPro;
using UnityEngine;

namespace Game.Gameplay.DayCycle
{
    public class FactDisplayer : MonoBehaviour
    {
        [SerializeField] private FactsListConfig _config;
        [SerializeField] private TMP_Text _fact;

        private List<string> _factsLeft = new();

        private void RefreshFactList()
        {
            _factsLeft = _config.Facts.ToList();
        }

        public void DisplayRandomFact()
        {
            if (StaticLanguageProvider.GetLanguage() != LanguageType.Ru)
            {
                _fact.gameObject.SetActive(false);
                return;
            }

            if (_factsLeft.Count == 0)
                RefreshFactList();

            int factIndex = Random.Range(0, _factsLeft.Count);
            string fact = _factsLeft[factIndex];
            _factsLeft.RemoveAt(factIndex);

            _fact.text = fact;
        }
    }
}