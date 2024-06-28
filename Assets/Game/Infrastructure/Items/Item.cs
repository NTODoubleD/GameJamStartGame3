using UnityEngine;

namespace Game.Infrastructure.Items
{
    public abstract class Item : MonoBehaviour
    {
        public ItemInfo Info { get; private set; }

        public virtual void Initialize(ItemInfo info)
        {
            Info = info;
        }
    }
}