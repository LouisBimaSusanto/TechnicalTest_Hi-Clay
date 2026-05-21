using Game.Core;
using UnityEngine;
namespace Game.Boss
{
    public class BossAnimator : MonoBehaviour
    {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int MeleeTrigger = Animator.StringToHash("Melee");
        private static readonly int RangedTrigger = Animator.StringToHash("Ranged");
        private static readonly int BurstTrigger = Animator.StringToHash("Burst");
        private static readonly int HitTrigger = Animator.StringToHash("Hit");
        private static readonly int DieTrigger = Animator.StringToHash("Die");
        private static readonly int PhaseTwoTrigger = Animator.StringToHash("PhaseTwo");
        private static readonly int FinalPhaseTrigger = Animator.StringToHash("FinalPhase");

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

        public void PlayMeleeAnimation() => animator.SetTrigger(MeleeTrigger);
        public void PlayRangedAnimation() => animator.SetTrigger(RangedTrigger);
        public void PlayBurstAnimation() => animator.SetTrigger(BurstTrigger);
        public void PlayPhaseTwoAnimation() => animator.SetTrigger(PhaseTwoTrigger);
        public void PlayFinalPhaseAnimation() => animator.SetTrigger(FinalPhaseTrigger);
        private void PlayHitAnimation() => animator.SetTrigger(HitTrigger);
        private void PlayDeathAnimation() => animator.SetTrigger(DieTrigger);
    }
}