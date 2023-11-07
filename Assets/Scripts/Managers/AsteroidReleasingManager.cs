using UnityEngine;

[RequireComponent(typeof(ObjectPoolingController))]
public class AsteroidReleasingManager : BaseManager<AsteroidReleasingManager>
{
    [SerializeField]
    private bool _isReleasingEnabled = true;
    
    public ObjectPoolingController ObjectPoolingController { get; private set; }

    private void Awake()
    {
        ObjectPoolingController = GetComponent<ObjectPoolingController>();
    }

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
        string[] allAsteroidTypes = new string[ObjectPoolingController.Pools.Count];
        for (int i = 0; i < ObjectPoolingController.Pools.Count; i++)
        {
            allAsteroidTypes[i] = ObjectPoolingController.Pools[i].PoolableNameType;
        }
        var randomAsteroidType = allAsteroidTypes[Random.Range(0, allAsteroidTypes.Length)];
        return ObjectPoolingController.GetFromPool(randomAsteroidType).gameObject;
    }
}
