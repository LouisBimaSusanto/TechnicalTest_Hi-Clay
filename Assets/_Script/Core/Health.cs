using UnityEngine;
using Game.StageSystem;
using System;

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
        public int CurrentHealth => currentHealth;
        public int MaxHealth => maxHealth;
        public event Action<int, int> OnHealthChanged;
        public event Action OnDeath;
        public event Action OnDamaged;

        private void Awake()
        {
            currentHealth = maxHealth;

            OnHealthChanged?.Invoke(
                currentHealth,
                maxHealth
                );
        }
        public void TakeDamage(int damage)
        {
            if (IsDead)
            {
                return;   
            }

            currentHealth -= damage;

            OnDamaged?.Invoke();

            OnHealthChanged?.Invoke(
                currentHealth,
                maxHealth
                );

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
                OnDeath?.Invoke();
                Destroy(gameObject);
            }
            else
            {
                OnDeath?.Invoke();
            }
        }
    }
}

