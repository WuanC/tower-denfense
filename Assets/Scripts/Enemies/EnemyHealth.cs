using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    private int maxHealth = 0;
    [SerializeField] private int enemyCoin = 10;
    public event Action<int> OnEnemyTakeDamage;
    public event Action<int> OnSetMaxHealth;
    private int currentHealth;

    public int MaxHealth
    {
        set {  
            maxHealth = value;
            currentHealth = value;
            OnSetMaxHealth?.Invoke(maxHealth);
        }
        get { return maxHealth; }
    }
    public int CurrentHealth
    {
        get { return currentHealth; }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnEnemyTakeDamage?.Invoke(currentHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        gameObject.SetActive(false);
        EventManager.Instance.Broadcast(EventID.OnDepositeCoins, enemyCoin);
        EventManager.Instance.Broadcast(EventID.OnEnemyDied, enemyCoin);
    }
}
