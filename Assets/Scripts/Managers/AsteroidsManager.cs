using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : BaseManager<AsteroidsManager>
{
    private Coroutine _releasingAsteroidsCoroutine;
    private List<GameObject> _asteroids { get; } = new List<GameObject>();
    private bool _isReleasingEnabled = true;

    public void StartReleasingAsteroidCoroutine()
    {
        StopReleasingAsteroidsCoroutine();
        _releasingAsteroidsCoroutine = StartCoroutine(ReleaseAsteroids());
    }

    private IEnumerator ReleaseAsteroids()
    {
        yield return new WaitUntil(() => _isReleasingEnabled);
        ReleaseRandomAsteroid();
        
        var frequency = Random.Range(
            LevelSettingsManager.Instance.CurrentLevel.AsteroidsReleasingFrequency.Item1,
            LevelSettingsManager.Instance.CurrentLevel.AsteroidsReleasingFrequency.Item2);
	    
        yield return new WaitForSecondsRealtime(frequency);
        _releasingAsteroidsCoroutine = StartCoroutine(ReleaseAsteroids());
    }

    private void ReleaseRandomAsteroid()
    {
        var randomAsteroid = GetRandomAsteroid();
        ReleaseAsteroid(randomAsteroid);
    }

    public void ReleaseAsteroid(GameObject asteroid)
    {
        asteroid.gameObject.SetActive(true);
        asteroid.transform.position = Vector3.zero;
        asteroid.GetComponent<AsteroidMovementController>().Release();

        _asteroids.Add(asteroid);
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

    public void ToggleReleasing()
    {
        if (_isReleasingEnabled)
        {
            StopNewObjectsReleasing();
            StopReleasingAsteroidsCoroutine();
        }
        else
        {
            StartNewObjectsReleasing();
            StartReleasingAsteroidCoroutine();
        }
    }

    /// <summary>
    /// Enable flag for creating new objects in coroutine (coroutine must be started also)
    /// </summary>
    private void StartNewObjectsReleasing()
    {
        _isReleasingEnabled = true;
    }

    /// <summary>
    /// Disable flag for creating new objects in coroutine (regardless if coroutine is started)
    /// </summary>
    private void StopNewObjectsReleasing()
    {
        _isReleasingEnabled = false;
    }
}
