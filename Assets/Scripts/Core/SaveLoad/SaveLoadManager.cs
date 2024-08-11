using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveLoadManager
{
    private static string GetFilePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }
    public static void Save(List<TowerSO> listTowerData, string fileName)
    {
        ButtonTowerWarpper buttonWarpper = new ButtonTowerWarpper();
        foreach(TowerSO twData in listTowerData)
        {
            
            buttonWarpper.listIdTowerData.Add(twData.Id);

        }

        string json = JsonUtility.ToJson(buttonWarpper, true);
        string path = GetFilePath(fileName);
        File.WriteAllText(path, json);
        Debug.Log("save: " + path);
    }
    public static List<int> LoadTower(string fileName)
    {
        string path = GetFilePath(fileName);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            ButtonTowerWarpper buttonListWrapper = JsonUtility.FromJson<ButtonTowerWarpper>(json);
            Debug.Log("load: " + path);
            return buttonListWrapper.listIdTowerData;
        }
        else
        {
            Debug.Log("loi");
            return new List<int>();
        }
    }
}
