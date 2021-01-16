using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Materials_Canvas : MonoBehaviour
{
    GameObject CurPlayOBJ;
    public Text Wood, Metal, Oil;
    void Start()
    {
        CurPlayOBJ = GameObject.Find("PlayerOBJ");
    }

    // Update is called once per frame
    void Update()
    {
        Wood.text = "Wood : " + CurPlayOBJ.GetComponent<Players_Script>().resources[0].ToString();
        Metal.text = "Metal : " + CurPlayOBJ.GetComponent<Players_Script>().resources[1].ToString();
        Oil.text = "Oil : " + CurPlayOBJ.GetComponent<Players_Script>().resources[2].ToString();
    }
}
