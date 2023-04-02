using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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