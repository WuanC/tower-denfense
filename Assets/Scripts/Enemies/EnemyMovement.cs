using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private ObjectPool<EnemyMovement> enemyPool;
    List<Vector3Int> path;
    [SerializeField] float moveSpeed;
    int targetIndex;

    private void OnEnable()
    {
        path = GameController.Instance.GetPathFinding().FindPath(GameController.Instance.GetPathFinding().startNode);
        targetIndex = 0;
        Spawn();    
    }
    private void Start()
    {
        EventManager.Instance.Register(EventID.OnPlaceTower, OnTowerPlace);
    }
    private void Update()
    {
        Move();
    }
    private void Spawn()
    {
        Vector3 pos = GameController.Instance.GetPathFinding().roadTile.GetCellCenterWorld(path[0]);
        transform.position = pos;
    }
        private void Move()
    {
        if (path != null && targetIndex < path.Count)
        {
            Vector3 targetPosition = GameController.Instance.GetPathFinding().roadTile.GetCellCenterWorld(path[targetIndex]);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetIndex++;
            }
        }
        else
        {
            EventManager.Instance.Broadcast(EventID.OnEnemyAttack, gameObject.GetComponent<EnemyHealth>().CurrentHealth);
            enemyPool.ReturnObject(this);
        }
    }
    public void Initialize(ObjectPool<EnemyMovement> pool,float speed, int health, int coins)
    {
        enemyPool = pool;
        moveSpeed = speed;
        gameObject.GetComponent<EnemyHealth>().MaxHealth = health;
    }
    public void OnTowerPlace(object obj)
    {
        Vector3Int currentPos = GameController.Instance.GetPathFinding().roadTile.WorldToCell(transform.position);
        path = GameController.Instance.GetPathFinding().FindPath(currentPos);
        targetIndex = 0;
        foreach(Vector3Int pos in path)
        {
            Debug.Log(pos.ToString());
        }
    }

}
