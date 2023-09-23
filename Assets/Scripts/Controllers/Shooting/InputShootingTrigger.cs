using UnityEngine;

public class InputShootingTrigger : ShootingTrigger
{
    [SerializeField]
    private KeyCode shootKey = KeyCode.Space;

    protected override void SetTrigger()
    {
        TriggerShoot = false;

        if (Input.GetKeyDown(shootKey))
        {
            TriggerShoot = true;
        }
    }
}
