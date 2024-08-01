using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private Transform parent;
    void OnEnable()
    {
        PlayerInventory.Instance.OnAddItem += Instance_OnAddItem;
    }

    private void Instance_OnAddItem(TowerButton obj)
    {
        Instantiate(obj, parent);
    }
    //private void OnDestroy()
    //{
    //    PlayerInventory.Instance.OnAddItem -= Instance_OnAddItem;
    //}


}
