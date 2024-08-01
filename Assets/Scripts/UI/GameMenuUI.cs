using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button quitBtn;
    private void OnEnable()
    {
        
    }
    private void Start()
    {
        GameStateManager.Instance.SetState(GameState.Start);
        startBtn.onClick.AddListener(() => GameStateManager.Instance.SetState(GameState.SelectMode));
        quitBtn.onClick.AddListener(() => GameStateManager.Instance.SetState(GameState.Start));
    }
}
