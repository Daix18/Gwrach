using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CambiarImagenesContinuamente : MonoBehaviour
{
    public Sprite[] imagenes; // Arreglo de las imágenes que quieres mostrar
    private int index = 0; // Índice de la imagen actual

    private Image botonImagen; // Referencia al componente Image del botón

    private void Start()
    {        botonImagen = GetComponent<Image>(); // Obtener la referencia al componente Image del botón
        StartCoroutine(CambiarImagenContinuamente());
    }

    private IEnumerator CambiarImagenContinuamente()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Esperar 2 segundos antes de cambiar la imagen (puedes ajustar este valor)
            index = (index + 1) % imagenes.Length; // Avanzar al siguiente índice circularmente
            botonImagen.sprite = imagenes[index]; // Asignar la siguiente imagen al botón
        }
    }
}
