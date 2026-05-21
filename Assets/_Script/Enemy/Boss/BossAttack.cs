using Game.Combat;
using Game.Core;
using Game.Enemy;
using UnityEngine;

namespace Game.Boss
{
    public class BossAttack : MonoBehaviour
    {
        private enum BossAttackType
        {
            None,
            Melee,
            Ranged,
            Burst
        }

        [Header("References")]
        [SerializeField] private Bullet bulletPrefab;

        [SerializeField] private Transform firePoint;

        [SerializeField] private Transform meleePoint;

        [Header("Ranged Attack")]
        [SerializeField] private float rangedCooldown = 2f;

        [Header("Burst Attack")]
        [SerializeField] private int burstBulletCount = 3;

        [SerializeField] private float burstSpread = 0.3f;

        [Header("Melee Attack")]
        [SerializeField] private float meleeCooldown = 1f;

        [SerializeField] private float meleeRadius = 2f;

        [SerializeField] private int meleeDamage = 2;

        [SerializeField] private LayerMask playerLayer;

        private Transform player;

        private Shooter shooter;

        private ShootCoolDown rangedHandler;

        private ShootCoolDown meleeHandler;

        private BossAttackType currentAttack;

        private EnemyAnimator bossAnimator;

        private void Awake()
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag(
                    "Player"
                );

            if (playerObject != null)
            {
                player = playerObject.transform;
            }

            shooter = new Shooter(
                bulletPrefab,
                firePoint,
                BulletOwner.Enemy
            );

            rangedHandler =
                new ShootCoolDown(rangedCooldown);

            meleeHandler =
                new ShootCoolDown(meleeCooldown);

            bossAnimator = GetComponent<EnemyAnimator>();
        }

        public void HandleAttack(
            bool isPhaseTwo,
            bool isFinalPhase
        )
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

            currentAttack =
                SelectAttack(
                    distance,
                    isPhaseTwo,
                    isFinalPhase
                );

            ExecuteAttack(currentAttack);
        }

        private BossAttackType SelectAttack(
            float distance,
            bool isPhaseTwo,
            bool isFinalPhase
        )
        {
            if (distance <= meleeRadius)
            {
                return BossAttackType.Melee;
            }

            if (isFinalPhase)
            {
                return BossAttackType.Burst;
            }

            if (isPhaseTwo)
            {
                return BossAttackType.Burst;
            }

            return BossAttackType.Ranged;
        }

        private void ExecuteAttack(
            BossAttackType attackType
        )
        {
            switch (attackType)
            {
                case BossAttackType.Melee:
                    TryMeleeAttack();
                    break;

                case BossAttackType.Ranged:
                    TryRangedAttack();
                    break;

                case BossAttackType.Burst:
                    AggressiveAttack();
                    break;
            }
        }

        private void TryMeleeAttack()
        {
            if (!meleeHandler.CanShoot())
            {
                return;
            }

            bossAnimator.PlayAttackAnimation();

            Collider2D playerHit =
                Physics2D.OverlapCircle(
                    meleePoint.position,
                    meleeRadius,
                    playerLayer
                );

            if (playerHit == null)
            {
                return;
            }

            IDamageable damageable =
                playerHit.GetComponent<IDamageable>();

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

            bossAnimator.PlayAttackAnimation();

            Vector2 direction =
                player.position -
                firePoint.position;

            shooter.Shoot(direction);
        }

        private void AggressiveAttack()
        {
            if (!rangedHandler.CanShoot())
            {
                return;
            }

            int halfBurst =
                burstBulletCount / 2;

            bossAnimator.PlayAttackAnimation();

            for (
                int i = -halfBurst;
                i <= halfBurst;
                i++
            )
            {
                Vector2 direction =
                    player.position -
                    firePoint.position;

                direction.x +=
                    i * burstSpread;

                shooter.Shoot(direction);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (meleePoint == null)
            {
                return;
            }

            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(
                meleePoint.position,
                meleeRadius
            );
        }
    }
}