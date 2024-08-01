using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClosetStrategy : IAttackStratery
{
    public EnemyHealth FindTargetEnemy(Vector3 myPosittion, float range, PathFinding path)
    {
        float minDistance = Mathf.Infinity;
        EnemyHealth targetEnemy = null;
        Collider2D[] colls = Physics2D.OverlapCircleAll(myPosittion, range);
        foreach (Collider2D collider2d in colls)
        {
            if (collider2d.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
            {
                float distance = Vector2.Distance(myPosittion, collider2d.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    targetEnemy = enemy;
                }
            }
        }
        return targetEnemy;
    }
}
