public class AsteroidPoolableController : BasePoolableController
{
    private const string ASTEROID_SMALL_POOLABLE = "AsteroidSmall";
    private const string ASTEROID_MEDIUM_POOLABLE = "AsteroidMedium";
    private const string ASTEROID_BIG_POOLABLE = "AsteroidBig";

    protected override string[] GetPoolableTypes()
    {
        return new string[]
        {
            ASTEROID_SMALL_POOLABLE,
            ASTEROID_MEDIUM_POOLABLE,
            ASTEROID_BIG_POOLABLE
        };
    }
}
