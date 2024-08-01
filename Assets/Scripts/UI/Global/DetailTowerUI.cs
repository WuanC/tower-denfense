using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DetailTowerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image towerImage;
    [SerializeField] private Button equipBtn;
    [SerializeField] private Button unEquipBtn;

    [SerializeField] private SelectTowerInventory inventoryTowerReady;
    private TowerSO tower;
    void Start()
    {
        EventManager.Instance.Register(EventID.OnClickButtonTower, ChangeDetailUI);
        equipBtn.onClick.AddListener(SelectTower);
        unEquipBtn.onClick.AddListener(RemoveTower);
    }
    public void ChangeDetailUI(object towerSO)
    {
        if(towerSO is TowerSO tower)
        {
            this.tower = tower;
            SetActiveChildren(true);
            nameText.text = tower.name.ToString();
            towerImage.sprite = tower.icons;
        }
    }
    public void SelectTower()
    {
        if(tower != null)
        {
            inventoryTowerReady.AddItem(tower);
        }
    }
    public void RemoveTower()
    {
        if (tower != null)
        {
            Debug.Log("xoa tower select");
            inventoryTowerReady.RemoveItem(tower);
        }
            
    }
    public void SetActiveChildren(bool active)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }
    private void OnDestroy()
    {
        EventManager.Instance?.Unregister(EventID.OnClickButtonTower, ChangeDetailUI);
        equipBtn.onClick.RemoveAllListeners();
        unEquipBtn.onClick.RemoveAllListeners();
    }

}
