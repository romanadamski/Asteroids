using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplayMenu : BaseMenu
{
    [SerializeField]
    private TextMeshProUGUI livesCounter;
    [SerializeField]
    private TextMeshProUGUI levelNumberCounter;
    [SerializeField]
    private TextMeshProUGUI scoreCounter;
    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private Image background;

    private void Awake()
    {
        SubscribeToEvents();
        menuButton.onClick.AddListener(OnGoToMainMenuClick);
    }

    private void SubscribeToEvents()
    {
        EventsManager.Instance.PlayerLoseLife += OnPlayerLoseLife;
        EventsManager.Instance.PlayerSpawned += OnPlayerSpawned;
        EventsManager.Instance.LevelStarted += OnGameplayStarted;
        EventsManager.Instance.ScoreUpdated += OnScoreUpdated;
    }

    private void OnGoToMainMenuClick()
    {
        GameplayManager.Instance.SetEndGameplayState();
    }

    private void OnGameplayStarted(LevelSettingsSO level)
    {
        levelNumberCounter.text = level.LevelNumber.ToString();
        background.sprite = level.BackgroundSprite;
    }

    private void OnPlayerSpawned(uint lives)
    {
        SetLivesCounter(lives);
    }

    private void OnPlayerLoseLife(uint lives)
    {
        SetLivesCounter(lives);
    }

    private void OnScoreUpdated(uint score)
    {
        scoreCounter.text = score.ToString();
    }

    private void SetLivesCounter(uint lives)
    {
        livesCounter.text = lives.ToString();
    }

    private void UnsubscribeFromEvents()
    {
        if (!EventsManager.Instance) return;

        EventsManager.Instance.PlayerLoseLife -= OnPlayerLoseLife;
        EventsManager.Instance.PlayerSpawned -= OnPlayerSpawned;
        EventsManager.Instance.LevelStarted -= OnGameplayStarted;
        EventsManager.Instance.ScoreUpdated -= OnScoreUpdated;
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}
