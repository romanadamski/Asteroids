using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MortalPlayerController : BaseMortalObjectController
{
    private GameplayMenu _gameplayMenu;//todo move to events
    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTags.ASTEROID, GameObjectTags.BULLET };
    }

    protected override void OnCollisionWithEnemy(Collision2D collision)
    {
        DecrementLive();
        if (_gameplayMenu || UIManager.Instance.TryGetMenuByType(out _gameplayMenu))
        {
            _gameplayMenu.SetLivesCounter(_livesCount);
        }
        EventsManager.Instance.OnPlayerLoseLife(_livesCount);
    }

    private void Start()
    {
        _livesCount = SettingsManager.Instance.Settings.PlayerStartLivesCount;
        if (_gameplayMenu || UIManager.Instance.TryGetMenuByType(out _gameplayMenu))
        {
            _gameplayMenu.SetLivesCounter(_livesCount);
        }
    }
}
