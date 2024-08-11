using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    [SerializeField] protected TowerSO towerData;
    [SerializeField] protected GameObject skillRangeIndicator;
    [SerializeField] protected GameObject informationUI;

     protected int currentLevel;
    protected static int currentCount;

    protected IAttackStratery attackStrategy;
    protected List<StrategyType> listStrategyType;
    protected StrategyType currentStrategyType;


    public event Action OnUpgrapeTower;
    public event Action<StrategyType> OnChangeStrategy;

    protected virtual void OnEnable()
    {
        currentLevel = 1;
        OnUpgrapeTower?.Invoke();
    }
    protected virtual void Start()
    {
        currentCount++;
        float scale = towerData.towerLevelDatas[currentLevel - 1].range * 2;
        skillRangeIndicator.transform.localScale = new Vector3(scale, scale, scale);
        SetActiveStatus(false);
        attackStrategy = new FirstStratery();
        currentStrategyType = StrategyType.First;
        listStrategyType = new List<StrategyType>()
        {
            StrategyType.First,
            StrategyType.Last,
            StrategyType.Closet,
            StrategyType.Strongest
        };
    }

    public void ChangeStrategy()
    {
        int index = listStrategyType.IndexOf(currentStrategyType);
        index++;
        if (index >= listStrategyType.Count)
        {
            index = 0;
        }
        SetStrategy(listStrategyType[index]);

        OnChangeStrategy?.Invoke(listStrategyType[index]);
    }
    protected void SetStrategy(StrategyType strategyType)
    {
        currentStrategyType = strategyType;
        switch(strategyType)
        {
            case StrategyType.First:
                attackStrategy = new FirstStratery();
                break;
            case StrategyType.Last:
                attackStrategy = new LastStrategy();
                break;
            case StrategyType.Closet:
                attackStrategy = new ClosetStrategy();
                break;
            case StrategyType.Strongest:
                attackStrategy = new StrongestStrategy();
                break;
        }
    }
    protected abstract void FindEnemy();
    public abstract void PerformAction();
    public virtual void SetActiveStatus(bool value)
    {
        skillRangeIndicator.SetActive(value);
        informationUI.SetActive(value);
    }
    public virtual bool UpdateTower()
    {
        if (currentLevel >= towerData.countLevel) return false;
        int costUpgrape = towerData.towerLevelDatas[currentLevel].cost;
        if (costUpgrape > GameController.Instance.GetCurretCoins()) return false;
        EventManager.Instance.Broadcast(EventID.OnWithDrawCoins, costUpgrape);
        currentLevel++;
        spriteRenderer.sprite = towerData.towerLevelDatas[currentLevel - 1].sprite;
        float scale = towerData.towerLevelDatas[currentLevel - 1].range * 2;
        skillRangeIndicator.transform.localScale = new Vector3(scale, scale, scale);
        OnUpgrapeTower?.Invoke();
        return true;
    }
    public int GetStartCoins()
    {
        return towerData.towerLevelDatas[0].cost;
    }
    public void SaleTower()
    {
        gameObject.SetActive(false);
        EventManager.Instance.Broadcast(EventID.OnDepositeCoins, towerData.towerLevelDatas[currentLevel - 1].cost);
        Destroy(gameObject);
    }
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    public bool CountPlaceValid()
    {
        return currentCount < towerData.maxPlace;
    }
    protected virtual void OnDestroy()
    {
        currentCount--;
    }
}
