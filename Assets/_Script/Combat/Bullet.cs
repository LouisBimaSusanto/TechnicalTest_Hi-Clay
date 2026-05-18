using Game.Core;
using UnityEngine;
using Game.Audio;

namespace Game.Combat
{
    public enum BulletOwner
    {
        Player,
        Enemy
    }

    public class Bullet : MonoBehaviour
    {
        [Header("Bullet Settings")]
        [SerializeField] private float moveSpeed = 15f;
        [SerializeField] private float lifeTime = 3f;
        [SerializeField] private int damage = 1;
        [SerializeField] private BulletOwner owner;

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

        public void Initialize(Vector2 direction, BulletOwner bulletOwner)
        {
            moveDirection = direction.normalized;
            owner = bulletOwner;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (owner == BulletOwner.Player && collision.CompareTag("Player"))
            {
                return;
            }

            if (owner == BulletOwner.Enemy && collision.CompareTag("Enemy"))
            {
                return;
            }

            IDamageable damageable = collision.GetComponent<IDamageable>();

            if (damageable == null)
            {
                return;
            }

            damageable.TakeDamage(damage);

            AudioManager.Instance.PlaySFX("BulletHit");

            Destroy(gameObject);
        }
    }
}