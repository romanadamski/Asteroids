using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    List<InputController> _inputControllers = new List<InputController>();

    private void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        HandleFixedInputs();
    }

    private void HandleInputs()
    {
        foreach (var input in _inputControllers)
        {
            input.OnUpdate();
        }
    }

    private void HandleFixedInputs()
    {
        foreach (var input in _inputControllers)
        {
            input.OnFixedUpdate();
        }
    }

    public void AddInputController(InputController inputController)
    {
        _inputControllers.Add(inputController);
    }

    public void RemoveInputController(InputController inputController)
    {
        _inputControllers.Remove(inputController);
    }
}
