using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMovement : MonoBehaviour
{
    GameObject mapManager;
    MapGenerator mapGen;

    public bool canMove = true;
    float horizInput;
    float vertInput;
    float xPos;
    float zPos;

    // Start is called before the first frame update
    void Start()
    {
        mapManager = GameObject.Find("/EventSystem");
        mapGen = mapManager.GetComponent<MapGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        xPos = transform.position.x;
        zPos = transform.position.z;

        //Prevents flying off map
        if(canMove == true)
        {
            //Moving along the / axis
            if(horizInput > 0f)
            {
                if(xPos < mapGen.length - 1)
                {
                    transform.position += new Vector3(1f, 0.5f, 0f);
                    canMove = false;
                    //SnapToGrid();
                }
            } else if (horizInput < 0f)
            {
                if(xPos > 0f)
                {
                    transform.position += new Vector3(-1f, 0.5f, 0f);
                    canMove = false;
                    //SnapToGrid();
                }
            }
            //Moving along the \ axis
            else if(vertInput > 0f)
            {
                if(zPos < mapGen.width - 1)
                {
                    transform.position += new Vector3(0f, 0.5f, 1f);
                    canMove = false;
                    //SnapToGrid();
                }
                
            } else if (vertInput < 0f)
            {
                if(zPos > 0f)
                {
                    transform.position += new Vector3(0f, 0.5f, -1f);
                    canMove = false;
                    //SnapToGrid();
                }
            }
        }

        //Determines if player can move or not.
        if(horizInput == 0f && vertInput == 0f)
        {
            canMove = true;
            SnapToGrid();
        }
    }

    void SnapToGrid()
    {
        
        //Snap  piece to grid
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
            transform.position = new Vector3(xPos, transform.position.y, zPos);
    }
}
