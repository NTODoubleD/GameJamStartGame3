using System;
using Cinemachine;
using DG.Tweening;
using DoubleDCore.UI.Base;
using Game.Gameplay.CharacterCamera;
using Game.Gameplay.Interaction;
using Game.UI;
using Game.UI.Pages;
using Game.Сompass;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Gameplay.Character
{
    public class CharacterFireplaceSittingController : MonoBehaviour
    {
        [Header("Camera Animation Settings")] [SerializeField]
        private float _inDuration = 3;

        [SerializeField] private Vector3 _inPosition = new Vector3(0, 5, -3);
        [Space] [SerializeField] private float _outDuration = 1;

        [SerializeField] private AnimatedAudio _fireplaceSoundSource;
        [SerializeField] private AnimatedAudio _mainThemeSoundSource;

        private Vector3 _outPosition;
        private bool _previousCompassActiveState;

        private CharacterAnimatorController _characterAnimatorController;
        private LocalMenuOpener _localMenuOpener;
        private GameInput _inputController;
        private IUIManager _uiManager;
        private TriggerCanvas _firePlaceTriggerCanvas;
        private CinemachineVirtualCamera _characterCamera;
        private CameraZoomController _cameraZoomController;
        private CompassController _compassController;

        private Tweener _currentTweener;

        [Inject]
        private void Init(CharacterAnimatorController characterAnimatorController, LocalMenuOpener localMenuOpener,
            GameInput inputController, IUIManager uiManager, TriggerCanvas firePlaceTriggerCanvas,
            CinemachineVirtualCamera characterCamera, CameraZoomController cameraZoomController, 
            CompassController compassController)
        {
            _characterAnimatorController = characterAnimatorController;
            _localMenuOpener = localMenuOpener;
            _inputController = inputController;
            _uiManager = uiManager;
            _firePlaceTriggerCanvas = firePlaceTriggerCanvas;
            _characterCamera = characterCamera;
            _cameraZoomController = cameraZoomController;
            _compassController = compassController;
        }

        //UNITY EVENT
        public void StartSitting()
        {
            _cameraZoomController.IsEnabled = false;
            _compassController.IsBlocked = true;
            
            _uiManager.ClosePage<PlayerMetricsPage>();
            _uiManager.ClosePage<ResourcePage>();
            _uiManager.ClosePage<QuestPage>();
            _uiManager.OpenPage<SittingPage>();

            if (_currentTweener != null && _currentTweener.IsActive())
                _currentTweener.Kill();

            var transposer = _characterCamera.GetCinemachineComponent<CinemachineTransposer>();
            _outPosition = transposer.m_FollowOffset;

            _currentTweener = DOTween
                .To(() => transposer.m_FollowOffset, x => transposer.m_FollowOffset = x,
                    _inPosition, _inDuration);

            _characterAnimatorController.AnimateSitting();
            _firePlaceTriggerCanvas.SetActive(false);
            _localMenuOpener.enabled = false;
            _inputController.UI.CloseMenu.performed += OnEscapePerfomed;

            _fireplaceSoundSource.AudioSource.Play();
            _fireplaceSoundSource.AudioSource
                .DOFade(_fireplaceSoundSource.TargetVolume, _fireplaceSoundSource.FadeDuration);

            _mainThemeSoundSource.AudioSource
                .DOFade(0, _mainThemeSoundSource.FadeDuration)
                .OnComplete(_mainThemeSoundSource.AudioSource.Stop);
        }

        private void StopSitting()
        {
            _compassController.IsBlocked = false;
            
            _uiManager.ClosePage<SittingPage>();
            _uiManager.OpenPage<PlayerMetricsPage>();
            _uiManager.OpenPage<ResourcePage>();
            _uiManager.OpenPage<QuestPage>();

            if (_currentTweener != null && _currentTweener.IsActive())
                _currentTweener.Kill();

            var transposer = _characterCamera.GetCinemachineComponent<CinemachineTransposer>();
            _currentTweener = DOTween
                .To(() => transposer.m_FollowOffset, x => transposer.m_FollowOffset = x,
                    _outPosition, _outDuration).OnComplete(() => { _cameraZoomController.IsEnabled = true; });

            _characterAnimatorController.AnimateStanding();
            _firePlaceTriggerCanvas.SetActive(true);
            _localMenuOpener.enabled = true;

            _inputController.Player.Enable();
            _inputController.UI.Disable();

            _fireplaceSoundSource.AudioSource
                .DOFade(0, _fireplaceSoundSource.FadeDuration)
                .OnComplete(_fireplaceSoundSource.AudioSource.Stop);

            _mainThemeSoundSource.AudioSource.Play();
            _mainThemeSoundSource.AudioSource
                .DOFade(_mainThemeSoundSource.TargetVolume, _mainThemeSoundSource.FadeDuration);
        }

        private void OnEscapePerfomed(InputAction.CallbackContext obj)
        {
            _inputController.UI.CloseMenu.performed -= OnEscapePerfomed;
            StopSitting();
        }

        [Serializable]
        private class AnimatedAudio
        {
            [field: SerializeField] public AudioSource AudioSource { get; private set; }
            [field: SerializeField] public float TargetVolume { get; private set; } = 0.5f;
            [field: SerializeField] public float FadeDuration { get; private set; } = 2;
        }
    }
}