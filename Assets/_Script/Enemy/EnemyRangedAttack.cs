using Game.Combat;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyRangedAttack : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private Bullet bulletPrefab;

        [SerializeField] private Transform firePoint;

        [SerializeField] private float attackCooldown = 1f;

        private EnemyDetection detection;

        private Shooter shooter;
        private ShootCoolDown cooldownHandler;

        private void Awake()
        {
            detection = GetComponent<EnemyDetection>();

            shooter = new Shooter(
                bulletPrefab,
                firePoint,
                BulletOwner.Enemy
                );

            cooldownHandler = new ShootCoolDown(attackCooldown);
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

            if (!cooldownHandler.CanShoot())
            {
                return;
            }

            Vector2 direction = detection.Player.position - firePoint.position;

            shooter.Shoot(direction);
        }
    }

}
