using UnityEngine;

public abstract class BaseInputController : MonoBehaviour
{
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }

    private void OnEnable()
    {
        InputManager.Instance.AddInputController(this);
    }

    protected virtual void OnDisable()
    {
        InputManager.Instance?.RemoveInputController(this);
    }
}
