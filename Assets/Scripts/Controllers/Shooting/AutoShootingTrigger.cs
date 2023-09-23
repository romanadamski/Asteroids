using UnityEngine;

public class AutoShootingTrigger : ShootingTrigger
{
    [Range(0.1f, 5.0f)]
    [SerializeField]
    private float shotingFrequency;

    private float _timeElapsed;

    protected override void SetTrigger()
    {
        TriggerShoot = false;

        _timeElapsed += Time.deltaTime;
        if (_timeElapsed < shotingFrequency) return;

        TriggerShoot = true;
        _timeElapsed = 0;
    }
}
