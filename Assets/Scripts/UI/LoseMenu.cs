using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseMenu : BaseMenu
{
    [SerializeField]
    private Button goToMainMenuButton;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private TextMeshProUGUI currentScore;
    [SerializeField]
    private TextMeshProUGUI highestScore;

    private void Awake()
    {
        goToMainMenuButton.onClick.AddListener(OnGoToMainMenuClick);
        restartButton.onClick.AddListener(OnRestartClick);
    }

    public override void Show()
    {
        base.Show();
        currentScore.text = GameplayManager.Instance.CurrentScore.ToString();
        highestScore.text = SaveManager.Instance.GetHighestScore().ToString();
    }

    private void OnGoToMainMenuClick()
    {
        GameplayManager.Instance.SetEndGameplayState();
    }

    private void OnRestartClick()
    {
        GameManager.Instance.SetLevelState();
    }
}
