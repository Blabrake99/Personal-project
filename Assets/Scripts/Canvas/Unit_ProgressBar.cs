using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Unit_ProgressBar : MonoBehaviour
{
    private Image image;
    void Start()
    {
        image = this.GetComponent<Image>();
    }
    //Call this to start the progress bar    
    public void StartTimer(float timeToBuild)
    {
        image = this.GetComponent<Image>();
        if (IsDone() == true)
        {
            image.fillAmount = 0;
        }
        image.fillAmount += 1.0f/timeToBuild * Time.deltaTime;
    }
    //this just resets the progress bar
    public void ResetProgressBar()
    {
        image = this.GetComponent<Image>();
        image.fillAmount = 0;
    }
    //this is to detect if the progress 
    //bar is done filling
    public bool IsDone()
    {
        image = this.GetComponent<Image>();
        if (image.fillAmount >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
