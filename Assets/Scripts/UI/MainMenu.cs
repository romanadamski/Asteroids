using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MainMenu : BaseMenu
{
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private TMP_Dropdown levelsDropdown;
    [SerializeField]
    private Button highscoresButton;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayClick);
        highscoresButton.onClick.AddListener(OnHighscoresClick);
        levelsDropdown.onValueChanged.AddListener(OnLevelsDropdownValueChanged);
    }

    private void Start()
    {
        SetDropdownValues();
        OnLevelsDropdownValueChanged(levelsDropdown.value);
    }

    private void SetDropdownValues()
    {
        List<TMP_Dropdown.OptionData> dropdownData = new List<TMP_Dropdown.OptionData>();

        foreach (var level in LevelSettingsManager.Instance.LevelSettings.LevelSettings)
        {
            dropdownData.Add(new CustomOptionData
            {
                text = $"Level {level.LevelNumber}",
                value = level.LevelNumber
            });
        }

        levelsDropdown.AddOptions(dropdownData);
    }

    private void OnPlayClick()
    {
        GameManager.Instance.SetLevelState();
    }

    private void OnHighscoresClick()
    {
        GameManager.Instance.SetHighscoresState();
    }

    private void OnLevelsDropdownValueChanged(int dropdownIndex)
    {
        var levelDropdownValue = levelsDropdown.options[dropdownIndex] as CustomOptionData;
        LevelSettingsManager.Instance.SetLevelNumber(levelDropdownValue.value);
    }
}
