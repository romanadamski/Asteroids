using UnityEngine;

public abstract class BaseInputController : MonoBehaviour
{
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }

    private void OnEnable()
    {
        InputManager.Instance.AddInputController(this);
    }

    private void OnDisable()
    {
        InputManager.Instance?.RemoveInputController(this);
    }
}
