using UnityEngine;

public class EscalerasSalir : MonoBehaviour
{
    public Collider2D[] collidersMontania;
    public Collider2D[] collidersLimites;

    public int LayerPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (Collider2D montania in collidersMontania)
            {
                montania.enabled = true;
            }

            foreach (Collider2D limite in collidersLimites)
            {
                limite.enabled = false; //Desactiva los limites de la montaña al salir
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = LayerPlayer; // Cambia la capa a 5 de nuevo
        }
    }
}
