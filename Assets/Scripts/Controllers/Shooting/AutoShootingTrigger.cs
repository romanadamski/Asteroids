using UnityEngine;

public class AutoShootingTrigger : ShootingTrigger
{
    [Range(1, 10)]
    [SerializeField]
    private int shotingFrequency;

    protected override void SetTrigger()
    {
        TriggerShoot = false;
        if (Time.frameCount % (750 / shotingFrequency) != 0) return;

        TriggerShoot = true;
    }
}
