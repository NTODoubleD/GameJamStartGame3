using System;
using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;

namespace Game.Gameplay
{
    public class DeerInfo
    {
        public string Name;
        public GenderType Gender;
        public DeerAge Age;
        public int AgeDays;
        public bool IsDead;
        public int DieDay;
        public DeerStatus StatusBeforeDeath = DeerStatus.None;

        private float _hungerDegree;
        private DeerStatus _status;
        
        public float HungerDegree
        {
            get { return _hungerDegree; }

            set
            {
                _hungerDegree = value;
                HungerChanged?.Invoke(value);
            }
        }

        public DeerStatus Status
        {
            get { return _status; }

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
        private static TranslatedText _noneText = new("Нет", "None");
        private static TranslatedText _standardText = new("Норма", "Standard");
        private static TranslatedText _sickText = new("Болеет", "Sick");
        private static TranslatedText _verySickText = new("Сильно болеет", "Very Sick");
        private static TranslatedText _killedText = new("Мертв", "Killed");

        private static TranslatedText _maleText = new("Мужской", "Male");
        private static TranslatedText _femaleText = new("Женский", "Female");
        private static TranslatedText _youngText = new("Молодой", "Young");
        private static TranslatedText _adultText = new("Взрослый", "Adult");
        private static TranslatedText _oldText = new("Старый", "Old");
        public static string ToText(this DeerStatus status)
        {
            return status switch
            {
                DeerStatus.None => _noneText.GetText(),
                DeerStatus.Standard => _standardText.GetText(),
                DeerStatus.Sick => _sickText.GetText(),
                DeerStatus.VerySick => _verySickText.GetText(),
                DeerStatus.Killed => _killedText.GetText(),
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }

        public static string ToText(this GenderType genderType)
        {
            return genderType switch
            {
                GenderType.None => _noneText.GetText(),
                GenderType.Male => _maleText.GetText(),
                GenderType.Female => _femaleText.GetText(),
                _ => throw new ArgumentOutOfRangeException(nameof(genderType), genderType, null)
            };
        }

        public static string ToText(this DeerAge age)
        {
            return age switch
            {
                DeerAge.None => _noneText.GetText(),
                DeerAge.Young => _youngText.GetText(),
                DeerAge.Adult => _adultText.GetText(),
                DeerAge.Old => _oldText.GetText(),
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