using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuildingCanvas : MonoBehaviour
{
    public Building[] bases;
    public GameObject CurrentBase;
    public GameObject currentSpawner;

    bool clicked;
    float timer = .1f;
    float quicktimer = .5f;
    public bool movingSpawner;
    bool onlyOnce;
    void Update()
    {
        bases = GameObject.FindObjectsOfType(typeof(Building)) as Building[];
        if (currentSpawner != false)
        {
            if (currentSpawner.GetComponent<Spawner>().curState == Spawner.SpawnerStates.unlocked)
            {
                onlyOnce = false;
                movingSpawner = true;
            }
            if (currentSpawner.GetComponent<Spawner>().curState == Spawner.SpawnerStates.Lock)
            {
                //this is here so when you place a spawner somewhere
                //if doesn't close out of the building window 
                if(onlyOnce == false)
                {
                    quicktimer = .5f;
                    onlyOnce = true;
                }
                movingSpawner = false;
            }
        }
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
        if (quicktimer <= 0)
        {
            HideIfClickedOutside(gameObject);
        }

        quicktimer -= Time.deltaTime;
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

    //this will detect if you click off the canvas or just want to
    //leave the canvas in general

    private void HideIfClickedOutside(GameObject panel)
    {
        
        if (Input.GetMouseButton(0) && panel.activeSelf &&
            !RectTransformUtility.RectangleContainsScreenPoint(
                panel.GetComponent<RectTransform>(),
                Input.mousePosition,
                Camera.main) && movingSpawner == false
                || Input.GetKey(KeyCode.Escape) && movingSpawner == false)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            if (gameObject.activeSelf == true)
            {

                Destroy(this.gameObject);
                if (currentSpawner != null)
                {
                    currentSpawner.SetActive(false);
                }
            }
        }
    }


}
