using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStratery
{
    public EnemyHealth FindTargetEnemy(Vector3 myPosittion, float range, PathFinding path);
}
