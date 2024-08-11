using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected int damage;
    protected float speed;
    protected ObjectPool<Projectile> projectilePool;
    protected Transform startTransform;
    protected EnemyHealth target;



    protected float timeElapsed;
    protected Vector3 lastPosTarget;

    protected virtual void OnEnable()
    {
        if(startTransform != null)
        {
            transform.position = startTransform.position;
        }
        timeElapsed = 0;


    }
    protected virtual void Update()
    {
        timeElapsed += Time.deltaTime;
        if (target != null)
        {
            FindEnemy(target.transform.position);
            lastPosTarget = target.transform.position;
        }
        else
        {
            FindEnemy(lastPosTarget);
        }
            

        if(timeElapsed >= 1) {
            projectilePool.ReturnObject(this);
        }
    }
    protected virtual void FindEnemy(Vector3 target)
    {

    }
    public virtual void Initial(Transform posSpawn ,ObjectPool<Projectile> projectilePool, EnemyHealth target, int damage, float speed)
    {
        this.startTransform = posSpawn;
        this.projectilePool = projectilePool;
        this.target = target;
        this.damage = damage;
        this.speed = speed;
        lastPosTarget = target.transform.position;
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
         {
            enemyHealth.TakeDamage(damage);
            projectilePool.ReturnObject(this);
        }
    }
}
