﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrint_For_Harvesters : MonoBehaviour
{
    RaycastHit hit;
    bool InRadiusOfResources;
    Vector3 movePoint;
    public GameObject prefab;
     Material Mat;
    List<Collider> ColliderList = new List<Collider>();
    public int[] costs = new int[3];
    Players_Script PS;
    void Start()
    {
        PS = GameObject.Find("PlayerOBJ").GetComponent<Players_Script>();
        Mat = gameObject.GetComponent<Renderer>().material;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            this.gameObject.transform.position = hit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        InRadiusOfResources = transform.GetChild(0).GetComponent<Check_For_Resources>().CheckForResources();
        if (Physics.Raycast(ray, out hit))
        {
            this.gameObject.transform.position = hit.point;

            if (ColliderList.Count <= 0 && hit.point.y < 2f 
                && InRadiusOfResources)
            {
                Mat.color = new Color(0, 1, 0, .4f);

                if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.KeypadEnter))
                {
                    Instantiate(prefab, this.gameObject.transform.position, transform.rotation);
                    PS.resources[0] -= costs[0];
                    PS.resources[1] -= costs[1];
                    PS.resources[2] -= costs[2];
                    Destroy(gameObject);
                }
            }
            else
            {
                Mat.color = new Color(1, 0, 0, .4f);
            }
        }

        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Escape))
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Building" || c.tag == "BluePrint")
        {
            ColliderList.Add(c);
        }
    }
    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Building" || c.tag == "BluePrint")
        {
            ColliderList.Remove(c);
        }
    }
}
