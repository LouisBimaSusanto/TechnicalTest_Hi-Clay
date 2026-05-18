using System.Collections;
using UnityEngine;

namespace Game.Feedback
{
    public class HitStop : MonoBehaviour
    {
        public static HitStop Instance;

        private void Awake()
        {
            if (Instance != null &&
                Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void Stop(
            float duration
        )
        {
            StartCoroutine(
                HitStopCoroutine(duration)
            );
        }

        private IEnumerator HitStopCoroutine(
            float duration
        )
        {
            Time.timeScale = 0f;

            yield return new WaitForSecondsRealtime(
                duration
            );

            Time.timeScale = 1f;
        }
    }
}