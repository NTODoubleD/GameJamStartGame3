using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.DayCycle
{
    [CreateAssetMenu(fileName = "Fact List", menuName = "Facts/List")]
    public class FactsListConfig : ScriptableObject
    {
        [SerializeField, TextArea] private string[] _facts;

        public IEnumerable<string> Facts => _facts;
    }
}