using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    [SerializeField] private TowerSO towerData;
    [SerializeField] private EnemyHealth target;
    private bool canAttack;
    [SerializeField] private Transform spawnBow;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject skillRangeIndicator;
    [SerializeField] private GameObject targetIndicator;
    [SerializeField] private GameObject statusUI;
    private int currentLevel;

    private IAttackStratery attackStrategy;
    private List<StrategyType> listStrategyType;
    private StrategyType currentStrategyType;


    public event Action OnUpgrapeTower;
    public event Action OnSaleTower;
    public event Action OnTowerAttack;
    public event Action<StrategyType> OnChangeStrategy;

    private void OnEnable()
    {
        currentLevel = 1;
    }
    private void Start()
    {
        canAttack = true;
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

    private void Update()
    {
        FindEnemy();
        if (canAttack)
        {
            StartCoroutine(Fire());
        }
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
    private void SetStrategy(StrategyType strategyType)
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
    void FindEnemy()
    {
        target = attackStrategy.FindTargetEnemy(transform.position, towerData.towerLevelDatas[currentLevel - 1].range, GameController.Instance.GetPathFinding());
        if(target != null)
        {
            targetIndicator.transform.position = Vector2.MoveTowards(targetIndicator.transform.position, target.transform.position, 10 * Time.deltaTime);
        }     
    }

    public IEnumerator Fire()
    {
        if (target == null) yield break;

            canAttack = false;
            
           // GameObject tmp = Instantiate(projectile, spawnBow);
            //tmp.GetComponent<Projectile>().SetTarget(target.transform);
            target.TakeDamage(towerData.towerLevelDatas[currentLevel - 1].damage);
            yield return new WaitForSeconds(towerData.towerLevelDatas[currentLevel - 1].attackCooldown);
            canAttack = true;
    }
    public void SetActiveStatus(bool value)
    {
        skillRangeIndicator.SetActive(value);
        statusUI.SetActive(value);
        targetIndicator.SetActive(value);
    }
    public void UpdateTower()
    {
        if (currentLevel >= towerData.countLevel) return;
        int costUpgrape = towerData.towerLevelDatas[currentLevel].cost;
        if (costUpgrape > GameController.Instance.GetCurretCoins()) return;
        EventManager.Instance.Broadcast(EventID.OnWithDrawCoins, costUpgrape);
        currentLevel++;
        spriteRenderer.sprite = towerData.towerLevelDatas[currentLevel - 1].sprite;
        float scale = towerData.towerLevelDatas[currentLevel - 1].range * 2;
        skillRangeIndicator.transform.localScale = new Vector3(scale, scale, scale);
        OnUpgrapeTower?.Invoke();
    }
    public int GetStartCoins()
    {
        return towerData.towerLevelDatas[0].cost;
    }
    public void SaleTower()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
