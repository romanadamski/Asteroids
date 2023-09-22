using UnityEngine;
//todo connect input to shooting, some IInput to easily swap between input type
public class ShootingInputController : BaseInputController
{
    [SerializeField]
    private KeyCode shootKey = KeyCode.Space;
    [SerializeField]
    private string bulletTypeName;
    [SerializeField]
    private int selectedTypeIndex;

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(shootKey))
        {
            ReleaseBullet();
            EventsManager.Instance.OnBulletFired();
        }
    }

    private void ReleaseBullet()
    {
        var bullet = GetBulletFromPool();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.gameObject.SetActive(true);

        bullet.Release(transform.up);
    }

    private BaseBulletMovementController GetBulletFromPool()
    {
        return ObjectPoolingManager.Instance.GetFromPool(bulletTypeName).GetComponent(bulletTypeName) as BaseBulletMovementController;
    }
}
