using Game.Combat;
using Game.Core;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMelee : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private int damage = 2;
        [SerializeField] private float attackRadius = 1f;
        [SerializeField] private float attackCooldown = 0.5f;
        [SerializeField] private LayerMask enemyLayer;

        private PlayerInputHandler inputHandler;
        private PlayerReference reference;

        private ShootCoolDown coolDownHandler;

        private void Awake()
        {
            inputHandler = GetComponent<PlayerInputHandler>();
            reference = GetComponent<PlayerReference>();

            coolDownHandler = new ShootCoolDown(attackCooldown);
        }

        private void OnEnable()
        {
            inputHandler.OnMeleePressed += HandleAttack;
        }

        private void OnDisable()
        {
            inputHandler.OnMeleePressed -= HandleAttack;
        }

        private void HandleAttack()
        {
            Debug.Log("Bertumbuk lah kita");
            if (!coolDownHandler.CanShoot())
            {
                return;
            }

            Collider2D[] hits =
                Physics2D.OverlapCircleAll(
                    reference.MeleePoint.position,
                    attackRadius,
                    enemyLayer
                    );

            foreach (Collider2D hit in hits)
            {
                IDamageable damageable = 
                    hit.GetComponent<IDamageable>();

                if (damageable == null)
                {
                    continue;
                }

                damageable.TakeDamage(damage);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (reference == null || reference.MeleePoint == null)
            {
                return;
            }

            Gizmos.color = Color.yellow;

            Gizmos.DrawSphere(
                reference.MeleePoint.position,
                attackRadius
                );
        }
    }

}
