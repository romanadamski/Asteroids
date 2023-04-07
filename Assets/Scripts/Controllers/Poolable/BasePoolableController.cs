using UnityEngine;

public abstract class BasePoolableController : MonoBehaviour
{
    [SerializeField]
    private int selectedTypeIndex;

    protected abstract string[] GetPoolableTypes();

    public string[] PoolableTypes => GetPoolableTypes();
    public string PoolableType;
}
