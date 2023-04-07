public class BulletPoolableController : BasePoolableController
{
    protected override string[] GetPoolableTypes()
    {
        return new string[]
        {
            typeof(CrossingEdgesBulletMovementController).Name,
            typeof(SimpleBulletMovementController).Name
        };
    }
}