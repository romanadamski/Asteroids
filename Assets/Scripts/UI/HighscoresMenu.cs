using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HighscoresMenu : BaseMenu
{
    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private TextMeshProUGUI highscores;

    private void Awake()
    {
        menuButton.onClick.AddListener(OnMenuClick);
    }

    public override void Show()
    {
        base.Show();
        SetHighscores();
    }

    private void OnMenuClick()
    {
        GameManager.Instance.GoToMainMenu();
    }

    private void SetHighscores()
    {
        var allHighscores = string.Join("\n", SaveManager.Instance.GetHighscore());
        highscores.text = allHighscores;
    }
}
