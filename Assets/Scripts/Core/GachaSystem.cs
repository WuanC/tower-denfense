using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GachaSystem : MonoBehaviour
{
    public event Action<List<TowerButton>, int> OnGachaSucess;
    [SerializeField] private List<TowerButton> towerButtons;
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
        if(towerButtons == null || towerButtons.Count ==0) {
            Debug.Log("loi danh sach towerButton");
            return;
        }
        List<TowerButton> tmp = new List<TowerButton>();
        for(int i = 0; i < countOfRolls; i++)
        {
            TowerButton item;
            if (this.countOfRolls % MYTHIC_PITY == 0 && this.countOfRolls != 0)
            {
                item = GetTowerButtonItemByRarity(1);
                this.countOfRolls = 0;
            }
            else
            {
                float randomValue = UnityEngine.Random.Range(0f, GetTotalWeight());
                item = GetTowerButtonItem(randomValue);
            }
            tmp.Add(item);
            PlayerInventory.Instance.AddItem(item);
            this.countOfRolls++;
        }
        OnGachaSucess?.Invoke(tmp, this.countOfRolls);


    }
    private int GetTotalWeight()
    {
        int totalWeight = 0;
        foreach(TowerButton button in towerButtons)
        {
            totalWeight += button.TowerSO.rarity;
        }
        return totalWeight;
    }
    private TowerButton GetTowerButtonItem(float randomValue)
    {
        foreach(TowerButton button in towerButtons)
        {
            if(randomValue < button.TowerSO.rarity)
            {
                return button;
            }
            randomValue -= button.TowerSO.rarity;
        }
        return null;
    }
    private TowerButton GetTowerButtonItemByRarity(float rarity)
    {
        return towerButtons.FirstOrDefault(t => t.TowerSO.rarity == rarity);
    }
    public void OnDestroy()
    {
        buy1Btn.onClick.RemoveAllListeners();
        buy10Btn.onClick.RemoveAllListeners();
    }
}
