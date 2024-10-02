using System;
using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using Game.Gameplay.Items;
using UnityEngine;

namespace Game.WorldMap
{
    [RequireComponent(typeof(Collider))]
    public class WorldInterestPoint : MonoBehaviour
    {
        [SerializeField] private TranslatedText _name;

        [SerializeField] private SortieResourceArgument _sortieResource;

        public string Name => _name.GetText();

        public SortieResourceArgument SortieResource => _sortieResource;
    }

    [Serializable]
    public class SortieResourceArgument
    {
        [field: SerializeField] public ResourcePriority Wood { get; private set; }
        [field: SerializeField] public ResourcePriority Moss { get; private set; }
        [field: SerializeField] public ResourcePriority HealGrass { get; private set; }
    }

    [Serializable]
    public class ResourcePriority
    {
        [field: SerializeField] public GameItemInfo Item { get; private set; }
        [field: Range(0, 10), SerializeField] public int Priority { get; private set; }
    }
}