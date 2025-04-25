using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioClip sonidoClic; // Sonido al hacer clic en un botón
    public AudioClip sonidoHover; // Sonido al pasar el mouse sobre un botón
    public AudioClip musicaFondo; // Música en bucle para el menú

    private AudioSource fuenteDeAudio; // Componente AudioSource para reproducir sonidos
    private AudioSource fuenteDeMusica; // Componente AudioSource dedicado para la música

    [Range(0f, 1f)] public float volumenSonidos = 1f; // Volumen de los sonidos (0 a 1)
    [Range(0f, 1f)] public float volumenMusica = 0.5f; // Volumen de la música (0 a 1)

    void Start()
    {
        // Inicializar el AudioSource para los sonidos
        fuenteDeAudio = gameObject.AddComponent<AudioSource>();
        fuenteDeAudio.playOnAwake = false; // No reproducir automáticamente
        fuenteDeAudio.volume = volumenSonidos; // Asignar volumen de los sonidos

        // Inicializar el AudioSource para la música
        fuenteDeMusica = gameObject.AddComponent<AudioSource>();
        fuenteDeMusica.loop = true; // Activar bucle para la música
        fuenteDeMusica.playOnAwake = false;
        fuenteDeMusica.volume = volumenMusica; // Asignar volumen de la música

        // Reproducir la música de fondo
        if (musicaFondo != null)
        {
            fuenteDeMusica.clip = musicaFondo;
            fuenteDeMusica.Play();
        }
    }

    // Método para reproducir el sonido de clic
    public void ReproducirSonidoClic()
    {
        if (sonidoClic != null)
        {
            fuenteDeAudio.PlayOneShot(sonidoClic, volumenSonidos);
        }
    }

    // Método para reproducir el sonido de hover
    public void ReproducirSonidoHover()
    {
        if (sonidoHover != null)
        {
            fuenteDeAudio.PlayOneShot(sonidoHover, volumenSonidos);
        }
    }

    // Método para detener la música (opcional, si deseas detenerla al cambiar de escena)
    public void DetenerMusica()
    {
        if (fuenteDeMusica != null)
        {
            fuenteDeMusica.Stop();
        }
    }
}