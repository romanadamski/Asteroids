using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AsteroidPoolableController : BasePoolableController
{
    protected override string[] GetPoolableTypes()
    {
        return new string[]
        {
            PoolableTypesConstants.ASTEROID_SMALL_POOLABLE,
            PoolableTypesConstants.ASTEROID_MEDIUM_POOLABLE,
            PoolableTypesConstants.ASTEROID_BIG_POOLABLE
        };
    }
}
