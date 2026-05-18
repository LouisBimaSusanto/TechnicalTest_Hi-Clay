using Game.Core;
using UnityEngine;

namespace Game.Feedback
{
    public class PlayerHitFeedback : MonoBehaviour
    {
        [SerializeField]
        private Health health;

        [SerializeField]
        private DamageFlash damageFlash;

        [SerializeField] private float hitStopDuration = 0.08f;

        private void OnEnable()
        {
            health.OnDamaged += HandleDamage;
        }

        private void OnDisable()
        {
            health.OnDamaged -= HandleDamage;
        }

        private void HandleDamage()
        {
            damageFlash.PlayFlash();
            HitStop.Instance?.Stop(hitStopDuration);
        }
    }
}