using DoubleDCore.Initialization.Base;
using UnityEngine;

namespace DoubleDCore.Initialization
{
    public abstract class MonoInitializer : MonoBehaviour, IInitializing
    {
        public void OnDestroy()
        {
            Deinitialize();
        }

        public abstract void Initialize();

        public abstract void Deinitialize();
    }
}