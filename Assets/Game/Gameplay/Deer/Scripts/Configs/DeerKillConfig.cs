using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using UnityEngine;

namespace Game.Gameplay.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Deer Killing Config", menuName = "Configs/Deer Killing")]
    public class DeerKillConfig : ScriptableObject
    {
        [SerializeField] private TranslatedText _confirmTitle;
        [SerializeField] private TranslatedText _confirmMessage;
        
        public string ConfirmTitle => _confirmTitle.GetText();
        public string ConfirmMessage => _confirmMessage.GetText();
    }
}