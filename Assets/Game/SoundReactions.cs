using Game.Monologue;
using UnityEngine;

namespace Game
{
    public class SoundReactions : MonoBehaviour
    {
        [SerializeField] private MonologueInfo _monologueInfo;

        private bool _isFirst = true;

        public void Play()
        {
            if (_isFirst == false)
                return;

            SoundsManager.Instance.PlaySound(_monologueInfo.VoiceClip, Camera.main.transform.position);
            _isFirst = false;
        }
    }
}