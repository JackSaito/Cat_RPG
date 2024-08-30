using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceMovement : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody rb;
    private bool isDragging = false;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                }
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            rb.useGravity = true;
            float xPos = transform.position.x;
            float zPos = transform.position.z;
            //Debug.Log("Floor " + Mathf.Floor(xPos) + " not floor " + xPos);
            if(Mathf.Abs(Mathf.Floor(xPos) - xPos) >= 0.5f)
            {
                xPos = Mathf.Ceil(xPos);
            } else if (Mathf.Abs(Mathf.Floor(xPos) - xPos) < 0.5f)
            {
                xPos = Mathf.Floor(xPos);
            }
            if(Mathf.Abs(Mathf.Floor(zPos) - zPos) >= 0.5f)
            {
                zPos = Mathf.Ceil(zPos);
            } else if (Mathf.Abs(Mathf.Floor(zPos) - zPos) < 0.5f)
            {
                zPos = Mathf.Floor(zPos);
            }
            transform.position = new Vector3(xPos, 2.5f, zPos);
        }
        if(isDragging == true)
        {
            rb.useGravity = false;
            MoveObject();
        }
    }

    void MoveObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        Vector3 mousePos = hit.point;
        rb.MovePosition(new Vector3(mousePos.x, 2.5f, mousePos.z));
    }
}
