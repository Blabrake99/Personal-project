using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Unit : MonoBehaviour
{
    public int spawnCounter;
    public UnitSpawnData[] spawnData;
    Players_Script PS;
    [SerializeField]
    Text[] UnitTxt;

    void Awake()
    {
        PS = GameObject.Find("PlayerOBJ").GetComponent<Players_Script>();

        for(int i = 0;i < UnitTxt.Length;i++)
        {
            UnitTxt[i].text = spawnData[i].unit.name + " Cost: " + spawnData[i].costs[0] + " wood , " + spawnData[i].costs[1] + " metal , " + spawnData[i].costs[2] + " oil";
        }
    }
    public void SpawnUnitButton(int SpawnIndex)
    {
        if (PS.resources[0] >= spawnData[SpawnIndex].costs[0] && PS.resources[1] >= spawnData[SpawnIndex].costs[1]
                && PS.resources[2] >= spawnData[SpawnIndex].costs[2])
        {
            PS.resources[0] -= spawnData[SpawnIndex].costs[1];
            PS.resources[1] -= spawnData[SpawnIndex].costs[1];
            PS.resources[2] -= spawnData[SpawnIndex].costs[1];
            this.gameObject.GetComponent<BuildingCanvas>().CurrentBase.GetComponent<Building>().Unit.Add(spawnData[SpawnIndex].unit);
            spawnCounter += 1;
        }
    }
}
[System.Serializable]
public class UnitSpawnData
{
    public GameObject unit;
    public int[] costs;
}
