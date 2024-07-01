using System;

namespace Game.Gameplay
{
    public class DeerInfo
    {
        public string Name;
        public GenderType Gender;
        public DeerAge Age;
        public bool IsDead;

        private float _hungerDegree;
        private DeerStatus _status;

        public float HungerDegree
        {
            get
            {
                return _hungerDegree;
            }

            set 
            { 
                _hungerDegree = value;
                HungerChanged?.Invoke(value);
            }
        }

        public DeerStatus Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
                StatusChanged?.Invoke(value);
            }
        }

        public event Action<float> HungerChanged;
        public event Action<DeerStatus> StatusChanged;
    }

    public static class DeerInfoExtensions
    {
        public static string ToText(this DeerStatus status)
        {
            return status switch
            {
                DeerStatus.None => "Нет",
                DeerStatus.Standard => "Норма",
                DeerStatus.Sick => "Болеет",
                DeerStatus.VerySick => "Сильно болеет",
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }

        public static string ToText(this GenderType genderType)
        {
            return genderType switch
            {
                GenderType.None => "Нет",
                GenderType.Male => "Мужской",
                GenderType.Female => "Женский",
                _ => throw new ArgumentOutOfRangeException(nameof(genderType), genderType, null)
            };
        }

        public static string ToText(this DeerAge age)
        {
            return age switch
            {
                DeerAge.None => "Нет",
                DeerAge.Young => "Молодой",
                DeerAge.Adult => "Взрослый",
                DeerAge.Old => "Старый",
                _ => throw new ArgumentOutOfRangeException(nameof(age), age, null)
            };
        }
    }

    public enum DeerAge
    {
        None,
        Young,
        Adult,
        Old
    }
}