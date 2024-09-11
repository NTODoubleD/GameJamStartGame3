using DoubleDCore.Initialization.Base;
using DoubleDCore.SaveSystem.Base;
using UnityEngine;
using Zenject;

namespace DoubleDCore.SaveSystem
{
    public abstract class MonoSaver : MonoBehaviour, ISaveObject, IInitializing
    {
        private ISaveController _saveController;

        [Inject]
        private void Init(ISaveController saveController)
        {
            _saveController = saveController;
        }

        public void Initialize()
        {
            _saveController.Subscribe(this);
        }

        public void Deinitialize()
        {
            _saveController.Unsubscribe(Key);
        }

        protected virtual void OnDestroy()
        {
            Deinitialize();
        }

        public abstract string Key { get; }
        public abstract string GetData();
        public abstract string GetDefaultData();
        public abstract void OnLoad(string data);
    }
}