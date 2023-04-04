using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InputManager : BaseManager<InputManager>
{
    List<BaseInputController> _inputControllers = new List<BaseInputController>();

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
            if (input.enabled && input.gameObject.activeInHierarchy)
            {
                input.OnUpdate();
            }
        }
    }

    private void HandleFixedInputs()
    {
        foreach (var input in _inputControllers)
        {
            if (input.enabled && input.gameObject.activeInHierarchy)
            {
                input.OnFixedUpdate();
            }
        }
    }

    public void AddInputController(BaseInputController inputController)
    {
        _inputControllers.Add(inputController);
    }

    public void RemoveInputController(BaseInputController inputController)
    {
        _inputControllers.Remove(inputController);
    }
}
