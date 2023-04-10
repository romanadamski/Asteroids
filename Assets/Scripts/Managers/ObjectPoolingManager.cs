using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolingManager : BaseManager<ObjectPoolingManager>
{
    [SerializeField]
    private List<Pool> pools = new List<Pool>();

    private void Start()
    {
        foreach (var pool in pools)
        {
            for (int i = 0; i < pool.StartPoolCount; i++)
            {
                var newObject = Instantiate(pool.PoolObjectPrefab.gameObject, pool.ObjectsParent);
                newObject.gameObject.SetActive(false);
                newObject.name = newObject.name.Replace("(Clone)", $"{newObject.GetInstanceID()}");
                pool.PooledObjects.Enqueue(newObject);
                pool.ObjectCount++;
            }
        }
    }

    public BasePoolableController GetFromPool(string poolableType)
    {
        var pool = GetPoolByPoolableNameType(poolableType);
        if (pool == null)
        {
            Debug.LogError($"There is no pool of {poolableType} type!");
            return null;
        }
        if (pool.PooledObjects.Count > 0)
        {
            var newObject = pool.PooledObjects.Dequeue();
            pool.ObjectsOutsidePool.Add(newObject);
            return newObject.GetComponent<BasePoolableController>();
        }
        else
        {
            if (pool.CanGrow)
            {
                pool.ObjectCount++;
                var newObject = Instantiate(pool.PoolObjectPrefab, pool.ObjectsParent);
                pool.ObjectsOutsidePool.Add(newObject.gameObject);
                return newObject;
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Returns all objects to their pools
    /// </summary>
    public void ReturnAllToPools()
    {
        foreach (var pool in pools)
        {
            pool.ReturnAllToPool();
        }
    }

    /// <summary>
    /// Returns all object of given type to pool
    /// </summary>
    /// <param name="poolableType">Type of poolable</param>
    public void ReturnAllToPool(string poolableType)
    {
        var pool = GetPoolByPoolableNameType(poolableType);
        pool.ReturnAllToPool();
    }

    public void ReturnToPool(BasePoolableController objectToReturn)
    {
        var pool = GetPoolByPoolableNameType(objectToReturn.PoolableType);
        pool.ReturnToPool(objectToReturn.gameObject);
    }

    private Pool GetPoolByPoolableNameType(string poolableType)
    {
        return pools.FirstOrDefault(x => x.PoolableNameType.Equals(poolableType));
    }

    private Pool GetPoolByPoolableComponentType<T>() where T : BasePoolableController
    {
        return pools.FirstOrDefault(x => x.PoolableComponentType.Equals(typeof(T).Name));
    }

    public string[] GetAllPoolableNamesByPoolableComponentType<T>() where T : BasePoolableController
    {
        return GetPoolByPoolableComponentType<T>().PoolObjectPrefab.PoolableTypes;
    }
}
