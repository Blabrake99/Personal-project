using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Actual_AI : MonoBehaviour
{

    #region AI_Stats_Based_On_UnitType
    public int health;
    public float buildSpeed;
    public int attackDamage;
    public float FogLookRange;
    public float fireRate;
    public float attackRange;
    #endregion

    [SerializeField]
    private Team team;

    public Team Teams => team;


    public Transform Target { get; private set; }


    [SerializeField]
    private Unit unit;

    public Unit Units => unit;

    public Vector3 GoToArea { get; set; }

    public StateMachine Statemachine => GetComponent<StateMachine>();


    public Vector3 LastIdlePos { get; private set; }

    public bool IsIdle;

    public bool Selected { get; set; }
    private void Awake()
    {
        LastIdlePos = this.transform.position;

        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        //initlizing dictinary with 3 states 
        //this is done so we can just reuse the state rather than regernerating a new 
        // version of that state 


        var states = new Dictionary<Type, BaseState>
        {
           { typeof(IdleState), new IdleState(this) },
           { typeof(ChaseState), new ChaseState(this) },
           { typeof(AttackState), new AttackState(this) },
           { typeof(MoveToState), new MoveToState(this) },
           { typeof(WanderState), new WanderState(this) }
        };

        GetComponent<StateMachine>().SetStates(states);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public void Fire()
    {
        if (Target != null)
        {
            if (Target.GetComponent<Actual_AI>() != null)
            {
                //the target loses health based on our attack damage 
                Target.GetComponent<Actual_AI>().health -= attackDamage;
                //this will let the target know who's attacking him 
                if (Target.GetComponent<Actual_AI>().Target == null)
                {
                    Target.GetComponent<Actual_AI>().SetTarget(this.gameObject.transform);
                }
            }
            if (Target.GetComponent<Building>() != null)
            {
                //the target loses health based on our attack damage 
                Target.GetComponent<Building>().health -= attackDamage;
            }
            if (Target.GetComponent<Turret>() != null)
            {
                //the target loses health based on our attack damage 
                Target.GetComponent<Turret>().health -= attackDamage;
                //this will let the target know who's attacking him 
                if (Target.GetComponent<Turret>().Target == null)
                {
                    Target.GetComponent<Turret>().Target = this.gameObject.transform;
                }
            }
        }
    }

    protected void Update()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        if (IsIdle == true)
        {
            LastIdlePos = this.transform.position;
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    //get unit type and get unit type from the target 
    // 
    public enum Team
    {
        blue,
        red,
        green,
        yellow,
        brown,
        pink,
        orange,
        black,
        white
    }
    public enum Unit
    {
        Tank,
        Solder,
        Helicopter
    }
    public void GoToLastIdlePos()
    {
        GoToArea = LastIdlePos;
        Selected = true;
    }
    public void SetTeam(String t)
    {
        team = (Team)System.Enum.Parse(typeof(Team), t);
    }


}
