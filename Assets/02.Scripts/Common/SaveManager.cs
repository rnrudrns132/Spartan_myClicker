using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class GameData
{
    public int Gold;
    public int Point;

    public int nowStage;
    public int nowEnemyCnt;

    public int[] UpgradeLvs;
    public Dictionary<int, int> WeaponDatas;
    public int nowWeaponIndex;

    public GameData()
    {
        UpgradeLvs = new int[3];
        WeaponDatas = new Dictionary<int, int>();
        WeaponDatas.Add(0, 0);
    }
}

public class SaveManager : MonoBehaviour
{
    string path;

    public void Initiailzer()
    {
        path = Path.Combine(Application.dataPath, "gameData.json");
    }

    public void SaveData(GameData target)
    {
        string jsonData = JsonConvert.SerializeObject(target);
        File.WriteAllText(path, jsonData);
    }
    public bool TryLoadData(out GameData data)
    {
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            data = JsonConvert.DeserializeObject<GameData>(jsonData);
            return true;
        }
        else
        {
            data = null;
            return false;
        }
    }
}
