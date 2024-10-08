using System.Collections.Generic;
using System.Linq;
using DoubleDCore.Extensions;
using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Gameplay.Sleigh;
using Game.Infrastructure.Items;
using Game.WorldMap;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Pages
{
    public class SortiePage : MonoPage, IUIPage, ISleighSendView
    {
        [SerializeField] private UIResourceProperty _prefab;
        [SerializeField] private Transform _resourcePropertyContainer;

        [SerializeField] private HerdExplorer _herdExplorer;
        [SerializeField] private Button _startSortieButton;

        [Space, SerializeField] private SleighSendController _sleighSendController;

        [Space, SerializeField] private int _freePointsAmount = 4;
        [SerializeField] private TMP_Text _dearAmountText;
        [SerializeField] private TMP_Text _freePointText;
        [SerializeField] private Button _continueClickButton;

        [Space, SerializeField] private RectTransform _herdUI;
        [SerializeField] private RectTransform _sliderUI;

        public event UnityAction<IReadOnlyDictionary<ItemInfo, int>, int> Sended;

        private GameInput _inputController;

        private readonly List<UIResourceProperty> _resourceSliders = new();

        private int _deerCapacity;
        private List<ItemInfo> _possibleResources;

        [Inject]
        private void Init(GameInput inputController)
        {
            _inputController = inputController;
        }

        public override void Initialize()
        {
            SetCanvasState(false);
        }

        public void Open()
        {
            SwitchSubMenu(false);

            _inputController.Player.Disable();
            _inputController.Map.Disable();

            _inputController.UI.Enable();

            _continueClickButton.onClick.AddListener(StartSortie);

            _herdExplorer.Reset();
            _herdExplorer.ChosenChanged += OnUserChosenChanged;

            foreach (var resourceProperty in _resourceSliders)
                resourceProperty.ChangeValue(0);

            OnUserChosenChanged();

            SetCanvasState(true);
        }

        public override void Close()
        {
            SetCanvasState(false);

            _continueClickButton.onClick.RemoveListener(StartSortie);

            _herdExplorer.ChosenChanged -= OnUserChosenChanged;

            _inputController.UI.Disable();

            _inputController.Map.Enable();
            //_inputController.Player.Enable();
        }

        public void StartSortie()
        {
            Close();

            var callback = new Dictionary<ItemInfo, int>();

            // for (int i = 0; i < _possibleResources.Count; i++)
            //     callback.Add(_possibleResources[i], _resourceSliders[i].GetResourceAmount());

            callback.Add(_currenSortieContext.Wood.Item, _currenSortieContext.Wood.Priority);
            callback.Add(_currenSortieContext.Moss.Item, _currenSortieContext.Moss.Priority);
            callback.Add(_currenSortieContext.HealGrass.Item, _currenSortieContext.HealGrass.Priority);

            Sended?.Invoke(callback, _herdExplorer.GetChosenDeerAmount());
        }

        public void Initialize(int deerCapacity, int currentDeerCount, IEnumerable<ItemInfo> possibleResources,
            int levelsToDistribute)
        {
            _deerCapacity = deerCapacity;
            _possibleResources = new List<ItemInfo>(possibleResources as ItemInfo[] ?? possibleResources.ToArray());

            if (_resourceSliders.Count <= 0)
                CreateSliders();

            for (int i = 0; i < _resourceSliders.Count; i++)
            {
                var item = _possibleResources[i];
                _resourceSliders[i].Refresh(item.Name, _sleighSendController.GetResourcesLimitLevel(item, 0));
            }
        }

        private readonly TranslatedText _dearAmountTranslate = new("{0} из {1}", "{0} of {1}");

        private void OnUserChosenChanged()
        {
            int chosenAmount = _herdExplorer.GetChosenDeerAmount();

            _dearAmountText.text = string.Format(_dearAmountTranslate.GetText(), chosenAmount, _deerCapacity);

            if (chosenAmount == _deerCapacity)
                _dearAmountText.text = _dearAmountText.text.Color(Color.green);

            if (chosenAmount == 0)
                _dearAmountText.text = _dearAmountText.text.Color(Color.red);

            if (chosenAmount == _deerCapacity)
                _herdExplorer.DisableAllActive();
            else
                _herdExplorer.UpdateDeerAvailability();

            UpdateSortieButtonState();

            for (int i = 0; i < _resourceSliders.Count; i++)
            {
                var item = _possibleResources[i];
                _resourceSliders[i].Refresh(item.Name,
                    _sleighSendController.GetResourcesLimitLevel(item, _herdExplorer.GetChosenDeerAmount()));
            }
        }

        private readonly TranslatedText _freePointTranslate = new("Свободные очки: {0}", "Free points: {0}");

        private void UpdateSortieButtonState()
        {
            int chosenDeerAmount = _herdExplorer.GetChosenDeerAmount();
            int chosenResourceAmount = _resourceSliders.Sum(r => r.GetResourceAmount());

            bool completeDeerCondition = chosenDeerAmount > 0;
            bool completeResourceCondition = chosenResourceAmount == _freePointsAmount;

            _continueClickButton.interactable = completeDeerCondition;
            _startSortieButton.interactable = completeDeerCondition && completeResourceCondition;

            string freePointText =
                string.Format(_freePointTranslate.GetText(), _freePointsAmount - chosenResourceAmount);

            _freePointText.text = completeResourceCondition
                ? freePointText.Color(Color.green)
                : freePointText.Color(Color.red);
        }

        private void CreateSliders()
        {
            foreach (var _ in _possibleResources)
            {
                var inst =
                    Instantiate(_prefab, Vector3.one, Quaternion.identity, _resourcePropertyContainer);

                inst.ValueChanged += SliderOnValueChanged;

                _resourceSliders.Add(inst);
            }
        }

        private void SliderOnValueChanged(UIResourceProperty uiResourceProperty)
        {
            UpdateSortieButtonState();

            if (_resourceSliders.Sum(r => r.GetResourceAmount()) <= _freePointsAmount)
                return;

            var temp = new List<UIResourceProperty>(_resourceSliders);

            temp.Remove(uiResourceProperty);
            temp.Remove(t => t.GetResourceAmount() <= 0);

            var randomResource = temp.Choose();

            randomResource.ChangeValue(randomResource.GetResourceAmount() - 1);
        }

        private void ContinueOnClicked()
        {
            SwitchSubMenu(true);
        }

        private void SwitchSubMenu(bool isSlider)
        {
            _herdUI.gameObject.SetActive(isSlider == false);
            _continueClickButton.gameObject.SetActive(isSlider == false);

            _sliderUI.gameObject.SetActive(isSlider);
            _startSortieButton.gameObject.SetActive(isSlider);
        }

        private SortieResourceArgument _currenSortieContext;

        public void SetResourcePriorities(SortieResourceArgument context)
        {
            _currenSortieContext = context;
        }
    }
}