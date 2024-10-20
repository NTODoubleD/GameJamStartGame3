using System;
using System.Collections.Generic;
using DoubleDCore.UI;
using DoubleDCore.UI.Base;
using Game.Notifications;
using UnityEngine;
using Zenject;

namespace Game.UI.Pages
{
    public class NotificationsPage : MonoPage, IUIPage
    {
        [SerializeField] private Transform _notificationsRoot;
        [SerializeField] private UINotification _notificationPrefab;

        private readonly Queue<UINotification> _notificationsToOpen = new();
        private readonly HashSet<UINotification> _notifications = new();
        private readonly HashSet<UINotification> _notificationsToRemove = new();
        private readonly Dictionary<UINotification, float> _notificationsTimeLeft = new();

        private NotificationsConfig _config;
        private float _currentOpenDelay;
        
        [Inject]
        private void Init(NotificationsConfig config)
        {
            _config = config;
        }

        public override void Initialize()
        {
            Open();
        }

        public void Open()
        {
            SetCanvasState(true);
        }

        public override void Close()
        {
            SetCanvasState(false);
        }

        private void Update()
        {
            if (IsDisplayed == false)
                return;
            
            float deltaTime = Time.deltaTime;
            _currentOpenDelay = Mathf.Max(0, _currentOpenDelay - deltaTime);

            if (Mathf.Approximately(0, _currentOpenDelay) && _notificationsToOpen.Count > 0)
            {
                var notification = _notificationsToOpen.Dequeue();
                notification.Open();
                
                _notifications.Add(notification);
                _notificationsTimeLeft[notification] = _config.ExpireTime;
                
                _currentOpenDelay = _config.OpenDelay;
            }
            
            _notificationsToRemove.Clear();

            foreach (var notification in _notifications)
            {
                _notificationsTimeLeft[notification] -= deltaTime;

                if (_notificationsTimeLeft[notification] <= 0)
                    _notificationsToRemove.Add(notification);
            }

            foreach (var notification in _notificationsToRemove)
                Remove(notification);
        }

        public void AddNotification(NotificationType type, string title, string description)
        {
            var typeSettings = _config.GetSettings(type);
            UINotification instance = Instantiate(_notificationPrefab, _notificationsRoot);

            instance.transform.SetSiblingIndex(0);
            instance.Initialize(typeSettings.Color, typeSettings.Icon, title, description);
            instance.CloseRequested += Remove;
            
            _notificationsToOpen.Enqueue(instance);
        }

        private void Remove(UINotification notification)
        {
            notification.CloseRequested -= Remove;
            _notifications.Remove(notification);
            _notificationsTimeLeft.Remove(notification);
            notification.Close();
        }
    }
}