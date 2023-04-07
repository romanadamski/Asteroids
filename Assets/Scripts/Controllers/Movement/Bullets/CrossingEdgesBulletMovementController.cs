public class CrossingEdgesBulletMovementController : BaseBulletMovementController
{
    protected override void OnOutsideScreen()
    {
        ScreenManager.Instance.HandleScreenEdgeCrossing(transform);
    }
}