using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SimpleBulletMovementController : BaseBulletMovementController
{
    public override void OnOutsideScreen()
    {
        ReturnToPool();
        _isReleased = false;
    }
}
