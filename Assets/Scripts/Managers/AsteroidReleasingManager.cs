using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidReleasingManager : BaseManager<AsteroidReleasingManager>
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
        randomAsteroid.transform.position = _asteroidsRandomizeHelper.GetRandomAsteroidPositionOutsideScreen();
        ReleaseAsteroid(randomAsteroid);
    }

    public void ReleaseAsteroid(GameObject asteroid)
    {
        asteroid.gameObject.SetActive(true);

        asteroid.GetComponent<AsteroidMovementController>().
            Release(_asteroidsRandomizeHelper.GetRandomAsteroidDirectionDependsOnPosition(asteroid.transform),
            _asteroidsRandomizeHelper.GetRandomAsteroidSpeed());
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
