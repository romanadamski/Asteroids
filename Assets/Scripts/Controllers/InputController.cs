using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class InputController : MonoBehaviour
{
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();

    private void Start()
    {
        //todo adding inputs in states?
        Managers.InputManager.AddInputController(this);
    }
}
