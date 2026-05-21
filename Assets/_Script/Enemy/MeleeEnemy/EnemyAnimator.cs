using Game.Core;
using UnityEngine;
namespace Game.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        // Cache hash agar lebih efisien
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        private static readonly int HitTrigger = Animator.StringToHash("Hit");
        private static readonly int DieTrigger = Animator.StringToHash("Die");

        private Animator animator;
        private Rigidbody2D rb;
        private Health health;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            health.OnDamaged += PlayHitAnimation;
            health.OnDeath += PlayDeathAnimation;
        }

        private void OnDisable()
        {
            health.OnDamaged -= PlayHitAnimation;
            health.OnDeath -= PlayDeathAnimation;
        }

        private void Update()
        {
            bool isMoving = rb.linearVelocity.magnitude > 0.1f;
            animator.SetBool(IsWalking, isMoving);
        }

        public void PlayAttackAnimation()
        {
            animator.SetTrigger(AttackTrigger);
        }

        private void PlayHitAnimation()
        {
            animator.SetTrigger(HitTrigger);
        }

        private void PlayDeathAnimation()
        {
            animator.SetTrigger(DieTrigger);
        }
    }
}