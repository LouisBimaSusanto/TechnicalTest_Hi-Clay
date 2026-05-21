using Game.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [SerializeField] private Health playerHealth;
        [SerializeField] Image fillImage;

        private void OnEnable()
        {
            playerHealth.OnHealthChanged += UpdateHealthBar;
        }

        private void OnDisable()
        {
            playerHealth.OnHealthChanged -= UpdateHealthBar;
        }

        private void UpdateHealthBar(int currentHealth, int maxHealth)
        {
            fillImage.fillAmount = (float)currentHealth / maxHealth;
        }

    }
}

