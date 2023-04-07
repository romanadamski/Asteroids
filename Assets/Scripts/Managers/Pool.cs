using System;
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

    /// <summary>
    /// Type choosen in prefab from Poolable type dropdown
    /// </summary>
    public string PoolableNameType => PoolObjectPrefab.GetComponent<BasePoolableController>().PoolableType;

    /// <summary>
    /// Type choosen by attached Poolable component
    /// </summary>
    public string PoolableComponentType => PoolObjectPrefab.GetComponent<BasePoolableController>().GetType().Name;

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