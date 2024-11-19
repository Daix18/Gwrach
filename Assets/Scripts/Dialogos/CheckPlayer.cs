using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    [SerializeField] private GameObject visualCue;

    bool isPlayerNearby = false;
    bool inConvo = false;
    bool convoFinished = false;

    public GameObject dialogueUI;
    public Conversation convo;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby)
        {
            if (!convoFinished)
            {
                visualCue.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E) && !inConvo)
            {
                dialogueUI.SetActive(true);
                DialogueManager.StartConversation(convo);
                inConvo = true;
            }
        }
        if(!isPlayerNearby || convoFinished)
        {
            visualCue.SetActive(false);
        }
        if (inConvo)
        {
            if (DialogueManager.HasConversationFinished())
            {
                dialogueUI.SetActive(false);
                inConvo = false;
                convoFinished = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            dialogueUI.SetActive(false); // Ocultar la tienda al salir del área de activación
            inConvo = false;
        }
    }
}
