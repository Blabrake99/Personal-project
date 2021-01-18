using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Unit : MonoBehaviour
{
    public int spawnCounter;
    [SerializeField]
    List<GameObject> Units = new List<GameObject>();
    [SerializeField]
    int[] recForUnits = new int[] { 20, 20, 20 };
    Players_Script PS;
    [SerializeField]
    Text CostTXT;
    void Awake()
    {
        PS = GameObject.Find("PlayerOBJ").GetComponent<Players_Script>();
        CostTXT.text = "Unit Cost: " + recForUnits[0] + " wood , " + recForUnits[1] + " metal , " + recForUnits[2] + " oil";
    }
    public void SolderButton()
    {
        if (PS.resources[0] >= recForUnits[0] && PS.resources[1] >= recForUnits[1]
                && PS.resources[2] >= recForUnits[2])
        {
            PS.resources[0] -= recForUnits[0];
            PS.resources[1] -= recForUnits[1];
            PS.resources[2] -= recForUnits[2];
            this.gameObject.GetComponent<BuildingCanvas>().CurrentBase.GetComponent<Building>().Unit.Add(Units[0]);
            spawnCounter += 1;
        }
    }

}
