using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Spawner : MonoBehaviour
{
    public GameObject SpawnerOBJ;
    GameObject spawner;
    public void Spawn_Spawner()
    {
        if (spawner == null)
        {
            this.gameObject.GetComponent<BuildingCanvas>().currentSpawner = Instantiate(SpawnerOBJ);
            spawner = this.gameObject.GetComponent<BuildingCanvas>().currentSpawner;
        }
        else
        {
            spawner.GetComponent<Spawner>().curState = Spawner.SpawnerStates.unlocked;
        }
    }
    void Update()
    {
        if(this.gameObject.GetComponent<BuildingCanvas>().currentSpawner != null)
        {
            spawner = this.gameObject.GetComponent<BuildingCanvas>().currentSpawner;
        }
    }
}
