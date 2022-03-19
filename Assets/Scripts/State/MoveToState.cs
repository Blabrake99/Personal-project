using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToState : BaseState
{
    private Actual_AI _Ai;
    private float attackTimer;
    private Collider[] inRangeColliders;
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
            if(!CheckForAggro())
                return typeof(IdleState);
            else
            {
                _Ai.SetTarget(CheckForAggro());
                return typeof(ChaseState);
            }
        }
        //run and gun
        if (CheckForAggro())
        {
            _Ai.SetTarget(CheckForAggro());
            if (_Ai.Target != null)
            {
                var distanceToTarget = Vector3.Distance(transform.position, _Ai.Target.transform.position);

                if (distanceToTarget <= _Ai.attackRange && _Ai.Target.gameObject.tag != "Building" ||
                    distanceToTarget + 2 <= _Ai.attackRange && _Ai.Target.gameObject.tag == "Building")
                {
                    attackTimer -= Time.deltaTime;

                    if (attackTimer <= 0f)
                    {
                        _Ai.Fire();
                        attackTimer = _Ai.fireRate;
                    }
                }
            }
        }
        //if (IsSelected())
        //{
        //    return typeof(MoveToState);
        //}

        return null;
    }
    private Transform CheckForAggro()
    {
        RaycastHit hit;

        inRangeColliders = Physics.OverlapSphere(transform.position, _Ai.chaseRange);

        for (int i = 0; i < inRangeColliders.Length; i++)
        {
            var temp = inRangeColliders[i].gameObject;

            if (inRangeColliders[i].gameObject.tag == "Unit")
                if (temp.gameObject.GetComponent<Actual_AI>().Teams != gameobj.GetComponent<Actual_AI>().Teams)
                    return temp.gameObject.transform;

            if (inRangeColliders[i].gameObject.tag == "Building")
                if (temp.gameObject.GetComponent<Building>().Teams.ToString() != gameobj.GetComponent<Actual_AI>().Teams.ToString())
                    return temp.gameObject.transform;

            if (inRangeColliders[i].gameObject.tag == "Turret")
                if (temp.gameObject.GetComponent<Turret>().Teams.ToString() != gameobj.GetComponent<Actual_AI>().Teams.ToString())
                    return temp.gameObject.transform;

        }
        return null;
    }
    //private bool IsSelected()
    //{
    //    if (_Ai.Selected)
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
}
