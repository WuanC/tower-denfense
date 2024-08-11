using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUI : Singleton<GlobalUI>
{
    [SerializeField] private Button[] buttonsTower = new Button[5];
    [SerializeField] private SelectTowerInventory inventoryTower;

    [SerializeField] private GameObject unitPanel;
    [SerializeField] private GameObject listUtilitiBtn;
    private void Start()
    {
        EventManager.Instance.Register(EventID.OnGameStateChanged, OnGameStateChange);
        inventoryTower.OnSelectionTowerChanged += InventoryTower_OnSelectionTowerChanged;
        for (int i = 0; i < buttonsTower.Length; i++)
        {
            {
                buttonsTower[i].interactable = false;
                buttonsTower[i].GetComponentInChildren<TextMeshProUGUI>().text =
                    null;
            }
        }
    }
    
    private void OnDestroy()
    {
       EventManager.Instance?.Unregister(EventID.OnGameStateChanged, OnGameStateChange);
        inventoryTower.OnSelectionTowerChanged -= InventoryTower_OnSelectionTowerChanged;
    }
    private void InventoryTower_OnSelectionTowerChanged(List<TowerSO> obj)
    {
        for (int i = 0; i < buttonsTower.Length; i++)
        {
            if (i < obj.Count)
            {
                Tower tmpTower = obj[i].towerPrefab;
                PreviewTower tmpPreviewTower = obj[i].previewTowerPrefab;
                
                buttonsTower[i].GetComponent<Image>().sprite = obj[i].icons;
                buttonsTower[i].onClick.AddListener(() => SetTower(tmpTower, tmpPreviewTower));
                buttonsTower[i].GetComponentInChildren<TextMeshProUGUI>().text =
                obj[i].towerLevelDatas[0].cost.ToString() + "$";
                buttonsTower[i].interactable = true;
            }
            else
            {
                buttonsTower[i].GetComponent<Image>().sprite = null;
                buttonsTower[i].GetComponentInChildren<TextMeshProUGUI>().text = null;
                buttonsTower[i].interactable = false;
            }
        }
    }

    private void SetTower(Tower tower, PreviewTower previewTower)
    {
        
        if (GameController.Instance == null) return;

        GameController.Instance.GetTowerPlaceManager().SetActive(false);
        GameController.Instance.GetTowerPlaceManager().SetTower(tower, previewTower);
        GameController.Instance.GetTowerPlaceManager().SetActive(true);
    }
    private void OnGameStateChange(object obj)
    {
        if (obj is GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Start:
                    unitPanel.SetActive(false);
                    listUtilitiBtn.SetActive(true);
                    gameObject.SetActive(false);
                    Debug.Log("a");
                    break;
                case GameState.SelectMode:
                case GameState.Selection:
                    listUtilitiBtn.SetActive(true);
                    gameObject.SetActive(true);
                    break;
                case GameState.Playing:
                    listUtilitiBtn.gameObject.SetActive(false);
                    break;
                case GameState.Victory:
                case GameState.Defeat:
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}
