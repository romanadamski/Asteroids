using UnityEngine;

public class MortalAsteroidController : BaseMortalObjectController
{
    private const float POSITION_RANDOMIZE_RANGE = 0.8f;

    [SerializeField]
    private SerializableTuple<int, int> pieceCount;
    [SerializeField]
    private string pieceType;

    protected override string[] GetEnemies()
    {
        return new string[] { GameObjectTagsConstants.BULLET };
    }

    protected override void OnCollisionWithEnemyEnter(Collision2D collision)
    {
        ObjectPoolingManager.Instance.ReturnToPool(gameObject.GetComponent<BasePoolableController>());
        var randomPieceCount = Random.Range(pieceCount.Item1, pieceCount.Item2 + 1);

        for (int i = 0; i < randomPieceCount; i++)
        {
            var asteroid = ObjectPoolingManager.Instance.GetFromPool(pieceType);
            
            asteroid.transform.position = new Vector2(Random.Range(transform.position.x - POSITION_RANDOMIZE_RANGE, transform.position.x + POSITION_RANDOMIZE_RANGE),
                Random.Range(transform.position.y - POSITION_RANDOMIZE_RANGE, transform.position.y + POSITION_RANDOMIZE_RANGE));

            AsteroidReleasingManager.Instance.ReleaseAsteroid(asteroid.gameObject);
        }

        EventsManager.Instance.OnAsteroidShotted();
    }
}
