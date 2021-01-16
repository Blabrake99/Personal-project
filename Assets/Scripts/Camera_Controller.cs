using UnityEngine;
using System.Collections.Generic;

public class Camera_Controller : MonoBehaviour
{
    //camera move speed 
    public float panSpeed = 20;
    //for mouse movement 
    public float panBorderThickness = 10f;
    //this if for the bounds of the camera 
    public Vector2 panLimit;

    //used to limit how fast you scroll with the scroll wheel
    public float ScrollSpeed = 20f;

    //this is the bounds for zooming in with the camera
    public float MaxY = 120f;
    public float MinY = 20f;

    void Update()
    {
        //get camera position for better movement
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        //used to detect when the user has scrolled 
        float Scroll = Input.GetAxis("Mouse ScrollWheel");

        //Lets you zoom in whit the scroll wheel
        pos.y -= Scroll * ScrollSpeed * 100f * Time.deltaTime;

        //the camera will nto be able to leave these bounds 
        // Don't forget to set them in the editor if i haven't already
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);
        pos.y = Mathf.Clamp(pos.y, MinY, MaxY);
        //this applies the position we want to move to the cameras position 
        transform.position = pos;
    }

}
