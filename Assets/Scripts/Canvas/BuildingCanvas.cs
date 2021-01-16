using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCanvas : MonoBehaviour
{
    public Building[] bases;
    public GameObject CurrentBase;
    public GameObject currentSpawner;

    bool clicked;
    float timer = .1f;
    void Update()
    {
        bases = GameObject.FindObjectsOfType(typeof(Building)) as Building[];
        if (Input.GetKey(KeyCode.Escape) || Input.GetMouseButton(1))
        {
            if (gameObject.activeSelf == true)
            {

                Destroy(this.gameObject);
                if (currentSpawner != null)
                {
                    currentSpawner.SetActive(false);
                }
            }
        }
        //this is for if you click off the page it'll just destroy it, but i know the issue of it
        // i'm just going to fix it after other things 

        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hitInfo = new RaycastHit();
        //    bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        //    if (hit)
        //    {
        //        if (hitInfo.transform.gameObject != this.gameObject )
        //        {
        //            if (gameObject.activeSelf == true)
        //            {
        //                Destroy(this.gameObject);
        //                if (currentSpawner != null)
        //                {
        //                    currentSpawner.SetActive(false);

        //                }
        //            }
        //        }
        //    }
        //}
        if (CurrentBase == null)
        {
            for (int i = 0; i < bases.Length; i++)
            {
                if (bases[i] != null)
                {
                    if (bases[i].InUI == true)
                    {
                        CurrentBase = bases[i].gameObject;
                        if (CurrentBase.GetComponent<Building>().Spawner == null)
                        {
                            CurrentBase.GetComponent<Building>().Spawner = currentSpawner;
                        }
                    }
                }
            }
        }
        if (CurrentBase != null && currentSpawner == null)
        {
            currentSpawner = CurrentBase.GetComponent<Building>().Spawner;
            if (currentSpawner != null && CurrentBase.GetComponent<Building>().Spawner != null)
            {
                currentSpawner.SetActive(true);
            }
        }
        if (CurrentBase != null)
        {
            if (CurrentBase.GetComponent<Building>().Spawner == null && currentSpawner != null)
            {
                CurrentBase.GetComponent<Building>().Spawner = currentSpawner;
            }
        }
    }

    public void DestroyBTN()
    {
        Destroy(this.gameObject, .05f);
    }
    public void DestroyBaseClick()
    {
        CurrentBase.GetComponent<Building>().DestroySpawnPoint();
        Destroy(CurrentBase);

    }
    public void SpawnUnit()
    {

    }
}
