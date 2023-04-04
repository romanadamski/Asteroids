using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State CurrentState;

    public void SetState(State state)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }
        CurrentState = state;
        state.Enter();
    }

    private void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }
}
