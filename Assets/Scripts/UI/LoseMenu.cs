using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;

public class LoseMenu : BaseMenu
{
    [SerializeField]
    private Button goToMainMenuButton;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    TextMeshProUGUI currentScore;
    [SerializeField]
    TextMeshProUGUI highestScore;

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
