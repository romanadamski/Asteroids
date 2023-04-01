using System;
using System.Collections;
using System.Collections.Generic;
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
}