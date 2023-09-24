
public class UpMovementTrigger : MovementTrigger
{
    protected override void SetAxis()
    {
        XDirection = transform.up.x;
        YDirection = transform.up.y;
    }
}
