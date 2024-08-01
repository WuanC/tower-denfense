using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewTower : MonoBehaviour
{
    [SerializeField] TowerSO towerData;
    [SerializeField] GameObject rangeSkillIndicator;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        int currentLevel = 1;
        float tmp = towerData.towerLevelDatas[currentLevel - 1].range * 2;
        rangeSkillIndicator.transform.localScale = new Vector3(tmp, tmp, tmp);
    }
}
