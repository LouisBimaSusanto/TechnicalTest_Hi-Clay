using Game.Combat;
using Game.Core;
using UnityEngine;
using Game.Audio;

namespace Game.Enemy
{
    public class EnemyMeleeAttack : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private int damage = 1;

        [SerializeField] private float attackRange = 1.5f;

        [SerializeField] private float attackCooldown = 1f;

        private EnemyDetection detection;

        private ShootCoolDown attackCooldwonHandler;

        private void Awake()
        {
            detection = GetComponent<EnemyDetection>();

            attackCooldwonHandler = new ShootCoolDown(attackCooldown);
        }

        private void Update()
        {
            HandleAttack();
        }

        private void HandleAttack()
        {
            if (!detection.HasDetectedPlayer)
            {
                return;
            }

            float distance = Vector2.Distance(
                transform.position,
                detection.Player.position
                );

            if (distance > attackRange)
            {
                return;
            }

            if (!attackCooldwonHandler.CanShoot())
            {
                return;
            }

            Attack();
        }

        private void Attack()
        {
            Debug.Log("Mampus kau, hahahahahhaha");
            AudioManager.Instance.PlaySFX("HitMelee");
            IDamageable damageable = detection.Player.GetComponent<IDamageable>();

            if (damageable == null)
            {
                return;
            }

            damageable.TakeDamage(damage);
        }
    }

}
