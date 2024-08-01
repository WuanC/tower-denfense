using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>, IInventory<TowerButton>
{
    [SerializeField] private List<TowerButton> buttons;
    public event Action<TowerButton> OnAddItem;
    public void AddItem(TowerButton item)
    {
        if (ContainsItem(item)) return;
        buttons.Add(item);
        OnAddItem?.Invoke(item);
    }
    

    public void Clear()
    {
        buttons.Clear();
    }

    public bool ContainsItem(TowerButton item)
    {
        return buttons.Contains(item);
    }

    public List<TowerButton> GetItems()
    {
        return buttons;
    }

    public bool RemoveItem(TowerButton item)
    {
        return buttons.Remove(item);
    }
}
