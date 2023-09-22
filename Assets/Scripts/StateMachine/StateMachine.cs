using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State CurrentState => _states?.Peek();
    private Stack<State> _states = new Stack<State>();
    private bool IsEmpty => _states.Count == 0;

    public void PushState(State state)
    {
        if (IsEmpty || CurrentState != state)
        {
            _states.Push(state);
        }

        CurrentState.Enter();
    }

    public void PopState()
    {
        if (IsEmpty) return;
        
        CurrentState?.Exit();
        _states.Pop();
    }

    public void SetState(State state)
    {
        PopState();
        PushState(state);
    }

    public void Clear()
    {
        if (IsEmpty) return;

        CurrentState?.Exit();
        _states.Clear();
    }
}
