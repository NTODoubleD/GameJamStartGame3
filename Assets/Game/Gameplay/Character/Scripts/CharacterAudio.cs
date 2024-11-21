using DoubleDCore.Extensions;
using UnityEngine;

namespace Game.Gameplay.Character
{
    public class CharacterAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _footstepSounds;

        public void PlayFootstepSound() =>
            PlaySound();

        private void PlaySound(float p1 = 0.85f, float p2 = 1.2f)
        {
            _audioSource.pitch = Random.Range(p1, p2);
            _audioSource.clip = _footstepSounds.Choose();
            _audioSource.Play();
        }
    }
}