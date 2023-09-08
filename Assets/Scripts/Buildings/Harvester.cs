using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : MonoBehaviour, IBuilding
{
    [SerializeField]
    private Team team;

    public Team Teams => team;

    public int health = 500;

    public int fullHealth = 500;
    [SerializeField]
    private Resources resources;

    public Resources resource => resources;

    //this is here for if we want to do 
    //Harvester Upgrades
    int GainedResources = 20;

    GameObject player;
    float timer = 2f; 
    void Awake()
    {
        player = GameObject.Find("PlayerOBJ");
        SetTeam(player.GetComponent<Players_Script>().Teams.ToString());
    }

    // Update is called once per frame
    void Update()
    {
       if(player.GetComponent<Players_Script>().Teams.ToString() == Teams.ToString())
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (resources == Resources.Wood)
                {
                    player.GetComponent<Players_Script>().resources[0] += GainedResources;
                } 
                if(resources == Resources.metal)
                {
                    player.GetComponent<Players_Script>().resources[1] += GainedResources;
                }
                if(resources == Resources.oil)
                {
                    player.GetComponent<Players_Script>().resources[2] += GainedResources;
                }
                timer = 2f;
            }
        } 
    }
    public void SetTeam(System.String t)
    {
        team = (Team)System.Enum.Parse(typeof(Team), t);
    }

    public string GetTeam()
    {
        return Teams.ToString();
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
    public enum Resources
    {
        Wood,
        metal,
        oil
    }
}
