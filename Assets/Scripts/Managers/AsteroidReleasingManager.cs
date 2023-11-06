using UnityEngine;

public class AsteroidReleasingManager : BaseManager<AsteroidReleasingManager>
{
    [SerializeField]
    private bool _isReleasingEnabled = true;
    [SerializeField]
    private ObjectPoolingController objectPoolingController;

    public void StartReleasingAsteroidCoroutine()
    {
        InvokeRepeating(nameof(ReleaseAsteroids), 0, AsteroidsRandomizeHelper.GetRandomAsteroidFrequency());
    }

    private void ReleaseAsteroids()
    {
        if (!_isReleasingEnabled) return;

        ReleaseRandomAsteroid();
    }

    private void ReleaseRandomAsteroid()
    {
        var randomAsteroid = GetRandomAsteroid();
        randomAsteroid.transform.position = AsteroidsRandomizeHelper.GetRandomAsteroidPositionOutsideScreen();
        ReleaseAsteroid(randomAsteroid);
    }

    public void ReleaseAsteroid(GameObject asteroid)
    {
        asteroid.gameObject.SetActive(true);
    }

    public void StopReleasingAsteroidsCoroutine()
    {
        CancelInvoke(nameof(ReleaseAsteroids));
    }

    private GameObject GetRandomAsteroid()
    {
        string[] allAsteroidTypes = new string[objectPoolingController.Pools.Count];
        for (int i = 0; i < objectPoolingController.Pools.Count; i++)
        {
            allAsteroidTypes[i] = objectPoolingController.Pools[i].PoolableNameType;
        }
        var randomAsteroidType = allAsteroidTypes[Random.Range(0, allAsteroidTypes.Length)];
        return objectPoolingController.GetFromPool(randomAsteroidType).gameObject;
    }
}
