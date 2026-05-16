using UnityEngine;

namespace Game.StageSystem
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("SpawnSettings")]
        [SerializeField] private GameObject enemyPrefab;
        public void SpawnEnemy()
        {
            GameObject spawnedEnemy = Instantiate(
                enemyPrefab,
                transform.position,
                Quaternion.identity
                );

            EnemyCounterManager.Instance?.RegisterEnemy();
        }
    }

}
