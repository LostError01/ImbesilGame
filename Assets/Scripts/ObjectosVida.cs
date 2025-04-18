using UnityEngine;
using UnityEngine.UI;

public class ObjectosVida : MonoBehaviour
{
    //Instancia
    public string idObjeto;

    public Slider barraVida;

    private void Start()
    {
        // Si este objeto ya fue destruido, eliminarse inmediatamente
        if (EnemyManager.ObjInstance.objetosDestruidos.Contains(idObjeto) && Movimiento.Perder)
        {
            Destroy(gameObject);
        }

        barraVida.maxValue = VidasJugador.vidaMaxima;
        barraVida.value = VidasJugador.vida;
    }

    public void OnDestroy()
    {
        // Si el objeto se destruye manualmente (no por cambio de escena), registrar su ID
        if (gameObject.scene.isLoaded && Movimiento.Perder)
        {
            EnemyManager.ObjInstance.objetosDestruidos.Add(idObjeto);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            barraVida.value = VidasJugador.vida + 20f;
            // Si el objeto colisiona con el jugador, destruirlo
            Destroy(gameObject);
        }
    }
}
