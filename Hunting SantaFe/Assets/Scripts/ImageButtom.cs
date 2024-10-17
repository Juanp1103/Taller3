using UnityEngine;
using Vuforia;
using UnityEngine.Video;

public class ImageButton : MonoBehaviour
{
    private ObserverBehaviour observerBehaviour;
    public VideoPlayer videoPlayer; // Asignar el VideoPlayer
    private bool isPlaying = false;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
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
            // Imagen rastreada, ahora podemos detectar toques.
        }
    }

    void Update()
    {
        // Detectar toque en la imagen rastreada
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == observerBehaviour.transform) // Si tocamos la imagen rastreada
                {
                    ToggleAudio();
                }
            }
        }
    }

    void ToggleAudio()
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

