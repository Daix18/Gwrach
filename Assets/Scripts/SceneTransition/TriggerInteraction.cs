using UnityEngine;

public class TriggerInteraction : MonoBehaviour
{
    public GameObject Player { get; set; }
    public bool CanInteract { get; set; }

    public GameObject visualCue;

    //private float timerInteraction;
    //private float interactionCooldown = 0.2f;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        visualCue.SetActive(false);
        
        CanInteract = false;
    }

    void Update()
    {
        Debug.Log(CanInteract);

        if (CanInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        visualCue.SetActive(true);

        if (collision.gameObject == Player)
        {
            CanInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CanInteract = false;

        visualCue.SetActive(false);
    }

    public virtual void Interact()
    {

    }
}
