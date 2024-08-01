using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private Color32 victoryColor;
    [SerializeField] private Color32 defeatColor;

    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Image titleImage;

    [SerializeField] private Button homeBtn;
    [SerializeField] private Button alterBtn;

    public void SetUI(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.Victory:
                titleText.text = "Victory!";
                titleImage.color = victoryColor;
                homeBtn.GetComponent<Image>().color = victoryColor;
                homeBtn.onClick.AddListener(() => StaticSceneManager.LoadScene("MenuScene"));

                alterBtn.GetComponent<Image>().color = victoryColor;


                alterBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
                gameObject.SetActive(true);
                break;
            case GameState.Defeat:

                titleText.text = "Defeat!";
                titleImage.color = defeatColor;
                homeBtn.GetComponent<Image>().color = defeatColor;
                homeBtn.onClick.AddListener(() => StaticSceneManager.LoadScene("MenuScene"));

                alterBtn.GetComponent<Image>().color = defeatColor;
                alterBtn.onClick.AddListener(StaticSceneManager.LoadCurrentScene);

                alterBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Again";
                gameObject.SetActive(true);
                break;
            default: 
                gameObject.SetActive(false);
                break;
        }
    }
    private void OnDisable()
    {
        homeBtn.onClick.RemoveAllListeners();
        alterBtn.onClick.RemoveAllListeners();
    }
}
