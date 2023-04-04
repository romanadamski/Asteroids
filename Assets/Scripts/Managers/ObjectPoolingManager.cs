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

    public BasePoolableController GetFromPool(string poolableType)/* where T : BasePoolableController*/
    {
        var pool = GetPoolByType(poolableType);
        if (pool == null)
        {
            Debug.LogError($"There is no pool of {poolableType} type!");
            return null;
        }
        if (pool.PooledObjects.Count > 0)
        {
            return pool.PooledObjects.Dequeue();
        }
        else
        {
            if (pool.CanGrow)
            {
                pool.ObjectCount++;
                var newObject = Instantiate(pool.PoolObjectPrefab, pool.ObjectsParent);
                return newObject;
            }
            else
            {
                return null;
            }
        }
    }

    public void ReturnToPool(BasePoolableController objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        var pool = GetPoolByType(objectToReturn.PoolableType);
        pool.PooledObjects.Enqueue(objectToReturn.GetComponent<BasePoolableController>());
    }

    private Pool GetPoolByType(string poolableType)
    {
        return pools.FirstOrDefault(x => x.ObjectType.Equals(poolableType));
    }
}
