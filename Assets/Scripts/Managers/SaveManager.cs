using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : BaseManager<SaveManager>
{
    private const string SAVE_KEY = "SAVE";
    private SaveData saveData;

    public void Save()
    {
        var saveString = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_KEY, saveString);
    }

    public void LoadSave()
    {
        var saveString = PlayerPrefs.GetString(SAVE_KEY);
        if (string.IsNullOrWhiteSpace(saveString))
        {
            saveData = new SaveData();
        }
        else
        {
            saveData = JsonUtility.FromJson<SaveData>(saveString);
        }
    }

    public List<uint> GetHighscore()
    {
        return saveData.Highscore.OrderByDescending(x => x).ToList();
    }

    public uint GetHighestScore()
    {
        var highscore = GetHighscore();
        return highscore.Count > 0
            ? highscore.First()
            : 0;
    }

    public void SetHighscore(uint score)
    {
        saveData.Highscore.Add(score);
        saveData.Highscore = saveData.Highscore.OrderByDescending(x => x).ToList();
        if (saveData.Highscore.Count > 10)
        {
            saveData.Highscore.Remove(saveData.Highscore.Last());
        }
    }
}
