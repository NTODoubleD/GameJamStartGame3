using System.Collections.Generic;
using DoubleDTeam.Containers;
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
        private InputController _inputManager;

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

        public void Initialize(int deerCapacity, int resourcesCapacity, int currentDeerCount)
        {
            Debug.Log($"INITIALIZED {deerCapacity} {resourcesCapacity} {currentDeerCount}");
        }

        public event UnityAction<IReadOnlyDictionary<ItemInfo, float>, int> Sended;
    }
}