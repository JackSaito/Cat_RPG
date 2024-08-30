using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    GameObject interactIcon;
    GameObject player;
    bool playerIn = false;
    public float growSpeed = 1f;
    public float qMarkMaxSize = 1f;

    GameObject parentUI;
    public GameObject dialogueUI;
    public DialogueTrigger dialogueTrigger;
    public bool convoStarted = false;
    CharacterMover characterMover;
    public float tempSpeed;

    
    // Start is called before the first frame update
    void Start()
    {
        interactIcon = gameObject.transform.GetChild(0).gameObject;
        parentUI = GameObject.Find("/UI");
        characterMover = FindObjectOfType<CharacterMover>();
        dialogueUI = parentUI.gameObject.transform.GetChild(0).gameObject;
        tempSpeed = characterMover.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if(playerIn == true)
            {
                float xDiff = player.transform.position.x - transform.position.x;
                if(xDiff > 0f)
                {
                    Vector3 flipScale = new Vector3( 1f, transform.localScale.y, transform.localScale.z);
                    gameObject.transform.localScale = flipScale;
                }
                else if(xDiff < 0f)
                {
                    Vector3 flipScale = new Vector3( -1f, transform.localScale.y, transform.localScale.z);
                    gameObject.transform.localScale = flipScale;
                }
                if(Input.GetButtonDown("Interact"))
                {
                    if(convoStarted == true)
                    {
                        continueDialogue();
                    }
                    if(convoStarted == false)
                    {
                        startDialogue();
                        convoStarted = true;
                        characterMover.speed = 0f;
                    }
                    
                }
            }
            float playerDist = Vector3.Distance(transform.position, player.transform.position);
            //Debug.Log(playerDist);
            if (transform.localScale.x < qMarkMaxSize)
            {
                interactIcon.transform.localScale = Vector3.one * growSpeed * (3f - playerDist);
            }
        }

        if(dialogueUI.activeInHierarchy == false)
        {
            convoStarted = false;
            characterMover.speed = tempSpeed;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.gameObject;
            playerIn = true;
            interactIcon.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerIn = false;
            interactIcon.SetActive(false);
            convoStarted = false;
        }
    }

    void startDialogue()
    {
        //Debug.Log("We should be talking here!");
        dialogueTrigger.TriggerDialogue();
    }
    void continueDialogue()
    {
        dialogueTrigger.nextLine();
    }
}
