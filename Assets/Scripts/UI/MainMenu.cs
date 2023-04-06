using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class MainMenu : BaseMenu
{
    [SerializeField]
    Button playButton;
    [SerializeField]
    TextMeshProUGUI highscores;

    void Awake()
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
        GameManager.Instance.StartGameplay();
    }

    private void SetHighscores()
    {
        var allHighscores = string.Join("\n", SaveManager.Instance.GetHighscore());
        highscores.text = allHighscores;
    }
}
