using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Instancia única para acceder desde otros scripts

    [System.Serializable]
    public class EscenaConMusica
    {
        public string nombreEscena; // Nombre de la escena
        public AudioClip musicaEscena; // Música específica para esta escena
    }

    public EscenaConMusica[] musicasPorEscena; // Lista de escenas con sus respectivas músicas
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
    }

    // Método que se ejecuta automáticamente cuando se carga una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Buscar la música correspondiente a la escena cargada
        AudioClip musicaParaEscena = ObtenerMusicaParaEscena(scene.name);

        if (musicaParaEscena != null)
        {
            debeReproducirMusica = true;
            CambiarMusica(musicaParaEscena); // Reproducir la música específica para esta escena
        }
        else
        {
            debeReproducirMusica = false;
            DetenerMusica(); // Detener la música si no hay música asignada para esta escena
        }
    }

    // Método para obtener la música correspondiente a una escena
    private AudioClip ObtenerMusicaParaEscena(string nombreEscena)
    {
        foreach (var escenaConMusica in musicasPorEscena)
        {
            if (escenaConMusica.nombreEscena == nombreEscena && escenaConMusica.musicaEscena != null)
            {
                return escenaConMusica.musicaEscena;
            }
        }
        return null; // No se encontró música para esta escena
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