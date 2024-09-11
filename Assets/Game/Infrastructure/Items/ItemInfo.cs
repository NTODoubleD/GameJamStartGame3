using System.Linq;
using DoubleDCore.Attributes;
using DoubleDCore.Extensions;
using DoubleDCore.GameResources.Base;
using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using UnityEngine;

namespace Game.Infrastructure.Items
{
    public abstract class ItemInfo : ScriptableObject, IResource
    {
        [ReadOnlyProperty, SerializeField] private string _id;
        [Space, SerializeField] private TranslatedText _name;

        public string ID => _id;

        public string Name => _name.GetText();

        private void OnValidate()
        {
            if (HasDuplicate(_id))
                Debug.LogError($"The project contains a duplicate ID: {ID}");

            if (string.IsNullOrEmpty(_id) == false)
                return;

            SetID();
        }

        private void SetID()
        {
            _id = GenerateID();
        }

        private string GenerateID(int number = 0)
        {
            string itemName = name.Replace(" ", "");

            itemName = string.IsNullOrEmpty(itemName) ? "empty" : itemName.ToLower();

            string result = "item/" + itemName + "/" + (number == 0 ? "" : number);

            if (HasDuplicate(result))
                return GenerateID(number + 1);

            return result;
        }

        private bool HasDuplicate(string id)
        {
            var items = Resources.LoadAll<ItemInfo>("").ToList();
            items.Remove(this);

            return items.Contains(i => i.ID == id);
        }

        public void Load()
        {
        }

        public void Release()
        {
        }
    }
}