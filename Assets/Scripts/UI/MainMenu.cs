using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenu : BaseMenu
{
    [SerializeField]
    Button playButton;

    void Awake()
    {
        playButton.onClick.AddListener(OnPlayClick);
    }

    private void OnPlayClick()
    {
        GameManager.Instance.StartGameplay();
    }
}
