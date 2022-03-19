using System;
using UnityEngine;

public class IdleState : BaseState
{
    private Actual_AI _Ai;
    private Collider[] inRangeColliders;
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

            if (_Ai.Target == null)
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
