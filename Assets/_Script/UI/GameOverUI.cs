using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Audio;

namespace Game.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private string mainMenuSceneName = "MainMenu";

        private void Start()
        {
            canvasGroup.alpha = 0f;

            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void Show()
        {
            AudioManager.Instance.StopBGM();
            AudioManager.Instance.PlaySFX("GameOver");

            canvasGroup.alpha = 0f;

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            canvasGroup
                .DOFade(1f, 0.5f)
                .SetUpdate(true);

            Time.timeScale = 0f;
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }

        public void ReturnToMainMenu()
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}