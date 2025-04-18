using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public Image dialogo; // Referencia al di�logo UI
    private bool jugadorEnRango = false; // Detectar proximidad del jugador

    void Start()
    {
        dialogo.gameObject.SetActive(false); // Ocultar di�logo al inicio
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // Verificar si el jugador est� en rango
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = false;
        }
    }

    void Update()
    {
        // Si el jugador est� en rango y hace clic derecho
        if (jugadorEnRango && Input.GetMouseButtonDown(1))
        {
            MostrarDialogo();
        }
    }

    void MostrarDialogo()
    {
        dialogo.gameObject.SetActive(true); // Mostrar di�logo
        Time.timeScale = 0f; // Pausar el juego
    }

    public void CerrarDialogo()
    {
        dialogo.gameObject.SetActive(false); // Ocultar di�logo
        Time.timeScale = 1f; // Reanudar el juego
    }
}
