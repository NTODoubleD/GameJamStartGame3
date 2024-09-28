using Game.Infrastructure.Items;
using UnityEngine;

namespace Game.Gameplay.Items
{
    [CreateAssetMenu(fileName = "NewGameItemInfo", menuName = "DoubleD Team/Create GameItemInfo", order = 0)]
    public class GameItemInfo : ItemInfo
    {
        [SerializeField] private Sprite _icon;

        public Sprite Icon => _icon;
    }
}