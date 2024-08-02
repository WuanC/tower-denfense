using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTower : Tower
{
    [SerializeField] private EnemyHealth target;
    [SerializeField] private Transform spawnProjectile;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject targetIndicator;

    protected override void FindEnemy()
    {
        target = attackStrategy.FindTargetEnemy(transform.position, towerData.towerLevelDatas[currentLevel - 1].range, GameController.Instance.GetPathFinding());
        if (target != null)
        {
            targetIndicator.transform.position = Vector2.MoveTowards(targetIndicator.transform.position, target.transform.position, 10 * Time.deltaTime);
        }
    }
    public override void PerformAction()
    {
        if (target == null) return;

        canAttack = false;

        target.TakeDamage(towerData.towerLevelDatas[currentLevel - 1].damage);
        // yield return new WaitForSeconds(towerData.towerLevelDatas[currentLevel - 1].attackCooldown);
        canAttack = true;
    }
    public override void SetActiveStatus(bool value)
    {
        base.SetActiveStatus(value);
        targetIndicator.SetActive(value);
    }
}
