using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public int StartPoolCount;
        public GameObject PoolObjectPrefab;
        public bool CanGrow;
        public Queue<GameObject> PooledObjects = new Queue<GameObject>();
        public Transform ObjectsParent;

        [HideInInspector]
        public int ObjectCount;
        public Type ObjectType => PoolObjectPrefab.GetComponent<BasePoolableController>().GetType();
    }

    [SerializeField]
    private List<Pool> pools = new List<Pool>();

    private void Start()
    {
        foreach (var pool in pools)
        {
            for (int i = 0; i < pool.StartPoolCount; i++)
            {
                var newObject = Instantiate(pool.PoolObjectPrefab, pool.ObjectsParent);
                newObject.SetActive(false);
                pool.PooledObjects.Enqueue(newObject);
                pool.ObjectCount++;
            }
        }
    }

    public GameObject GetFromPool<T>() where T : BasePoolableController
    {
        var pool = GetPoolByType<T>();
        if (pool == null)
        {
            Debug.LogError($"There is no pool of {typeof(T).Name} type!");
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

    public void ReturnToPool(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
        var poolableType = GetPoolableTypeByGameObject(objectToReturn);
        var pool = GetPoolByType(poolableType);
        pool.PooledObjects.Enqueue(objectToReturn);
    }

    private Type GetPoolableTypeByGameObject(GameObject objectToReturn)
    {
        return objectToReturn.GetComponent<BasePoolableController>().GetType();
    }

    private Pool GetPoolByType<T>() where T : BasePoolableController
    {
        return GetPoolByType(typeof(T));
    }

    private Pool GetPoolByType(Type objectType)
    {
        return pools.FirstOrDefault(x => x.ObjectType.Equals(objectType));
    }
}
