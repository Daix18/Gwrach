using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogue, navButtonText;
    public Image speakerSprite;
    private int currentIndex;
    private Conversation currentConvo;
    private static DialogueManager instance;
    private Animator anim;
    private Coroutine typing;
    private string fullText; // Variable para almacenar el texto completo de la línea actual
    private bool isComplete = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            anim = GetComponent<Animator>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {

    }

    public static void StartConversation(Conversation convo)
    {
        instance.anim.SetBool("isOpen", true);
        instance.currentIndex = 0;
        instance.currentConvo = convo;
        instance.speakerName.text = "";
        instance.dialogue.text = "";
        instance.navButtonText.text = ">";
        instance.ReadNext();
    }

    public void ReadNext()
    {
        if (!isComplete)
        {
            ShowFullText();
            return;
        }

        if (currentIndex > currentConvo.getLenght())
        {
            Debug.Log("Caca");
            instance.anim.SetBool("isOpen", false);

            Speaker initiator = currentConvo.GetInitiator();
            if (initiator != null)
            {
                MarkConversationAsFinished(initiator);
                GameManager.THIS._firstNPC = true;
            }

            return;
        }

        if (typing != null)
        {
            StopCoroutine(typing);
        }

        speakerName.text = currentConvo.GetLineByIndex(currentIndex).speaker.GetName();
        fullText = currentConvo.GetLineByIndex(currentIndex).dialogue;
        speakerSprite.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.GetSprite();
        typing = StartCoroutine(TypeText(fullText));
        isComplete = false;

        currentIndex++;
        if (currentIndex >= currentConvo.getLenght())
        {
            navButtonText.text = "X";
        }
    }

    private IEnumerator TypeText(string text)
    {
        dialogue.text = "";
        int index = 0;
        while (index < text.Length)
        {
            dialogue.text += text[index];
            index++;
            yield return new WaitForSeconds(0.05f);
        }
        typing = null;
        isComplete = true; // Marca que el texto está completamente mostrado
        Debug.Log("Text completed: " + dialogue.text); // Para depuración
    }

    private void ShowFullText()
    {
        if (typing != null)
        {
            StopCoroutine(typing); // Asegura que detenemos cualquier escritura en progreso
        }
        dialogue.text = fullText; // Asegura que se muestre el texto completo de la línea actual
        typing = null;
        isComplete = true; // Marca que el texto está completamente mostrado
    }

    private Dictionary<Speaker, bool> conversationCompletion = new Dictionary<Speaker, bool>();

    public bool HasConversationFinished(Speaker speaker)
    {
        return conversationCompletion.TryGetValue(speaker, out bool isFinished) && isFinished;
    }

    public void MarkConversationAsFinished(Speaker speaker)
    {
        conversationCompletion[speaker] = true;
    }
}