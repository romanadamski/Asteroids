
public class UpMovementTrigger : MovementTrigger
{
    protected override void SetAxis()
    {
        XAxis = transform.up.x;
        YAxis = transform.up.y;
    }
}
