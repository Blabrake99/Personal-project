using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Building : MonoBehaviour
{
    public GameObject Base_blueprint;
    [SerializeField] GameObject buildingUI;
    [SerializeField] string buildingTxt;
    int[] cost;
    private void Start()
    {
        IBlueprint blueprint = (IBlueprint)Base_blueprint.GetComponent(typeof(IBlueprint));
        cost = blueprint.getCosts();
    }
    public void spawn_Base_blueprint()
    {
        if (!GameObject.FindGameObjectWithTag("BluePrint"))
            Instantiate(Base_blueprint);
        else
            Destroy(GameObject.FindGameObjectWithTag("BluePrint"));
    }
    public void OnHoverEnter()
    {
        buildingUI.SetActive(true);
        Text t = buildingUI.GetComponentInChildren<Text>();
        t.text = cost[0] + " Wood" + Environment.NewLine + cost[1] + " Metal" + Environment.NewLine + cost[2] + " Oil" + Environment.NewLine
           + buildingTxt;
    }
    public void OnHoverExit()
    {
        Text t = buildingUI.GetComponentInChildren<Text>();
        t.text = "";
        buildingUI.SetActive(false);
    }
}
