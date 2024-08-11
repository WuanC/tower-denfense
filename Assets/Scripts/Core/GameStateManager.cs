using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Start,
    SelectMode,
    Selection,
    Playing,
    Victory,
    Defeat,
}
public class GameStateManager : Singleton<GameStateManager>
{
    public GameState currentState;

    public void SetState(GameState state)
    {
        currentState = state;
        EventManager.Instance.Broadcast(EventID.OnGameStateChanged, currentState);
    }
}
