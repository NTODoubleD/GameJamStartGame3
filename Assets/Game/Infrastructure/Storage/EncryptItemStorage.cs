using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Infrastructure.Storage
{
    [Serializable]
    public class EncryptItemStorage
    {
        public List<string> Items;

        public EncryptItemStorage(ItemStorage storage)
        {
            Items = new List<string>(storage.Items.Select(i => i.Info.ID));
        }
    }
}