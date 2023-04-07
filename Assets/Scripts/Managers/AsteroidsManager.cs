using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : BaseManager<AsteroidsManager>
{
    private Coroutine _releasingAsteroidsCoroutine;
    private AsteroidsRandomizeHelper _asteroidsRandomizeHelper;

    [SerializeField]
    private bool _isReleasingEnabled = true;

    private void Start()
    {
        _asteroidsRandomizeHelper = new AsteroidsRandomizeHelper();
    }

    public void StartReleasingAsteroidCoroutine()
    {
        StopReleasingAsteroidsCoroutine();
        _releasingAsteroidsCoroutine = StartCoroutine(ReleaseAsteroids());
    }

    //todo releasing asteroid
    private IEnumerator ReleaseAsteroids()
    {
        yield return new WaitUntil(() => _isReleasingEnabled);

        ReleaseRandomAsteroid();

        yield return new WaitForSecondsRealtime(_asteroidsRandomizeHelper.GetRandomAsteroidFrequency());

        //releasing in recursion
        _releasingAsteroidsCoroutine = StartCoroutine(ReleaseAsteroids());
    }

    private void ReleaseRandomAsteroid()
    {
        var randomAsteroid = GetRandomAsteroid();
        ReleaseAsteroid(randomAsteroid);
        var randomAsteroid2 = GetRandomAsteroid();
        ReleaseAsteroid(randomAsteroid2);

        randomAsteroid.transform.position = new Vector2(2.5f, 2);
        randomAsteroid2.transform.position = new Vector2(0, -2);
        randomAsteroid.GetComponent<AsteroidMovementController>().Release(_asteroidsRandomizeHelper.GetRandomAsteroidDirection(), _asteroidsRandomizeHelper.GetRandomAsteroidSpeed());
        randomAsteroid2.GetComponent<AsteroidMovementController>().Release(_asteroidsRandomizeHelper.GetRandomAsteroidDirection(), _asteroidsRandomizeHelper.GetRandomAsteroidSpeed());
    }

    public void ReleaseAsteroid(GameObject asteroid)
    {
        asteroid.gameObject.SetActive(true);
        asteroid.transform.position = Vector3.zero;
        //asteroid.GetComponent<AsteroidMovementController>().Release();

        //_asteroids.Add(asteroid);
    }

    public void StopReleasingAsteroidsCoroutine()
    {
        if (_releasingAsteroidsCoroutine == null) return;
        StopCoroutine(_releasingAsteroidsCoroutine);
    }

    private GameObject GetRandomAsteroid()
    {
        var allAsteroidTypes = ObjectPoolingManager.Instance.GetAllPoolableNamesByPoolableComponentType<AsteroidPoolableController>();
        var randomAsteroidType = allAsteroidTypes[Random.Range(0, allAsteroidTypes.Length)];
        return ObjectPoolingManager.Instance.GetFromPool(randomAsteroidType).gameObject;
    }
}
