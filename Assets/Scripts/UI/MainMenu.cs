using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MainMenu : BaseMenu
{
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private TextMeshProUGUI highscores;
    [SerializeField]
    private TMP_Dropdown levelsDropdown;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayClick);
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

        foreach (var level in LevelSettingsManager.Instance.LevelSettings)
        {
            dropdownData.Add(new CustomOptionData
            {
                text = $"Level {level.LevelNumber}",
                value = level.LevelNumber
            });
        }

        levelsDropdown.AddOptions(dropdownData);
    }

    public override void Show()
    {
        base.Show();
        SetHighscores();
    }

    private void OnPlayClick()
    {
        GameManager.Instance.SetLevelState();
    }

    private void SetHighscores()
    {
        var allHighscores = string.Join("\n", SaveManager.Instance.GetHighscore());
        highscores.text = allHighscores;
    }

    private void OnLevelsDropdownValueChanged(int dropdownIndex)
    {
        var levelDropdownValue = levelsDropdown.options[dropdownIndex] as CustomOptionData;
        LevelSettingsManager.Instance.SetLevelNumber(levelDropdownValue.value);
    }
}
