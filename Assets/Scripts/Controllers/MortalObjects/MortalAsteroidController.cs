using UnityEngine;

public class MortalAsteroidController : BaseMortalObjectController
{
    private const float POSITION_RANDOMIZE_RANGE = 0.8f;

    [SerializeField]
    private SerializableTuple<int, int> pieceCountRange;
    [SerializeField]
    private string pieceType;

    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.PLAYER_BULLET, GameObjectTagsConstants.ENEMY_BULLET };
    }

    protected override void OnTriggerWithEnemyEnter(Collider2D collider)
    {
        AsteroidReleasingManager.Instance.ObjectPoolingController.ReturnToPool(gameObject);
        var randomPieceCount = Random.Range(pieceCountRange.Item1, pieceCountRange.Item2 + 1);

        for (int i = 0; i < randomPieceCount; i++)
        {
            var asteroid = AsteroidReleasingManager.Instance.ObjectPoolingController.GetFromPool(pieceType);

            var randomPosition = new Vector2(
                Random.Range(transform.position.x - POSITION_RANDOMIZE_RANGE, transform.position.x + POSITION_RANDOMIZE_RANGE),
                Random.Range(transform.position.y - POSITION_RANDOMIZE_RANGE, transform.position.y + POSITION_RANDOMIZE_RANGE));
            asteroid.transform.position = randomPosition;

            AsteroidReleasingManager.Instance.ReleaseAsteroid(asteroid.gameObject);
        }
        //todo audio
        //EventsManager.Instance.OnAsteroidShotted(collider.tag);
    }
}
