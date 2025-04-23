using UnityEngine;
using UnityEngine.UI;

public class ObjectosVida : MonoBehaviour
{
    // Instancia
    public string idObjeto;

    public Slider barraVida;

    private void Start()
    {
        // Si este objeto ya fue destruido, eliminarse inmediatamente
        if (EnemyManager.Instance.objetosDestruidos.Contains(idObjeto) && Movimiento.Perder)
        {
            Destroy(gameObject);
        }

        // Inicializar la barra de vida
        barraVida.maxValue = VidasJugador.vidaMaxima;
        barraVida.value = VidasJugador.vida;
    }

    public void OnDestroy()
    {
        // Si el objeto se destruye manualmente (no por cambio de escena), registrar su ID
        if (gameObject.scene.isLoaded && Movimiento.Perder)
        {
            EnemyManager.Instance.objetosDestruidos.Add(idObjeto);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Aumentar la vida del jugador
            float nuevaVida = VidasJugador.vida + 50f;

            // Verificar que la vida no exceda el valor máximo
            if (nuevaVida > VidasJugador.vidaMaxima)
            {
                nuevaVida = VidasJugador.vidaMaxima;
            }

            // Actualizar la vida del jugador
            VidasJugador.vida = nuevaVida;

            // Sincronizar la barra de vida
            barraVida.value = VidasJugador.vida;

            Debug.Log($"Vida del jugador aumentada a: {VidasJugador.vida}");

            // Destruir el ítem
            Destroy(gameObject);
        }
    }
}