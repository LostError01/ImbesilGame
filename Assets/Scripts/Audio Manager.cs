using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Agregar esta directiva para usar IEnumerator

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Instancia única para acceder desde otros scripts
    public AudioClip musicaFondo; // Música de fondo principal
    private AudioSource fuenteDeMusica; // Componente AudioSource para la música

    private AudioClip ultimaMusicaReproducida; // Almacena la última música reproducida
    private bool debeReproducirMusica = true; // Controla si la música debe reproducirse en la escena actual

    [Range(0f, 1f)] public float volumenMaximo = 0.8f; // Volumen máximo de la música
    [Range(0.1f, 5f)] public float duracionFade = 1f; // Duración del fade (en segundos)

    void Awake()
    {
        // Patrón Singleton: Asegura que solo haya una instancia del AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantener el objeto entre escenas
        }
        else
        {
            Destroy(gameObject); // Evitar duplicados
        }

        // Suscribirse al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        // Inicializar el AudioSource para la música
        fuenteDeMusica = gameObject.AddComponent<AudioSource>();
        fuenteDeMusica.loop = true; // Activar bucle para la música
        fuenteDeMusica.playOnAwake = false;
        fuenteDeMusica.volume = 0f; // Comenzar con volumen en 0 para el fade-in

        // Reproducir la música de fondo si está asignada
        if (musicaFondo != null)
        {
            CambiarMusica(musicaFondo);
        }
    }

    // Método para cambiar la música de fondo
    public void CambiarMusica(AudioClip nuevaMusica)
    {
        if (fuenteDeMusica != null && nuevaMusica != null)
        {
            StopAllCoroutines(); // Detener cualquier fade en curso
            StartCoroutine(FadeOutAndChangeMusic(nuevaMusica)); // Cambiar música con fade
        }
    }

    // Método para detener la música
    public void DetenerMusica()
    {
        if (fuenteDeMusica != null)
        {
            StopAllCoroutines(); // Detener cualquier fade en curso
            StartCoroutine(FadeOut()); // Detener música con fade
        }
    }

    // Método que se ejecuta automáticamente cuando se carga una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verificar si la escena actual es una de las permitidas para reproducir música
        if (EsEscenaPermitida(scene.name))
        {
            debeReproducirMusica = true;
            if (ultimaMusicaReproducida != null)
            {
                CambiarMusica(ultimaMusicaReproducida); // Reanudar la última música con fade
            }
        }
        else
        {
            debeReproducirMusica = false;
            DetenerMusica(); // Detener la música con fade
        }
    }

    // Método para verificar si la escena actual está en la lista permitida
    private bool EsEscenaPermitida(string nombreEscena)
    {
        string[] escenasPermitidas = { "Ciudad Level", "Level 2", "Level 3" };
        foreach (string escena in escenasPermitidas)
        {
            if (nombreEscena == escena)
            {
                return true;
            }
        }
        return false;
    }

    // Corrutina para realizar un fade-in
    private IEnumerator FadeIn()
    {
        float tiempoInicial = Time.time;
        while (Time.time - tiempoInicial < duracionFade)
        {
            fuenteDeMusica.volume = Mathf.Lerp(0f, volumenMaximo, (Time.time - tiempoInicial) / duracionFade);
            yield return null;
        }
        fuenteDeMusica.volume = volumenMaximo; // Asegurar el volumen final
    }

    // Corrutina para realizar un fade-out
    private IEnumerator FadeOut()
    {
        float tiempoInicial = Time.time;
        float volumenInicial = fuenteDeMusica.volume;

        while (Time.time - tiempoInicial < duracionFade)
        {
            fuenteDeMusica.volume = Mathf.Lerp(volumenInicial, 0f, (Time.time - tiempoInicial) / duracionFade);
            yield return null;
        }

        fuenteDeMusica.Stop(); // Detener la música después del fade-out
        fuenteDeMusica.volume = volumenMaximo; // Restaurar volumen para futuras reproducciones
    }

    // Corrutina para cambiar la música con fade-out y fade-in
    private IEnumerator FadeOutAndChangeMusic(AudioClip nuevaMusica)
    {
        // Realizar fade-out
        yield return StartCoroutine(FadeOut());

        // Cambiar la música
        fuenteDeMusica.clip = nuevaMusica;
        fuenteDeMusica.Play();

        // Almacenar la última música reproducida
        ultimaMusicaReproducida = nuevaMusica;

        // Realizar fade-in
        yield return StartCoroutine(FadeIn());
    }

    // Limpiar el evento al destruir el objeto
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}