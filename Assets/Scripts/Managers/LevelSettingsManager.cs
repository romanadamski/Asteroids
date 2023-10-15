using System.Linq;
using UnityEngine;

public class LevelSettingsManager : BaseManager<LevelSettingsManager>
{
    [SerializeField]
    private LevelSettingsGroupSO levelSettings;
    public LevelSettingsGroupSO LevelSettings => levelSettings;

    public uint CurrentLevelNumber { get; private set; }

    public LevelSettingsSO CurrentLevel { get; private set; }

    public void SetCurrentLevel()
    {
        CurrentLevel = GetLevelByNumber(CurrentLevelNumber);
    }

    public void SetLevelNumber(uint levelNumber)
    {
        CurrentLevelNumber = levelNumber;
    }

    private LevelSettingsSO GetLevelByNumber(uint levelNumber)
    {
        return levelSettings.GetLevelByNumber(levelNumber);
    }
}
