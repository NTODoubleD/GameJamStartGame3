﻿using System.Collections.Generic;
using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Data;
using DoubleDCore.TranslationTools.Extensions;
using Game.Infrastructure.Items;
using Game.Infrastructure.Storage;
using Game.UI.Pages;
using Zenject;

namespace Game.Gameplay.Buildings
{
    public class TownHallBuilding : UpgradableBuilding
    {
        private ItemStorage _itemStorage;
        private readonly TranslatedText _labelText = new("Ресурсы", "Resources");

        [Inject]
        private void Init(ItemStorage itemStorage)
        {
            _itemStorage = itemStorage;
        }

        public void OpenResourcePage()
        {
            UIManager.OpenPage<ResourceWatcherPage, ResourcePageArgument>(new ResourcePageArgument()
            {
                Label = _labelText.GetText(),
                Resource = new Dictionary<ItemInfo, int>(_itemStorage.Resources)
            });
        }
    }
}