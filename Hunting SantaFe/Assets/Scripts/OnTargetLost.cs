using UnityEngine;
using UnityEngine.Video;

public class OnTargetLostHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Asignar el VideoPlayer en el Inspector
    public GameObject[] objectsToHide; // Lista de objetos que desaparecer�n al perder la imagen
    private bool isPlaying = false;

    // Funci�n que se llama cuando se pierde la imagen (On Target Lost)
    public void OnTargetLost()
    {
        if (isPlaying)
        {
            videoPlayer.Pause();  // Pausar el audio si la imagen se pierde
            isPlaying = false;
        }
        HideObjects();  // Ocultar los objetos cuando la imagen se pierde
    }

    // Funci�n para ocultar todos los objetos
    private void HideObjects()
    {
        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(false);  // Ocultar cada objeto
        }
    }

    // Funci�n para reproducir o pausar el audio (llamada desde el bot�n de UI)
    public void ToggleAudio()
    {
        if (isPlaying)
        {
            videoPlayer.Pause();  // Pausar el audio del VideoPlayer
        }
        else
        {
            videoPlayer.Play();  // Reproducir el audio del VideoPlayer
        }
        isPlaying = !isPlaying;
    }
}
