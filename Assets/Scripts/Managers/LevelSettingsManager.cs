using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelSettingsManager : BaseManager<LevelSettingsManager>
{
    [SerializeField]
    private List<LevelSettingsSO> levelSettings;
    public List<LevelSettingsSO> LevelSettings => levelSettings;

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
        return levelSettings.FirstOrDefault(x => x.LevelNumber.Equals(levelNumber));
    }
}
