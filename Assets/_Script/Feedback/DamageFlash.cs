using DG.Tweening;
using UnityEngine;

namespace Game.Feedback
{
    public class DamageFlash : MonoBehaviour
    {
        [Header("Flash Settings")]
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Color flashColor = Color.red;

        [SerializeField]
        private float flashDuration = 0.15f;

        private Color originalColor;

        private Sequence flashSequence;

        private void Awake()
        {
            originalColor =
                spriteRenderer.color;
        }

        public void PlayFlash()
        {
            flashSequence?.Kill();

            spriteRenderer.color =
                flashColor;

            flashSequence = DOTween.Sequence();

            flashSequence.AppendInterval(0.05f);

            flashSequence.Append(
                spriteRenderer.DOColor(
                    originalColor,
                    flashDuration
                )
            );

            flashSequence.SetUpdate(true);
        }

        private void OnDestroy()
        {
            flashSequence?.Kill();
        }
    }
}