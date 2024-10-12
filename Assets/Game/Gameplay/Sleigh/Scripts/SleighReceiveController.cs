using Game.Gameplay.DayCycle;
using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using System.Collections.Generic;
using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using DoubleDCore.UI.Base;
using Game.Gameplay.Items;
using Game.UI.Pages;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Sleigh
{
    public class SleighReceiveController : MonoBehaviour
    {
        [SerializeField] private DayCycleController _dayCycleController;

        private IReadOnlyDictionary<GameItemInfo, int> _currentResult;

        private IUIManager _uiManager;
        private ItemStorage _itemStorage;
        
        private readonly TranslatedText _labelText = new("Результат вылазки", "Expedition Result");
        private readonly TranslatedText _headingText = new("Получено ресурсов:\n", "Resources Obtained:\n");
        
        [Inject]
        private void Init(IUIManager uiManager, ItemStorage itemStorage)
        {
            _uiManager = uiManager;
            _itemStorage = itemStorage;
        }

        private void OnEnable()
        {
            _dayCycleController.DayStarted += ShowReceivedSleigh;
        }

        private void OnDisable()
        {
            _dayCycleController.DayStarted -= ShowReceivedSleigh;
        }

        private void ShowReceivedSleigh()
        {
            if (_currentResult == null)
                return;

            foreach (var keyPair in _currentResult)
            {
                _itemStorage.AddItems(keyPair.Key, keyPair.Value);
                Debug.Log($"RECEIVED {keyPair.Key.Name} {keyPair.Value}");
            }

            _uiManager.OpenPage<ResourceWatcherPage, ResourcePageArgument>(new ResourcePageArgument()
            {
                Label = _labelText.GetText(),
                TextHeading = _headingText.GetText(),
                Resource = new Dictionary<GameItemInfo, int>(_currentResult)
            });

            _currentResult = null;
        }

        public void SetReceiveInfo(IReadOnlyDictionary<GameItemInfo, int> result)
        {
            _currentResult = result;
        }
    }
}