using Cinemachine;
using DG.Tweening;
using DoubleDCore.UI.Base;
using Game.Gameplay.Interaction;
using Game.UI;
using Game.UI.Pages;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Gameplay.Character
{
    public class CharacterFireplaceSittingController : MonoBehaviour
    {
        [Header("Camera Animation Settings")] 
        [SerializeField] private float _inDuration = 3;
        [SerializeField] private Vector3 _inPosition = new Vector3(0, 5, -3);
        [Space]
        [SerializeField] private float _outDuration = 1;
        private Vector3 _outPosition;
        
        private CharacterAnimatorController _characterAnimatorController;
        private LocalMenuOpener _localMenuOpener;
        private GameInput _inputController;
        private IUIManager _uiManager;
        private TriggerCanvas _firePlaceTriggerCanvas;
        private CinemachineVirtualCamera _characterCamera;

        private Tweener _currentTweener;

        [Inject]
        private void Init(CharacterAnimatorController characterAnimatorController, LocalMenuOpener localMenuOpener,
            GameInput inputController, IUIManager uiManager, TriggerCanvas firePlaceTriggerCanvas,
            CinemachineVirtualCamera characterCamera)
        {
            _characterAnimatorController = characterAnimatorController;
            _localMenuOpener = localMenuOpener;
            _inputController = inputController;
            _uiManager = uiManager;
            _firePlaceTriggerCanvas = firePlaceTriggerCanvas;
            _characterCamera = characterCamera;
        }
        
        //UNITY EVENT
        public void StartSitting()
        {
            _uiManager.ClosePage<PlayerMetricsPage>();
            _uiManager.ClosePage<ResourcePage>();
            _uiManager.ClosePage<QuestPage>();
            _uiManager.OpenPage<SittingPage>();
            
            if (_currentTweener != null && _currentTweener.IsActive())
                _currentTweener.Kill();

            var transposer = _characterCamera.GetCinemachineComponent<CinemachineTransposer>();
            _outPosition = transposer.m_FollowOffset;
            _currentTweener = DOTween.To(() => transposer.m_FollowOffset, x => transposer.m_FollowOffset = x, _inPosition, _inDuration);
            
            _characterAnimatorController.AnimateSitting();
            _firePlaceTriggerCanvas.SetActive(false);
            _localMenuOpener.enabled = false;
            _inputController.UI.CloseMenu.performed += OnEscapePerfomed;
        }
        
        private void StopSitting()
        {
            _uiManager.ClosePage<SittingPage>();
            _uiManager.OpenPage<PlayerMetricsPage>();
            _uiManager.OpenPage<ResourcePage>();
            _uiManager.OpenPage<QuestPage>();
            
            if (_currentTweener != null && _currentTweener.IsActive())
                _currentTweener.Kill();
            
            var transposer = _characterCamera.GetCinemachineComponent<CinemachineTransposer>();
            _currentTweener = DOTween.To(() => transposer.m_FollowOffset, x => transposer.m_FollowOffset = x, _outPosition, _outDuration);
            
            _characterAnimatorController.AnimateStanding();
            _firePlaceTriggerCanvas.SetActive(true);
            _localMenuOpener.enabled = true;
            
            _inputController.Player.Enable();
            _inputController.UI.Disable();
        }

        private void OnEscapePerfomed(InputAction.CallbackContext obj)
        {
            _inputController.UI.CloseMenu.performed -= OnEscapePerfomed;
            StopSitting();
        }
    }
}