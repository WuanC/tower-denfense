using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private TextMeshProUGUI textCurrentHealth;
    private EnemyHealth enemyHealth;
    private void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }
    void OnEnable()
    {
        healthImage.fillAmount = 1;
        enemyHealth.OnEnemyTakeDamage += EnemyHealth_OnEnemyTakeDamage;
        enemyHealth.OnSetMaxHealth += EnemyHealth_OnSetMaxHealth;
    }
    private void EnemyHealth_OnSetMaxHealth(int obj)
    {
        textCurrentHealth.text = $"{obj}/{obj}";
    }

    private void EnemyHealth_OnEnemyTakeDamage(int currentHealth)
    {
        healthImage.fillAmount = (float)currentHealth / enemyHealth.MaxHealth;
        textCurrentHealth.text = $"{currentHealth}/{enemyHealth.MaxHealth}";
    }

    private void OnDisable()
    {
        enemyHealth.OnEnemyTakeDamage -= EnemyHealth_OnEnemyTakeDamage;
        enemyHealth.OnSetMaxHealth -= EnemyHealth_OnSetMaxHealth;
    }
}
