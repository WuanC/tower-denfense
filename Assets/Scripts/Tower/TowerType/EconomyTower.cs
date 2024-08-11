using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyTower : Tower
{
    protected override void Start()
    {
        base.Start();
        EventManager.Instance.Register(EventID.OnWaveStart, EconomyTower_OnWaveStart);
    }
    private void EconomyTower_OnWaveStart(object obj)
    {
        if(obj is WaveInfo waveInfo)
        {
            if (waveInfo.currentWave == 1) return;
            PerformAction();
        }
    }
    protected override void FindEnemy()
    {

    }
    public override void PerformAction()
    {
        EventManager.Instance.Broadcast(EventID.OnDepositeCoins, towerData.towerLevelDatas[currentLevel - 1].damage);
    }
    protected override void OnDestroy()
    {
       base.OnDestroy();
       EventManager.Instance.Unregister(EventID.OnWaveStart, EconomyTower_OnWaveStart);
    }
}
