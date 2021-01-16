using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : BaseState
{
    //? makes it nullable
    private Vector3? _destination;
    private float stopdist = 1f;
    private float turnspeed = 1f;
    private readonly LayerMask _layerMask = LayerMask.NameToLayer("Walls");
    private float _rayDistance = 3.5f;
    private Quaternion _desiredRotation;
    private Vector3 _direction;
    private Actual_AI _AI;

    //passes in the drone to the BaseStates constuctor 
    public WanderState(Actual_AI AI) : base(AI.gameObject)
    {
        _AI = AI;
    }
    //you need to override Tick from BaseState becouse base state is
    //abstract
    public override Type Tick()
    {
        var chaseTarget = CheckForAggro();

        //here we set the chase target then set the state to chase state
        if(chaseTarget != null)
        {
            _AI.SetTarget(chaseTarget);
            return typeof(ChaseState);
        }

        if(_destination.HasValue == false ||
            Vector3.Distance(transform.position, _destination.Value) <= stopdist)
        {
            FindRandomDestination();
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, _desiredRotation, Time.deltaTime * turnspeed);

        if (IsForwardBlocked())
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _desiredRotation, 0.2f);
        } else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * GameSettings.Speed);
        }

        //Debug.DrawRay(transform.position, _direction * _rayDistance, Color.red);
        while (IsPathBlocked())
        {
            FindRandomDestination();
        }

        if(IsSelected())
        {
            return typeof(MoveToState);
        }
        return null;
    }
    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, 0.5f, _rayDistance, _layerMask);
    }
    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, _direction);
        return Physics.SphereCast(ray, 0.5f, _rayDistance, _layerMask);
    }
    private void FindRandomDestination()
    {
        Vector3 testPosition = (transform.position + (transform.forward * 4f))
            + new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f, UnityEngine.Random.Range(-4.5f, 4.5f));

        _destination = new Vector3(testPosition.x, 1f, testPosition.z);

        _direction = Vector3.Normalize(_destination.Value - transform.position);
        _direction = new Vector3(_direction.x, 0f, _direction.z);
        _desiredRotation = Quaternion.LookRotation(_direction);
    }

    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    private bool IsSelected()
    {
        if(_AI.Selected == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Transform CheckForAggro()
    {
        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for(var i = 0; i < 24; i++)
        {
            if(Physics.Raycast(pos, direction, out hit, GameSettings.AggroRadius))
            {
                var ai = hit.collider.GetComponent<Actual_AI>();
                if(ai != null && ai.Teams != gameobj.GetComponent<Actual_AI>().Teams)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return ai.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * GameSettings.AggroRadius, Color.yellow);
                }
               
            }
            else
            {
                Debug.DrawRay(pos, direction * GameSettings.AggroRadius, Color.white);
            }
             direction = stepAngle * direction;
        }
        return null;
    }

    void Update()
    {
        
    }
}
