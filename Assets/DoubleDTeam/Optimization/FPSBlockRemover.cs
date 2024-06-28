using UnityEngine;

namespace DoubleDTeam.Optimization
{
    public class FPSBlockRemover : MonoBehaviour
    {
        private void Awake()
        {
#if UNITY_EDITOR
            Application.targetFrameRate = 1000;
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.targetFrameRate = 60;
#endif
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.targetFrameRate = 60;
#endif
        }
    }
}