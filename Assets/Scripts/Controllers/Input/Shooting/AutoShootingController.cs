using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AutoShootingController : MonoBehaviour
{
    private string bulletTypeName = "SimpleBulletMovementController";

    private void Update()
    {
        ReleaseBullet();
        EventsManager.Instance.OnBulletFired();
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
