using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolingManager : BaseManager<ObjectPoolingManager>
{
    [SerializeField]
    private List<Pool> pools = new List<Pool>();
    public List<Pool> Pools => pools;

    private Transform _objectsParent;

    private void Start()
    {
        CreateObjectsParent();

        foreach (var pool in pools)
        {
            pool.PoolObjectPrefab.gameObject.SetActive(false);

            for (int i = 0; i < pool.StartPoolCount; i++)
            {
                var newObject = Instantiate(pool.PoolObjectPrefab.gameObject, _objectsParent);
                newObject.name = newObject.name.Replace("(Clone)", $"{newObject.GetInstanceID()}");
                pool.PooledObjects.Enqueue(newObject);
                pool.ObjectCount++;
            }

            pool.PoolObjectPrefab.gameObject.SetActive(true);
        }
    }

    private void CreateObjectsParent()
    {
        _objectsParent = Instantiate(new GameObject(GetType().Name).transform, GameLauncher.Instance.GamePlane.transform);
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
                var newObject = Instantiate(pool.PoolObjectPrefab, _objectsParent);
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

    public void ReturnToPool(BasePoolableController objectToReturn)
    {
        var pool = GetPoolByPoolableNameType(objectToReturn.PoolableType);
        pool.ReturnToPool(objectToReturn.gameObject);
    }

    private Pool GetPoolByPoolableNameType(string poolableType)
    {
        return pools.FirstOrDefault(x => x.PoolableNameType.Equals(poolableType));
    }

    private IEnumerable<Pool> GetPoolByPoolableComponentType<T>() where T : BasePoolableController =>
        pools.Where(x => x.PoolableComponentType.Equals(typeof(T)) || x.PoolableComponentType.IsSubclassOf(typeof(T)));

    public string[] GetAllPoolableNamesByPoolableComponentType<T>() where T : BasePoolableController
    {
        var poolsOfType = GetPoolByPoolableComponentType<T>().Where(x => x.PoolObjectPrefab.TryGetComponent<T>(out _));
        var poolsOfTypeCount = poolsOfType.Count();
        var poolNamesOfType = new string[poolsOfTypeCount];
        
        for(int i = 0; i < poolsOfTypeCount; i++)
        {
            poolNamesOfType[i] = poolsOfType.ElementAt(i).PoolableNameType;
        }

        return poolNamesOfType;
    }
}
