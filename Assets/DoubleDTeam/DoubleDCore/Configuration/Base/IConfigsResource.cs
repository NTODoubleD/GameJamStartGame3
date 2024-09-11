using UnityEngine;

namespace DoubleDCore.Configuration.Base
{
    public interface IConfigsResource
    {
        public TConfigType GetConfig<TConfigType>() where TConfigType : ScriptableObject;
    }
}