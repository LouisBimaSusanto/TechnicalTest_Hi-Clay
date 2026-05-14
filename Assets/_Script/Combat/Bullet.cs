using UnityEngine;

namespace Game.Combat
{
    public class Bullet : MonoBehaviour
    {
        [Header("Bullet Settings")]
        [SerializeField] private float moveSpeed = 15f;
        [SerializeField] private float lifeTime = 3f;

        private Vector2 moveDirection;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            HandleMovement();
        }

        public void Initialize(Vector2 direction)
        {
            moveDirection = direction.normalized;
        }

        private void HandleMovement()
        {
            transform.Translate(
                moveDirection * moveSpeed * Time.deltaTime
            );
        }
    }
}