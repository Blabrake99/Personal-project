using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private Team team;
    private Collider[] inRangeColliders;
    public Team Teams => team;

    public Transform Target;

    private float RandomRotateTimer = 3;
   
    int FogLookRange = 7;
    public int health = 700;
    private int _attackDamage = 20;
    private float _fireRate = .2f;
    public float _attackRange = 10;
    void Awake()
    {
        SetTeam(GameObject.Find("PlayerOBJ").GetComponent<Players_Script>().Teams.ToString());
    }
    void Update()
    {
        var target = CheckForAggro();
        //if(gameObject.transform.rotation.x < 0 || gameObject.transform.rotation.x >0)
        //{
        //    gameObject.transform.rotation = new Quaternion(0,transform.rotation.y,0,0);
        //}
        //if (gameObject.transform.rotation.z < 0 || gameObject.transform.rotation.z > 0)
        //{
        //    gameObject.transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
        //}
        if (target != null)
        {
            Target = target;
            //transform.LookAt(Target);
            _fireRate -= Time.deltaTime;
            if(_fireRate <= 0)
            {
                Fire();
                _fireRate = .2f;
            }
            

        } 
        //else if (target == null && RandomRotateTimer <= 0)
        //{
        //    transform.rotation = new Quaternion( transform.rotation.x,Random.rotation.y, 
        //        transform.rotation.z, transform.rotation.w);
        //    RandomRotateTimer = Random.Range(2, 4);
        //}
        //RandomRotateTimer -= Time.deltaTime;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Fire()
    {
        if (Target != null)
        {
            if (Target.GetComponent<Actual_AI>() != null)
            {
                //the target loses health based on our attack damage 
                Target.GetComponent<Actual_AI>().health -= _attackDamage;
                //this will let the target know who's attacking him 
                if (Target.GetComponent<Actual_AI>().Target == null)
                {
                    Target.GetComponent<Actual_AI>().SetTarget(this.gameObject.transform);
                }
            }
            if (Target.GetComponent<Building>() != null)
            {
                //the target loses health based on our attack damage 
                Target.GetComponent<Building>().health -= _attackDamage;
            }
            if (Target.GetComponent<Turret>() != null)
            {
                //the target loses health based on our attack damage 
                Target.GetComponent<Turret>().health -= _attackDamage;
                //this will let the target know who's attacking him 
                if (Target.GetComponent<Turret>().Target == null)
                {
                    Target.GetComponent<Turret>().Target = this.gameObject.transform;
                }
            }
        }
    }

    //Quaternion startingAngle = Quaternion.AngleAxis(-20, Vector3.up);
    //Quaternion stepAngle = Quaternion.AngleAxis(10, Vector3.up);
    private Transform CheckForAggro()
    {
        RaycastHit hit;

        inRangeColliders = Physics.OverlapSphere(transform.position, 5);

        for (int i = 0; i < inRangeColliders.Length; i++)
        {
            var temp = inRangeColliders[i].gameObject;

            if (inRangeColliders[i].gameObject.tag == "Unit")
                if (temp.gameObject.GetComponent<Actual_AI>().Teams.ToString() != Teams.ToString())
                    return temp.gameObject.transform;

            if (inRangeColliders[i].gameObject.tag == "Building")
                if (temp.gameObject.GetComponent<Building>().Teams.ToString() != Teams.ToString())
                    return temp.gameObject.transform;

            if (inRangeColliders[i].gameObject.tag == "Turret")
                if (temp.gameObject.GetComponent<Turret>().Teams.ToString() != Teams.ToString())
                    return temp.gameObject.transform;

        }
        return null;
    }
    public void SetTeam(System.String t)
    {
        team = (Team)System.Enum.Parse(typeof(Team), t);
    }
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

}
