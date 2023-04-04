using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseInputController : MonoBehaviour
{
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();

    private void Start()
    {
        //todo adding inputs in states?
        InputManager.Instance.AddInputController(this);
    }

    private void OnDestroy()
    {
        InputManager.Instance.RemoveInputController(this);
    }
}
