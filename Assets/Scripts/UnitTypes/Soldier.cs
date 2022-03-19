using System;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour 
{
    private Actual_AI _Ai;
    private int _health =100;
    private float _speed = 3;
    private int _attackDamage = 20;
    private float _fogRange = 5;
    private float _fireRate = .2f;
    private float _attackRange = 5f;
    private float _chaserange = 6f;
    //private Soldier(Actual_AI ai, int health, float speed, float buildSpeed, int attackDamage,
    //    float Lookrange, float fireRate, float attackRange) : base(ai.gameObject, 100,3,3,20,5,.2f,5)
    //{
    //    //_health = health;
    //    //_speed = base.speed;
    //    //_buildSpeed = base.buildSpeed;
    //    //_attackDamage = base.attackDamage;
    //    //_Lookrange = base.Lookrange;
    //    //_fireRate = base.fireRate;
    //    //_attackDamage = base.attackDamage;

    //}
    public void Start()
    {
        _Ai = this.gameObject.GetComponent<Actual_AI>();
        _Ai.health = _health;
        _Ai.attackDamage = _attackDamage;
        _Ai.FogLookRange = _fogRange;
        _Ai.fireRate = _fireRate;
        _Ai.attackRange = _attackRange;
        _Ai.chaseRange = _chaserange;
        _Ai.fullHealth = _health;
        //base.setGameOBJ(this.gameObject);
    }
    

    //public override Type Tick()
    //{
    //    BaseState State = this.FindCurrentState();
    //    return null;
    //}

    //public override Type FindCurrentUnitType()
    //{
    //    return typeof(Soldier);
    //}
}
