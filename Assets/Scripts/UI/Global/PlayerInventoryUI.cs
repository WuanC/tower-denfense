using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private TowerButton towerButtonPrefab;
    void OnEnable()
    {
        foreach(TowerSO tb in PlayerInventory.Instance.GetItems())
        {
            TowerButton tmpTowerBtn = Instantiate(towerButtonPrefab, parent);
            tmpTowerBtn.SetTowerData(tb);
        }
    }
    private void OnDisable()
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }



}
