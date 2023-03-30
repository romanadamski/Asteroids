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
        public string Tag;
        public bool CanGrow;
        public Queue<GameObject> PooledObjects = new Queue<GameObject>();
        public Transform ObjectsParent;

        [HideInInspector]
        public int ObjectCount;
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

    public GameObject GetFromPool(string tag)
    {
        var pool = GetPoolByTag(tag);
        if (pool.PooledObjects.Count > 0)
        {
            return pool.PooledObjects.Dequeue();
        }
        else
        {
            if (pool.CanGrow)
            {
                pool.ObjectCount++;
                return Instantiate(pool.PoolObjectPrefab, pool.ObjectsParent);
            }
            else
            {
                return null;
            }
        }
    }

    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
        GetPoolByTag(tag).PooledObjects.Enqueue(objectToReturn);
    }

    private Pool GetPoolByTag(string tag)
    {
        return pools.FirstOrDefault(x => x.Tag == tag);
    }
}
