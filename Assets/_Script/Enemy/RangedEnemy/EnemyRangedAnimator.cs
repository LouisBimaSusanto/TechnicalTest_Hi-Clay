using Game.Core;
using UnityEngine;
namespace Game.Enemy
{
    public class EnemyRangedAnimator : MonoBehaviour
    {
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
            animator.SetBool(IsWalking, rb.linearVelocity.magnitude > 0.1f);
        }

        public void PlayAttackAnimation() => animator.SetTrigger(AttackTrigger);
        private void PlayHitAnimation() => animator.SetTrigger(HitTrigger);
        private void PlayDeathAnimation() => animator.SetTrigger(DieTrigger);
    }
}