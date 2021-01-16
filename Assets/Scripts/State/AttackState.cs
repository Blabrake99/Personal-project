using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float attackTimer;
    private Actual_AI _Ai;

    public AttackState(Actual_AI ai) : base(ai.gameObject)
    {
        _Ai = ai;
    }
    public override Type Tick()
    {
        if (_Ai.Target == null)
            return typeof(IdleState);

        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0f)
        {
            _Ai.Fire();
            attackTimer = _Ai.fireRate;
        }
        if (IsSelected())
        {
            return typeof(MoveToState);
        }
        return null;
    }
    private bool IsSelected()
    {
        if (_Ai.Selected == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
