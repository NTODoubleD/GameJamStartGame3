using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Game.Gameplay.Blood.Scripts
{
    public class BloodAppearAnimation : MonoBehaviour
    {
        [SerializeField] private DecalProjector _decalProjector;
        [SerializeField] private float _duration = 0.2f;
        [SerializeField] private Ease _ease = Ease.Linear;

        private void OnEnable()
        {
            Vector3 targetSize = _decalProjector.size;
            _decalProjector.size = new Vector3(0, 0, _decalProjector.size.z);
            
            DOTween.To(() => _decalProjector.size, x => _decalProjector.size = x, targetSize, _duration).SetEase(_ease);
        }
    }
}