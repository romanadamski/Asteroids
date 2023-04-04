using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GameplayMenu : BaseMenu
{
    [SerializeField]
    TextMeshProUGUI livesCounter;

    public void SetLivesCounter(uint lives)
    {
        livesCounter.text = lives.ToString();
    }
}
