using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Game.Monologue
{
    [CreateAssetMenu(fileName = "NewMonologueGroupInfo",
        menuName = "DoubleD Team/Create MonologueGroupInfo", order = 0)]
    public class MonologueGroupInfo : ScriptableObject
    {
        [SerializeField] private List<MonologueInfo> _monologues;

        public IReadOnlyList<MonologueInfo> Monologues => _monologues;

        private readonly PropertyInfo _animationField =
            typeof(MonologueInfo).GetProperty("AnimationDelay", BindingFlags.Instance | BindingFlags.Public);

        private void OnValidate()
        {
            foreach (var monologueInfo in Monologues)
            {
                if (monologueInfo.Clip != null && monologueInfo.AnimationDelay == 0)
                    _animationField.SetValue(monologueInfo, monologueInfo.Clip.length);
            }
        }
    }
}