using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] float speed;
    private void Update()
    {
        if (target == null || !target.gameObject.activeSelf)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);

    }
    private void LateUpdate()
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
         {
            Destroy(gameObject);
            collision.gameObject.SetActive(false);
         }
    }
}
