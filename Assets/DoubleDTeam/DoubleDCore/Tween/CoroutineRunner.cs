using DoubleDCore.Tween.Base;
using UnityEngine;

namespace DoubleDCore.Tween
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        private void OnEnable()
        {
            DontDestroyOnLoad(this);
        }
    }
}