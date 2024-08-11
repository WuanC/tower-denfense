using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GachaSystem : MonoBehaviour
{
    public event Action<List<TowerSO>, int> OnGachaSucess;
    [SerializeField] private List<TowerSO> listTowerData;
    [SerializeField] private Button buy1Btn;
    [SerializeField] private Button buy10Btn;

    public const int MYTHIC_PITY = 40;
    private int countOfRolls = 0;    
    private void Start()
    {
        buy1Btn.onClick.AddListener(() => RollGacha(1));
        buy10Btn.onClick.AddListener(() => RollGacha(10));
    }

    public void RollGacha(int countOfRolls)
    {
        if(listTowerData == null || listTowerData.Count ==0) {
            Debug.Log("loi danh sach towerButton");
            return;
        }
        List<TowerSO> tmpListTowerSO = new List<TowerSO>();
        for(int i = 0; i < countOfRolls; i++)
        {
            TowerSO tmpTowerData;
            if (this.countOfRolls % MYTHIC_PITY == 0 && this.countOfRolls != 0)
            {
                tmpTowerData = GetTowerButtonItemByRarity(1);
                this.countOfRolls = 0;
            }
            else
            {
                float randomValue = UnityEngine.Random.Range(0f, GetTotalWeight());
                tmpTowerData = GetTowerButtonItem(randomValue);
            }
            tmpListTowerSO.Add(tmpTowerData);
            PlayerInventory.Instance.AddItem(tmpTowerData);
            this.countOfRolls++;
        }
        OnGachaSucess?.Invoke(tmpListTowerSO, this.countOfRolls);


    }
    private int GetTotalWeight()
    {
        int totalWeight = 0;
        foreach(TowerSO towerData in listTowerData)
        {
            totalWeight += towerData.rarity;
        }
        return totalWeight;
    }
    private TowerSO GetTowerButtonItem(float randomValue)
    {
        foreach(TowerSO towerData in listTowerData)
        {
            if(randomValue < towerData.rarity)
            {
                return towerData;
            }
            randomValue -= towerData.rarity;
        }
        return null;
    }
    private TowerSO GetTowerButtonItemByRarity(float rarity)
    {
        return listTowerData.FirstOrDefault(t => t.rarity == rarity);
    }
    public void OnDestroy()
    {
        buy1Btn.onClick.RemoveAllListeners();
        buy10Btn.onClick.RemoveAllListeners();
    }
}
