using System.Collections.Generic;
using Game.Gameplay.Items;

namespace Game.Gameplay.Crafting
{
    public interface ICraftingRecepie
    {
        IReadOnlyDictionary<GameItemInfo, int> OutputItems { get; }
        IReadOnlyDictionary<GameItemInfo, int> InputItems { get; }
    }
}