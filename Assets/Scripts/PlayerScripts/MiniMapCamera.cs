using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    Camera_Controller camera;
    void Start()
    {
        camera = FindObjectOfType<Camera_Controller>();
    }

    void Update()
    {
        transform.position = new Vector3(camera.transform.position.x, transform.position.y, camera.transform.position.z);
    }
}
