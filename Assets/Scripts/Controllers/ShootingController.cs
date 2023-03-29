using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : InputController
{
    public override void HandleFixedInput()
    {
    }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Shot");
        }
    }
}
