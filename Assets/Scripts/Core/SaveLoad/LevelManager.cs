using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [SerializeField] Mode mode;
    [SerializeField] Button[] buttons;

    private void Start()
    {
        int unlockLevel = 1;
        switch (mode)
        {
            case Mode.Story:
                unlockLevel = PlayerPrefs.GetInt(GameConstants.STORY_UNLOCK, 1);
                break;
            case Mode.Maze:
                unlockLevel = PlayerPrefs.GetInt(GameConstants.MAZE_UNLOCK, 1);
                break;
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < unlockLevel) buttons[i].interactable = true;
            else buttons[i].interactable = false;
        }
    }
    public void LoadScene(int level)
    {
        string sceneName = null;
        switch (mode)
        {
            case Mode.Story:
                StaticLevel.currentLevelStoryMode = level;
                StaticLevel.mode = mode;
                sceneName = GameConstants.STORY_SCENE + level;
                break;
            case Mode.Maze:
                StaticLevel.currentLevelMazeMode = level;
                StaticLevel.mode = mode;
                sceneName = GameConstants.MAZE_SCENE + level;
                break;
        }

        StaticSceneManager.LoadScene(sceneName);
    }
}
public enum Mode
{
    Story,
    Maze,
}
