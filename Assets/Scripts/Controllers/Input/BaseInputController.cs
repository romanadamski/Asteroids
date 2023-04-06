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
        Debug.Log($"Added input: {GetType()}");
    }

    private void OnDisable()
    {
        Debug.Log($"Removed input: {GetType()}");
        InputManager.Instance?.RemoveInputController(this);
    }
}
