using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoseMenu : BaseMenu
{
    [SerializeField]
    Button goToMainMenuButton;

    void Awake()
    {
        goToMainMenuButton.onClick.AddListener(OnGoToMainMenuClick);
    }

    private void OnGoToMainMenuClick()
    {
        GameplayManager.Instance.SetEndGameplayState();
    }
}
