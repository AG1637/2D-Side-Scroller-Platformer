using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Image healthBarFill;
    private Enemy enemy;
    private Canvas healthBarCanvas;

    private void Start()
    {
        healthBarFill = GetComponent<Image>();
        enemy = GetComponentInParent<Enemy>();
        healthBarCanvas = GetComponentInParent<Canvas>();
        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (enemy != null)
        {
            float fillAmount = (float)enemy.health / enemy.maxHealth;
            healthBarFill.fillAmount = fillAmount;
        }
    }
}
