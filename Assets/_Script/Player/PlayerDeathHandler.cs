using Game.CameraSystem;
using Game.Core;
using Game.UI;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Game.Audio;

namespace Game.Player
{
    public class PlayerDeathHandler : MonoBehaviour
    {
        [SerializeField] private Health health;

        [SerializeField] private Animator animator;

        [SerializeField] private float gameOverDelay = 2f;

        private PlayerInputHandler inputHandler;
        private Rigidbody2D rb;

        private void Awake()
        {
            inputHandler = GetComponent<PlayerInputHandler>();

            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            health.OnDeath += HandleDeath;
        }

        private void OnDisable()
        {
            health.OnDeath -= HandleDeath;
        }

        private void HandleDeath()
        {
            StartCoroutine(DeathSequence());
        }

        private IEnumerator DeathSequence()
        {
            DisablePlayer();

            AudioManager.Instance.PlaySFX("PlayerDeath");

            animator.SetTrigger("Die");

            CameraShake.Instance?.ShakeCamera(2f);

            yield return new WaitForSeconds(gameOverDelay);

            UIManager.Instance.ShowGameOverUI();
            Destroy(gameObject);
        }

        private void DisablePlayer()
        {
            inputHandler.enabled = false;

            rb.linearVelocity = Vector2.zero;
        }
    }
}
