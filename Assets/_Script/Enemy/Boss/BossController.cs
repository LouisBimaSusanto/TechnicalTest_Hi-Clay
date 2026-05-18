using Game.CameraSystem;
using Game.Core;
using UnityEngine;

namespace Game.Boss
{
    public class BossController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Health health;

        [Header("Phase Settings")]
        [SerializeField]
        [Range(0f, 1f)]
        private float phaseTwoThreshold = 0.5f;

        [SerializeField]
        [Range(0f, 1f)]
        private float finalPhaseThreshold = 0.2f;

        private BossAttack attack;

        private bool isPhaseTwo;
        private bool isFinalPhase;

        public bool IsPhaseTwo => isPhaseTwo;

        public bool IsFinalPhase => isFinalPhase;

        private void Awake()
        {
            attack = GetComponent<BossAttack>();
        }

        private void Update()
        {
            HandlePhase();

            attack.HandleAttack(
                isPhaseTwo,
                isFinalPhase
            );
        }

        private void HandlePhase()
        {
            float healthPercent =
                health.CurrentHealthPercent;

            if (!isPhaseTwo &&
                healthPercent <= phaseTwoThreshold)
            {
                EnterPhaseTwo();
            }

            if (!isFinalPhase &&
                healthPercent <= finalPhaseThreshold)
            {
                EnterFinalPhase();
            }
        }

        private void EnterPhaseTwo()
        {
            isPhaseTwo = true;

            CameraShake.Instance
                ?.ShakeCamera(3f);

            Debug.Log("Boss Entered Phase 2");
        }

        private void EnterFinalPhase()
        {
            isFinalPhase = true;

            CameraShake.Instance
                ?.ShakeCamera(5f);

            Debug.Log("Boss Entered Final Phase");
        }
    }
}