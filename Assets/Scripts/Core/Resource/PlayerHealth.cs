using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    private void Start()
    {
        EventManager.Instance.Register(EventID.OnEnemyAttack, TakeDamage);

        currentHealth = maxHealth;
        EventManager.Instance.Broadcast(EventID.OnPlayerHealthChange, new HealthStats(currentHealth, maxHealth));
    }
    public void Die()
    {
        currentHealth = 0;
        GameStateManager.Instance.SetState(GameState.Defeat);
    }
    public void TakeDamage(object obj)
    {
        if(obj is int damage) TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        EventManager.Instance.Broadcast(EventID.OnPlayerHealthChange, new HealthStats(currentHealth, maxHealth));
    }
    private void OnDisable()
    {
        EventManager.Instance?.Unregister(EventID.OnEnemyAttack, TakeDamage);
    }

}
public struct HealthStats
{
    public HealthStats(int cur, int max)
    {
        currentHealth = cur;
        maxHealth = max;
    }
    public int maxHealth, currentHealth;
}
