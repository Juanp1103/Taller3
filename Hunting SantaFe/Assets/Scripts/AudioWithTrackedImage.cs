using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class AudioWithTrackedImage : MonoBehaviour
{
    private ObserverBehaviour observerBehaviour;
    public AudioSource audioSource;
    public Button playPauseButton;
    private bool isPlaying = false;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        playPauseButton.gameObject.SetActive(false); // Ocultar el bot�n al inicio
        playPauseButton.onClick.AddListener(ToggleAudio); // Asignar la funci�n al bot�n
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
            // Mostrar bot�n cuando la imagen es rastreada
            playPauseButton.gameObject.SetActive(true);
        }
        else
        {
            // Ocultar bot�n cuando la imagen no se rastrea
            playPauseButton.gameObject.SetActive(false);
            if (audioSource.isPlaying) audioSource.Pause(); // Pausar el audio si la imagen se pierde
        }
    }

    void ToggleAudio()
    {
        if (isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
        isPlaying = !isPlaying;
    }
}
