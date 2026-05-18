using UnityEngine;
namespace Game.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");

        private Animator animator;
        private PlayerInputHandler inputHandler;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            inputHandler = GetComponent<PlayerInputHandler>();
        }

        private void OnEnable()
        {
            inputHandler.OnShootPressed += PlayAttackAnimation;
            inputHandler.OnMeleePressed += PlayAttackAnimation;
        }

        private void OnDisable()
        {
            inputHandler.OnShootPressed -= PlayAttackAnimation;
            inputHandler.OnMeleePressed -= PlayAttackAnimation;
        }

        private void Update()
        {
            bool isMoving = inputHandler.MoveInput.magnitude > 0.1f;
            animator.SetBool(IsWalking, isMoving);
        }

        private void PlayAttackAnimation()
        {
            animator.SetTrigger(AttackTrigger);
        }
    }
}