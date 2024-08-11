using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinWallet : MonoBehaviour
{
    [SerializeField] private int startCoins;
    private int currentCoins;
    private void Start()
    {
        EventManager.Instance.Register(EventID.OnDepositeCoins, Deposit);
        EventManager.Instance.Register(EventID.OnWithDrawCoins, WithDraw);

        EventManager.Instance.Broadcast(EventID.OnCurrentCoinsChange, startCoins);
        currentCoins = startCoins;
    }
    private void Deposit(object coins)
    {
        currentCoins += (int)coins;
        EventManager.Instance.Broadcast(EventID.OnCurrentCoinsChange, currentCoins);
    }
    private void WithDraw(object coins)
    {
        if (currentCoins < (int)coins) return;
        currentCoins -= (int)coins;
        EventManager.Instance.Broadcast(EventID.OnCurrentCoinsChange, currentCoins);
    }
    public int GetCurrentCoins()
    {
        return currentCoins;
    }
    private void OnDisable()
    {
        EventManager.Instance?.Unregister(EventID.OnDepositeCoins, Deposit);
        EventManager.Instance?.Unregister(EventID.OnWithDrawCoins, WithDraw);
    }
}
