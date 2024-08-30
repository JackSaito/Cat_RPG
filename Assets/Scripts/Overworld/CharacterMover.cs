using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float speed;
    float vertTrans;
    float horiTrans;
    float playerX;
    float playerY;
    float playerZ;

    public GameObject spriteImage;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horiTrans = Input.GetAxis("Horizontal");
        vertTrans = Input.GetAxis("Vertical");

        //Making sure player's sprite is facing the right direction
        //Debug.Log(horiTrans);
        float spriteScaleX = spriteImage.transform.localScale.x;
        float spriteScaleY = spriteImage.transform.localScale.y;
        float spriteScaleZ = spriteImage.transform.localScale.z;
        Vector3 spriteDim = new Vector3(spriteScaleX, spriteScaleY, spriteScaleZ);
        if(horiTrans < -0.15)
        {
            if (spriteScaleX > 0)
            {
                spriteDim.x *= -1f;
                spriteImage.transform.localScale = spriteDim;
            }
        } else if(horiTrans > 0.15)
        {
            if (spriteScaleX < 0)
            {
                spriteDim.x *= -1f;
                spriteImage.transform.localScale = spriteDim;
            }
        }
    }

    void FixedUpdate()
    {
        //Movement of Main Character
        horiTrans = Input.GetAxis("Horizontal");
        vertTrans = Input.GetAxis("Vertical");
        Vector3 displace = new Vector3(horiTrans, 0f, vertTrans);
        if(Mathf.Abs(horiTrans) >= 1f && Mathf.Abs(vertTrans) >= 1f)
        {
            displace = 0.9f * displace.normalized;
        } else if (horiTrans != 0f && vertTrans != 0f)
        {
            displace = 0.88f * displace;
        }

        //Debug.Log(Vector3.Distance(new Vector3(0f,0f,0f), displace));
        rb.MovePosition(transform.position + (displace * Time.deltaTime * speed));
        //transform.Translate(horiz, 0f, verti);
        
    }
}
