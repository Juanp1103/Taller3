using UnityEngine;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    void Start()
    {
        // Desactivar el renderizado del video
        videoPlayer.renderMode = VideoRenderMode.APIOnly;

        // Asignar el AudioSource para el audio del video
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);

        // Reproducir el video (solo el audio se escuchará)
        videoPlayer.Play();
    }
}

