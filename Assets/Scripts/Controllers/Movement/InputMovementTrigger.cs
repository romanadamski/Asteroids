using UnityEngine;

public class InputMovementTrigger : MovementTrigger
{
    private readonly string HORIZONTAL_AXIS_NAME = "Horizontal";
    private readonly string VERTICAL_AXIS_NAME = "Vertical";

    protected override void SetAxis()
    {
        XAxis = Input.GetAxis(HORIZONTAL_AXIS_NAME);
        YAxis = Input.GetAxis(VERTICAL_AXIS_NAME);
    }
}
