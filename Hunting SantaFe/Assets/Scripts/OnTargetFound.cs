using UnityEngine;
using UnityEngine.UI;

public class OnTargetFoundHandler : MonoBehaviour
{
    public Button playPauseButton;   // Asignar el botón en el Inspector
    public GameObject[] objectsToShow; // Lista de objetos que se mostrarán al encontrar la imagen

    // Función que se llama cuando la imagen es rastreada (On Target Found)
    public void OnTargetFound()
    {
        playPauseButton.gameObject.SetActive(true);  // Mostrar el botón cuando la imagen es rastreada
        ShowObjects();  // Mostrar los objetos cuando se detecte la imagen
    }

    // Función para mostrar todos los objetos
    private void ShowObjects()
    {
        foreach (GameObject obj in objectsToShow)
        {
            obj.SetActive(true);  // Mostrar cada objeto
        }
    }
}

