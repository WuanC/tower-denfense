using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Knight : MonoBehaviour
{
    private PathFinding pathFinding;
    private List<Vector3Int> path;
    private int targetIndex;
    private ObjectPool<Knight> knightPool;
    private float moveSpeed;
    private int damage;
    [SerializeField] private SpriteRenderer knightSprite;
    private void OnEnable()
    {
        pathFinding = GameController.Instance.GetPathFinding();
        path = pathFinding.FindPath(pathFinding.endNode, pathFinding.startNode);
        if (path == null) return;
        targetIndex = 0;
        GetStartPos();

    }
    private void Start()
    {
        EventManager.Instance.Register(EventID.OnPlaceTower, Knight_OnTowerPlace);
    }
    private void GetStartPos()
    {
        Vector2 startPos = pathFinding.roadTile.GetCellCenterWorld(pathFinding.endNode);
        transform.position = startPos;
    }
    public void Knight_OnTowerPlace(object obj)
    {
        if (pathFinding is not PathFindingAStar) return;
        Vector3Int currentPos = pathFinding.roadTile.WorldToCell(transform.position);
        path = GameController.Instance.GetPathFinding().FindPath(currentPos, pathFinding.startNode);
        targetIndex = 1;
    }
    public void Initial(ObjectPool<Knight> pool ,float moveSpeed, int damage, Sprite knightSprite)
    {
        this.knightPool = pool;
        this.moveSpeed = moveSpeed;
        this.damage = damage;
        this.knightSprite.sprite = knightSprite;
    }
    private void Update()
    {
        if (path == null)
        {
            knightPool.ReturnObject(this);
        }
        else if (path != null && targetIndex < path.Count)
        {
            Vector3 targetPosition = pathFinding.roadTile.GetCellCenterWorld(path[targetIndex]);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetIndex++;
            }
        }
        else if (targetIndex >= path.Count)
        {            
            knightPool.ReturnObject(this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<EnemyHealth>(out EnemyHealth enemy)){
            enemy.TakeDamage(damage);
            knightPool.ReturnObject(this);
        }
    }
}
