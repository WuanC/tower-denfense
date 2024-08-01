using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private TowerSO towerSO;
    [SerializeField] private TextMeshProUGUI costText;

    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(() => EventManager.Instance.Broadcast(EventID.OnClickButtonTower, towerSO));
        costText.text = towerSO.towerLevelDatas[0].cost.ToString() + "$";
        GetComponent<Image>().sprite = towerSO.icons;
    }
    public TowerSO TowerSO { get { return towerSO; } }
}
