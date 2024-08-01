using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventID
{
    #region Wave Info
    OnWaveStart,
    OnSkipWave,
    OnWaveEnd,
    #endregion

    #region coins event
    OnCurrentCoinsChange,
    OnDepositeCoins,
    OnWithDrawCoins,
    #endregion

    #region Player Health event
    OnPlayerHealthChange,
    OnEnemyAttack,
    #endregion

    OnPlaceTower,

    OnEnemyDied,

    OnClickButtonTower,
    OnGameStateChanged,

}
