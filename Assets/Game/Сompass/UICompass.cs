using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Сompass
{
    public class UICompass : CommpassView
    {
        private Image _compassImage;

        private CharacterMover _characterMover;

        [Inject]
        private void Init(CharacterMover characterMover)
        {
            _characterMover = characterMover;
        }

        private void Awake()
        {
            _compassImage = GetComponent<Image>();
        }

        public override void UpdateCompass(Vector3 targetPosition)
        {
            var compassPosition = _characterMover.transform.position;

            var direction = new Vector2(targetPosition.x, targetPosition.z) -
                            new Vector2(compassPosition.x, compassPosition.z);

            var angle = Vector2.SignedAngle(Vector2.right, direction);

            _compassImage.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public override void SetActive(bool active)
        {
            _compassImage.DOFade(active ? 1 : 0, 1f);
        }
    }
}