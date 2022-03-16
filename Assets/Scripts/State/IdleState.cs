using System;
using UnityEngine;

public class IdleState : BaseState
{
    private Actual_AI _Ai;
    public IdleState(Actual_AI ai) : base(ai.gameObject)
    {
        _Ai = ai;
    }

    public override Type Tick()
    {
        _Ai.IsIdle = true;
        float dist = Vector3.Distance(transform.position, _Ai.LastIdlePos);

        if (!IsSelected())
        {
            if (dist >= .1f)
            {
                _Ai.GoToLastIdlePos();
            }
            else
            {
                _Ai.IsIdle = true;
            }
        }
        var chaseTarget = CheckForAggro();

        //here we set the chase target then set the state to chase state
        if (chaseTarget != null || _Ai.Target != null)
        {
            _Ai.IsIdle = false;

            if(_Ai.Target == null)
                _Ai.SetTarget(chaseTarget);

            return typeof(ChaseState);
        }

        if (IsSelected())
        {
            return typeof(MoveToState);
        }
        return null;
    }
    //Quaternion startingAngle = Quaternion.AngleAxis(-70, Vector3.up);
    //Quaternion stepAngle = Quaternion.AngleAxis(15, Vector3.up);
    private Transform CheckForAggro()
    {
        RaycastHit hit;
        //var angle = transform.rotation * startingAngle;
        //var direction = angle * Vector3.forward;
        //for (var i = 0; i < 20; i++)
        //{//Raycast(pos, direction, out hit, _Ai.FogLookRange)
        if (Physics.SphereCast(transform.position, 1, transform.forward, out hit))
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
