using Game.Core;
using UnityEngine;

namespace Game.Boss
{
    public class BossController : MonoBehaviour
    {
        [SerializeField] private Health health;

        private BossAttack attack;

        private bool isPhaseTwo;

        private void Awake()
        {
            attack = GetComponent<BossAttack>();
        }

        private void Update()
        {
            HandlePhase();
            attack.HandleAttack(isPhaseTwo);
        }

        private void HandlePhase()
        {
            if (isPhaseTwo)
            {
                return;
            }

            float healthPercent = health.CurrentHealthPercent;

            if (healthPercent <= 0.5f)
            {
                EnterPhasedTwo();
            }
        }

        private void EnterPhasedTwo()
        {
            isPhaseTwo = true;

            Debug.Log("Boss Phase 2");
        }
    }
}

