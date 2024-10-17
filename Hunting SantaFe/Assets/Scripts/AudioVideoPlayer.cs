using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using UnityEngine.Video;

public class AudioWithTrackedImageAndButton : MonoBehaviour
{
    private ObserverBehaviour observerBehaviour;
    public VideoPlayer videoPlayer;  // Asignar el VideoPlayer
    public Button playPauseButton;   // Asignar el botón
    public Transform targetPoint;    // El punto en la imagen que el botón debe seguir
    private bool isPlaying = false;
    private bool isTargetFound = false; // Verificar si la imagen es rastreada

    void Start()
    {
        videoPlayer.playOnAwake = false;
        videoPlayer.Pause(); // Garantizar que el audio esté en pausa al inicio

        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        playPauseButton.gameObject.SetActive(false);  // Desactivar el botón inicialmente
        playPauseButton.onClick.AddListener(ToggleAudio);
    }

    private void OnDestroy()
    {
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            // Imagen encontrada (Target Found)
            isTargetFound = true;
            playPauseButton.gameObject.SetActive(true);  // Mostrar el botón cuando la imagen esté rastreada
        }
        else
        {
            // Imagen perdida (Target Lost)
            isTargetFound = false;
            playPauseButton.gameObject.SetActive(false); // Ocultar el botón si la imagen se pierde
            if (isPlaying)
            {
                videoPlayer.Pause();  // Pausar el audio si la imagen se pierde
                isPlaying = false;
            }
        }
    }
    void Update()
    {
        if (isTargetFound && targetPoint != null)
        {
            // Convertir la posición del targetPoint a coordenadas de pantalla
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetPoint.position);

            // Verificar si el punto de la pantalla está visible (dentro de la pantalla)
            if (screenPosition.z > 0 && screenPosition.x >= 0 && screenPosition.x <= Screen.width
                && screenPosition.y >= 0 && screenPosition.y <= Screen.height)
            {
                // Ajustar la posición del botón a las coordenadas de pantalla
                playPauseButton.transform.position = screenPosition;
            }
            else
            {
                // Si el punto está fuera de la pantalla, desactivar el botón
                playPauseButton.gameObject.SetActive(false);
            }
        }
    }

    void ToggleAudio()
    {
        if (!isTargetFound) return;  // Si la imagen no está rastreada, no hacer nada

        if (isPlaying)
        {
            videoPlayer.Pause();  // Pausar el audio del VideoPlayer
        }
        else
        {
            videoPlayer.Play();  // Reproducir el audio del VideoPlayer
        }
        isPlaying = !isPlaying;  // Cambiar el estado de reproducción
    }
}
