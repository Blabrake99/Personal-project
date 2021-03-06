﻿using UnityEngine;
using UnityEngine.UI;
public class HideBuildingUI : MonoBehaviour
{
    public GameObject BuildingCanvas;
    private bool SetActive = true;


    public void OnButtonPress()
    {
        if(SetActive == false)
        {
            BuildingCanvas.SetActive(true);
            this.GetComponentInChildren<Text>().text = "Hide Building Menu";
            SetActive = true;
        }
        else
        {
            BuildingCanvas.SetActive(false);
            this.GetComponentInChildren<Text>().text = "Unhide Building Menu";
            SetActive = false;
        }
    }

}
