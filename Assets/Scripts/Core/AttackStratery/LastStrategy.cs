using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStrategy : IAttackStratery
{
    public EnemyHealth FindTargetEnemy(Vector3 myPosittion, float range, PathFinding path)
    {
        if(path == null) return null;
        int minPosEnemy = int.MaxValue;
        float maxDistance = float.MaxValue;
        EnemyHealth targetEnemy = null;
        Collider2D[] colls = Physics2D.OverlapCircleAll(myPosittion, range);
        foreach (Collider2D collider2d in colls)
        {
            if (collider2d.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
            {
                Vector3Int enemyPos = path.roadTile.WorldToCell(enemy.transform.position);
                int enemyIndex = path.FindPath(path.startNode, path.endNode).FindIndex(p => p == enemyPos);
                if (enemyIndex <= minPosEnemy && enemyIndex + 1 < path.FindPath(path.startNode, path.endNode).Count)
                {
                    Vector3Int tmpVector3Int = path.FindPath(path.startNode, path.endNode)[enemyIndex + 1];
                    float currentEnemeyDistance = Vector2.Distance(enemy.transform.position,
                        path.roadTile.GetCellCenterWorld(tmpVector3Int));
                    if (enemyIndex == minPosEnemy)
                    {
                        if (currentEnemeyDistance <= maxDistance) continue;
                    }
                    maxDistance = currentEnemeyDistance;
                    minPosEnemy = enemyIndex;
                    targetEnemy = enemy;
                }
            }
        }
        return targetEnemy;
    }
}
