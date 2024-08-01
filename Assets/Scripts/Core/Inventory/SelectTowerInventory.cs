using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTowerInventory : MonoBehaviour, IInventory<TowerSO>
{
    [SerializeField] private List<TowerSO> inventoryTower;
    private const int maxCount = 5;
    public event Action<List<TowerSO>> OnSelectionTowerChanged;

    public void AddItem(TowerSO item)
    {
        if (inventoryTower.Count >= maxCount || inventoryTower.Contains(item))
        {
            Debug.Log("Danh sach tower selection da day hoặc cái đó đã xuất hiện");
            return;
        }
        inventoryTower.Add(item);
        OnSelectionTowerChanged?.Invoke(inventoryTower);
    }

    public bool RemoveItem(TowerSO item)
    {
        inventoryTower.Remove(item);
        OnSelectionTowerChanged?.Invoke(inventoryTower);
        return true;
    }

    public bool ContainsItem(TowerSO item)
    {
        return inventoryTower.Contains(item);
    }

    public List<TowerSO> GetItems()
    {
        return inventoryTower;
    }

    public void Clear()
    {
        inventoryTower.Clear();
    }
}
