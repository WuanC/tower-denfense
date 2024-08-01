using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FirstStratery : IAttackStratery
{
    public EnemyHealth FindTargetEnemy(Vector3 myPosition, float range, PathFinding path)
    {
        int minPosEnemy = 0;
        float minDistance = Mathf.Infinity;
        EnemyHealth targetEnemy = null;
        Collider2D[] colls = Physics2D.OverlapCircleAll(myPosition, range);
        foreach(Collider2D collider2d in colls)
        {
            if(collider2d.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
            {
                Vector3Int enemyCellPos = path.roadTile.WorldToCell(enemy.transform.position);
   
                int enemyIndex = path.FindPath(path.startNode).FindIndex(p => p == enemyCellPos);
                
                if (enemyIndex >= minPosEnemy)
                {
                    Vector3Int tmpVector3Int;
                    if (enemyIndex == path.FindPath(path.startNode).Count - 1)
                    {
                        tmpVector3Int = path.FindPath(path.startNode)[enemyIndex];
                    }
                    else
                    {
                        tmpVector3Int = path.FindPath(path.startNode)[enemyIndex + 1];
                    }

                    float currentEnemeyDistance = Vector2.Distance(enemy.transform.position,
                        path.roadTile.GetCellCenterWorld(tmpVector3Int));
                    if (enemyIndex == minPosEnemy)
                    {
                        if (currentEnemeyDistance >= minDistance) continue;
                    }

                    minDistance = currentEnemeyDistance;
                    minPosEnemy = enemyIndex;
                    targetEnemy = enemy;

                }
            }

        }
        return targetEnemy;
    }
}
