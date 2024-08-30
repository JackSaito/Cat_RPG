using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followerMovement : MonoBehaviour
{
    public GameObject playerParent;
    public GameObject spriteImage;
    GameObject mainChar;
    int i = 0;
    public List<GameObject> movingChars = new List<GameObject>();
    string myName;
    
    
    Vector3 oldPos;
    Vector3 newPos;
    Rigidbody rb;
    bool moveTime = false;
    public float waitTime = 0.5f;
    public float speed = 0.5f;
    public float moveDist = 10f;

    // Start is called before the first frame update
    void Start()
    {
        //Setting up basic heirarchy info
        playerParent = transform.parent.gameObject;
        myName = this.gameObject.name;
        //Gets number of characters in front of it
        int charCount = playerParent.transform.childCount;
        i = 0;
        while (i < charCount)
        {
            string charName = playerParent.transform.GetChild(i).gameObject.name;
            if(charName == myName)
            {
                break;
            } else 
            {
                movingChars.Add(playerParent.transform.GetChild(i).gameObject);
            }
            i++;
        }

        rb = gameObject.GetComponent<Rigidbody>();
        oldPos = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        newPos = transform.position;
        float dist = Vector3.Distance(transform.position, movingChars[0].transform.position);
        //Debug.Log(dist);

        float spriteScaleX = spriteImage.transform.localScale.x;
        Vector3 spriteDim = new Vector3(spriteScaleX, 1f, 1f);
        if(transform.position.x - movingChars[0].transform.position.x > 0)
        {
            if(spriteScaleX > 0)
            {
                spriteDim.x = -1f;
                spriteImage.transform.localScale = spriteDim;
            }
        }
        if(transform.position.x - movingChars[0].transform.position.x < 0)
        {
            if(spriteScaleX < 0)
            {
                spriteDim.x = 1f;
                spriteImage.transform.localScale = spriteDim;
            }
        }
        if(newPos != oldPos && dist > moveDist)
        {
            StartCoroutine(delayedMove(waitTime));
        }

        if(dist > 10f)
        {
            transform.position = movingChars[i-1].transform.position;
        }
    }

    private IEnumerator delayedMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        moveTime = true;
        //Vector3 destination = Vector3.Lerp(transform.position, delayPos, Time.deltaTime * speed);
    }

    void FixedUpdate()
    {
        if (moveTime == true)
        {
            int x = 0;
            if(i == 1)
            {
                x = 0;
            } else if (i > 1) 
            {
                x = i - 1;
            }
            Vector3 delayPos = movingChars[x].transform.position;
            Vector3 goalPos = new Vector3(delayPos.x - transform.position.x, 0f, delayPos.z - transform.position.z);
            //Debug.Log(delayPos);
            rb.MovePosition(transform.position + (goalPos * Time.deltaTime * speed));
        }
        
    }
}
