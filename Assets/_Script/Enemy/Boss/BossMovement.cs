using UnityEngine;

namespace Game.Boss
{
    public class BossMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float stopDistance = 4f;

        private Transform player;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

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

            Vector2 direction = player.position - transform.position;

            float distence = direction.magnitude;

            if (distence <= stopDistance)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }

            rb.linearVelocity = direction.normalized * moveSpeed;
        }
    }
}
