using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControldeDialogos : MonoBehaviour
{
    private Animator anim; 
    private Queue<string> coladialogos;
    Textos texto;
    [SerializeField] TextMeshProUGUI TextoPantalla;
    public void ActivarCartel(Textos textosObjeto)
    {
        if (anim == null)
        {
            Debug.LogError("El componente Animator no está asignado correctamente.");
            return;
        }

        if (textosObjeto == null)
        {
            Debug.LogError("El objeto Textos es nulo.");
            return;
        }

        anim.SetBool("Cartel", true);
        texto = textosObjeto;
        Activartexto();
    }
    public void Activartexto()
    {
        coladialogos.Clear();
        foreach(string textosguardar in texto.arraytextos)
        {
            coladialogos.Enqueue(textosguardar);
        }
        SiguinteFrase();
    }
    public void SiguinteFrase()
    {
        if(coladialogos.Count==0)
        { 
            cierraCartel();
            return;
        
        }
        string siguientefrase = coladialogos.Dequeue();
        TextoPantalla.text = siguientefrase;
    }
    void cierraCartel()
    {
        anim.SetBool("Cartel", false);
    }
}
