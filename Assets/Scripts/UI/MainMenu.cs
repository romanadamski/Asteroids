using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenu : BaseMenu
{
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button highscoresButton;
    [SerializeField]
    private LevelButton levelButtonPrefab;
    [SerializeField]
    private ObjectPoolingController buttonPooling;

    private List<LevelButton> _levelButtons = new List<LevelButton>();

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayClick);
        highscoresButton.onClick.AddListener(OnHighscoresClick);
    }

    private void Start()
    {
        SetLevels();
    }

    private void SetLevels()
    {
        foreach (var level in LevelSettingsManager.Instance.LevelSettings.LevelSettings)
        {
            var levelButton = buttonPooling.GetFromPool().GetComponent<LevelButton>();
            levelButton.Init(level.LevelNumber);
            levelButton.gameObject.SetActive(true);
            _levelButtons.Add(levelButton);
        }
    }

    private void OnPlayClick()
    {
        GameManager.Instance.SetLevelState();
    }

    private void OnHighscoresClick()
    {
        GameManager.Instance.SetHighscoresState();
    }
}
