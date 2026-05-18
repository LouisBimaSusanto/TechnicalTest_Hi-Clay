using UnityEngine;

namespace Game.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] private GameOverUI gameOverUI;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void ShowGameOverUI()
        {
            gameOverUI.Show();
        }
    }

}
