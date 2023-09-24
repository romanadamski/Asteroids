using UnityEngine;

public class InputShootingTrigger : ShootingTrigger
{
    [SerializeField]
    private KeyCode shootKey = KeyCode.Space;

    protected override void SetTrigger()
    {
        TriggerShoot = false;

        if (!Input.GetKeyDown(shootKey)
            || GameplayManager.Instance.IsPaused) return;
        
        TriggerShoot = true;
    }
}
