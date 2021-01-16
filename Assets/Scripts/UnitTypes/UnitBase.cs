using System;
using System.Collections.Generic;
using UnityEngine;
public abstract class UnitBase : MonoBehaviour
{
    protected GameObject gameobj { get; set; }
    //public struct UnitStats
    //{
    //    public int health;
    //    public float speed;
    //    public float buildSpeed;
    //    public int attackDamage;
    //    public float Lookrange;
    //    public float fireRate;
    //    public float attackRange;

    //    public UnitStats init(int health, float speed, float buildSpeed, int attackDamage,
    //        float Lookrange, float fireRate, float attackRange)
    //    {
    //        this.health = health;
    //        this.speed = speed;
    //        this.buildSpeed = buildSpeed;
    //        this.attackDamage = attackDamage;
    //        this.Lookrange = Lookrange;
    //        this.fireRate = fireRate;
    //        this.attackRange = attackRange;

    //        return this;
    //    }
    //}
    //UnitStats unitstats;

    public int health;
    public float speed;
    public float buildSpeed;
    public int attackDamage;
    public float Lookrange;
    public float fireRate;
    public float attackRange;



    public UnitBase(GameObject gameobj, int health, float speed, float buildSpeed, int attackDamage,
        float Lookrange, float fireRate, float attackRange)
    {
        this.health = health;
        this.speed = speed;
        this.buildSpeed = buildSpeed;
        this.attackDamage = attackDamage;
        this.Lookrange = Lookrange;
        this.fireRate = fireRate;
        this.attackRange = attackRange;
    }

    //public UnitBase(GameObject gameobj, UnitStats unitstats)
    //{
    //    this.unitstats = unitstats;
    //}



    //only accsesible by the states classes

    //maybe change later [wood, steal, money]
    protected int[] cost;
    public abstract Type Tick();

    
    public abstract Type FindCurrentUnitType();
    public BaseState FindCurrentState()
    {
        return gameobj.GetComponent<StateMachine>().CurState;
    }
    public void setGameOBJ(GameObject gameobj)
    {
        this.gameobj = gameobj;
    }
}
