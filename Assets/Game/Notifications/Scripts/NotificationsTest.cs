using System;
using DoubleDCore.UI.Base;
using Game.UI.Pages;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Notifications
{
    public class NotificationsTest : MonoBehaviour
    {
        private NotificationsPage _page;
        private IUIManager _uiManager;

        [Inject]
        private void Init(IUIManager uiManager)
        {
            _uiManager = uiManager;
        }

        private void Start()
        {
            _page = _uiManager.GetPage<NotificationsPage>();
        }

        [Button]
        private void AddTestInfo()
        {
            AddInfo("Тестовая инфа", "Описание тестовой инфы");
        }
        
        [Button]
        private void AddTestWarning()
        {
            AddWarning("Тестовое предупреждение", "Описание тестового предупреждения");
        }
        
        [Button]
        private void AddTestError()
        {
            AddError("Тестовая ошибка", "Описание тестовой ошибки!");
        }
        
        [Button]
        private void AddTestDeath()
        {
            AddDeath("Тестовая смерть", "Кто-то умер...");
        }
        
        [PropertySpace]

        [Button]
        private void AddInfo(string title, string text)
        {
            _page.AddNotification(NotificationType.Info, title, text);
        }
        
        [Button]
        private void AddWarning(string title, string text)
        {
            _page.AddNotification(NotificationType.Warning, title, text);
        }
        
        [Button]
        private void AddError(string title, string text)
        {
            _page.AddNotification(NotificationType.Error, title, text);
        }
        
        [Button]
        private void AddDeath(string title, string text)
        {
            _page.AddNotification(NotificationType.Death, title, text);
        }
    }
}