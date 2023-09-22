using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShootingTrigger : ShootingTrigger
{
    [Range(1, 10)]
    [SerializeField]
    private int shotingFrequency;

    protected override void SetTrigger()
    {
        TriggerShoot = false;
        if (Time.frameCount % (20 * shotingFrequency) != 0) return;

        TriggerShoot = true;
    }
}
