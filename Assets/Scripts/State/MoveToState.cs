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
            if(!CheckForAggro())
                return typeof(IdleState);
            else
            {
                _Ai.SetTarget(CheckForAggro());
                return typeof(ChaseState);
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
        //var angle = transform.rotation * startingAngle;
        //var direction = angle * Vector3.forward;
        //for (var i = 0; i < 20; i++)
        //{//Raycast(pos, direction, out hit, _Ai.FogLookRange)
        if (Physics.SphereCast(transform.position, .5f, transform.forward, out hit))
        {
            var ai = hit.collider.GetComponent<Actual_AI>();
            var building = hit.collider.GetComponent<Building>();
            var Turret = hit.collider.GetComponent<Turret>();
            if (ai != null && ai.Teams != gameobj.GetComponent<Actual_AI>().Teams)
            {
                return ai.transform;
            }
            //else
            //{
            //    Debug.DrawRay(pos, direction * _Ai.FogLookRange, Color.yellow);
            //}
            if (building != null && building.Teams.ToString() != gameobj.GetComponent<Actual_AI>().Teams.ToString())
            {
                return building.transform;
            }
            //else
            //{
            //    Debug.DrawRay(pos, direction * _Ai.FogLookRange, Color.yellow);
            //}
            if (Turret != null && Turret.Teams.ToString() != gameobj.GetComponent<Actual_AI>().Teams.ToString())
            {
                return Turret.transform;
            }
            //else
            //{
            //    Debug.DrawRay(pos, direction * _Ai.FogLookRange, Color.yellow);
            //}
            //}
            //else
            //{
            //    Debug.DrawRay(pos, direction * _Ai.FogLookRange, Color.white);
            //}
            //direction = stepAngle * direction;
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
