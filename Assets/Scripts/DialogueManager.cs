using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    
    public GameObject dialogueUI;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    
    private Queue<string> sentences;
    
    public int totalSentences = 0;
    public int sentenceCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        //Debug.Log("Starting conversation with " + dialogue.Name);
        dialogueUI.SetActive(true);
        nameText.text = dialogue.Name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        totalSentences = sentences.Count;
        sentenceCount = totalSentences;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        sentenceCount = sentences.Count;
        if(sentences.Count == 0 && totalSentences > 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //Debug.Log(sentence);
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        dialogueUI.SetActive(false);
    }
}
