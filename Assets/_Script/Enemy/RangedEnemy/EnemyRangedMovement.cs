using UnityEngine;

namespace Game.Enemy
{
    public class EnemyRangedMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;

        [SerializeField] private float preferredDistance = 5f;

        private Rigidbody2D rb;
        private EnemyDetection detection;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            detection = GetComponent<EnemyDetection>();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (!detection.HasDetectedPlayer)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }

            Vector2 direction =
                detection.Player.position -
                transform.position;

            float distance = direction.magnitude;

            if (distance <= preferredDistance)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }

            rb.linearVelocity =
                direction.normalized * moveSpeed;
        }

    }
}

