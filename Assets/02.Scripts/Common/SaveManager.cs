using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveManager : MonoBehaviour
{
    string path;

    public void Initiailzer()
    {
        path = Path.Combine(Application.dataPath, "gameData.json");
    }

    public void SaveData(PlayerData target)
    {
        string jsonData = JsonConvert.SerializeObject(target);
        File.WriteAllText(path, jsonData);
    }
    public bool TryLoadData(out PlayerData data)
    {
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            data = JsonConvert.DeserializeObject<PlayerData>(jsonData);
            return true;
        }
        else
        {
            data = null;
            return false;
        }
    }
}
