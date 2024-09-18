using System;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Deer Images Config", menuName = "Configs/DeerImagesConfig")]
    public class DeerImagesConfig : ScriptableObject
    {
        [SerializeField] private DeerImage[] _deerImages;
        [SerializeField] private Color _maleFrameColor;
        [SerializeField] private Color _femaleFrameColor;

        public Color MaleFrameColor => _maleFrameColor;
        public Color FemaleFrameColor => _femaleFrameColor;

        public Sprite GetDeerImage(DeerAge age, GenderType gender)
        {
            return _deerImages.First(x => x.Age == age && x.Gender == gender).Image;
        }
        
        [Serializable]
        private struct DeerImage
        {
            public DeerAge Age;
            public GenderType Gender;
            public Sprite Image;
        }
    }
}