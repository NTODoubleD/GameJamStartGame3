using System.Collections.Generic;
using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using Game.UI.Pages;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Buildings
{
    public class TownHallBuilding : UpgradableBuilding
    {
        [SerializeField] private TownHallLevelsConfig _levelsConfig;
        
        private ItemStorage _itemStorage;
        private readonly TranslatedText _labelText = new("Ресурсы", "Resources");

        [Inject]
        private void Init(ItemStorage itemStorage)
        {
            _itemStorage = itemStorage;
        }

        public override ILevelsConfig GetLevelsConfig()
        {
            return _levelsConfig;
        }
    }
}