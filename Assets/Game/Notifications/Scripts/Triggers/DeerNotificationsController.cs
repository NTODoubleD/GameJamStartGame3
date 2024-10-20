using DoubleDCore.UI.Base;
using Game.Gameplay;
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

        private NotificationsPage _notificationsPage;

        public DeerNotificationsController(DeerFabric deerFabric, 
            DayCycleController dayCycleController, IUIManager uiManager, Herd herd)
        {
            _deerFabric = deerFabric;
            _dayCycleController = dayCycleController;
            _uiManager = uiManager;
            _herd = herd;

            _dayCycleController.DayEnded += SubscribeToDeer;
            _dayCycleController.DayStarted += ShowDeerHunger;
            _dayCycleController.DayStarted += ShowDeerIllnesses;
        }

        public void SubscribeToDeer()
        {
            _dayCycleController.DayEnded -= SubscribeToDeer;
            
            _deerFabric.Created += OnDeerBorn;
            
            foreach (var deer in _herd.CurrentHerd)
                deer.Died += OnDeerDied;
        }

        private void OnDeerBorn(Deer deer)
        {
            _notificationsPage = _uiManager.GetPage<NotificationsPage>();
            
            string name = deer.DeerInfo.Name;
            _notificationsPage.AddNotification(NotificationType.Info, "Рождение оленя", $"У вас родился олень {name}");
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
            else if (info.AgeDays == 7)
                descText = $"Ваш олень {name} умер от старости";
            else if (info.HungerDegree < 0.4f)
                descText = $"Ваш олень {name} умер от голода";
            else
                return;
            
            _notificationsPage.AddNotification(NotificationType.Death, "Смерть оленя", descText);
        }

        private void ShowDeerHunger()
        {
            _notificationsPage = _uiManager.GetPage<NotificationsPage>();
            bool showWarning = false;
            
            foreach (var deer in _herd.CurrentHerd)
            {
                if (Mathf.Approximately(deer.DeerInfo.HungerDegree, 0.4f))
                {
                    showWarning = true;
                    break;
                }
            }
            
            if (showWarning)
                _notificationsPage.AddNotification(NotificationType.Error, "Олени голодают", "Ваши олени голодают, если их не покормить, они умрут");
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
                _notificationsPage.AddNotification(NotificationType.Error, "Олени болеют", "Ваши сильно болеют, если их не вылечить, они умрут");
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