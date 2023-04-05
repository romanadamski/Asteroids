using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelSettingsManager : BaseManager<LevelSettingsManager>
{
    [SerializeField]
    private List<LevelSettingsScriptableObject> levelSettings;
    public List<LevelSettingsScriptableObject> LevelSettings => levelSettings;

    [SerializeField]
    private uint currentLevelNumber;

    public LevelSettingsScriptableObject CurrentLevel { get; private set; }

    public void Init()
    {
        SetCurrentLevel();
    }

    private void SetCurrentLevel()
    {
        CurrentLevel = GetLevelByNumber(currentLevelNumber);
    }

    public LevelSettingsScriptableObject GetLevelByNumber(uint levelNumber)
    {
        return levelSettings.FirstOrDefault(x => x.LevelNumber.Equals(levelNumber));
    }
}
