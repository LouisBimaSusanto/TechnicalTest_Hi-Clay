using UnityEngine;

namespace Game.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float stopDistance = 1.5f;

        [SerializeField] private Vector2 moveDirection = Vector2.left;

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

            Vector2 direction = detection.Player.position - transform.position;

            float distance = direction.magnitude;

            if (distance <= stopDistance)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }

            rb.linearVelocity = direction.normalized * moveSpeed;
        }

    }

}
