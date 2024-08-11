using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{

    protected override void FindEnemy(Vector3 target)
    {
        target = target + new Vector3(0, 0.25f);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
