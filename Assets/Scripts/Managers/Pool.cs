using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Pool
{
    [SerializeField]
    private int startPoolCount;
    public int StartPoolCount => startPoolCount;

    [SerializeField]
    private BasePoolableController poolObjectPrefab;
    public BasePoolableController PoolObjectPrefab => poolObjectPrefab;

    [SerializeField]
    private bool canGrow;
    public bool CanGrow => canGrow;

    [SerializeField]
    private Queue<BasePoolableController> pooledObjects = new Queue<BasePoolableController>();
    public Queue<BasePoolableController> PooledObjects => pooledObjects;

    [SerializeField]
    private Transform parent;
    public Transform Parent
    {
        get
        {
            if (parent) return parent;

            var newParent = new GameObject(PoolableNameType);
            parent = newParent.transform;
            newParent.transform.SetParent(GameLauncher.Instance.GamePlane.transform);

            return parent;
        }
    }

    public string PoolableNameType => PoolObjectPrefab.GetComponent<BasePoolableController>().PoolableType;

    public List<BasePoolableController> ObjectsOutsidePool { get; private set; } = new List<BasePoolableController>();

    public void ReturnAllToPool()
    {
        foreach (var poolObject in ObjectsOutsidePool.ToList())
        {
            ReturnToPool(poolObject);
        }
        ObjectsOutsidePool.Clear();
    }

    public void ReturnToPool(BasePoolableController objectToReturn)
    {
        if (!PooledObjects.Contains(objectToReturn))
        {
            objectToReturn.gameObject.SetActive(false);
            PooledObjects.Enqueue(objectToReturn);
        }
    }
}