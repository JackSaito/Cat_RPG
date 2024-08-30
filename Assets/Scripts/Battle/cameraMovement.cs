using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public float sensitivity = 10f;
    public float zoomSpeed = 5f;
    public float rotateSpeed = 45f;
    public float turnSmooth = 0.5f;
    private Vector3 dragOrigin;
    float deltaAngle = 22f;
    float turnInput = 0f;
    public float upperAngle = 60f;
    public float lowerAngle = 15f;

    Camera cam;

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        //Scroll to zoom
        handleZoom();

        //Use E and Q to turn the camera
        if(Input.GetKey(KeyCode.E))
        {
            turnInput = 1f;
        } else if(Input.GetKey(KeyCode.Q))
        {
            turnInput = -1f;
        } else {
            turnInput = 0f;
        }
        float finalAngle = Mathf.Clamp(deltaAngle + rotateSpeed * turnInput, lowerAngle, upperAngle);
        Quaternion target = Quaternion.Euler(finalAngle, 45f, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * turnSmooth);
        
        // Check for right mouse button down to drag the screen
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        // Check for right mouse button being held
        if (!Input.GetMouseButton(1)) return;

        // Calculate the difference in mouse position
        Vector3 dragDelta = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);

        // Calculate the new camera position
        Vector3 newPosition = transform.position;
        newPosition.x -= dragDelta.x * sensitivity; 
        newPosition.z += dragDelta.x * sensitivity;
        newPosition.y -= dragDelta.y * sensitivity; 

        // Update the camera position
        transform.position = newPosition;

        // Update the drag origin for the next frame
        dragOrigin = Input.mousePosition;
    }

    private void handleZoom()
    {
        float zoom = Input.mouseScrollDelta.y;

        if (Mathf.Abs(zoom) > 0)
        {
            float zoomDelta = zoom * zoomSpeed;
            //Debug.Log(zoomDelta);
            Camera cam2 = Camera.main;
            float newSize = Mathf.Clamp(cam2.orthographicSize - zoomDelta, 1f, 6f);
            cam2.orthographicSize = newSize;
            sensitivity = newSize * 3f;
        }
    }
}
