using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    
    [SerializeField] private Button skipWaveBtn;
    [SerializeField] private Button startBtn;
    [SerializeField] private Button optionBtn;
    [SerializeField] private Button homeBtn;

    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI currentHealthText;

    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private GameObject optionPanelUI;
    private void Start()
    {
        startBtn.onClick.AddListener(() => GameStateManager.Instance.SetState(GameState.Playing));
        skipWaveBtn.onClick.AddListener(() => EventManager.Instance.Broadcast(EventID.OnSkipWave, null));
        optionBtn.onClick.AddListener(() =>
        {
            bool isActive = optionPanelUI.activeSelf;
            if(isActive) { Time.timeScale = 1.0f; }
            else { Time.timeScale = 0; }
            optionPanelUI.SetActive(!isActive);
        });
        homeBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            StaticSceneManager.LoadScene("MenuScene");
        });
        EventManager.Instance.Register(EventID.OnGameStateChanged, OnStateChanged);

        EventManager.Instance.Register(EventID.OnCurrentCoinsChange, OnCurrentCoinsChange);

        EventManager.Instance.Register(EventID.OnWaveStart, OnWaveStart);
        EventManager.Instance.Register(EventID.OnWaveEnd, OnWaveEnd);
        EventManager.Instance.Register(EventID.OnSkipWave, OnSkipWave);

        EventManager.Instance.Register(EventID.OnPlayerHealthChange, SetPlayerHp);

    }
    #region wave ui
    private void OnWaveEnd(object obj)
    {
        if(obj is bool active)
        skipWaveBtn.gameObject.SetActive(active);
    }
    private void OnSkipWave(object obj)
    {
        skipWaveBtn.gameObject.SetActive(false);
    }
    private void OnWaveStart(object obj)
    {
        if (obj is WaveInfo waveInfo)
        {
            currentWaveText.text = waveInfo.currentWave.ToString() + "/" + waveInfo.waveLength;
            skipWaveBtn.gameObject.SetActive(false);
            EventManager.Instance.Broadcast(EventID.OnDepositeCoins, waveInfo.bonusCoins);
        }
    }
    #endregion

    #region coins ui
    private void OnCurrentCoinsChange(object obj)
    {
        if (obj is int coins)
            coinsText.text = coins.ToString() + "$";
    }
    #endregion


    private void SetPlayerHp(object obj)
    {
        if(obj is HealthStats health)
        {
            currentHealthText.text = health.currentHealth.ToString() + "/"+ health.maxHealth.ToString() ;
        }
    }
    private void OnStateChanged(object obj)
    {
        if(obj is GameState gameState)
        {
            switch(gameState)
            {
                case GameState.Selection:
                    break;
                case GameState.Playing:
                    startBtn.gameObject.SetActive(false);
                    optionBtn.gameObject.SetActive(true);
                    break;
                case GameState.Defeat:
                    gameOverUI.SetUI(gameState);
                    optionBtn.gameObject.SetActive(false);
                    break;
                case GameState.Victory:
                    gameOverUI.SetUI(gameState);
                    optionBtn.gameObject.SetActive(false);
                    break;
            }
        }
    }
    private void OnDisable()
    {
        skipWaveBtn.onClick.RemoveAllListeners();
        startBtn.onClick.RemoveAllListeners();
        optionBtn.onClick.RemoveAllListeners();
        if (EventManager.Instance == null) return;
        EventManager.Instance.Unregister(EventID.OnGameStateChanged, OnStateChanged);
        EventManager.Instance.Unregister(EventID.OnCurrentCoinsChange, OnCurrentCoinsChange);
        EventManager.Instance.Unregister(EventID.OnWaveStart, OnWaveStart);
        EventManager.Instance.Unregister(EventID.OnWaveEnd, OnWaveEnd);
        EventManager.Instance.Unregister(EventID.OnSkipWave, OnSkipWave);
        EventManager.Instance.Unregister(EventID.OnPlayerHealthChange, SetPlayerHp);
    }

}
