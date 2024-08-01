using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaUI : MonoBehaviour
{
    [SerializeField] private GachaSystem gachaSystem;
    [SerializeField] private ShowGachaUI gachaResultUI;
    [SerializeField] private Image gachaImage;
    [SerializeField] private TextMeshProUGUI gachaName;

    [SerializeField] private Image mythicPityImage;
    [SerializeField] private TextMeshProUGUI mythicPityText;

    [SerializeField] private Image legendaryPityImage;
    [SerializeField] private TextMeshProUGUI legendaryPityText;
    private List<TowerButton> towerShow;
    private int currentIndex = 0;
    private void Start()
    {
        gachaSystem.OnGachaSucess += GachaSystem_OnGachaSucess;
        gachaResultUI.OnShowNext += GachaResultUI_OnShowNext;
        mythicPityImage.fillAmount = 0;
        mythicPityText.text = "Mythic Pity: 0 / " + GachaSystem.MYTHIC_PITY;
    }

    private void GachaResultUI_OnShowNext()
    {
        if(currentIndex < towerShow.Count)
        {
            gachaImage.sprite = towerShow[currentIndex].TowerSO.icons;
            gachaName.text = towerShow[currentIndex].TowerSO.name;
            gachaResultUI.gameObject.SetActive(true);
            currentIndex++;
        }
    }

    private void GachaSystem_OnGachaSucess(List<TowerButton> obj, int countOfRoll)
    {
        towerShow = obj;
        currentIndex = 0;
        GachaResultUI_OnShowNext();
        mythicPityText.text = $"Mythic Pity: {countOfRoll} / {GachaSystem.MYTHIC_PITY}";
        mythicPityImage.fillAmount = (float) countOfRoll / GachaSystem.MYTHIC_PITY;
    }
}
