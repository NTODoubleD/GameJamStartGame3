using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using UnityEngine;

namespace Game.WorldMap
{
    [RequireComponent(typeof(Collider))]
    public abstract class WorldInterestPoint : MonoBehaviour
    {
        [SerializeField] private TranslatedText _name;

        public string Name => _name.GetText();

        public abstract void Interact();
    }
}