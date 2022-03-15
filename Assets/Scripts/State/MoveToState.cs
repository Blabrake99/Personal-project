using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToState : BaseState
{
    private Actual_AI _Ai;
    private float attackTimer;
    public MoveToState(Actual_AI ai) : base(ai.gameObject)
    {
        _Ai = ai;
    }
    public override Type Tick()
    {
        if (_Ai.GoToArea == Vector3.zero)
            return typeof(IdleState);

        transform.LookAt(_Ai.GoToArea);
        transform.Translate(Vector3.forward * Time.deltaTime * GameSettings.Speed);

        var distance = Vector3.Distance(transform.position, _Ai.GoToArea);

        if (distance <= .3f)
        {
            _Ai.Selected = false;
            _Ai.GoToArea = Vector3.zero;
            return typeof(IdleState);
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
