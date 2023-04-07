using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : BaseMenu
{
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private TextMeshProUGUI highscores;

    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayClick);
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
}
