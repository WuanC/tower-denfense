using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField] TowerSO towerData;
    private Tower tower;
    private int currentLevel;
    private const string MAX_LEVEL = "MAX";
    private const string STR_FIRST = "First";
    private const string STR_LAST = "Last";
    private const string STR_CLOSET = "Close";
    private const string STR_STRONGEST = "Strongest";



    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textCurrentLevel;

    [SerializeField] private TextMeshProUGUI textCurrentDamage;
    [SerializeField] private TextMeshProUGUI textCurrentCooldown;
    [SerializeField] private TextMeshProUGUI textCurrentRange;

    [SerializeField] private TextMeshProUGUI textNextDamage;
    [SerializeField] private TextMeshProUGUI textNextCooldown;
    [SerializeField] private TextMeshProUGUI textNextRange;

    [SerializeField] private Button upgradeBtn;
    [SerializeField] private Button strategyBtn;
    [SerializeField] private Button saleBtn;

    [SerializeField] private TextMeshProUGUI upgradeBtnText;
    [SerializeField] private TextMeshProUGUI strategyText;


    

    private void Awake()
    {
        tower = GetComponent<Tower>();
        upgradeBtn.onClick.AddListener(tower.UpdateTower);
        saleBtn.onClick.AddListener(tower.SaleTower);
        strategyBtn.onClick.AddListener(tower.ChangeStrategy);

        tower.OnUpgrapeTower += UpdateStatus;
        tower.OnChangeStrategy += Tower_OnChangeStrategy;
    }
    private void Start()
    {
        Tower_OnChangeStrategy(StrategyType.First);
    }
    private void Tower_OnChangeStrategy(StrategyType obj)
    {
        switch (obj)
        {
            case StrategyType.First:
                strategyText.text = STR_FIRST;
                break;
            case StrategyType.Last:
                strategyText.text = STR_LAST;
                break;
            case StrategyType.Closet:
                strategyText.text = STR_CLOSET;
                break;
            case StrategyType.Strongest:
                strategyText.text = STR_STRONGEST;
                break;
        }
    }
    private void OnEnable()
    {
        UpdateStatus();
    }
    private void OnDestroy()
    {
        upgradeBtn.onClick.RemoveAllListeners();
        saleBtn.onClick.RemoveAllListeners();
        strategyBtn.onClick.RemoveAllListeners();
        tower.OnUpgrapeTower -= UpdateStatus;
        tower.OnChangeStrategy -= Tower_OnChangeStrategy;
    }
    void UpdateStatus()
    {
        currentLevel = tower.GetCurrentLevel();
        textName.text = towerData.towerName;
        textCurrentLevel.text = "Level [" + currentLevel.ToString() + "]";

        textCurrentDamage.text = towerData.towerLevelDatas[currentLevel - 1].damage.ToString();
        textCurrentCooldown.text = towerData.towerLevelDatas[currentLevel - 1].attackCooldown.ToString();
        textCurrentRange.text = towerData.towerLevelDatas[currentLevel - 1].range.ToString();

        if(currentLevel == towerData.countLevel)
        {
            textNextDamage.text = MAX_LEVEL;
            textNextCooldown.text = MAX_LEVEL;
            textNextRange.text = MAX_LEVEL;
            upgradeBtnText.text = MAX_LEVEL;
            upgradeBtn.enabled = false;
        }
        else
        {
            textNextDamage.text = towerData.towerLevelDatas[currentLevel].damage.ToString();
            textNextCooldown.text = towerData.towerLevelDatas[currentLevel].attackCooldown.ToString();
            textNextRange.text = towerData.towerLevelDatas[currentLevel].range.ToString();
            upgradeBtnText.text = towerData.towerLevelDatas[currentLevel].cost.ToString();
        }

    }
    

}
