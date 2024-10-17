using UnityEngine;
using UnityEngine.UI;

public class OnTargetFoundHandler : MonoBehaviour
{
    public Button playPauseButton;   // Asignar el bot�n en el Inspector
    public GameObject[] objectsToShow; // Lista de objetos que se mostrar�n al encontrar la imagen

    // Funci�n que se llama cuando la imagen es rastreada (On Target Found)
    public void OnTargetFound()
    {
        playPauseButton.gameObject.SetActive(true);  // Mostrar el bot�n cuando la imagen es rastreada
        ShowObjects();  // Mostrar los objetos cuando se detecte la imagen
    }

    // Funci�n para mostrar todos los objetos
    private void ShowObjects()
    {
        foreach (GameObject obj in objectsToShow)
        {
            obj.SetActive(true);  // Mostrar cada objeto
        }
    }
}

