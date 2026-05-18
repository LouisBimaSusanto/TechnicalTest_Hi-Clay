using System.Collections;
using UnityEngine;
using Game.UI;

namespace Game.StageSystem
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private StageManagerUI stageUI;

        [Header("Stage 1")]
        [SerializeField] private EnemySpawner[] stage1Spawners;
        [SerializeField] int stage1EnemyCount = 5;

        [Header("Stage 2")]
        [SerializeField] private EnemySpawner[] stage2EnemyMeleeSpawners;
        [SerializeField] private EnemySpawner[] stage2EnemyRangedSpawners;

        [SerializeField] private int stage2MeleeCount = 4;
        [SerializeField] private int stage2RangedCount = 3;

        [Header("Boss Stage")]
        [SerializeField] private EnemySpawner bossSpawner;

        private int currentStage = 1;

        private void Start()
        {
            StartCoroutine(RunStages());
        }

        private IEnumerator RunStages()
        {
            stageUI.ShowStage("Stage 1");

            yield return new WaitForSeconds(2f);

            yield return StartCoroutine(Stage1());

            currentStage = 2;

            stageUI.ShowStage("Stage 2");

            yield return new WaitForSeconds(2f);

            yield return StartCoroutine(Stage2());

            stageUI.ShowStage("Boss Stage");

            yield return new WaitForSeconds(2f);

            currentStage = 3;

            yield return StartCoroutine(BossStage());
        }

        private IEnumerator Stage1()
        {
            SpawnEnemies(
                stage1Spawners,
                stage1EnemyCount
                );

            yield return null; //Wait for second until systemd done to do task

            yield return new WaitUntil(
                () => EnemyCounterManager.Instance.AllEnemiesDefeated
                );
        }

        private IEnumerator Stage2()
        {
            SpawnEnemies(
            stage2EnemyMeleeSpawners,
            stage2MeleeCount
            );

            SpawnEnemies(
                stage2EnemyRangedSpawners,
                stage2RangedCount
                );

            yield return null;

            yield return new WaitUntil(
                () => EnemyCounterManager.Instance.AllEnemiesDefeated
                ); 
        }

        private IEnumerator BossStage()
        {
            bossSpawner.SpawnEnemy();

            yield return null;

            yield return new WaitUntil(
                () => EnemyCounterManager.Instance.AllEnemiesDefeated
                );

            Debug.Log("GAME COMPLETE");
        }

        private void SpawnEnemies(EnemySpawner[] spawners, int count)
        {
            for (int i = 0; i < count; i++)
            {
                EnemySpawner randomSpawner = spawners[Random.Range(0, spawners.Length)];

                randomSpawner.SpawnEnemy();
            }
        }

    }
}

