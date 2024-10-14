using UnityEngine;

namespace Game.UI.Pages
{
    [CreateAssetMenu(menuName = "Configs/AdditionalInfo", fileName = "Additional Info Opener Config")]
    public class AdditionalInfoOpenConfig : ScriptableObject
    {
        [SerializeField] private float _openTime;
        
        public float OpenTime => _openTime;
    }
}