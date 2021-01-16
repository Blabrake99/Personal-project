using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Building : MonoBehaviour
{
    public GameObject Base_blueprint;

    public void spawn_Base_blueprint()
    {
       Instantiate(Base_blueprint);
    }
    
}
