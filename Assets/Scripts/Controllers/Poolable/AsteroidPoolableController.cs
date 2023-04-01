using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AsteroidPoolableController : BasePoolableController
{
    public override string[] GetPoolableTypes()
    {
        return new string[]
        {
            typeof(AsteroidMovementController).Name
        };
    }
}
