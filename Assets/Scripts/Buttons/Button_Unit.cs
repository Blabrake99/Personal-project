using System.Collections.Generic;
using UnityEngine;
public class Button_Unit : MonoBehaviour
{
    public int spawnCounter;
    public List<GameObject> Units = new List<GameObject>();
    public void SolderButton()
    {
        this.gameObject.GetComponent<BuildingCanvas>().CurrentBase.GetComponent<Building>().Unit.Add(Units[0]);
        spawnCounter += 1;
    }

}
