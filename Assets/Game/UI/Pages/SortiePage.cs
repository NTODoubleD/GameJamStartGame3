using System.Collections.Generic;
using System.Linq;
using DoubleDTeam.Containers;
using DoubleDTeam.Extensions;
using DoubleDTeam.InputSystem;
using DoubleDTeam.UI;
using DoubleDTeam.UI.Base;
using Game.Gameplay.Sleigh;
using Game.Infrastructure.Items;
using Game.InputMaps;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UI.Pages
{
    public class SortiePage : MonoPage, IUIPage, ISleighSendView
    {
        [SerializeField] private UIResourceProperty _prefab;
        [SerializeField] private Transform _resourcePropertyContainer;

        [Space, SerializeField] private SleighSendController _sleighSendController;
        [SerializeField] private UIResourceProperty _chooseDeerAmountSlider;

        [Space, SerializeField] private int _freePointsAmount = 4;

        private InputController _inputManager;

        private readonly List<UIResourceProperty> _resourceSliders = new();
        private List<ItemInfo> _possibleResources;

        private void Awake()
        {
            _inputManager = Services.ProjectContext.GetModule<InputController>();

            Close();
        }

        public void Open()
        {
            _inputManager.EnableMap<UIInputMap>();

            SetCanvasState(true);
        }

        public override void Close()
        {
            SetCanvasState(false);

            _inputManager.EnableMap<PlayerInputMap>();
        }

        public void StartSortie()
        {
            Close();

            var callback = new Dictionary<ItemInfo, int>();

            for (int i = 0; i < _possibleResources.Count; i++)
                callback.Add(_possibleResources[i], _resourceSliders[i].GetResourceAmount());

            Sended?.Invoke(callback, _chooseDeerAmountSlider.GetResourceAmount());
        }

        public void Initialize(int deerCapacity, int currentDeerCount, IEnumerable<ItemInfo> possibleResources,
            int levelsToDistribute)
        {
            _chooseDeerAmountSlider.Refresh("", Mathf.Min(deerCapacity, currentDeerCount));

            var itemInfos = possibleResources as ItemInfo[] ?? possibleResources.ToArray();

            if (_resourceSliders.Count <= 0)
                CreateSliders(itemInfos);

            for (int i = 0; i < _resourceSliders.Count; i++)
            {
                var item = itemInfos[i];
                _resourceSliders[i].Refresh(item.Name,
                    _sleighSendController.GetResourcesLimitLevel(item, _chooseDeerAmountSlider.GetResourceAmount()));
            }
        }

        private void CreateSliders(IEnumerable<ItemInfo> possibleResources)
        {
            _possibleResources = new List<ItemInfo>(possibleResources);

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
            if (_resourceSliders.Sum(r => r.GetResourceAmount()) <= _freePointsAmount)
                return;

            var temp = new List<UIResourceProperty>(_resourceSliders);

            temp.Remove(uiResourceProperty);
            temp.Remove(t => t.GetResourceAmount() <= 0);

            var randomResource = temp.Choose();

            randomResource.ChangeValue(randomResource.GetResourceAmount() - 1);
        }


        public event UnityAction<IReadOnlyDictionary<ItemInfo, int>, int> Sended;
    }
}