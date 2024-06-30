using UnityEngine;

namespace Game.UI.Pages
{
    public class ConstantConditionObject : ConditionObject
    {
        [SerializeField] private bool _isDone;

        public override bool ConditionIsDone()
        {
            return _isDone;
        }
    }
}