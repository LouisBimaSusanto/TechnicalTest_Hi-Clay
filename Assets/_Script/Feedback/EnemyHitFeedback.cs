using Game.Core;
using UnityEngine;

namespace Game.Feedback
{
    public class EnemyHitFeedback : MonoBehaviour
    {
        [SerializeField]
        private Health health;

        [SerializeField]
        private DamageFlash damageFlash;

        [SerializeField] private float hitStopDuration = 0.05f;

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