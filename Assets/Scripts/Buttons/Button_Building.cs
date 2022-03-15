using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Building : MonoBehaviour
{
    public GameObject Base_blueprint;

    public void spawn_Base_blueprint()
    {
        if (!GameObject.FindGameObjectWithTag("BluePrint"))
            Instantiate(Base_blueprint);
        else
            Destroy(GameObject.FindGameObjectWithTag("BluePrint"));
    }
    
}
