using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    [SerializeField] private GameObject visualCue;

    bool isPlayerNearby = false;

    public GameObject dialogueUI;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearby)
        {
            visualCue.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueUI.SetActive(true); 
            }
        }
        else
        {
            visualCue.SetActive(false);
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
        }
    }
}
