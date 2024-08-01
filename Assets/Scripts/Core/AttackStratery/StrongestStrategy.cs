using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongestStrategy : IAttackStratery
{
    public EnemyHealth FindTargetEnemy(Vector3 myPosittion, float range, PathFinding path)
    {
        int minPosEnemy = 0;
        EnemyHealth targetEnemy = null;
        int lastIndex = path.FindPath(path.startNode).FindIndex(node => node == path.endNode);
        Collider2D[] colls = Physics2D.OverlapCircleAll(myPosittion, range);
        foreach (Collider2D collider2d in colls)
        {
            if (collider2d.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
            {
                Vector3Int enemyPos = path.roadTile.WorldToCell(enemy.transform.position);
                int enemyIndex = path.FindPath(path.startNode).FindIndex(p => p == enemyPos);
                if (minPosEnemy > enemyIndex)
                {
                    minPosEnemy = enemyIndex;
                    targetEnemy = enemy;
                }
            }
        }
        return targetEnemy;
    }
}
