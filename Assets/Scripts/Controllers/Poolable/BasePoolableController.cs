using System;
using UnityEngine;

public abstract class BasePoolableController : MonoBehaviour
{
    public string PoolableType;
    public abstract string[] GetPoolableTypes();
}
