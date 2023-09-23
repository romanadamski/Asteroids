using UnityEngine;

[RequireComponent(typeof(ShootingTrigger))]
public class ShootingController : MonoBehaviour
{
    [SerializeField]
    private string bulletTypeName;

    private ShootingTrigger _shootingTrigger;

    private void Awake()
    {
        _shootingTrigger = GetComponent<ShootingTrigger>();

        _shootingTrigger.OnFixedUpdate += OnUpdate;
    }

    private void OnUpdate()
    {
        if (_shootingTrigger.TriggerShoot)
        {
            ReleaseBullet();
        }
    }

    private void ReleaseBullet()
    {
        Debug.Log($"{name} release bullet {bulletTypeName}");
        var bullet = GetBulletFromPool();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.gameObject.SetActive(true);

        bullet.Release(transform.up);

        EventsManager.Instance.OnBulletFired();
    }

    private BaseBulletMovementController GetBulletFromPool()
    {
        return ObjectPoolingManager.Instance.GetFromPool(bulletTypeName).GetComponent<BaseBulletMovementController>();
    }
}
