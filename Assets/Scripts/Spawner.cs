using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public enum SpawnerStates
    {
        Lock,
        unlocked
    };
    public SpawnerStates curState = SpawnerStates.unlocked;

    public float locktimer = .01f;

    RaycastHit hit;
    Vector3 movePoint;
    Vector3 lastpos;
    void Start()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            this.gameObject.transform.position = hit.point;
        }
    }

    void Update()
    {
        if (curState == SpawnerStates.unlocked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                this.gameObject.transform.position = hit.point;
            }
            if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.KeypadEnter))
            {
                curState = SpawnerStates.Lock;
                lastpos = this.gameObject.transform.position;
            }
            if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Escape))
            {
                this.gameObject.transform.position = lastpos;
            }
        }
    }
}
