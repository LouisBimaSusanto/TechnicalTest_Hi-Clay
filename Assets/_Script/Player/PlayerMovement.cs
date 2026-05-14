using UnityEngine;

namespace Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Speed")]
        [SerializeField] private float moveSpeed = 7f;

        private PlayerInputHandler inputHandler;
        private PlayerReference reference;

        private bool isFacingRight = true;
        public bool IsFacingRight => isFacingRight;

        private void Awake()
        {
            inputHandler = GetComponent<PlayerInputHandler>();
            reference = GetComponent<PlayerReference>();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            Vector2 moveDirection = inputHandler.MoveInput.normalized;

            Vector2 velocity = moveDirection * moveSpeed;

            reference.Rigidbody2D.linearVelocity = velocity;

            HandleFlip(moveDirection.x);
        }

        private void HandleFlip(float moveDirectionX)
        {
            if (moveDirectionX > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (moveDirectionX < 0f && isFacingRight)
            {
                Flip();
            }
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;

            Vector3 localScale = transform.localScale;

            localScale.x *= -1f;

            transform.localScale = localScale;
        }
    }
}

