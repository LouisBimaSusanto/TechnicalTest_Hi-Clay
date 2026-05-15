using UnityEngine;

namespace Game.Core
{
    public class Health : MonoBehaviour, IDamageable
    {
        [Header("Health Settings")]
        [SerializeField] private int maxHealth = 5;

        private int currentHealth;
        public bool IsDead => currentHealth <= 0;
        private void Awake()
        {
            currentHealth = maxHealth;
        }
        public void TakeDamage(int damage)
        {
            if (IsDead)
            {
                return;   
            }

            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}

