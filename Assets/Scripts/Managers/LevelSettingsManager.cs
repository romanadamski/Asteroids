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
    public uint CurrentLevelNumber => currentLevelNumber;

    public LevelSettingsScriptableObject CurrentLevel { get; private set; }

    public void SetCurrentLevel()
    {
        CurrentLevel = GetLevelByNumber(currentLevelNumber);
    }

    private LevelSettingsScriptableObject GetLevelByNumber(uint levelNumber)
    {
        return levelSettings.FirstOrDefault(x => x.LevelNumber.Equals(levelNumber));
    }
}
