using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class ObjectPoolingManagerTests
{
    [Test]
    public void ReturnToPool_ReturnObject_ObjectIsReturned()
    {
        //arrange
        var objectPoolingManager = CreateFakeObjectPoolingManager();
        //act
        objectPoolingManager.ReturnToPool(CreateFakePoolableObject<AsteroidPoolableController>());
        var allAsteroidTypes = ObjectPoolingManager.Instance.GetAllPoolableNamesByPoolableComponentType<AsteroidPoolableController>();

        //assert
        Assert.IsNotNull(objectPoolingManager.GetFromPool(allAsteroidTypes[0]));
    }

    private ObjectPoolingManager CreateFakeObjectPoolingManager()
    {
        GameObject managerGameObject = new GameObject();
        ObjectPoolingManager poolingManager = managerGameObject.AddComponent<ObjectPoolingManager>();
        GameObject asteroidGameObject = new GameObject();
        AsteroidPoolableController asteroidPrefab = asteroidGameObject.AddComponent<AsteroidPoolableController>();
        asteroidPrefab.PoolableType = "AsteroidSmall";
        poolingManager.Pools.Add(new Pool
            {
                StartPoolCount = 30,
                PoolObjectPrefab = asteroidPrefab,
                CanGrow = false
            }
        );
        poolingManager.Invoke("Start", 0);

        return poolingManager;
    }

    private BasePoolableController CreateFakePoolableObject<T>() where T : BasePoolableController
    {
        GameObject gameObject = new GameObject();
        BasePoolableController poolableAsteroid = gameObject.AddComponent<T>();
        poolableAsteroid.PoolableType = "AsteroidSmall";

        return poolableAsteroid;
    }
}
