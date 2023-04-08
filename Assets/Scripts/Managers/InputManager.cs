using System.Collections.Generic;

public class InputManager : BaseManager<InputManager>
{
    private List<BaseInputController> _inputControllers = new List<BaseInputController>();

    public bool IsInputEnabled { get; private set; } = true;

    private void Update()
    {
        if (!IsInputEnabled) return;

        HandleInputs();
    }

    private void FixedUpdate()
    {
        if (!IsInputEnabled) return;

        HandleFixedInputs();
    }

    private void HandleInputs()
    {
        foreach (var input in _inputControllers)
        {
            if (input != null && input.enabled && input.gameObject.activeInHierarchy)
            {
                input.OnUpdate();
            }
        }
    }

    private void HandleFixedInputs()
    {
        foreach (var input in _inputControllers)
        {
            if (input != null && input.enabled && input.gameObject.activeInHierarchy)
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

    public void ToggleInput(bool toggle)
    {
        IsInputEnabled = toggle;
    }
}
