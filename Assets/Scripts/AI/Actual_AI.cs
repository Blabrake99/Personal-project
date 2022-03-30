﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Actual_AI : MonoBehaviour
{

    #region AI_Stats_Based_On_UnitType
    public int fullHealth;
    public int health;
    public float buildSpeed;
    public int attackDamage;
    public float FogLookRange;
    public float fireRate;
    public float attackRange;
    public float chaseRange;
    public float moveSpeed;
    #endregion

    [SerializeField]
    private Team team;

    public NavMeshAgent navAgent;

    public Team Teams => team;

    [SerializeField]
    GameObject CircleUnderTroop;

    public Transform Target { get; private set; }

    [SerializeField]
    private Unit unit;

    public Unit Units => unit;

    public Vector3 GoToArea { get; set; }

    public StateMachine Statemachine => GetComponent<StateMachine>();

    public Vector3 LastIdlePos { get; private set; }

    public bool IsIdle;

    public bool Selected { get; set; }

    Players_Script Player;
    private void Awake()
    {
        LastIdlePos = this.transform.position;
        Player = GameObject.Find("PlayerOBJ").GetComponent<Players_Script>();
        InitializeStateMachine();
        //sets health if i forget to set full health value 
    }
    private void InitializeStateMachine()
    {
        //initlizing dictinary with 5 states 
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
    private void Start()
    {
        Invoke("enableNavAgent", .1f);
    }
    void enableNavAgent()
    {
        navAgent.enabled = true;
        navAgent.speed = moveSpeed;
    }
    public void SetTarget(Transform target)
    {
        Target = target;
        GoToArea = target.transform.position;
    }

    public void Fire()
    {
        if (Target != null)
        {
            if (Target.GetComponent<Actual_AI>() != null)
            {
                //the target loses health based on our attack damage 
                Target.GetComponent<Actual_AI>().health -= GetDamageDone(Target.GetComponent<Actual_AI>());
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
        //this figures out if the unit is selected by the player
        if (Player.isSelecting)
        {
            for (int i = 0; i <= Player.SelectedUnitList.Count; i++)
            {
                if (Player.SelectedUnitList.Contains(this.gameObject))
                {
                    CircleOn();
                }
                else
                {
                    CircleOff();
                }
            }
        }
        if (IsIdle)
        {
            LastIdlePos = this.transform.position;
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    //this is for debugging the states
    public void PrintState(string state)
    {
        print(state);
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
    int GetDamageDone(Actual_AI target)
    {
        if (unit == Unit.Solder)
        {
            if (target.unit == Unit.Helicopter)
                return Mathf.RoundToInt(attackDamage * 1.5f);
            else if (target.unit == Unit.Tank)
                return Mathf.RoundToInt(attackDamage * .5f);
            else
                return attackDamage;

        }
        if (unit == Unit.Tank)
        {
            if (target.unit == Unit.Helicopter)
                return Mathf.RoundToInt(attackDamage * .5f);
            else if (target.unit == Unit.Solder)
                return Mathf.RoundToInt(attackDamage * 1.5f);
            else
                return attackDamage;
        }
        if (unit == Unit.Helicopter)
        {
            if (target.unit == Unit.Helicopter)
                return attackDamage;
            else if (target.unit == Unit.Solder)
                return Mathf.RoundToInt(attackDamage * .5f);
            else
                return Mathf.RoundToInt(attackDamage * 1.5f);
        }
        return attackDamage;
    }
    public void MoveUnit(Vector3 _destination)
    {
        if (navAgent.enabled && GoToArea != Vector3.zero)
        {
            navAgent.isStopped = false;
            if (navAgent.destination != _destination)
                navAgent.SetDestination(_destination);
        }
    }
    public void StopUnit()
    {
        navAgent.isStopped = true;
    }
    public void GoToLastIdlePos()
    {
        MoveUnit(LastIdlePos);
        Selected = true;
    }
    public void SetTeam(String t)
    {
        team = (Team)System.Enum.Parse(typeof(Team), t);
    }
    public void CircleOn()
    {
        CircleUnderTroop.SetActive(true);
    }
    public void CircleOff()
    {
        CircleUnderTroop.SetActive(false);
    }
}
