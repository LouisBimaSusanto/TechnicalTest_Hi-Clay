using UnityEngine;

namespace Game.Boss
{
    public class BossMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField]
        private float moveSpeed = 2f;

        [SerializeField]
        private float stopDistance = 4f;

        [Header("Phase Speed Multipliers")]
        [SerializeField]
        private float phaseTwoSpeedMultiplier = 1.5f;

        [SerializeField]
        private float finalPhaseSpeedMultiplier = 2f;

        private Transform player;

        private Rigidbody2D rb;

        private BossController controller;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            controller =
                GetComponent<BossController>();

            GameObject playerObject =
                GameObject.FindGameObjectWithTag(
                    "Player"
                );

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (player == null)
            {
                return;
            }

            Vector2 direction =
                player.position -
                transform.position;

            float distance =
                direction.magnitude;

            if (distance <= stopDistance)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }

            float currentSpeed = moveSpeed;

            if (controller.IsPhaseTwo)
            {
                currentSpeed *=
                    phaseTwoSpeedMultiplier;
            }

            if (controller.IsFinalPhase)
            {
                currentSpeed *=
                    finalPhaseSpeedMultiplier;
            }

            rb.linearVelocity =
                direction.normalized *
                currentSpeed;

            HandleFlip(direction.x);
        }

        private void HandleFlip(float moveDirection)
        {
            if (moveDirection == 0f)
            {
                return;
            }

            Vector3 localScale =
                transform.localScale;

            localScale.x =
                Mathf.Abs(localScale.x) *
                Mathf.Sign(moveDirection);

            transform.localScale = localScale;
        }
    }
}