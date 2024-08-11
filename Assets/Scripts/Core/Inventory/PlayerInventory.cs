using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>, IInventory<TowerSO>
{
    [SerializeField] private List<TowerSO> towerSOList;

    [SerializeField] private List<TowerSO> ownerButtons;
    public event Action<TowerSO> OnAddItem;
    protected override void Awake()
    {
        base.Awake();
        ownerButtons = new List<TowerSO>();
        foreach (int Id in SaveLoadManager.LoadTower(GameConstants.OWNER_TOWER_FILE))
        {
            TowerSO tmpTowerSO = FindById(Id);
            ownerButtons.Add(tmpTowerSO);
        }
    }
    public void AddItem(TowerSO item)
    {
        if (ContainsItem(item)) return;
        ownerButtons.Add(item);
        OnAddItem?.Invoke(item);
    }
    

    public void Clear()
    {
        ownerButtons.Clear();
    }

    public bool ContainsItem(TowerSO item)
    {
        return ownerButtons.FirstOrDefault(btn => item.Id == btn.Id) != null;
    }

    public List<TowerSO> GetItems()
    {
        return ownerButtons;
    }

    public bool RemoveItem(TowerSO item)
    {
        return ownerButtons.Remove(item);
    }
    private TowerSO FindById(int id)
    {
        return towerSOList.FirstOrDefault(i => i.Id == id);
    }
    private void OnApplicationQuit()
    {
        SaveLoadManager.Save(ownerButtons, GameConstants.OWNER_TOWER_FILE);
    }
}
