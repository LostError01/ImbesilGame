using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importar TextMeshPro

public class NPC : MonoBehaviour
{
    public Image dialogo; // Referencia al diálogo UI
    public Image iconoAlerta; // Referencia al icono de alerta en la UI
    public TMP_Text textoInteraccion; // Referencia al texto "Presiona Enter para interactuar" (TextMeshPro)
    public AudioClip sonidoCierreDialogo; // Referencia al archivo de audio para el sonido
    private AudioSource fuenteDeAudio; // Componente AudioSource para reproducir el sonido

    private bool jugadorEnRango = false; // Detectar proximidad del jugador

    void Start()
    {
        dialogo.gameObject.SetActive(false); // Ocultar diálogo al inicio
        iconoAlerta.gameObject.SetActive(false); // Ocultar icono de alerta al inicio
        if (textoInteraccion != null)
        {
            textoInteraccion.gameObject.SetActive(false); // Ocultar texto al inicio
        }

        // Inicializar el AudioSource
        fuenteDeAudio = gameObject.AddComponent<AudioSource>();
        if (sonidoCierreDialogo != null)
        {
            fuenteDeAudio.clip = sonidoCierreDialogo;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // Verificar si el jugador está en rango
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = true;
            MostrarAlerta(); // Mostrar icono de alerta y texto
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = false;
            OcultarAlerta(); // Ocultar icono de alerta y texto
        }
    }

    void Update()
    {
        // Si el jugador está en rango y presiona Enter
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.Return))
        {
            MostrarDialogo();
            OcultarAlerta(); // Ocultar el texto y el icono al abrir el diálogo
        }
    }

    void MostrarAlerta()
    {
        iconoAlerta.gameObject.SetActive(true); // Mostrar icono de alerta
        if (textoInteraccion != null)
        {
            textoInteraccion.gameObject.SetActive(true); // Mostrar texto
        }
    }

    void OcultarAlerta()
    {
        iconoAlerta.gameObject.SetActive(false); // Ocultar icono de alerta
        if (textoInteraccion != null)
        {
            textoInteraccion.gameObject.SetActive(false); // Ocultar texto
        }
    }

    void MostrarDialogo()
    {
        dialogo.gameObject.SetActive(true); // Mostrar diálogo
        Time.timeScale = 0f; // Pausar el juego
    }

    public void CerrarDialogo()
    {
        dialogo.gameObject.SetActive(false); // Ocultar diálogo
        Time.timeScale = 1f; // Reanudar el juego

        // Reproducir el sonido si está asignado
        if (fuenteDeAudio != null && sonidoCierreDialogo != null)
        {
            fuenteDeAudio.PlayOneShot(sonidoCierreDialogo);
        }
    }
}