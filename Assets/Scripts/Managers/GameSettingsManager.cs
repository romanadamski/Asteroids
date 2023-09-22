using UnityEngine;

public class GameSettingsManager : BaseManager<GameSettingsManager>
{
    [SerializeField]
    private GameSettingsSO settings;
    public GameSettingsSO Settings => settings;
}
