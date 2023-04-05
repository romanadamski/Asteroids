using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                pool.PooledObjects.Enqueue(newObject.GetComponent<BasePoolableController>());
                pool.ObjectCount++;
            }
        }
    }

    public BasePoolableController GetFromPool(string poolableType)
    {
        var pool = GetPoolByType(poolableType);
        if (pool == null)
        {
            Debug.LogError($"There is no pool of {poolableType} type!");
            return null;
        }
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
                pool.ObjectCount++;
                var newObject = Instantiate(pool.PoolObjectPrefab, pool.ObjectsParent);
                pool.ObjectsOutsidePool.Add(newObject);
                return newObject;
            }
            else
            {
                return null;
            }
        }
    }

    public void ReturnAllToPool(string poolableType)
    {
        var pool = GetPoolByType(poolableType);
        pool.ReturnAllToPool();
    }

    public void ReturnToPool(BasePoolableController objectToReturn)
    {
        var pool = GetPoolByType(objectToReturn.PoolableType);
        pool.ReturnToPool(objectToReturn);
    }

    private Pool GetPoolByType(string poolableType)
    {
        return pools.FirstOrDefault(x => x.ObjectType.Equals(poolableType));
    }
}
