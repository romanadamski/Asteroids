using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Pool
{
    public int StartPoolCount;
    public BasePoolableController PoolObjectPrefab;
    public bool CanGrow;
    public Queue<BasePoolableController> PooledObjects = new Queue<BasePoolableController>();
    public Transform ObjectsParent;

    [HideInInspector]
    public int ObjectCount;
    public string ObjectType => PoolObjectPrefab.GetComponent<BasePoolableController>().PoolableType;

    [HideInInspector]
    public List<BasePoolableController> ObjectsOutsidePool = new List<BasePoolableController>();

    public void ReturnAllToPool()
    {
        foreach (var poolObject in ObjectsOutsidePool.ToList())
        {
            ReturnToPool(poolObject);
        }
    }

    public void ReturnToPool(BasePoolableController objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        PooledObjects.Enqueue(objectToReturn);
        ObjectsOutsidePool.Remove(objectToReturn);
    }
}