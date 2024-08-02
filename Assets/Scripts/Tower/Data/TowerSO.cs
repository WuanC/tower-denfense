using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TowerSO : ScriptableObject
{
    public string towerName;
    public Sprite icons;
    public int countLevel;
    public TowerLevelData[] towerLevelDatas;

    public Tower towerPrefab;
    public PreviewTower previewTowerPrefab;

    public int count;
    public int rarity;
}

