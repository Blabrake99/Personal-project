﻿using System.Collections.Generic;
using UnityEngine;
public class BluePrint : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    public GameObject prefab;
   // Material Mat;
    List<Collider> ColliderList = new List<Collider>();
    void Start()
    {
        //Mat = gameObject.GetComponent<Renderer>().material;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            this.gameObject.transform.position = hit.point;
        }
    }


    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            this.gameObject.transform.position = hit.point;
            if (ColliderList.Count <= 0 && hit.point.y < 2f)
            {
                //Mat.color = new Color(0, 1, 0, .4f);

                if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.KeypadEnter))
                {
                    Instantiate(prefab, this.gameObject.transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }
            //else
            //{
            //    Mat.color = new Color(1, 0, 0, .4f);
            //}
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
