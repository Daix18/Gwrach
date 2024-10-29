using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractivo : MonoBehaviour
{
    // Referencia al componente ControldeDialogos
    private ControldeDialogos controlDialogos;

    private void Start()
    {
        // Obtener referencia al componente ControldeDialogos
        controlDialogos = FindObjectOfType<ControldeDialogos>();
    }

    public Textos textosComponent;

    private void OnMouseDown()
    {
        if (controlDialogos != null && textosComponent != null)
        {
            controlDialogos.ActivarCartel(textosComponent);
        }
        else
        {
            Debug.LogWarning("No se encontró el componente ControldeDialogos o el componente Textos.");
        }
    }
}