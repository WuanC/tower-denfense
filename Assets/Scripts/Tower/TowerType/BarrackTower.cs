using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BarrackTower : Tower
{

    [SerializeField] private Knight knightPrefabs;
    [SerializeField] private float moveSpeed;
    [SerializeField] Sprite[] spriteKnight;
    private ObjectPool<Knight> knightPool;
    private float timer = 0;
    protected override void Start()
    {
        base.Start();
        knightPool = new ObjectPool<Knight>(knightPrefabs, 10);
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            PerformAction();
            timer = towerData.towerLevelDatas[currentLevel - 1].attackCooldown;
        }
    }
    public override void PerformAction()
    {
        Knight knight = knightPool.GetObject();
        knight.Initial(knightPool, moveSpeed, towerData.towerLevelDatas[currentLevel - 1].damage, spriteKnight[currentLevel - 1]);

    }
    protected override void FindEnemy()
    {
        
    }
    public override bool UpdateTower()
    {
        return base.UpdateTower();
    }
}
