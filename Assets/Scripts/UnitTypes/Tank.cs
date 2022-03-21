using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    private Actual_AI _Ai;

    private int _health = 200;
    private float _speed = 8;
    private int _attackDamage = 50;
    private float _Lookrange = 5;
    private float _fireRate = .4f;
    private float _attackRange = 6f;
    private float _chaserange = 7f;
    //static UnitStats unitStats = unitStats.init(200,2,2,10,5,2,3);

    //private Tank(Actual_AI ai, int health, float speed, float buildSpeed, int attackDamage,
    //    float Lookrange, float fireRate, float attackRange) : base(ai.gameObject, 100,3,3,20,5,.2f,5)
    //{
    //    base.setGameOBJ(this.gameObject);
    //    _Ai = ai;
    //}
    public void Start()
    {
        _Ai = this.gameObject.GetComponent<Actual_AI>();
        _Ai.health = _health;
        _Ai.attackDamage = _attackDamage;
        _Ai.FogLookRange = _Lookrange;
        _Ai.fireRate = _fireRate;
        _Ai.attackRange = _attackRange;
        _Ai.chaseRange = _chaserange;
        _Ai.fullHealth = _health;
        _Ai.moveSpeed = _speed;
        //base.setGameOBJ(this.gameObject);
    }

    //public override Type Tick()
    //{
    //   BaseState State = this.FindCurrentState();

    //    if(State is AttackState)
    //    {
           
    //    }

    //    return null;
    //}

    //public override Type FindCurrentUnitType()
    //{
    //    return typeof(Tank);
    //}
}
