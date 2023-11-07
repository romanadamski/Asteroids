using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolingController : MonoBehaviour
{
    [SerializeField]
    private List<Pool> pools;
    public List<Pool> Pools => pools;

    [SerializeField]
    private bool isGameplayPooling;
    public bool IsGameplayPooling => isGameplayPooling;

    private void Awake()
    {
        foreach (var pool in pools)
        {
            pool.PoolObjectPrefab.gameObject.SetActive(false);

            for (int i = 0; i < pool.StartPoolCount; i++)
            {
                var newObject = Instantiate(pool.PoolObjectPrefab, pool.Parent);
                newObject.gameObject.SetActive(false);
                SetObjectName(newObject.gameObject);
                pool.PooledObjects.Enqueue(newObject);
            }
        }

        ObjectPoolingManager.Instance.AddPoolController(this);
    }

    private void SetObjectName(GameObject poolableObject)
    {
        poolableObject.name = poolableObject.name.Replace("(Clone)", $"{poolableObject.GetInstanceID()}");
    }

    /// <summary>
    /// Returns pooled object of given type
    /// </summary>
    /// <param name="poolableType">Name of pooled object set in Pool component</param>
    /// <returns></returns>
    public BasePoolableController GetFromPool(string poolableType = "")
    {
        var pool = GetPoolByPoolableNameType(poolableType);
        if (pool.PooledObjects.Count > 0)
        {
            var newObject = pool.PooledObjects.Dequeue();
            pool.ObjectsOutsidePool.Add(newObject);
            return newObject;
        }
        else
        {
            if (pool.CanGrow)
            {
                var newObject = Instantiate(pool.PoolObjectPrefab, pool.Parent);
                SetObjectName(newObject.gameObject);
                pool.ObjectsOutsidePool.Add(newObject);
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

    public void ReturnToPool(GameObject objectToReturn)
    {
        ReturnToPool(objectToReturn.GetComponent<BasePoolableController>());
    }

    public void ReturnToPool(BasePoolableController objectToReturn)
    {
        var pool = GetPoolByPoolableNameType(objectToReturn.PoolableType);
        pool.ReturnToPool(objectToReturn);
    }

    private Pool GetPoolByPoolableNameType(string poolableType)
    {
        if (string.IsNullOrWhiteSpace(poolableType))
        {
            return pools.First();
        }

        return pools.FirstOrDefault(x => x.PoolableNameType.Equals(poolableType));
    }
}
