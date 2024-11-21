using DoubleDCore.UI.Base;
using Game.Gameplay;
using Game.Gameplay.Configs;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts;
using Game.UI.Pages;
using UnityEngine;

namespace Game.Notifications.Triggers
{
    public class DeerNotificationsController
    {
        private readonly DeerFabric _deerFabric;
        private readonly DayCycleController _dayCycleController;
        private readonly IUIManager _uiManager;
        private readonly Herd _herd;
        private readonly DeerHungerConfig _deerHungerConfig;
        private readonly DeerAgeConfig _deerAgeConfig;

        private NotificationsPage _notificationsPage;

        public DeerNotificationsController(DeerFabric deerFabric, 
            DayCycleController dayCycleController, IUIManager uiManager, Herd herd,
            DeerHungerConfig deerHungerConfig, DeerAgeConfig deerAgeConfig)
        {
            _deerFabric = deerFabric;
            _dayCycleController = dayCycleController;
            _uiManager = uiManager;
            _herd = herd;
            _deerHungerConfig = deerHungerConfig;
            _deerAgeConfig = deerAgeConfig;

            _dayCycleController.DayEnded += SubscribeToDeer;
            _dayCycleController.DayStarted += UnsubscribeFromDeer;
            _dayCycleController.DayStarted += ShowDeerHunger;
            _dayCycleController.DayStarted += ShowDeerIllnesses;
        }

        private void UnsubscribeFromDeer()
        {
            _deerFabric.Created -= OnDeerBorn;
            
            foreach (var deer in _herd.CurrentHerd)
                deer.Died -= OnDeerDied;
        }

        private void SubscribeToDeer()
        {
            _deerFabric.Created += OnDeerBorn;
            
            foreach (var deer in _herd.CurrentHerd)
                deer.Died += OnDeerDied;
        }

        private void OnDeerBorn(Deer deer)
        {
            _notificationsPage = _uiManager.GetPage<NotificationsPage>();
            
            string name = deer.DeerInfo.Name;
            _notificationsPage.AddNotification(NotificationType.Info, $"{name} появился на свет", $"У вас родился олень {name}");
        }

        private void OnDeerDied(Deer deer)
        {
            deer.Died -= OnDeerDied;
            _notificationsPage = _uiManager.GetPage<NotificationsPage>();
            
            DeerInfo info = deer.DeerInfo;
            string name = info.Name;
            string descText;

            if (info.StatusBeforeDeath == DeerStatus.VerySick)
                descText = $"Ваш олень {name} умер от болезни";
            else if (info.AgeDays == _deerAgeConfig.AgeTable[DeerAge.None])
                descText = $"Ваш олень {name} умер от старости";
            else if (info.HungerDegree < _deerHungerConfig.MinimalHungerDegree)
                descText = $"Ваш олень {name} умер от голода";
            else
                return;

            _notificationsPage.AddNotification(NotificationType.Death, $"{name} ушёл из жизни", descText);
        }

        private void ShowDeerHunger()
        {
            _notificationsPage = _uiManager.GetPage<NotificationsPage>();
            bool showWarning = false;
            
            foreach (var deer in _herd.CurrentHerd)
            {
                if (Mathf.Approximately(deer.DeerInfo.HungerDegree, _deerHungerConfig.MinimalHungerDegree))
                {
                    showWarning = true;
                    break;
                }
            }
            
            if (showWarning)
                _notificationsPage.AddNotification(NotificationType.Error, "Стадо голодает", "Ваши олени голодают, если их не покормить, они умрут");
        }

        private void ShowDeerIllnesses()
        {
            _notificationsPage = _uiManager.GetPage<NotificationsPage>();
            bool showWarning = false;
            
            foreach (var deer in _herd.CurrentHerd)
            {
                if (deer.DeerInfo.Status == DeerStatus.VerySick)
                {
                    showWarning = true;
                    break;
                }
            }
            
            if (showWarning)
                _notificationsPage.AddNotification(NotificationType.Error, "Стадо болеет", "Ваши сильно болеют, если их не вылечить, они умрут");
        }

        ~DeerNotificationsController()
        {
            _deerFabric.Created -= OnDeerBorn;
            _dayCycleController.DayEnded -= SubscribeToDeer;
            _dayCycleController.DayStarted -= ShowDeerHunger;
            _dayCycleController.DayStarted -= ShowDeerIllnesses;
            
            foreach (var deer in _herd.CurrentHerd)
                deer.Died -= OnDeerDied;
        }
    }
}