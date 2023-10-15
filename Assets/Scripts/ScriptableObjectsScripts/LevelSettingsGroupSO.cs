using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelSettingsGroup", fileName = "LevelSettingsGroup")]
public class LevelSettingsGroupSO : ScriptableObject
{
    [SerializeField]
    private List<LevelSettingsSO> levelSettings;
    public List<LevelSettingsSO> LevelSettings => levelSettings;

    public LevelSettingsSO GetLevelByNumber(uint levelNumber)
    {
        return levelSettings.FirstOrDefault(x => x.LevelNumber.Equals(levelNumber));
    }
}
