using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    private Actual_AI _Ai;

    public ChaseState(Actual_AI ai) : base(ai.gameObject)
    {
        _Ai = ai;
    }

    public override Type Tick()
    {
        if (_Ai.Target == null)
            return typeof(IdleState);

        _Ai.MoveUnit(_Ai.GoToArea);

        var distance = Vector3.Distance(transform.position, _Ai.Target.transform.position);
        
        if (distance <= _Ai.attackRange && _Ai.Target.gameObject.tag != "Building" || 
            distance + 2 <= _Ai.attackRange && _Ai.Target.gameObject.tag == "Building")
        {
            return typeof(AttackState);
        }
        if (IsSelected())
        {
            return typeof(MoveToState);
        }
        return null;
    }

    private bool IsSelected()
    {
        if (_Ai.Selected)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
