using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> _availableStates = new Dictionary<Type, BaseState>();

    public BaseState CurState { get; private set; }
    public event Action<BaseState> OnStateChange;


    public void SetStates(Dictionary<Type, BaseState> states)
    {
        _availableStates = states;
    }

    private void Update()
    {
        if(CurState == null)
        {
            CurState = _availableStates.Values.First();
        }

        //the ?. makes sure that current state's not null
        //before doing the next line
        var nextState = CurState?.Tick();
        //if it is null it'll generate a new state 
        if(nextState != null && nextState != CurState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }
    //this changes to a new state that should be active
    //for example if the enemt is close enough to chase it'ss switch to 
    //chase 
    private void SwitchToNewState(Type nextState)
    {
        CurState = _availableStates[nextState];
        OnStateChange?.Invoke(CurState);
    }

}