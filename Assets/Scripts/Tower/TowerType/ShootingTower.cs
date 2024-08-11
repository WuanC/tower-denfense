using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTower : Tower
{
    private EnemyHealth target;
    private ObjectPool<Projectile> projectilePool;

    [SerializeField] private Transform spawnProjectile;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private GameObject targetIndicator;
    [SerializeField] private float projectileSpeed;

    [SerializeField] private TowerAnimation towerAnimation;

    private float timer;
    protected override void Start()
    {
        base.Start();
        projectilePool = new ObjectPool<Projectile>(projectilePrefab, 3, spawnProjectile);
        if(towerAnimation != null ) 
        towerAnimation.OnReadyAnimation += SpawnProjectile;
    }

    private void Update()
    {
        FindEnemy();
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            PerformAction();            
        }

    }
    protected override void FindEnemy()
    {
        target = attackStrategy.FindTargetEnemy(transform.position, towerData.towerLevelDatas[currentLevel - 1].range, GameController.Instance.GetPathFinding());
        if (target != null)
        {
            targetIndicator.transform.position = Vector2.MoveTowards(targetIndicator.transform.position, target.transform.position, 10 * Time.deltaTime);
            ProcessRotateSpawnProjectile();
        }
    }
    public override void PerformAction()
    {
        if (target == null) return;
        if (towerAnimation != null)
        {
            towerAnimation.PlayAttack();
        }
        else
        {
            SpawnProjectile();
        }


    }
    private void SpawnProjectile()
    {
        if (target == null) return;
        Projectile projectile = projectilePool.GetObject();
        projectile.Initial(spawnProjectile, projectilePool, target, towerData.towerLevelDatas[currentLevel - 1].damage, projectileSpeed);
        timer = towerData.towerLevelDatas[currentLevel - 1].attackCooldown;
    }
    private void ProcessRotateSpawnProjectile()
    {
        if (towerAnimation == null) return;
            Vector3Int myTransform = GameController.Instance.GetPathFinding().GetCellInMap(transform.position);
        Vector3Int targetTransform = GameController.Instance.GetPathFinding().GetCellInMap(target.transform.position);
        if (myTransform.y >= targetTransform.y)
        {
            towerAnimation.transform.eulerAngles  = new Vector3(towerAnimation.transform.eulerAngles.x, 0, towerAnimation.transform.eulerAngles.z);
        }
        else
        {
            towerAnimation.transform.eulerAngles  = new Vector3(towerAnimation.transform.eulerAngles.x, 180, towerAnimation.transform.eulerAngles.z);
        }
    }
    public override void SetActiveStatus(bool value)
    {
        base.SetActiveStatus(value);
        targetIndicator.SetActive(value);
    }
}
