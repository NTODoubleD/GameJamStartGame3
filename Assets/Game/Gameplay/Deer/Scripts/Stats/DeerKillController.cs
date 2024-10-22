using DoubleDCore.Service;
using DoubleDCore.UI.Base;
using Game.Gameplay.Configs;
using Game.UI.Pages;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Deers
{
    public class DeerKillController : MonoService
    {
        [SerializeField] private CharacterAnimatorController _characterAnimatorController;
        [SerializeField] private DeerKillConfig _deerKillConfig;

        private IUIManager _uiManager;
        
        [Inject]
        private void Init(IUIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public bool CanKill(Deer deer)
        {
            return deer.DeerInfo.Age != DeerAge.Young && deer.DeerInfo.IsDead == false;
        }

        public void Kill(Deer deer)
        {
            if (CanKill(deer))
            {
                _uiManager.OpenPage<ConfirmPage, ConfirmPageArgument>
                (new ConfirmPageArgument(_deerKillConfig.ConfirmTitle, _deerKillConfig.ConfirmMessage,
                    () => StartKilling(deer), () => CancelKilling(deer)));
            }
            else
            {
                Debug.LogError("CAN'T KILL THIS DEER");
            }
        }

        private void StartKilling(Deer deer)
        {
            _characterAnimatorController.AnimateKilling(() => ApplyKill(deer));
        }

        private void CancelKilling(Deer deer)
        {
            deer.EnterWalkingState();
        }

        private void ApplyKill(Deer deer)
        {
            deer.Kill();
        }
    }
}