using Game.Combat;
using Game.Core;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Game.Boss
{
    public class BossAttack : MonoBehaviour
    {
        [Header("Refrence")]
        [SerializeField] private Bullet bulletPrefab;

        [SerializeField] private Transform firePoint;

        [SerializeField] private Transform meleePoint;

        [Header("Ranged Attack")]
        [SerializeField] private float rangedCooldown = 2f;

        [Header("Melee Attack")]
        [SerializeField] private float meleeCooldown = 1f;
        [SerializeField] private float meleeRadius = 2f;
        [SerializeField] private int meleeDamage = 2;
        [SerializeField] private LayerMask playerLayer;

        private Transform player;
        private Shooter shooter;

        private ShootCoolDown rangedHandler;
        private ShootCoolDown meleeHandler;

        private void Awake()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }

            shooter = new Shooter(
                bulletPrefab,
                firePoint,
                BulletOwner.Enemy
                );

            rangedHandler = new ShootCoolDown(rangedCooldown);

            meleeHandler = new ShootCoolDown(meleeCooldown);
        }

        public void HandleAttack(bool isPhasedTwo)
        {
            if (player == null)
            {
                return;
            }

            float distance =
                Vector2.Distance(
                    transform.position,
                    player.position
                    );

            if (distance <= meleeRadius)
            {
                TryMeleeAttack();
            }
            else
            {
                TryRangedAttack();
            }

            if (isPhasedTwo)
            {
                AggresiveAttack();
            }
        }

        private void TryMeleeAttack()
        {
            if (!meleeHandler.CanShoot())
            {
                return;
            }

            Collider2D playerHit = Physics2D.OverlapCircle(
                meleePoint.position,
                meleeRadius,
                playerLayer
                );

            if (playerHit == null)
            {
                return;
            }

            IDamageable damageable = playerHit.GetComponent<IDamageable>();

            if (damageable == null)
            {
                return;
            }

            damageable.TakeDamage(meleeDamage);
        }

        private void TryRangedAttack()
        {
            if (!rangedHandler.CanShoot())
            {
                return;
            }

            Vector2 direction = player.position - firePoint.position;

            shooter.Shoot(direction);
        }

        private void AggresiveAttack()
        {
            if (!rangedHandler.CanShoot())
            {
                return;
            }

            Vector2 direction = player.position - firePoint.position;

            direction.x += Random.Range(-0.5f, 0.5f);

            shooter.Shoot(direction);
        }

    }
}

