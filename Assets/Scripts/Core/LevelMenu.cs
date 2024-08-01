using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    private void Start()
    {
        int unlockLevel = PlayerPrefs.GetInt("UnlockLevel", 1);
        Debug.Log(unlockLevel);
        for(int i = 0; i < buttons.Length; i++)
        {
            if (i < unlockLevel) buttons[i].interactable = true;
            else buttons[i].interactable = false;
        }

    }
    public void LoadScene(int level)
    {
        StaticLevel.currentLevelStoryMode = level;
        StaticSceneManager.LoadScene("Level " + level);
    }
}
