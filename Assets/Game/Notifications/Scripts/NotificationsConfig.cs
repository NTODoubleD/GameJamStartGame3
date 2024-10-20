using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Notifications
{
    [CreateAssetMenu(fileName = "Notifications Config", menuName = "Configs/Notifications")]
    public class NotificationsConfig : SerializedScriptableObject
    {
        [OdinSerialize] private float _expireTime;
        [OdinSerialize] private float _openDelay;
        [OdinSerialize] private Dictionary<NotificationType, NotificationTypeSettings> _typesSettings = new();

        public float ExpireTime => _expireTime;
        public float OpenDelay => _openDelay;

        public NotificationTypeSettings GetSettings(NotificationType type)
        {
            return _typesSettings[type];
        }
    }

    [Serializable]
    public class NotificationTypeSettings
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private Color _color;

        public Sprite Icon => _icon;
        public Color Color => _color;
    }
}