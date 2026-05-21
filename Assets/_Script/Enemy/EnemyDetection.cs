using UnityEngine;

namespace Game.Enemy
{
    public class EnemyDetection : MonoBehaviour
    {
        [Header("Detection Settings")]
        [SerializeField] private float detectionRange = 5f;

        private Transform player;

        public bool HasDetectedPlayer =>
            player != null &&
            Vector2.Distance(
                transform.position,
                player.position
                ) <= detectionRange;

        public Transform Player => player;

        private void Start()
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.purple;

            Gizmos.DrawSphere(
                transform.position,
                detectionRange
                );
        }

    }
}

