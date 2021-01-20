using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_For_Resources : MonoBehaviour
{
    [SerializeField]
    private Resources resources;

    public Resources resource => resources;
    List<Collider> ColliderList = new List<Collider>();
    

    public bool CheckForResources()
    {
        if (ColliderList.Count > 0)
            return true;
        else
            return false;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == resource.ToString())
        {
            ColliderList.Add(c);
        }
    }
    void OnTriggerExit(Collider c)
    {
        if (c.tag == resource.ToString())
        {
            ColliderList.Remove(c);
        }
    }
    public enum Resources
    {
        Tree,
        metal,
        oil
    }
}
