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

    private void OnEnable()
    {
        InputManager.Instance.AddInputController(this);
    }

    private void OnDisable()
    {
        InputManager.Instance?.RemoveInputController(this);
    }
}
