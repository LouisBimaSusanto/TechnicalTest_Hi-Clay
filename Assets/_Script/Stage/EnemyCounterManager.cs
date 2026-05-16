using UnityEngine;

namespace Game.StageSystem
{
    public class EnemyCounterManager : MonoBehaviour
    {
        public static EnemyCounterManager Instance;

        private int activeEnemyCount;

        public int ActiveEnemyCount => activeEnemyCount;

        public bool AllEnemiesDefeated => activeEnemyCount <= 0;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void RegisterEnemy()
        {
            activeEnemyCount++;
        }

        public void UnregisteredEnemy()
        {
            activeEnemyCount--;

            if (activeEnemyCount < 0)
            {
                activeEnemyCount = 0;
            }
        }
    }

}
