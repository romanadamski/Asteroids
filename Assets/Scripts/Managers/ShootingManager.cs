using UnityEngine;

[RequireComponent(typeof(ObjectPoolingController))]
public class ShootingManager : BaseManager<ShootingManager>
{
    public ObjectPoolingController ObjectPoolingController { get; private set; }

    private void Awake()
    {
        ObjectPoolingController = GetComponent<ObjectPoolingController>();
    }
}
