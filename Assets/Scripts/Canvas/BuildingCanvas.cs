using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class BuildingCanvas : MonoBehaviour
{
    public Building[] bases;
    public Building CurrentBase;
    public GameObject currentSpawner;

    float quicktimer = .5f;
    public bool movingSpawner;
    bool onlyOnce;
    void Update()
    {
        bases = GameObject.FindObjectsOfType(typeof(Building)) as Building[];
        if (currentSpawner != null)
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
                if(!onlyOnce)
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
                    if (bases[i].InUI)
                    {
                        CurrentBase = bases[i];
                        if (CurrentBase.Spawner == null)
                        {
                            CurrentBase.Spawner = currentSpawner;
                        }
                    }
                }
            }
        }
        if (CurrentBase != null && currentSpawner == null)
        {
            currentSpawner = CurrentBase.Spawner;
            if (currentSpawner != null)
            {
                currentSpawner.SetActive(true);
            }
        }
        if (CurrentBase != null)
        {
            if (CurrentBase.Spawner == null && currentSpawner != null)
            {
                CurrentBase.Spawner = currentSpawner;
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
        CurrentBase.DestroySpawnPoint();
        Destroy(CurrentBase.gameObject);

    }
    public void DestroySpawnPoint()
    {
        CurrentBase.DestroySpawnPoint();
    }
    //this will detect if you click off the canvas or just want to
    //leave the canvas in general

    private void HideIfClickedOutside(GameObject panel)
    {
        
        if (Input.GetMouseButton(0) && panel.activeSelf &&
            !RectTransformUtility.RectangleContainsScreenPoint(
                panel.GetComponent<RectTransform>(), Input.mousePosition,
                Camera.main) && !movingSpawner || Input.GetKey(KeyCode.Escape)  || Input.GetMouseButton(1))
        {
            if (movingSpawner)
                CurrentBase.DestroySpawnPoint();

            if (EventSystem.current.IsPointerOverGameObject())
                return;
            if (gameObject.activeSelf)
            {
                CurrentBase.hideBuildingUI.gameObject.SetActive(true);
                Destroy(this.gameObject);
                if (currentSpawner != null)
                {
                    currentSpawner.SetActive(false);
                }
            }
        }
    }


}
