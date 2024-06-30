using Game.UI.Pages;
using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public abstract class DeerInteractionCondition : ConditionObject
    {
        [SerializeField] protected Deer Deer;
    }
}