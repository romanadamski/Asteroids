using UnityEngine;

public class MortalEnemyController : BaseMortalObjectController
{
    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.ASTEROID, GameObjectTagsConstants.PLAYER_BULLET };
    }

    protected override void OnTriggerWithEnemyEnter(Collider2D collision)
    {
        //todo audio

        gameObject.SetActive(false);
        Respawn();
    }
}
