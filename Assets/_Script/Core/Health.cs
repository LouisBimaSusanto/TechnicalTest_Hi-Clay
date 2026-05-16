using UnityEngine;
using Game.StageSystem;

namespace Game.Core
{
    public class Health : MonoBehaviour, IDamageable
    {
        [Header("Health Settings")]
        [SerializeField] private int maxHealth = 5;
        [SerializeField] private bool isEnemy;

        private int currentHealth;
        public bool IsDead => currentHealth <= 0;
        public float CurrentHealthPercent => (float)currentHealth / maxHealth;

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
            if (isEnemy)
            {
                EnemyCounterManager.Instance?.UnregisteredEnemy();
            }
            Destroy(gameObject);
        }
    }
}

